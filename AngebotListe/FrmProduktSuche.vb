Imports System.Data.SqlClient
Imports System.Globalization

' Produkt für eine Karte auswählen. Alle aktiven Produkte werden einmal geladen (ohne Bilder)
' und beim Tippen nur noch lokal gefiltert – früher gab es pro Tastendruck eine Datenbankabfrage.
Public Class FrmProduktSuche

    ' Wird von ProductCard gelesen (Einzelauswahl)
    Public SelectedProduct As DataRow

    ' Mehrfachauswahl: Häkchen vor jedem Produkt, Ergebnis in AusgewaehlteProdukte.
    ' Die Häkchen bleiben beim Filtern erhalten (sie stehen in der Datentabelle).
    Public Property MehrfachAuswahl As Boolean = False
    Public Property VorhandeneIds As New HashSet(Of Integer)   ' schon im Angebot -> grau
    Public ReadOnly Property AusgewaehlteProdukte As New List(Of DataRow)

    Private dtProdukte As DataTable

    Private Sub FrmProduktSuche_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tema.GridStil(dgvErgebnisse)
        SetzeHinweis(txtSuche, "Name, Art.-Nr., Beschreibung oder Kategorie …")
        LadeProdukte()
    End Sub

    Private Sub FrmProduktSuche_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtSuche.Focus()
    End Sub

    Private Sub LadeProdukte()
        Try
            Dim dt As New DataTable()
            Using con As New SqlConnection(Baglanti.AngebotListe)
                Using da As New SqlDataAdapter(
                    "SELECT ProduktId, ProduktNr, Name, Beschreibung, Preis, Kategorie FROM Products " &
                    "WHERE ISNULL(Aktiv, 1) = 1 ORDER BY Name", con)
                    da.Fill(dt)
                End Using
            End Using
            dt.Columns.Add("Auswahl", GetType(Boolean)).DefaultValue = False
            For Each r As DataRow In dt.Rows
                r("Auswahl") = False
            Next
            dt.Columns("Auswahl").SetOrdinal(0)
            dtProdukte = dt
        Catch ex As SqlException
            MessageBox.Show("Die Produkte konnten nicht geladen werden. Bitte Internetverbindung prüfen." &
                            vbCrLf & vbCrLf & ex.Message, "Produkt auswählen", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtProdukte = Nothing
            Return
        End Try

        dgvErgebnisse.DataSource = dtProdukte.DefaultView
        With dgvErgebnisse
            .Columns("Auswahl").Visible = MehrfachAuswahl
            .Columns("Auswahl").HeaderText = ""
            .Columns("Auswahl").Width = 40
            .ReadOnly = Not MehrfachAuswahl
            For Each c As DataGridViewColumn In .Columns
                c.ReadOnly = (c.Name <> "Auswahl")
            Next
            .Columns("ProduktId").Visible = False
            .Columns("ProduktNr").HeaderText = "Art.-Nr."
            .Columns("ProduktNr").Width = 80
            .Columns("Name").HeaderText = "Bezeichnung"
            .Columns("Name").Width = 230
            .Columns("Beschreibung").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns("Beschreibung").MinimumWidth = 120
            .Columns("Kategorie").Width = 110
            .Columns("Preis").Width = 90
            .Columns("Preis").DefaultCellStyle.Format = "N2"
            .Columns("Preis").DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("de-DE")
            .Columns("Preis").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Preis").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        FilterAnwenden()
        AuswahlAnzeigen()
    End Sub

    ' ── Mehrfachauswahl ──

    Private Sub MehrfachModusEinrichten(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not MehrfachAuswahl Then Return
        lblTitel.Text = "Produkte hinzufügen"
        lblUntertitel.Text = "Alle gewünschten Produkte anhaken (auch über mehrere Suchen hinweg), dann Hinzufügen"
        Text = "Produkte hinzufügen"
        Dim rechts As Integer = btnUebernehmen.Right
        btnUebernehmen.Width = 170
        btnUebernehmen.Left = rechts - 170
        btnAbbrechen.Left = btnUebernehmen.Left - 10 - btnAbbrechen.Width
    End Sub

    ' Häkchen sofort übernehmen (nicht erst beim Verlassen der Zelle)
    Private Sub dgvErgebnisse_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvErgebnisse.CurrentCellDirtyStateChanged
        If dgvErgebnisse.IsCurrentCellDirty Then dgvErgebnisse.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub dgvErgebnisse_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvErgebnisse.CellValueChanged
        If MehrfachAuswahl AndAlso e.RowIndex >= 0 Then AuswahlAnzeigen()
    End Sub

    Private Function AnzahlAusgewaehlt() As Integer
        If dtProdukte Is Nothing Then Return 0
        Return dtProdukte.AsEnumerable().Count(Function(r) r.Field(Of Boolean)("Auswahl"))
    End Function

    Private Sub AuswahlAnzeigen()
        If Not MehrfachAuswahl Then
            lblAuswahl.Text = ""
            Return
        End If
        Dim n As Integer = AnzahlAusgewaehlt()
        lblAuswahl.Text = If(n = 0, "Noch nichts ausgewählt", If(n = 1, "1 Produkt ausgewählt", $"{n} Produkte ausgewählt"))
        lblAuswahl.ForeColor = If(n = 0, Tema.TextGedimmt, Tema.Akzent)
        btnUebernehmen.Text = If(n = 0, "Hinzufügen", $"Hinzufügen ({n})")
    End Sub

    Private Sub UmschaltenZeile(index As Integer)
        If index < 0 OrElse index >= dgvErgebnisse.Rows.Count Then Return
        Dim drv = TryCast(dgvErgebnisse.Rows(index).DataBoundItem, DataRowView)
        If drv Is Nothing Then Return
        drv.Row("Auswahl") = Not CBool(drv.Row("Auswahl"))
        dgvErgebnisse.InvalidateRow(index)
        AuswahlAnzeigen()
    End Sub

    ' Produkte, die schon im Angebot sind, grau anzeigen
    Private Sub dgvErgebnisse_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvErgebnisse.CellFormatting
        If VorhandeneIds.Count = 0 OrElse e.RowIndex < 0 Then Return
        Dim drv = TryCast(dgvErgebnisse.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing OrElse Not VorhandeneIds.Contains(CInt(drv.Row("ProduktId"))) Then Return
        e.CellStyle.ForeColor = Tema.TextGedimmt
        If dgvErgebnisse.Columns(e.ColumnIndex).Name = "Name" Then
            e.Value = Convert.ToString(e.Value) & "   (bereits im Angebot)"
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub txtSuche_TextChanged(sender As Object, e As EventArgs) Handles txtSuche.TextChanged
        FilterAnwenden()
    End Sub

    Private Sub FilterAnwenden()
        If dtProdukte Is Nothing Then Return
        Dim s As String = txtSuche.Text.Trim().Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]")
        dtProdukte.DefaultView.RowFilter = If(s = "", "",
            $"Name LIKE '%{s}%' OR Beschreibung LIKE '%{s}%' OR Kategorie LIKE '%{s}%' " &
            $"OR CONVERT(ProduktNr, 'System.String') LIKE '%{s}%'")
        lblAnzahl.Text = If(dtProdukte.DefaultView.Count = 1, "1 Produkt", $"{dtProdukte.DefaultView.Count} Produkte")
        btnUebernehmen.Enabled = MehrfachAuswahl OrElse dtProdukte.DefaultView.Count > 0
    End Sub

    ' Pfeiltasten im Suchfeld bewegen die Auswahl in der Liste
    Private Sub txtSuche_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSuche.KeyDown
        If dgvErgebnisse.Rows.Count = 0 Then Return
        Dim idx As Integer = If(dgvErgebnisse.CurrentRow Is Nothing, -1, dgvErgebnisse.CurrentRow.Index)
        Select Case e.KeyCode
            Case Keys.Down : idx = Math.Min(idx + 1, dgvErgebnisse.Rows.Count - 1)
            Case Keys.Up : idx = Math.Max(idx - 1, 0)
            Case Else : Return
        End Select
        dgvErgebnisse.CurrentCell = dgvErgebnisse.Rows(idx).Cells("Name")
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub Uebernehmen()
        If MehrfachAuswahl Then
            AusgewaehlteProdukte.Clear()
            AusgewaehlteProdukte.AddRange(dtProdukte.AsEnumerable().Where(Function(r) r.Field(Of Boolean)("Auswahl")))
            If AusgewaehlteProdukte.Count = 0 AndAlso dgvErgebnisse.CurrentRow IsNot Nothing Then
                ' Nichts angehakt -> die markierte Zeile nehmen
                Dim d = TryCast(dgvErgebnisse.CurrentRow.DataBoundItem, DataRowView)
                If d IsNot Nothing Then AusgewaehlteProdukte.Add(d.Row)
            End If
            If AusgewaehlteProdukte.Count = 0 Then Return
            DialogResult = DialogResult.OK
            Close()
            Return
        End If
        If dgvErgebnisse.CurrentRow Is Nothing Then Return
        Dim drv = TryCast(dgvErgebnisse.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Return
        SelectedProduct = drv.Row
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnUebernehmen_Click(sender As Object, e As EventArgs) Handles btnUebernehmen.Click
        Uebernehmen()
    End Sub

    Private Sub dgvErgebnisse_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvErgebnisse.CellDoubleClick
        If e.RowIndex < 0 Then Return
        If MehrfachAuswahl Then
            ' Doppelklick auf eine Zeile = anhaken/abhaken (das Kästchen selbst schaltet schon beim Einfachklick)
            If dgvErgebnisse.Columns(e.ColumnIndex).Name <> "Auswahl" Then UmschaltenZeile(e.RowIndex)
        Else
            Uebernehmen()
        End If
    End Sub

    Private Sub btnAbbrechen_Click(sender As Object, e As EventArgs) Handles btnAbbrechen.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    ' Neue Produkte anlegen, danach Liste neu laden (Suchtext bleibt erhalten)
    Private Sub btnVerwalten_Click(sender As Object, e As EventArgs) Handles btnVerwalten.Click
        Using frm As New ArtikelEinfügen()
            frm.ShowDialog(Me)
        End Using
        LadeProdukte()
        txtSuche.Focus()
    End Sub

End Class
