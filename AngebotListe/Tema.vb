Imports System.Drawing
Imports System.Windows.Forms

' Gemeinsames Erscheinungsbild aller Formulare (modernes, flaches Design nach Fluent-Richtlinien:
' Segoe UI, neutrale Flächen, eine Akzentfarbe für Hauptaktionen).
' Die Formulare selbst sind im Designer gestaltet; dieses Modul wird nur für Dinge verwendet,
' die zur Laufzeit entstehen (z. B. Spalten einer DataGridView nach dem Laden der Daten).
Public Module Tema

    Public ReadOnly Akzent As Color = Color.FromArgb(26, 115, 232)
    Public ReadOnly AkzentDunkel As Color = Color.FromArgb(21, 87, 176)
    Public ReadOnly Kopf As Color = Color.FromArgb(31, 42, 68)
    Public ReadOnly Flaeche As Color = Color.FromArgb(244, 246, 250)
    Public ReadOnly Rahmen As Color = Color.FromArgb(225, 229, 235)
    Public ReadOnly TextFarbe As Color = Color.FromArgb(31, 41, 55)
    Public ReadOnly TextGedimmt As Color = Color.FromArgb(107, 114, 128)
    Public ReadOnly Gefahr As Color = Color.FromArgb(217, 48, 37)
    Public ReadOnly WhatsAppGruen As Color = Color.FromArgb(31, 168, 85)
    Public ReadOnly AuswahlHell As Color = Color.FromArgb(232, 240, 254)

    ' Einheitliche Tabellen: helle Kopfzeile, nur waagerechte Linien, ruhige Auswahlfarbe
    Public Sub GridStil(dgv As DataGridView)
        If dgv Is Nothing Then Return
        With dgv
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .GridColor = Rahmen
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .EnableHeadersVisualStyles = False

            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 36
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 243, 246)
            .ColumnHeadersDefaultCellStyle.ForeColor = TextGedimmt
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.SelectionBackColor = .ColumnHeadersDefaultCellStyle.BackColor
            .ColumnHeadersDefaultCellStyle.SelectionForeColor = .ColumnHeadersDefaultCellStyle.ForeColor
            .ColumnHeadersDefaultCellStyle.Padding = New Padding(6, 0, 6, 0)

            .DefaultCellStyle.Font = New Font("Segoe UI", 9.75F)
            .DefaultCellStyle.ForeColor = TextFarbe
            .DefaultCellStyle.BackColor = Color.White
            .DefaultCellStyle.SelectionBackColor = AuswahlHell
            .DefaultCellStyle.SelectionForeColor = TextFarbe
            .DefaultCellStyle.Padding = New Padding(6, 0, 6, 0)
            .RowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252)
            .RowTemplate.Height = 32
        End With
    End Sub

    ' Grauer Platzhaltertext in einem einzeiligen Textfeld (z. B. "Suchen …")
    <Runtime.InteropServices.DllImport("user32.dll", CharSet:=Runtime.InteropServices.CharSet.Unicode)>
    Private Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As String) As IntPtr
    End Function

    Public Sub SetzeHinweis(tb As TextBox, text As String)
        Try
            SendMessage(tb.Handle, &H1501, New IntPtr(1), text)   ' EM_SETCUEBANNER
        Catch
        End Try
    End Sub

End Module
