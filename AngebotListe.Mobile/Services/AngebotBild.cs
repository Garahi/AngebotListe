using System.Globalization;
using AngebotListe.Mobile.Models;
using SkiaSharp;

namespace AngebotListe.Mobile.Services;

// Erzeugt das Bild, das per WhatsApp verschickt wird – als Liste wie in der App:
// oben Angebot-Nr / Datum, je Produkt eine Karte (Bild links, Name + Beschreibung, Preis rechts),
// ganz unten EINMAL der grüne Balken mit dem Firmennamen.
// Viele Produkte → mehrere Bilder (je max. 8), damit WhatsApp sie nicht unleserlich verkleinert.
public static class AngebotBild
{
    public const string StandardFirma = "My Fresh Fruit Yeşil GmbH";
    public static string Firmenname
    {
        get => Preferences.Get("firmenname", StandardFirma);
        set => Preferences.Set("firmenname", string.IsNullOrWhiteSpace(value) ? StandardFirma : value.Trim());
    }

    const int Breite = 1080, Rand = 32, KopfH = 190, FussH = 140;
    const int ZeileH = 230, ZeilenAbstand = 20, BildGr = 190, ProSeite = 8;

    static readonly SKColor Gruen = new(0x1B, 0x5E, 0x20);
    static readonly SKColor Hintergrund = new(0xF3, 0xF4, 0xF6);
    static readonly SKColor Schwarz = new(0x1F, 0x29, 0x37);
    static readonly SKColor Grau = new(0x6B, 0x72, 0x80);
    static readonly CultureInfo De = new("de-DE");

    static SKTypeface Schrift(bool fett) =>
        SKTypeface.FromFamilyName(null, fett ? SKFontStyle.Bold : SKFontStyle.Normal) ?? SKTypeface.Default;

    /// <summary>Erstellt die Bilder und gibt die Dateipfade zurück.</summary>
    public static async Task<List<string>> ErstellenAsync(Angebot angebot, IList<AngebotProdukt> produkte)
    {
        var pfade = new List<string>();
        int seiten = Math.Max(1, (int)Math.Ceiling(produkte.Count / (double)ProSeite));
        var stempel = DateTime.Now.ToString("HHmmss");
        for (int s = 0; s < seiten; s++)
        {
            var teil = produkte.Skip(s * ProSeite).Take(ProSeite).ToList();
            var bytes = Zeichnen(angebot, teil, produkte.Count, s + 1, seiten);
            var pfad = Path.Combine(FileSystem.CacheDirectory,
                $"Angebot_{Sicher(angebot.AngebotNr)}_{stempel}_{s + 1}.jpg");
            await File.WriteAllBytesAsync(pfad, bytes);
            pfade.Add(pfad);
        }
        return pfade;
    }

