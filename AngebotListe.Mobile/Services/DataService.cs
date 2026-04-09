using AngebotListe.Mobile.Models;
using Microsoft.Data.SqlClient;

namespace AngebotListe.Mobile.Services;

public static class DataService
{
    // ⚠️ Bağlantı dizisi tek yerde — değiştirmek için sadece burası
    private const string ConnectionString =
        "Data Source=185.187.170.107;Initial Catalog=AngebotListe;" +
        "User Id=salih;Password=Garahi1943;TrustServerCertificate=True;" +
        "Encrypt=False;Connection Timeout=10;";

    public static string GetConnectionString() => ConnectionString;

    private static SqlConnection NewConn() => new(ConnectionString);

    // ═══════════════════════════════════════════════════════════════════
    // AUTHENTICATION
    // ═══════════════════════════════════════════════════════════════════
    public static async Task<(bool Success, string Message, int UserId, string Username, string Role)>
        AuthenticateAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            return (false, "Benutzername darf nicht leer sein.", 0, "", "");

        try
        {
            using var con = NewConn();
            await con.OpenAsync();

            using var cmd = new SqlCommand(
                "SELECT TOP 1 Id, Username, PasswordHash, Role, IsActive FROM Users WHERE Username = @u", con);
            cmd.Parameters.AddWithValue("@u", username.Trim());

            using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync())
                return (false, "Benutzername oder Passwort falsch.", 0, "", "");

            var isActive = rd["IsActive"] != DBNull.Value && (bool)rd["IsActive"];
            if (!isActive)
                return (false, "Dieser Benutzer ist deaktiviert.", 0, "", "");

            var storedHash = rd["PasswordHash"] == DBNull.Value ? "" : (string)rd["PasswordHash"];

            // BCrypt veya plain text karşılaştırma
            bool ok;
            if (!string.IsNullOrEmpty(storedHash) &&
                (storedHash.StartsWith("$2a$") || storedHash.StartsWith("$2b$") || storedHash.StartsWith("$2y$")))
            {
                try { ok = BCrypt.Net.BCrypt.Verify(password, storedHash); }
                catch { ok = false; }
            }
            else
            {
                ok = (storedHash == password);
            }

            if (!ok)
                return (false, "Benutzername oder Passwort falsch.", 0, "", "");

