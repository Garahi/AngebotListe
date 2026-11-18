// =====================================================================================
//  AngebotListe – Mobil-API für die iPhone-App (AngebotListe.Mobile)
//  • Gleiches Muster wie YolcuApi / KankonSite.Api: läuft auf dem PC "kankon",
//    sabit adres über Tailscale Funnel:  https://kankon.taila21b62.ts.net:10000
//  • Die App spricht nur noch mit dieser API – SQL-Zugangsdaten sind nicht mehr in der App.
//  • Login liefert ein Token (Bearer), gültig 30 Tage (bis die API neu startet).
// =====================================================================================
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using SkiaSharp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string conn = builder.Configuration.GetConnectionString("AngebotListe")
              ?? throw new InvalidOperationException("ConnectionStrings:AngebotListe fehlt in appsettings.json");

// ---------------- Log (api-log.txt im Projektordner) ----------------
var logPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "api-log.txt");
var logLock = new object();
void Log(string msg)
{
    var line = $"{DateTime.Now:dd.MM HH:mm:ss}  {msg}";
    try { lock (logLock) File.AppendAllText(logPath, line + Environment.NewLine); } catch { }
}
Log("=== AngebotListe API Start ===");

app.Use(async (ctx, next) =>
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    try { await next(); }
    finally { Log($"{ctx.Request.Method} {ctx.Request.Path}{System.Text.RegularExpressions.Regex.Replace(ctx.Request.QueryString.ToString(), "([?&]t=)[^&]*", "$1***")} = {ctx.Response.StatusCode} ({sw.ElapsedMilliseconds} ms)"); }
});

var sessions = new ConcurrentDictionary<string, Session>();
var failed = new ConcurrentDictionary<string, (int Count, DateTime First)>();

IResult Err(int status, string msg) => Results.Json(new { error = msg }, statusCode: status);

Session? Auth(HttpRequest req)
{
    var h = req.Headers.Authorization.ToString();
    string token;
    if (h.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) token = h[7..].Trim();
    else if (req.Query.TryGetValue("t", out var qt) && !string.IsNullOrEmpty(qt)) token = qt.ToString();   // Bild-URLs (ohne Header)
    else return null;
    if (sessions.TryGetValue(token, out var s))
    {
        if (s.Expires > DateTime.UtcNow) return s;
        sessions.TryRemove(token, out _);
    }
    return null;
}

async Task<IResult> Safe(HttpRequest req, Func<Task<IResult>> f, bool login = false)
{
    if (!login && Auth(req) is null) return Err(401, "Bitte neu anmelden.");
    try { return await f(); }
    catch (SqlException ex) { Log("SQL-FEHLER: " + ex.Message); return Err(500, "Datenbankfehler: " + ex.Message); }
    catch (Exception ex) { Log("FEHLER: " + ex); return Err(500, "Serverfehler: " + ex.Message); }
}

async Task<SqlConnection> Open()
{
    var c = new SqlConnection(conn);
    await c.OpenAsync();
    return c;
}

static object Db(object? v) => v ?? DBNull.Value;

// ---------------- Test im Browser ----------------
app.MapGet("/", () => Results.Text("AngebotListe API läuft ✔  " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), "text/plain; charset=utf-8"));

