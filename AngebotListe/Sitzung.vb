' Angemeldeter Benutzer (wird nach dem Login in Program.Main gesetzt)
Public Module Sitzung

    Public Property BenutzerId As Integer
    Public Property Benutzername As String = ""
    Public Property Rolle As String = ""

    Public ReadOnly Property IstAdmin As Boolean
        Get
            Return String.Equals(Rolle, "Admin", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    Public Sub Anmelden(id As Integer, name As String, rolle As String)
        BenutzerId = id
        Benutzername = name
        Sitzung.Rolle = rolle
    End Sub

End Module
