' Verbindungszeichenfolgen an EINER Stelle (vorher in sieben Dateien kopiert).
' Hinweis: Zugangsdaten im Programm sind für jeden auslesbar, der die EXE besitzt.
' Langfristig sollte der Zugriff über einen eigenen Server (API) laufen statt direkt auf SQL Server.
Public Module Baglanti

    ' Angebote, Produkte, Benutzer
    Public ReadOnly AngebotListe As String =
        "Data Source=185.187.170.107;Initial Catalog=AngebotListe;Persist Security Info=True;" &
        "User Id=salih;Password=Garahi1943;TrustServerCertificate=True;Encrypt=False;Connection Timeout=15;"

    ' Lizenzverwaltung
    Public ReadOnly Lizenz As String =
        "Data Source=185.187.170.107;Initial Catalog=SalihLicenseAdmin;Persist Security Info=True;" &
        "User Id=salih;Password=Garahi1943;TrustServerCertificate=True;Encrypt=False;Connection Timeout=15;"

End Module
