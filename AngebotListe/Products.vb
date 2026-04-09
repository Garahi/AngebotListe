Imports System.Data.SqlClient

Public Class Product
    Public Property ID As Integer
    Public Property ProduktId As Integer      ' ✔ EKLENDİ
    Public Property ProduktNr As String      ' (bunu da kullanıyorsun, o da kalsın)
    Public Property Name As String
    Public Property Beschreibung As String
    Public Property Preis As Decimal
    Public Property Bild As Byte()   ' ✅ YENİ
    Public Property BildPfad As String
    Public Property Kategorie As String
    Public Property Aktiv As Boolean
    Public Property ErstelltAm As DateTime
    Public Property AngebotID As Integer?
    Public Property OriginalProductID As Integer

End Class