// ---------------- Login ----------------
app.MapPost("/api/login", (HttpContext ctx, LoginBody body) => Safe(ctx.Request, async () =>
{
    var ip = ctx.Connection.RemoteIpAddress?.ToString() ?? "";
    if (failed.TryGetValue(ip, out var f) && f.Count >= 10 && DateTime.UtcNow - f.First < TimeSpan.FromMinutes(15))
        return Err(429, "Zu viele Versuche. Bitte 15 Minuten warten.");

    if (string.IsNullOrWhiteSpace(body.Username)) return Err(400, "Benutzername darf nicht leer sein.");

    await using var con = await Open();
    await using var cmd = new SqlCommand("SELECT TOP 1 Id, Username, PasswordHash, Role, IsActive FROM Users WHERE Username = @u", con);
    cmd.Parameters.AddWithValue("@u", body.Username.Trim());
    await using var rd = await cmd.ExecuteReaderAsync();

    bool ok = false; int id = 0; string name = "", role = "User";
    if (await rd.ReadAsync())
    {
        if (rd["IsActive"] == DBNull.Value || !Convert.ToBoolean(rd["IsActive"]))
            return Err(403, "Dieser Benutzer ist deaktiviert.");
        var stored = rd["PasswordHash"] as string ?? "";
        ok = stored.StartsWith("$2") ? SafeVerify(body.Password ?? "", stored) : stored == (body.Password ?? "");
        id = Convert.ToInt32(rd["Id"]);
        name = rd["Username"] as string ?? "";
        role = rd["Role"] as string ?? "User";
    }
    if (!ok)
    {
        failed.AddOrUpdate(ip, (1, DateTime.UtcNow), (_, o) => DateTime.UtcNow - o.First > TimeSpan.FromMinutes(15) ? (1, DateTime.UtcNow) : (o.Count + 1, o.First));
        return Err(401, "Benutzername oder Passwort falsch.");
    }
    failed.TryRemove(ip, out _);
    var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    sessions[token] = new Session(id, name, role, DateTime.UtcNow.AddDays(30));
    Log($"Login: {name}");
    return Results.Json(new { token, userId = id, username = name, role });
}, login: true));

static bool SafeVerify(string pw, string hash) { try { return BCrypt.Net.BCrypt.Verify(pw, hash); } catch { return false; } }

app.MapPost("/api/logout", (HttpRequest req) =>
{
    var h = req.Headers.Authorization.ToString();
    if (h.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) sessions.TryRemove(h[7..].Trim(), out _);
    return Results.Ok();
});

// ---------------- Produkte ----------------
// Liste OHNE Bilder (vorher kamen alle Fotos in voller Größe mit -> sehr langsam).
// hatBild/bildVersion: die App lädt pro Produkt ein kleines Vorschaubild über /api/products/{id}/bild nach.
app.MapGet("/api/products", (HttpRequest req, string? search) => Safe(req, async () =>
{
    var sql = "SELECT ProduktId, ProduktNr, Name, Beschreibung, Preis, Kategorie, Aktiv, " +
              "CAST(ISNULL(DATALENGTH(Bild), 0) AS int) AS BildLaenge FROM Products WHERE Aktiv = 1";
    if (!string.IsNullOrWhiteSpace(search)) sql += " AND (Name LIKE @s OR Beschreibung LIKE @s OR CAST(ProduktNr AS nvarchar(100)) LIKE @s)";
    sql += " ORDER BY Name";
    await using var con = await Open();
    await using var cmd = new SqlCommand(sql, con);
    if (!string.IsNullOrWhiteSpace(search)) cmd.Parameters.AddWithValue("@s", "%" + search + "%");
    await using var rd = await cmd.ExecuteReaderAsync();
    var list = new List<object>();
    while (await rd.ReadAsync())
    {
        var len = Convert.ToInt32(rd["BildLaenge"]);
        list.Add(new
        {
            produktId = Convert.ToInt32(rd["ProduktId"]),
            produktNr = rd["ProduktNr"] == DBNull.Value ? null : rd["ProduktNr"].ToString(),
            name = rd["Name"] as string,
            beschreibung = rd["Beschreibung"] as string,
            preis = rd["Preis"] == DBNull.Value ? 0m : Convert.ToDecimal(rd["Preis"]),
            kategorie = rd["Kategorie"] as string,
            aktiv = rd["Aktiv"] != DBNull.Value && Convert.ToBoolean(rd["Aktiv"]),
            hatBild = len > 0,
            bildVersion = len
        });
    }
    return Results.Json(list);
}));