            var id = Convert.ToInt32(rd["Id"]);
            var uname = rd["Username"] as string ?? "";
            var role = rd["Role"] as string ?? "User";
            return (true, "Erfolgreich angemeldet.", id, uname, role);
        }
        catch (Exception ex)
        {
            return (false, "Fehler: " + ex.Message, 0, "", "");
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    // PRODUCTS
    // ═══════════════════════════════════════════════════════════════════
    public static async Task<List<Product>> GetProductsAsync(string? search = null)
    {
        var list = new List<Product>();
        try
        {
            using var con = NewConn();
            await con.OpenAsync();

            var sql = "SELECT ProduktId, ProduktNr, Name, Beschreibung, Preis, Kategorie, Aktiv, Bild " +
                      "FROM Products WHERE Aktiv = 1";
            if (!string.IsNullOrWhiteSpace(search))
                sql += " AND (Name LIKE @s OR Beschreibung LIKE @s OR ProduktNr LIKE @s)";
            sql += " ORDER BY Name";

            using var cmd = new SqlCommand(sql, con);
            if (!string.IsNullOrWhiteSpace(search))
                cmd.Parameters.AddWithValue("@s", "%" + search + "%");

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Product
                {
                    ProduktId = rd.GetInt32(rd.GetOrdinal("ProduktId")),
                    ProduktNr = rd["ProduktNr"] as string,
                    Name = rd["Name"] as string,
                    Beschreibung = rd["Beschreibung"] as string,
                    Preis = rd["Preis"] == DBNull.Value ? 0 : Convert.ToDecimal(rd["Preis"]),
                    Kategorie = rd["Kategorie"] as string,
                    Aktiv = rd["Aktiv"] != DBNull.Value && (bool)rd["Aktiv"],
                    Bild = rd["Bild"] == DBNull.Value ? null : (byte[])rd["Bild"],
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GetProductsAsync error: " + ex.Message);
        }
        return list;
    }

    // ═══════════════════════════════════════════════════════════════════
    // ANGEBOTE
    // ═══════════════════════════════════════════════════════════════════
    public static async Task<List<Angebot>> GetAngeboteAsync()
    {
        var list = new List<Angebot>();
        try
        {
            using var con = NewConn();
            await con.OpenAsync();

            using var cmd = new SqlCommand(
                "SELECT a.AngebotID, a.AngebotNr, a.Datum, a.Beschreibung, " +
                "  (SELECT COUNT(*) FROM AngebotProdukte ap WHERE ap.AngebotID = a.AngebotID) AS ProduktAnzahl " +
                "FROM AngebotListe a ORDER BY a.AngebotID DESC", con);

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Angebot
                {
                    AngebotID = rd.GetInt32(rd.GetOrdinal("AngebotID")),
                    AngebotNr = rd["AngebotNr"] as string,
                    Datum = rd["Datum"] == DBNull.Value ? null : (DateTime?)rd["Datum"],
                    Beschreibung = rd["Beschreibung"] as string,
                    ProduktAnzahl = rd["ProduktAnzahl"] == DBNull.Value ? 0 : Convert.ToInt32(rd["ProduktAnzahl"]),
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GetAngeboteAsync error: " + ex.Message);
        }
        return list;
    }

    public static async Task<List<AngebotProdukt>> GetAngebotProdukteAsync(int angebotId)
    {
        var list = new List<AngebotProdukt>();
        try
        {
            using var con = NewConn();
            await con.OpenAsync();

            using var cmd = new SqlCommand(
                "SELECT ID, AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild " +
                "FROM AngebotProdukte WHERE AngebotID = @aid", con);
            cmd.Parameters.AddWithValue("@aid", angebotId);

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new AngebotProdukt
                {
                    ID = rd.GetInt32(rd.GetOrdinal("ID")),
                    AngebotID = rd.GetInt32(rd.GetOrdinal("AngebotID")),
                    ProduktID = rd.GetInt32(rd.GetOrdinal("ProduktID")),
                    ProduktName = rd["ProduktName"] as string,
                    Beschreibung = rd["Beschreibung"] as string,
                    Preis = rd["Preis"] == DBNull.Value ? 0 : Convert.ToDecimal(rd["Preis"]),
                    Bild = rd["Bild"] == DBNull.Value ? null : (byte[])rd["Bild"],
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GetAngebotProdukteAsync error: " + ex.Message);
        }
        return list;
    }

    public static async Task<int> CreateOrUpdateAngebotAsync(string angebotNr, List<Product> produkte)
    {
        try
        {
            using var con = NewConn();
            await con.OpenAsync();

            // Var mı kontrol et
            int angebotId;
            using (var check = new SqlCommand(
                "SELECT TOP 1 AngebotID FROM AngebotListe WHERE AngebotNr=@nr ORDER BY AngebotID DESC", con))
            {
                check.Parameters.AddWithValue("@nr", angebotNr);
                var existing = await check.ExecuteScalarAsync();
                if (existing != null && existing != DBNull.Value)
                {
                    angebotId = Convert.ToInt32(existing);
                    using var del = new SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con);
                    del.Parameters.AddWithValue("@aid", angebotId);
                    await del.ExecuteNonQueryAsync();
                }
                else
                {
                    using var ins = new SqlCommand(
                        "INSERT INTO AngebotListe (Datum, Beschreibung, AngebotNr) " +
                        "VALUES (@d, @b, @nr); SELECT SCOPE_IDENTITY();", con);
                    ins.Parameters.AddWithValue("@d", DateTime.Now);
                    ins.Parameters.AddWithValue("@b", "Mobile App");
                    ins.Parameters.AddWithValue("@nr", angebotNr);
                    angebotId = Convert.ToInt32(await ins.ExecuteScalarAsync());
                }
            }

            // Ürünleri ekle
            foreach (var p in produkte)
            {
                using var cmd = new SqlCommand(
                    "INSERT INTO AngebotProdukte (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild) " +
                    "VALUES (@aid, @pid, @name, @desc, @preis, @bild)", con);
                cmd.Parameters.AddWithValue("@aid", angebotId);
                cmd.Parameters.AddWithValue("@pid", p.ProduktId);
                cmd.Parameters.AddWithValue("@name", (object?)p.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@desc", (object?)p.Beschreibung ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@preis", p.Preis);
                cmd.Parameters.Add("@bild", System.Data.SqlDbType.VarBinary).Value =
                    (object?)p.Bild ?? DBNull.Value;
                await cmd.ExecuteNonQueryAsync();
            }

            return angebotId;
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
            using var con = NewConn();
            await con.OpenAsync();

            using (var del1 = new SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con))
            {
                del1.Parameters.AddWithValue("@aid", angebotId);
                await del1.ExecuteNonQueryAsync();
            }
            using (var del2 = new SqlCommand("DELETE FROM AngebotListe WHERE AngebotID=@aid", con))
            {
                del2.Parameters.AddWithValue("@aid", angebotId);
                await del2.ExecuteNonQueryAsync();
            }
            return true;
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
            using var con = NewConn();
            await con.OpenAsync();
            using var cmd = new SqlCommand(
                "INSERT INTO AngebotProdukte (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild) " +
                "VALUES (@aid, @pid, @name, @desc, @preis, @bild)", con);
            cmd.Parameters.AddWithValue("@aid", angebotId);
            cmd.Parameters.AddWithValue("@pid", p.ProduktId);
            cmd.Parameters.AddWithValue("@name", (object?)p.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@desc", (object?)p.Beschreibung ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@preis", p.Preis);
            cmd.Parameters.Add("@bild", System.Data.SqlDbType.VarBinary).Value =
                (object?)p.Bild ?? DBNull.Value;
            await cmd.ExecuteNonQueryAsync();
            return true;
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
            using var con = NewConn();
            await con.OpenAsync();
            using var cmd = new SqlCommand(
                "UPDATE AngebotProdukte SET ProduktName=@name, Beschreibung=@desc, Preis=@preis, Bild=@bild " +
                "WHERE ID=@id", con);
            cmd.Parameters.AddWithValue("@id", ap.ID);
            cmd.Parameters.AddWithValue("@name", (object?)ap.ProduktName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@desc", (object?)ap.Beschreibung ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@preis", ap.Preis);
            cmd.Parameters.Add("@bild", System.Data.SqlDbType.VarBinary).Value =
                (object?)ap.Bild ?? DBNull.Value;
            await cmd.ExecuteNonQueryAsync();
            return true;
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
            using var con = NewConn();
            await con.OpenAsync();
            using var cmd = new SqlCommand("DELETE FROM AngebotProdukte WHERE ID=@id", con);
            cmd.Parameters.AddWithValue("@id", angebotProduktId);
            await cmd.ExecuteNonQueryAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("DeleteAngebotProdukt error: " + ex.Message);
            return false;
        }
    }

    // ═══════════════════════════════════════════════════════════════════
    // ANGEBOT NR — günlük artan numara (ANG-yyyyMMdd-NNNN)
    // ═══════════════════════════════════════════════════════════════════
    public static async Task<string> NextAngebotNrAsync()
    {
        var prefix = "ANG-" + DateTime.Now.ToString("yyyyMMdd") + "-";
        try
        {
            using var con = NewConn();
            await con.OpenAsync();
            using var cmd = new SqlCommand(
                "SELECT TOP 1 AngebotNr FROM AngebotListe " +
                "WHERE AngebotNr LIKE @p + '%' ORDER BY AngebotNr DESC", con);
            cmd.Parameters.AddWithValue("@p", prefix);
            var last = await cmd.ExecuteScalarAsync() as string;
            int n = 1;
            if (!string.IsNullOrEmpty(last))
            {
                var parts = last.Split('-');
                if (parts.Length >= 3 && int.TryParse(parts[2], out var num)) n = num + 1;
            }
            return prefix + n.ToString("D4");
        }
        catch
        {
            return prefix + "0001";
        }
    }
}
