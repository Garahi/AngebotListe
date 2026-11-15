using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using AngebotListe.Mobile.Models;

namespace AngebotListe.Mobile.Services;

// Alle Daten kommen jetzt über die AngebotListe-API auf dem PC "kankon"
// (sabit adres über Tailscale Funnel, wie Yolcu Transport und KankonSite).
// SQL-Zugangsdaten stehen nicht mehr in der App.
public static class DataService
{
    // ⚠️ Sunucu adresi tek yerde — C:\AngebotListe\AngebotListe\Sunucu\ADRES.txt ile aynı olmalı
    public const string ServerAdresse = "https://kankon.taila21b62.ts.net:10000";

    private static readonly HttpClient Http = new()
    {
        BaseAddress = new Uri(ServerAdresse),
        Timeout = TimeSpan.FromSeconds(30)
    };

    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    // Token wird gespeichert, damit die App nach dem Neustart angemeldet bleibt
    private static string? _token = Preferences.Get("api_token", (string?)null);

    // Wird ausgelöst, wenn die Anmeldung abgelaufen ist (z. B. nach Neustart des Servers)
    public static event Action? AnmeldungAbgelaufen;

    private static HttpRequestMessage Anfrage(HttpMethod m, string url, object? body = null)
    {
        var r = new HttpRequestMessage(m, url);
        if (_token != null) r.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        if (body != null) r.Content = JsonContent.Create(body, options: Json);
        return r;
    }

    private static async Task<HttpResponseMessage> Senden(HttpMethod m, string url, object? body = null)
    {
        var res = await Http.SendAsync(Anfrage(m, url, body));
        if (res.StatusCode == HttpStatusCode.Unauthorized)
        {
            Abmelden();
            MainThread.BeginInvokeOnMainThread(() => AnmeldungAbgelaufen?.Invoke());
        }
        return res;
    }

    private static async Task<T?> Laden<T>(string url)
    {
        using var res = await Senden(HttpMethod.Get, url);
        if (!res.IsSuccessStatusCode) return default;
        return await res.Content.ReadFromJsonAsync<T>(Json);
    }

    private static async Task<string> Fehlertext(HttpResponseMessage res)
    {
        try
        {
            var e = await res.Content.ReadFromJsonAsync<JsonElement>(Json);
            if (e.TryGetProperty("error", out var t)) return t.GetString() ?? res.ReasonPhrase ?? "";
        }
        catch { }
        return $"Serverfehler ({(int)res.StatusCode})";
    }

    public static void Abmelden()
    {
        _token = null;
        Preferences.Remove("api_token");
        Preferences.Set("logged_in", false);
    }

    // Für die Übertragung nur die Datenfelder (keine Bild-/Anzeige-Hilfsfelder)
    private static object Dto(Product p) => new { p.ProduktId, p.Name, p.Beschreibung, p.Preis, p.Bild };

    // ═══════════════════════════════════════════════════════════════════
    // AUTHENTICATION
    // ═══════════════════════════════════════════════════════════════════
    private record LoginAntwort(string Token, int UserId, string Username, string Role);