// Produktbild, auf Wunsch verkleinert (?w=240). Verkleinerte Bilder werden im Speicher zwischengespeichert.
var bildCache = new ConcurrentDictionary<string, byte[]>();
app.MapGet("/api/products/{id:int}/bild", (HttpRequest req, HttpResponse res, int id, int? w) => Safe(req, async () =>
{
    await using var con = await Open();
    await using var cmd = new SqlCommand("SELECT Bild FROM Products WHERE ProduktId = @id", con);
    cmd.Parameters.AddWithValue("@id", id);
    if (await cmd.ExecuteScalarAsync() is not byte[] bild || bild.Length == 0) return Results.NotFound();
    var breite = Math.Clamp(w ?? 0, 0, 2000);
    var daten = breite == 0 ? bild : bildCache.GetOrAdd($"{id}:{bild.Length}:{breite}", _ => Verkleinern(bild, breite));
    res.Headers.CacheControl = "private, max-age=2592000";
    return Results.Bytes(daten, "image/jpeg");
}));

// Neues Produkt (wie im Windows-Programm "Produkte verwalten")
app.MapPost("/api/products", (HttpRequest req, ProduktSpeichern p) => Safe(req, async () =>
{
    if (string.IsNullOrWhiteSpace(p.Name)) return Err(400, "Produktname fehlt.");
    await using var con = await Open();
    if (await ArtNrVergeben(con, p.ProduktNr, 0)) return Err(409, $"Die Artikelnummer {p.ProduktNr} ist schon vergeben.");
    await using var cmd = new SqlCommand(
        "INSERT INTO Products (ProduktNr, Name, Beschreibung, Preis, Bild, Kategorie, Aktiv, ErstelltAm) " +
        "VALUES (@nr, @name, @beschr, @preis, @bild, @kat, 1, GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS int);", con);
    ProduktParameter(cmd, p);
    var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
    Log($"Produkt angelegt: {id} {p.Name}");
    return Results.Json(new { produktId = id });
}));

// Produkt ändern – Bild nur, wenn ein neues mitgeschickt wird; ErstelltAm bleibt unverändert
app.MapPut("/api/products/{id:int}", (HttpRequest req, int id, ProduktSpeichern p) => Safe(req, async () =>
{
    if (string.IsNullOrWhiteSpace(p.Name)) return Err(400, "Produktname fehlt.");
    await using var con = await Open();
    if (await ArtNrVergeben(con, p.ProduktNr, id)) return Err(409, $"Die Artikelnummer {p.ProduktNr} ist schon vergeben.");
    var neuesBild = p.Bild is { Length: > 0 };
    await using var cmd = new SqlCommand(
        "UPDATE Products SET ProduktNr = @nr, Name = @name, Beschreibung = @beschr, Preis = @preis, Kategorie = @kat" +
        (neuesBild ? ", Bild = @bild" : "") + " WHERE ProduktId = @id", con);
    ProduktParameter(cmd, p);
    cmd.Parameters.AddWithValue("@id", id);
    if (await cmd.ExecuteNonQueryAsync() == 0) return Err(404, "Produkt nicht gefunden.");
    return Results.Json(new { produktId = id });
}));

// Löschen – wird das Produkt in Angeboten verwendet, wird es nur deaktiviert (sonst fehlen alte Angebote)
app.MapDelete("/api/products/{id:int}", (HttpRequest req, int id) => Safe(req, async () =>
{
    await using var con = await Open();
    int verwendet;
    await using (var chk = new SqlCommand("SELECT COUNT(*) FROM AngebotProdukte WHERE ProduktID = @id", con))
    {
        chk.Parameters.AddWithValue("@id", id);
        verwendet = Convert.ToInt32(await chk.ExecuteScalarAsync());
    }
    var sql = verwendet > 0 ? "UPDATE Products SET Aktiv = 0 WHERE ProduktId = @id" : "DELETE FROM Products WHERE ProduktId = @id";
    await using var cmd = new SqlCommand(sql, con);
    cmd.Parameters.AddWithValue("@id", id);
    await cmd.ExecuteNonQueryAsync();
    Log($"Produkt {(verwendet > 0 ? "deaktiviert" : "gelöscht")}: {id}");
    return Results.Json(new { deaktiviert = verwendet > 0, verwendet });
}));