    static string Sicher(string? s) =>
        string.Concat((s ?? "Angebot").Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));

    static byte[] Zeichnen(Angebot angebot, List<AngebotProdukt> produkte, int gesamt, int seite, int seiten)
    {
        int hoehe = KopfH + produkte.Count * (ZeileH + ZeilenAbstand) + Rand + FussH;
        using var surface = SKSurface.Create(new SKImageInfo(Breite, hoehe, SKColorType.Rgba8888, SKAlphaType.Premul));
        var c = surface.Canvas;
        c.Clear(Hintergrund);

        using var fett = Schrift(true);
        using var normal = Schrift(false);
        using var paint = new SKPaint { IsAntialias = true };

        // ── Kopf: weiße Karte mit Angebot-Nr und Datum ────
        var kopf = new SKRect(Rand, Rand, Breite - Rand, KopfH - 16);
        paint.Color = SKColors.White;
        c.DrawRoundRect(kopf, 32, 32, paint);
        paint.Color = Schwarz;
        using (var f = new SKFont(fett, 46))
            c.DrawText(Kuerzen("Angebot " + angebot.AngebotNr, f, kopf.Width - 64), kopf.Left + 36, kopf.Top + 66, SKTextAlign.Left, f, paint);
        var unter = $"Datum: {angebot.DatumFormatted}   ·   Produkte: {gesamt}";
        if (seiten > 1) unter += $"   ·   Seite {seite}/{seiten}";
        paint.Color = Grau;
        using (var f = new SKFont(normal, 32))
            c.DrawText(unter, kopf.Left + 36, kopf.Top + 116, SKTextAlign.Left, f, paint);

        // ── Produktzeilen ──────────────────────────────
        float y = KopfH;
        foreach (var p in produkte)
        {
            ZeileZeichnen(c, p, y, fett, normal);
            y += ZeileH + ZeilenAbstand;
        }

        // ── Unten EINMAL: grüner Balken mit Firmenname ──
        float fy = hoehe - FussH;
        paint.Color = Gruen;
        c.DrawRect(0, fy, Breite, FussH, paint);
        paint.Color = SKColors.White;
        using (var f = new SKFont(fett, 52))
            c.DrawText(Kuerzen(Firmenname, f, Breite - 2 * Rand), Breite / 2f, fy + FussH / 2f + 18, SKTextAlign.Center, f, paint);

        using var bild = surface.Snapshot();
        using var daten = bild.Encode(SKEncodedImageFormat.Jpeg, 92);
        return daten.ToArray();
    }

    static void ZeileZeichnen(SKCanvas c, AngebotProdukt p, float y, SKTypeface fett, SKTypeface normal)
    {
        using var paint = new SKPaint { IsAntialias = true, Color = SKColors.White };
        var karte = new SKRect(Rand, y, Breite - Rand, y + ZeileH);
        c.DrawRoundRect(karte, 28, 28, paint);

        // Bild links (ganz sichtbar, nicht zugeschnitten)
        float pad = (ZeileH - BildGr) / 2f;
        var feld = new SKRect(karte.Left + pad, y + pad, karte.Left + pad + BildGr, y + pad + BildGr);
        c.Save();
        c.ClipRoundRect(new SKRoundRect(feld, 18, 18), SKClipOperation.Intersect, true);
        if (p.Bild is { Length: > 0 })
        {
            try
            {
                using var bmp = SKBitmap.Decode(p.Bild);
                if (bmp != null)
                {
                    using var img = SKImage.FromBitmap(bmp);
                    c.DrawImage(img, Einpassen(bmp.Width, bmp.Height, feld),
                                new SKSamplingOptions(SKCubicResampler.Mitchell), null);
                }
            }
            catch { /* Bild defekt → leeres Feld */ }
        }
        c.Restore();

        // Preis rechts
        using var fPreis = new SKFont(fett, 48);
        var preisText = p.Preis.ToString("N2", De) + " €";
        float pw = fPreis.MeasureText(preisText);
        paint.Color = Gruen;
        c.DrawText(preisText, karte.Right - 36, karte.MidY + 17, SKTextAlign.Right, fPreis, paint);

        // Name + Beschreibung in der Mitte
        using var fName = new SKFont(fett, 38);
        using var fBeschr = new SKFont(normal, 32);
        float tx = feld.Right + 32, tBreite = karte.Right - 36 - pw - 32 - tx;
        var name = Umbrechen(p.ProduktName ?? "", fName, tBreite, 2);
        var beschr = (p.Beschreibung ?? "").Trim();
        var bZeilen = beschr.Length > 0 && !string.Equals(beschr, p.ProduktName?.Trim(), StringComparison.OrdinalIgnoreCase)
            ? Umbrechen(beschr, fBeschr, tBreite, name.Count > 1 ? 1 : 2) : new List<string>();

        float blockH = name.Count * 48 + bZeilen.Count * 42;
        float ty = karte.MidY - blockH / 2 + 36;
        paint.Color = Schwarz;
        foreach (var z in name) { c.DrawText(z, tx, ty, SKTextAlign.Left, fName, paint); ty += 48; }
        paint.Color = Grau;
        foreach (var z in bZeilen) { c.DrawText(z, tx, ty, SKTextAlign.Left, fBeschr, paint); ty += 42; }
    }

    static SKRect Einpassen(int w, int h, SKRect feld)
    {
        float f = Math.Min(feld.Width / w, feld.Height / h);
        float bw = w * f, bh = h * f;
        return SKRect.Create(feld.MidX - bw / 2, feld.MidY - bh / 2, bw, bh);
    }

    static string Kuerzen(string text, SKFont f, float max)
    {
        if (f.MeasureText(text) <= max) return text;
        while (text.Length > 1 && f.MeasureText(text + "…") > max) text = text[..^1];
        return text.TrimEnd() + "…";
    }

    static List<string> Umbrechen(string text, SKFont f, float max, int maxZeilen)
    {
        var zeilen = new List<string>();
        var woerter = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var aktuell = "";
        int i = 0;
        for (; i < woerter.Length; i++)
        {
            var test = aktuell.Length == 0 ? woerter[i] : aktuell + " " + woerter[i];
            if (f.MeasureText(test) <= max || aktuell.Length == 0) { aktuell = test; continue; }
            zeilen.Add(aktuell);
            aktuell = woerter[i];
            if (zeilen.Count == maxZeilen) break;
        }
        if (zeilen.Count < maxZeilen && aktuell.Length > 0) { zeilen.Add(aktuell); i = woerter.Length; }
        if (zeilen.Count > 0)
            zeilen[^1] = i < woerter.Length ? Kuerzen(zeilen[^1] + "…", f, max) : Kuerzen(zeilen[^1], f, max);
        return zeilen;
    }
}
