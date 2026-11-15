Imports System.IO

' Programmeinstellungen (früher fest im Code): Firmenname, PDF-Ordner, WhatsApp-Kontaktdatei.
' Gespeichert pro Windows-Benutzer unter %AppData%\AngebotListe\einstellungen.txt (Schlüssel=Wert).
Public Module Einstellungen

    Private ReadOnly Datei As String =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AngebotListe", "einstellungen.txt")

    Public Property Firmenname As String = "My Fresh Fruit Yeşil GmbH"
    Public Property PdfOrdner As String = "C:\AngebotListe\WhatsapaYollanilan"

    ' Kontaktliste für WhatsApp (Zeilen "Name|Telefon"), gleicher Ort wie bisher
    Public ReadOnly Property KontaktDatei As String = "C:\AngebotListe\whatsapp_kontakte.txt"

    Public Sub Laden()
        Try
            If File.Exists(Datei) Then
                For Each zeile In File.ReadAllLines(Datei)
                    Dim i As Integer = zeile.IndexOf("="c)
                    If i <= 0 Then Continue For
                    Dim wert As String = zeile.Substring(i + 1).Trim()
                    Select Case zeile.Substring(0, i).Trim()
                        Case "Firmenname" : If wert <> "" Then Firmenname = wert
                        Case "PdfOrdner" : If wert <> "" Then PdfOrdner = wert
                    End Select
                Next
            End If
        Catch
            ' Standardwerte verwenden
        End Try
        ProductCard.GlobalFirmaAdi = Firmenname
    End Sub

    Public Sub Speichern()
        Directory.CreateDirectory(Path.GetDirectoryName(Datei))
        File.WriteAllLines(Datei, {"Firmenname=" & Firmenname, "PdfOrdner=" & PdfOrdner})
        ProductCard.GlobalFirmaAdi = Firmenname
    End Sub

    ' Ordner mit abschließendem "\" (wird bei Bedarf angelegt)
    Public Function PdfOrdnerPfad() As String
        Dim o As String = PdfOrdner.TrimEnd("\"c) & "\"
        Directory.CreateDirectory(o)
        Return o
    End Function

    Public Function PdfPfad(angebotNr As String) As String
        Return Path.Combine(PdfOrdnerPfad(), angebotNr & ".pdf")
    End Function

End Module