static async Task<bool> ArtNrVergeben(SqlConnection con, string? nr, int eigeneId)
{
    if (string.IsNullOrWhiteSpace(nr)) return false;
    await using var cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE CAST(ProduktNr AS nvarchar(100)) = @nr AND ProduktId <> @id", con);
    cmd.Parameters.AddWithValue("@nr", nr.Trim());
    cmd.Parameters.AddWithValue("@id", eigeneId);
    return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
}

static void ProduktParameter(SqlCommand cmd, ProduktSpeichern p)
{
    cmd.Parameters.AddWithValue("@nr", string.IsNullOrWhiteSpace(p.ProduktNr) ? DBNull.Value : (object)p.ProduktNr.Trim());
    cmd.Parameters.AddWithValue("@name", p.Name!.Trim());
    cmd.Parameters.AddWithValue("@beschr", (object?)p.Beschreibung?.Trim() ?? "");
    cmd.Parameters.Add("@preis", System.Data.SqlDbType.Decimal).Value = p.Preis;
    cmd.Parameters.Add("@bild", System.Data.SqlDbType.VarBinary, -1).Value = (object?)p.Bild ?? DBNull.Value;
    cmd.Parameters.AddWithValue("@kat", (object?)p.Kategorie?.Trim() ?? "");
}

// Bild auf maximale Kantenlänge verkleinern (weißer Hintergrund für PNG mit Transparenz)
static byte[] Verkleinern(byte[] bild, int max)
{
    try
    {
        using var bmp = SKBitmap.Decode(bild);
        if (bmp == null) return bild;
        float f = Math.Min(1f, (float)max / Math.Max(bmp.Width, bmp.Height));
        int w = Math.Max(1, (int)(bmp.Width * f)), h = Math.Max(1, (int)(bmp.Height * f));
        using var surface = SKSurface.Create(new SKImageInfo(w, h));
        surface.Canvas.Clear(SKColors.White);
        using (var img = SKImage.FromBitmap(bmp))
            surface.Canvas.DrawImage(img, new SKRect(0, 0, w, h), new SKSamplingOptions(SKCubicResampler.Mitchell));
        using var snap = surface.Snapshot();
        using var data = snap.Encode(SKEncodedImageFormat.Jpeg, 82);
        return data.ToArray();
    }
    catch
    {
        return bild;
    }
}

// ---------------- Angebote ----------------
app.MapGet("/api/angebote", (HttpRequest req) => Safe(req, async () =>
{
    await using var con = await Open();
    await using var cmd = new SqlCommand(
        "SELECT a.AngebotID, a.AngebotNr, a.Datum, a.Beschreibung, " +
        "(SELECT COUNT(*) FROM AngebotProdukte ap WHERE ap.AngebotID = a.AngebotID) AS ProduktAnzahl " +
        "FROM AngebotListe a ORDER BY a.AngebotID DESC", con);
    await using var rd = await cmd.ExecuteReaderAsync();
    var list = new List<object>();
    while (await rd.ReadAsync())
        list.Add(new
        {
            angebotID = Convert.ToInt32(rd["AngebotID"]),
            angebotNr = rd["AngebotNr"] as string,
            datum = rd["Datum"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rd["Datum"]),
            beschreibung = rd["Beschreibung"] as string,
            produktAnzahl = rd["ProduktAnzahl"] == DBNull.Value ? 0 : Convert.ToInt32(rd["ProduktAnzahl"])
        });
    return Results.Json(list);
}));