    public static async Task<(bool Success, string Message, int UserId, string Username, string Role)>
        AuthenticateAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            return (false, "Benutzername darf nicht leer sein.", 0, "", "");
        try
        {
            using var res = await Http.SendAsync(Anfrage(HttpMethod.Post, "/api/login",
                new { username = username.Trim(), password }));
            if (!res.IsSuccessStatusCode)
                return (false, await Fehlertext(res), 0, "", "");
            var a = await res.Content.ReadFromJsonAsync<LoginAntwort>(Json);
            if (a == null) return (false, "Unerwartete Antwort vom Server.", 0, "", "");
            _token = a.Token;
            Preferences.Set("api_token", a.Token);
            return (true, "Erfolgreich angemeldet.", a.UserId, a.Username, a.Role);
        }
        catch (Exception ex)
        {
            return (false, "Server nicht erreichbar. Bitte Internetverbindung prüfen.\n" + ex.Message, 0, "", "");
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    // PRODUCTS
    // ═══════════════════════════════════════════════════════════════════
    // Die Produktliste wird einmal geladen und 2 Minuten im Speicher gehalten;
    // die Suche filtert dann direkt auf dem iPhone (vorher: Serveranfrage bei jedem Buchstaben).
    private static List<Product>? _produkte;
    private static DateTime _produkteZeit;

    public static void ProdukteNeuLaden() => _produkte = null;

    public static async Task<List<Product>> GetProductsAsync(string? search = null, bool neuLaden = false)
    {
        try
        {
            if (neuLaden || _produkte == null || DateTime.Now - _produkteZeit > TimeSpan.FromMinutes(2))
            {
                _produkte = await Laden<List<Product>>("/api/products") ?? new();
                _produkteZeit = DateTime.Now;
            }
            if (string.IsNullOrWhiteSpace(search)) return _produkte.ToList();
            var s = search.Trim();
            return _produkte.Where(p =>
                    (p.Name ?? "").Contains(s, StringComparison.OrdinalIgnoreCase) ||
                    (p.Beschreibung ?? "").Contains(s, StringComparison.OrdinalIgnoreCase) ||
                    (p.Kategorie ?? "").Contains(s, StringComparison.OrdinalIgnoreCase) ||
                    (p.ProduktNr ?? "").Contains(s, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GetProductsAsync error: " + ex.Message);
            return _produkte?.ToList() ?? new();
        }
    }

    // Adresse des (verkleinerten) Produktbilds; Version sorgt dafür, dass ein geändertes Bild neu geladen wird
    public static string BildUrl(int produktId, int version, int breite) =>
        $"{ServerAdresse}/api/products/{produktId}/bild?w={breite}&v={version}&t={Uri.EscapeDataString(_token ?? "")}";

    private record ProduktIdAntwort(int ProduktId);

    // Neues Produkt anlegen (ProduktId = 0) oder vorhandenes ändern. Bild nur senden, wenn neu gewählt.
    public static async Task<(bool Ok, string Meldung)> SaveProductAsync(Product p)
    {
        try
        {
            var body = new { p.ProduktNr, p.Name, p.Beschreibung, p.Preis, p.Kategorie, Bild = p.Bild };
            using var res = p.ProduktId == 0
                ? await Senden(HttpMethod.Post, "/api/products", body)
                : await Senden(HttpMethod.Put, $"/api/products/{p.ProduktId}", body);
            if (!res.IsSuccessStatusCode) return (false, await Fehlertext(res));
            ProdukteNeuLaden();
            return (true, "Gespeichert.");
        }
        catch (Exception ex)
        {
            return (false, "Server nicht erreichbar.\n" + ex.Message);
        }
    }

    private record LoeschAntwort(bool Deaktiviert, int Verwendet);

    // Löschen; wird das Produkt in Angeboten verwendet, deaktiviert der Server es stattdessen (wie am PC)
    public static async Task<(bool Ok, string Meldung)> DeleteProductAsync(int produktId)
    {
        try
        {
            using var res = await Senden(HttpMethod.Delete, $"/api/products/{produktId}");
            if (!res.IsSuccessStatusCode) return (false, await Fehlertext(res));
            ProdukteNeuLaden();
            var a = await res.Content.ReadFromJsonAsync<LoeschAntwort>(Json);
            return a?.Deaktiviert == true
                ? (true, $"Das Produkt wird in {a.Verwendet} Angebot(en) verwendet und wurde deshalb deaktiviert (nicht gelöscht).")
                : (true, "Produkt gelöscht.");
        }
        catch (Exception ex)
        {
            return (false, "Server nicht erreichbar.\n" + ex.Message);
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    // ANGEBOTE
    // ═══════════════════════════════════════════════════════════════════
    public static async Task<List<Angebot>> GetAngeboteAsync()
    {
        try { return await Laden<List<Angebot>>("/api/angebote") ?? new(); }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GetAngeboteAsync error: " + ex.Message);
            return new();
        }
    }

    public static async Task<List<AngebotProdukt>> GetAngebotProdukteAsync(int angebotId)
    {
        try { return await Laden<List<AngebotProdukt>>($"/api/angebote/{angebotId}/produkte") ?? new(); }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GetAngebotProdukteAsync error: " + ex.Message);
            return new();
        }
    }

    private record IdAntwort(int AngebotID);

    public static async Task<int> CreateOrUpdateAngebotAsync(string angebotNr, List<Product> produkte)
    {
        try
        {
            using var res = await Senden(HttpMethod.Post, "/api/angebote",
                new { angebotNr, produkte = produkte.Select(Dto).ToList() });
            if (!res.IsSuccessStatusCode) return -1;
            var a = await res.Content.ReadFromJsonAsync<IdAntwort>(Json);
            return a?.AngebotID ?? -1;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("CreateOrUpdateAngebotAsync error: " + ex.Message);
            return -1;
        }
    }

    public static async Task<bool> DeleteAngebotAsync(int angebotId)
    {
        try
        {
            using var res = await Senden(HttpMethod.Delete, $"/api/angebote/{angebotId}");
            return res.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("DeleteAngebotAsync error: " + ex.Message);
            return false;
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    // ANGEBOTPRODUKT — tek satır CRUD (detay sayfasında düzenleme için)
    // ═══════════════════════════════════════════════════════════════════
    public static async Task<bool> AddProduktToAngebotAsync(int angebotId, Product p)
    {
        try
        {
            using var res = await Senden(HttpMethod.Post, $"/api/angebote/{angebotId}/produkte", Dto(p));
            return res.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("AddProduktToAngebot error: " + ex.Message);
            return false;
        }
    }

    public static async Task<bool> UpdateAngebotProduktAsync(AngebotProdukt ap)
    {
        try
        {
            using var res = await Senden(HttpMethod.Put, $"/api/positionen/{ap.ID}",
                new { ap.ProduktName, ap.Beschreibung, ap.Preis, ap.Bild });
            return res.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("UpdateAngebotProdukt error: " + ex.Message);
            return false;
        }
    }

    public static async Task<bool> DeleteAngebotProduktAsync(int angebotProduktId)
    {
        try
        {
            using var res = await Senden(HttpMethod.Delete, $"/api/positionen/{angebotProduktId}");
            return res.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("DeleteAngebotProdukt error: " + ex.Message);
            return false;
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    // ANGEBOT NR — günlük artan numara (ANG-yyyyMMdd-NNNN), vergibt der Server
    // ═══════════════════════════════════════════════════════════════════
    private record NrAntwort(string AngebotNr);

    public static async Task<string> NextAngebotNrAsync()
    {
        try
        {
            var a = await Laden<NrAntwort>("/api/angebote/naechste-nr");
            if (!string.IsNullOrEmpty(a?.AngebotNr)) return a.AngebotNr;
        }
        catch { }
        return "ANG-" + DateTime.Now.ToString("yyyyMMdd") + "-0001";
    }
}
