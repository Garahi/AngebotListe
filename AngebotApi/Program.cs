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
    finally { Log($"{ctx.Request.Method} {ctx.Request.Path}{ctx.Request.QueryString} = {ctx.Response.StatusCode} ({sw.ElapsedMilliseconds} ms)"); }
});

var sessions = new ConcurrentDictionary<string, Session>();
var failed = new ConcurrentDictionary<string, (int Count, DateTime First)>();

IResult Err(int status, string msg) => Results.Json(new { error = msg }, statusCode: status);

Session? Auth(HttpRequest req)
{
    var h = req.Headers.Authorization.ToString();
    if (!h.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) return null;
    var token = h[7..].Trim();
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
app.MapGet("/api/products", (HttpRequest req, string? search) => Safe(req, async () =>
{
    var sql = "SELECT ProduktId, ProduktNr, Name, Beschreibung, Preis, Kategorie, Aktiv, Bild FROM Products WHERE Aktiv = 1";
    if (!string.IsNullOrWhiteSpace(search)) sql += " AND (Name LIKE @s OR Beschreibung LIKE @s OR CAST(ProduktNr AS nvarchar(100)) LIKE @s)";
    sql += " ORDER BY Name";
    await using var con = await Open();
    await using var cmd = new SqlCommand(sql, con);
    if (!string.IsNullOrWhiteSpace(search)) cmd.Parameters.AddWithValue("@s", "%" + search + "%");
    await using var rd = await cmd.ExecuteReaderAsync();
    var list = new List<object>();
    while (await rd.ReadAsync())
        list.Add(new
        {
            produktId = Convert.ToInt32(rd["ProduktId"]),
            produktNr = rd["ProduktNr"] == DBNull.Value ? null : rd["ProduktNr"].ToString(),
            name = rd["Name"] as string,
            beschreibung = rd["Beschreibung"] as string,
            preis = rd["Preis"] == DBNull.Value ? 0m : Convert.ToDecimal(rd["Preis"]),
            kategorie = rd["Kategorie"] as string,
            aktiv = rd["Aktiv"] != DBNull.Value && Convert.ToBoolean(rd["Aktiv"]),
            bild = rd["Bild"] as byte[]
        });
    return Results.Json(list);
}));

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
        "INSERT INTO AngebotProdukte (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild) VALUES (@aid, @pid, @name, @desc, @preis, @bild)", con, tx);
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