app.MapGet("/api/angebote/naechste-nr", (HttpRequest req) => Safe(req, async () =>
{
    var prefix = "ANG-" + DateTime.Now.ToString("yyyyMMdd") + "-";
    await using var con = await Open();
    await using var cmd = new SqlCommand("SELECT TOP 1 AngebotNr FROM AngebotListe WHERE AngebotNr LIKE @p + '%' ORDER BY AngebotNr DESC", con);
    cmd.Parameters.AddWithValue("@p", prefix);
    var last = await cmd.ExecuteScalarAsync() as string;
    int n = 1;
    if (!string.IsNullOrEmpty(last))
    {
        var parts = last.Split('-');
        if (parts.Length >= 3 && int.TryParse(parts[2], out var num)) n = num + 1;
    }
    return Results.Json(new { angebotNr = prefix + n.ToString("D4") });
}));

app.MapGet("/api/angebote/{id:int}/produkte", (HttpRequest req, int id) => Safe(req, async () =>
{
    await using var con = await Open();
    await using var cmd = new SqlCommand("SELECT ID, AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild FROM AngebotProdukte WHERE AngebotID = @aid", con);
    cmd.Parameters.AddWithValue("@aid", id);
    await using var rd = await cmd.ExecuteReaderAsync();
    var list = new List<object>();
    while (await rd.ReadAsync())
        list.Add(new
        {
            id = Convert.ToInt32(rd["ID"]),
            angebotID = Convert.ToInt32(rd["AngebotID"]),
            produktID = rd["ProduktID"] == DBNull.Value ? 0 : Convert.ToInt32(rd["ProduktID"]),
            produktName = rd["ProduktName"] as string,
            beschreibung = rd["Beschreibung"] as string,
            preis = rd["Preis"] == DBNull.Value ? 0m : Convert.ToDecimal(rd["Preis"]),
            bild = rd["Bild"] as byte[]
        });
    return Results.Json(list);
}));

// Angebot anlegen oder (bei gleicher Nummer) Produkte ersetzen – wie das Windows-Programm
app.MapPost("/api/angebote", (HttpRequest req, AngebotSpeichern body) => Safe(req, async () =>
{
    if (string.IsNullOrWhiteSpace(body.AngebotNr)) return Err(400, "Angebot-Nr. fehlt.");
    await using var con = await Open();
    await using var tx = (SqlTransaction)await con.BeginTransactionAsync();
    int angebotId;
    await using (var chk = new SqlCommand("SELECT TOP 1 AngebotID FROM AngebotListe WHERE AngebotNr=@nr ORDER BY AngebotID DESC", con, tx))
    {
        chk.Parameters.AddWithValue("@nr", body.AngebotNr);
        var ex = await chk.ExecuteScalarAsync();
        if (ex is not null && ex != DBNull.Value)
        {
            angebotId = Convert.ToInt32(ex);
            await using var del = new SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con, tx);
            del.Parameters.AddWithValue("@aid", angebotId);
            await del.ExecuteNonQueryAsync();
        }
        else
        {
            await using var ins = new SqlCommand("INSERT INTO AngebotListe (Datum, Beschreibung, AngebotNr) VALUES (@d, @b, @nr); SELECT SCOPE_IDENTITY();", con, tx);
            ins.Parameters.AddWithValue("@d", DateTime.Now);
            ins.Parameters.AddWithValue("@b", "Mobile App");
            ins.Parameters.AddWithValue("@nr", body.AngebotNr);
            angebotId = Convert.ToInt32(await ins.ExecuteScalarAsync());
        }
    }
    foreach (var p in body.Produkte ?? new())
        await InsertPosition(con, tx, angebotId, p);
    await tx.CommitAsync();
    return Results.Json(new { angebotID = angebotId });
}));

app.MapDelete("/api/angebote/{id:int}", (HttpRequest req, int id) => Safe(req, async () =>
{
    await using var con = await Open();
    await using var tx = (SqlTransaction)await con.BeginTransactionAsync();
    foreach (var sql in new[] { "DELETE FROM AngebotProdukte WHERE AngebotID=@aid", "DELETE FROM AngebotListe WHERE AngebotID=@aid" })
    {
        await using var cmd = new SqlCommand(sql, con, tx);
        cmd.Parameters.AddWithValue("@aid", id);
        await cmd.ExecuteNonQueryAsync();
    }
    await tx.CommitAsync();
    return Results.Ok();
}));

// ---------------- Einzelne Positionen ----------------
app.MapPost("/api/angebote/{id:int}/produkte", (HttpRequest req, int id, ProduktDto p) => Safe(req, async () =>
{
    await using var con = await Open();
    await InsertPosition(con, null, id, p);
    return Results.Ok();
}));

app.MapPut("/api/positionen/{id:int}", (HttpRequest req, int id, PositionDto ap) => Safe(req, async () =>
{
    await using var con = await Open();
    await using var cmd = new SqlCommand("UPDATE AngebotProdukte SET ProduktName=@name, Beschreibung=@desc, Preis=@preis, Bild=@bild WHERE ID=@id", con);
    cmd.Parameters.AddWithValue("@id", id);
    cmd.Parameters.AddWithValue("@name", Db(ap.ProduktName));
    cmd.Parameters.AddWithValue("@desc", Db(ap.Beschreibung));
    cmd.Parameters.AddWithValue("@preis", ap.Preis);
    cmd.Parameters.Add("@bild", System.Data.SqlDbType.VarBinary, -1).Value = Db(ap.Bild);
    await cmd.ExecuteNonQueryAsync();
    return Results.Ok();
}));

app.MapDelete("/api/positionen/{id:int}", (HttpRequest req, int id) => Safe(req, async () =>
{
    await using var con = await Open();
    await using var cmd = new SqlCommand("DELETE FROM AngebotProdukte WHERE ID=@id", con);
    cmd.Parameters.AddWithValue("@id", id);
    await cmd.ExecuteNonQueryAsync();
    return Results.Ok();
}));

async Task InsertPosition(SqlConnection con, SqlTransaction? tx, int angebotId, ProduktDto p)
{
    await using var cmd = new SqlCommand(
        "INSERT INTO AngebotProdukte (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild) " +
        // Kein Bild mitgeschickt (App lädt die Liste ohne Bilder) -> Bild des Produkts übernehmen
        "VALUES (@aid, @pid, @name, @desc, @preis, COALESCE(@bild, (SELECT Bild FROM Products WHERE ProduktId = @pid)))", con, tx);
    cmd.Parameters.AddWithValue("@aid", angebotId);
    cmd.Parameters.AddWithValue("@pid", p.ProduktId);
    cmd.Parameters.AddWithValue("@name", Db(p.Name));
    cmd.Parameters.AddWithValue("@desc", Db(p.Beschreibung));
    cmd.Parameters.AddWithValue("@preis", p.Preis);
    cmd.Parameters.Add("@bild", System.Data.SqlDbType.VarBinary, -1).Value = Db(p.Bild);
    await cmd.ExecuteNonQueryAsync();
}

app.Run();

record Session(int UserId, string Name, string Role, DateTime Expires);
record LoginBody(string? Username, string? Password);
record ProduktDto(int ProduktId, string? Name, string? Beschreibung, decimal Preis, byte[]? Bild);
record PositionDto(string? ProduktName, string? Beschreibung, decimal Preis, byte[]? Bild);
record AngebotSpeichern(string AngebotNr, List<ProduktDto>? Produkte);
record ProduktSpeichern(string? ProduktNr, string? Name, string? Beschreibung, decimal Preis, string? Kategorie, byte[]? Bild);
