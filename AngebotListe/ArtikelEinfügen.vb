Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO

' Produkte verwalten: Liste links (Suche, Filter), Bearbeitung rechts.
' Das Layout ist komplett im Designer angelegt.
Public Class ArtikelEinfügen

    Private ReadOnly cs As String = Baglanti.AngebotListe
    Private ReadOnly de As CultureInfo = CultureInfo.GetCultureInfo("de-DE")

    Private dtProdukte As DataTable
    Private aktuelleId As Integer = 0          ' 0 = neuer Artikel
    Private bildGeaendert As Boolean = False   ' Bild nur speichern, wenn es wirklich geändert wurde
    Private ladeLaeuft As Boolean = False

    ' ───────────────────────────── Start ─────────────────────────────

    Private Sub ArtikelEinfügen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tema.GridStil(dgvProdukte)
        picBild.AllowDrop = True
        SetzeHinweis(txtSuche, "Suchen nach Name, Art.-Nr., Kategorie …")
        If Not LadeProdukte() Then Return
        NeuerArtikel()
        BeginInvoke(New Action(AddressOf NachDemAnzeigen))
    End Sub

    Private Function LadeProdukte(Optional auswahlId As Integer = 0) As Boolean
        Try
            Dim dt As New DataTable()
            Using con As New SqlConnection(cs)
                Using da As New SqlDataAdapter(
                    "SELECT ProduktId, ProduktNr, Name, Beschreibung, Preis, Kategorie, " &
                    "CAST(ISNULL(Aktiv, 0) AS bit) AS Aktiv, ErstelltAm FROM Products ORDER BY Name", con)
                    da.Fill(dt)
                End Using
            End Using
            dtProdukte = dt
        Catch ex As SqlException
            MessageBox.Show("Die Produkte konnten nicht geladen werden. Bitte Internetverbindung prüfen." &
                            vbCrLf & vbCrLf & ex.Message, "Produkte verwalten", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try

        ladeLaeuft = True
        dgvProdukte.DataSource = dtProdukte.DefaultView
        SpaltenEinrichten()
        FilterAnwenden()
        KategorienFuellen()
        ladeLaeuft = False

        If auswahlId > 0 Then ZeileAuswaehlen(auswahlId)
        Return True
    End Function

    Private Sub SpaltenEinrichten()
        With dgvProdukte
            .Columns("ProduktId").Visible = False
            .Columns("ErstelltAm").Visible = False

            .Columns("ProduktNr").HeaderText = "Art.-Nr."
            .Columns("ProduktNr").Width = 80
            .Columns("Name").HeaderText = "Bezeichnung"
            .Columns("Name").Width = 200
            .Columns("Beschreibung").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns("Beschreibung").MinimumWidth = 120
            .Columns("Kategorie").Width = 110
            .Columns("Preis").HeaderText = "Preis"
            .Columns("Preis").Width = 80
            .Columns("Preis").DefaultCellStyle.Format = "N2"
            .Columns("Preis").DefaultCellStyle.FormatProvider = de
            .Columns("Preis").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Preis").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Aktiv").Width = 56
        End With
    End Sub

    Private Sub KategorienFuellen()
        Dim aktuell As String = cmbKategorie.Text
        Dim liste As New SortedSet(Of String)(StringComparer.CurrentCultureIgnoreCase) From
            {"Obst", "Gemüse", "Fleisch", "Milchprodukte", "Backwaren", "Getränke", "Sonstige"}
        For Each r As DataRow In dtProdukte.Rows
            Dim k As String = If(IsDBNull(r("Kategorie")), "", r("Kategorie").ToString().Trim())
            If k <> "" Then liste.Add(k)
        Next
        cmbKategorie.BeginUpdate()
        cmbKategorie.Items.Clear()
        For Each k In liste
            cmbKategorie.Items.Add(k)
        Next
        cmbKategorie.EndUpdate()
        cmbKategorie.Text = aktuell
    End Sub

    ' ───────────────────────────── Suche / Filter ─────────────────────────────

    Private Sub txtSuche_TextChanged(sender As Object, e As EventArgs) Handles txtSuche.TextChanged
        FilterAnwenden()
    End Sub

    Private Sub chkNurAktive_CheckedChanged(sender As Object, e As EventArgs) Handles chkNurAktive.CheckedChanged
        FilterAnwenden()
    End Sub

    Private Sub FilterAnwenden()
        If dtProdukte Is Nothing Then Return
        Dim teile As New List(Of String)
        Dim s As String = FilterText(txtSuche.Text.Trim())
        If s <> "" Then
            teile.Add($"(Name LIKE '%{s}%' OR Beschreibung LIKE '%{s}%' OR Kategorie LIKE '%{s}%' " &
                      $"OR CONVERT(ProduktNr, 'System.String') LIKE '%{s}%')")
        End If
        If chkNurAktive.Checked Then teile.Add("Aktiv = true")

        Dim vorher As Boolean = ladeLaeuft
        ladeLaeuft = True
        dtProdukte.DefaultView.RowFilter = String.Join(" AND ", teile)
        ladeLaeuft = vorher

        lblAnzahl.Text = $"{dtProdukte.DefaultView.Count} von {dtProdukte.Rows.Count} Artikeln"
        If aktuelleId > 0 Then
            ZeileAuswaehlen(aktuelleId, nurMarkieren:=True)
        Else
            AuswahlAufheben()
        End If
    End Sub

    Private Sub AuswahlAufheben()
        Dim vorher As Boolean = ladeLaeuft
        ladeLaeuft = True
        dgvProdukte.ClearSelection()
        dgvProdukte.CurrentCell = Nothing
        ladeLaeuft = vorher
    End Sub

    ' Das Grid markiert beim ersten Anzeigen von selbst die erste Zeile – bei "Neuer Artikel" wieder aufheben
    ' (wird per BeginInvoke aus Load aufgerufen, weil Shown bei eingebetteten Formularen nicht ausgelöst wird)
    Private Sub NachDemAnzeigen()
        If aktuelleId = 0 Then AuswahlAufheben()
        txtArtNr.Focus()
    End Sub

    ' Sonderzeichen für DataView.RowFilter maskieren
    Private Shared Function FilterText(t As String) As String
        Dim sb As New Text.StringBuilder()
        For Each ch As Char In t
            Select Case ch
                Case "'"c : sb.Append("''")
                Case "["c, "]"c, "*"c, "%"c : sb.Append("[").Append(ch).Append("]")
                Case Else : sb.Append(ch)
            End Select
        Next
        Return sb.ToString()
    End Function

    Private Sub ZeileAuswaehlen(id As Integer, Optional nurMarkieren As Boolean = False)
        Dim vorher As Boolean = ladeLaeuft
        ladeLaeuft = nurMarkieren
        dgvProdukte.ClearSelection()
        For Each r As DataGridViewRow In dgvProdukte.Rows
            If CInt(r.Cells("ProduktId").Value) = id Then
                r.Selected = True
                dgvProdukte.CurrentCell = r.Cells("Name")
                Exit For
            End If
        Next
        ladeLaeuft = vorher
    End Sub

    Private Sub dgvProdukte_SelectionChanged(sender As Object, e As EventArgs) Handles dgvProdukte.SelectionChanged
        If ladeLaeuft OrElse dgvProdukte.SelectedRows.Count = 0 Then Return
        Dim drv = TryCast(dgvProdukte.SelectedRows(0).DataBoundItem, DataRowView)
        If drv IsNot Nothing Then ZeigeArtikel(drv.Row)
    End Sub

    ' ───────────────────────────── Anzeigen / Neu ─────────────────────────────

    Private Sub ZeigeArtikel(r As DataRow)
        aktuelleId = CInt(r("ProduktId"))
        txtArtNr.Text = Wert(r("ProduktNr"))
        txtName.Text = Wert(r("Name"))
        txtBeschreibung.Text = Wert(r("Beschreibung"))
        txtPreis.Text = If(IsDBNull(r("Preis")), "", CDec(r("Preis")).ToString("0.00", de))
        cmbKategorie.Text = Wert(r("Kategorie"))
        chkAktiv.Checked = CBool(r("Aktiv"))

        lblDetailTitel.Text = "Artikel bearbeiten"
        lblInfo.Text = If(IsDBNull(r("ErstelltAm")), "Gespeichert ✓",
                          "Angelegt am " & CDate(r("ErstelltAm")).ToString("dd.MM.yyyy"))
        btnLoeschen.Enabled = True
        LadeBild(aktuelleId)
    End Sub

    Private Shared Function Wert(o As Object) As String
        Return If(o Is Nothing OrElse IsDBNull(o), "", o.ToString())
    End Function

    Private Sub NeuerArtikel()
        aktuelleId = 0
        AuswahlAufheben()

        txtArtNr.Clear()
        txtName.Clear()
        txtBeschreibung.Clear()
        txtPreis.Clear()
        cmbKategorie.Text = ""
        chkAktiv.Checked = True
        BildSetzen(Nothing)
        bildGeaendert = False

        lblDetailTitel.Text = "Neuer Artikel"
        lblInfo.Text = "Noch nicht gespeichert"
        btnLoeschen.Enabled = False
        txtArtNr.Focus()
    End Sub

    Private Sub btnNeu_Click(sender As Object, e As EventArgs) Handles btnNeu.Click
        NeuerArtikel()
    End Sub

    ' ───────────────────────────── Bild ─────────────────────────────

    Private Sub LadeBild(id As Integer)
        BildSetzen(Nothing)
        bildGeaendert = False
        Try
            Using con As New SqlConnection(cs)
                con.Open()
                Using cmd As New SqlCommand("SELECT Bild, BildPfad FROM Products WHERE ProduktId = @id", con)
                    cmd.Parameters.AddWithValue("@id", id)
                    Using rd = cmd.ExecuteReader()
                        If rd.Read() Then
                            If Not IsDBNull(rd("Bild")) Then
                                BildSetzen(ImageHelper.ByteArrayToImage(CType(rd("Bild"), Byte())))
                            ElseIf Not IsDBNull(rd("BildPfad")) AndAlso File.Exists(rd("BildPfad").ToString()) Then
                                BildSetzen(BildAusDatei(rd("BildPfad").ToString()))
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch
            ' Ohne Bild weiterarbeiten
        End Try
    End Sub

    ' Lädt ein Bild, ohne die Datei zu sperren (Image.FromFile hält die Datei bis zum Schließen offen)
    Private Shared Function BildAusDatei(pfad As String) As Image
        Using fs As New FileStream(pfad, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Using tmp As Image = Image.FromStream(fs)
                Return New Bitmap(tmp)
            End Using
        End Using
    End Function

    Private Sub BildSetzen(img As Image)
        Dim alt As Image = picBild.Image
        picBild.Image = img
        If alt IsNot Nothing AndAlso alt IsNot img Then alt.Dispose()
        lnkBildEntfernen.Visible = (img IsNot Nothing)
    End Sub

    Private Sub BildUebernehmen(pfad As String)
        Try
            BildSetzen(BildAusDatei(pfad))
            bildGeaendert = True
        Catch ex As Exception
            MessageBox.Show("Das Bild konnte nicht geladen werden: " & ex.Message, "Bild",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub picBild_Click(sender As Object, e As EventArgs) Handles picBild.Click
        If dlgBild.ShowDialog(Me) = DialogResult.OK Then BildUebernehmen(dlgBild.FileName)
    End Sub

    Private Sub picBild_DragEnter(sender As Object, e As DragEventArgs) Handles picBild.DragEnter
        e.Effect = If(e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub picBild_DragDrop(sender As Object, e As DragEventArgs) Handles picBild.DragDrop
        Dim dateien = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If dateien IsNot Nothing AndAlso dateien.Length > 0 Then BildUebernehmen(dateien(0))
    End Sub

    Private Sub lnkBildEntfernen_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkBildEntfernen.LinkClicked
        BildSetzen(Nothing)
        bildGeaendert = True
    End Sub

    ' ───────────────────────────── Speichern ─────────────────────────────

    Private Function PreisLesen(ByRef preis As Decimal) As Boolean
        Dim t As String = txtPreis.Text.Trim().Replace("€", "").Trim()
        If t = "" Then preis = 0 : Return True
        ' "12,50" (deutsch) und "12.50" werden beide akzeptiert
        If Not t.Contains(",") AndAlso t.Count(Function(c) c = "."c) = 1 Then t = t.Replace(".", ",")
        Return Decimal.TryParse(t, NumberStyles.Number, de, preis) AndAlso preis >= 0
    End Function

    Private Sub btnSpeichern_Click(sender As Object, e As EventArgs) Handles btnSpeichern.Click
        Dim name As String = txtName.Text.Trim()
        If name = "" Then
            MessageBox.Show("Bitte eine Bezeichnung eingeben.", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtName.Focus()
            Return
        End If

        Dim preis As Decimal
        If Not PreisLesen(preis) Then
            MessageBox.Show("Bitte einen gültigen Preis eingeben (z. B. 12,50).", "Speichern",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPreis.Focus()
            Return
        End If

        Dim artNr As String = txtArtNr.Text.Trim()
        Dim bild As Byte() = If(bildGeaendert, ImageHelper.ImageToByteArray(picBild.Image), Nothing)

        Try
            Using con As New SqlConnection(cs)
                con.Open()

                ' Doppelte Artikelnummer verhindern
                If artNr <> "" Then
                    Using chk As New SqlCommand(
                        "SELECT COUNT(*) FROM Products WHERE CAST(ProduktNr AS nvarchar(100)) = @nr AND ProduktId <> @id", con)
                        chk.Parameters.AddWithValue("@nr", artNr)
                        chk.Parameters.AddWithValue("@id", aktuelleId)
                        If CInt(chk.ExecuteScalar()) > 0 Then
                            MessageBox.Show($"Die Artikelnummer {artNr} ist schon vergeben.", "Speichern",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtArtNr.Focus()
                            Return
                        End If
                    End Using
                End If

                Dim sql As String
                If aktuelleId = 0 Then
                    sql = "INSERT INTO Products (ProduktNr, Name, Beschreibung, Preis, Bild, Kategorie, Aktiv, ErstelltAm) " &
                          "VALUES (@nr, @name, @beschr, @preis, @bild, @kat, @aktiv, GETDATE()); " &
                          "SELECT CAST(SCOPE_IDENTITY() AS int);"
                Else
                    ' Angelegt-Datum und Angebotszuordnung bleiben unverändert (wurden früher überschrieben)
                    sql = "UPDATE Products SET ProduktNr = @nr, Name = @name, Beschreibung = @beschr, Preis = @preis, " &
                          "Kategorie = @kat, Aktiv = @aktiv" & If(bildGeaendert, ", Bild = @bild", "") &
                          " WHERE ProduktId = @id; SELECT @id;"
                End If

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@nr", If(artNr = "", CType(DBNull.Value, Object), artNr))
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@beschr", txtBeschreibung.Text.Trim())
                    cmd.Parameters.Add("@preis", SqlDbType.Decimal).Value = preis
                    cmd.Parameters.Add("@bild", SqlDbType.VarBinary, -1).Value = If(bild Is Nothing, CType(DBNull.Value, Object), bild)
                    cmd.Parameters.AddWithValue("@kat", cmbKategorie.Text.Trim())
                    cmd.Parameters.AddWithValue("@aktiv", chkAktiv.Checked)
                    cmd.Parameters.AddWithValue("@id", aktuelleId)
                    aktuelleId = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As SqlException
            MessageBox.Show("Speichern fehlgeschlagen: " & ex.Message, "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        bildGeaendert = False
        txtSuche.Clear()
        If Not chkAktiv.Checked Then chkNurAktive.Checked = False   ' damit der gespeicherte Artikel sichtbar bleibt
        LadeProdukte(aktuelleId)
        lblInfo.Text = "Gespeichert ✓"
    End Sub

    ' ───────────────────────────── Löschen ─────────────────────────────

    Private Sub btnLoeschen_Click(sender As Object, e As EventArgs) Handles btnLoeschen.Click
        If aktuelleId = 0 Then Return
        Try
            Using con As New SqlConnection(cs)
                con.Open()

                Dim verwendet As Integer
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM AngebotProdukte WHERE ProduktID = @id", con)
                    cmd.Parameters.AddWithValue("@id", aktuelleId)
                    verwendet = CInt(cmd.ExecuteScalar())
                End Using

                If verwendet > 0 Then
                    ' In Angeboten verwendete Artikel nicht löschen, sonst fehlen sie in alten Angeboten
                    If MessageBox.Show($"'{txtName.Text}' wird in {verwendet} Angebot(en) verwendet und kann nicht gelöscht werden." &
                                       vbCrLf & vbCrLf & "Stattdessen deaktivieren? Der Artikel erscheint dann nicht mehr in der Produktsuche.",
                                       "Löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
                    Using cmd As New SqlCommand("UPDATE Products SET Aktiv = 0 WHERE ProduktId = @id", con)
                        cmd.Parameters.AddWithValue("@id", aktuelleId)
                        cmd.ExecuteNonQuery()
                    End Using
                    LadeProdukte(aktuelleId)
                    lblInfo.Text = "Deaktiviert"
                    Return
                End If

                If MessageBox.Show($"'{txtName.Text}' endgültig löschen?", "Löschen",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
                Using cmd As New SqlCommand("DELETE FROM Products WHERE ProduktId = @id", con)
                    cmd.Parameters.AddWithValue("@id", aktuelleId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SqlException
            MessageBox.Show("Löschen fehlgeschlagen: " & ex.Message, "Löschen", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        LadeProdukte()
        NeuerArtikel()
    End Sub

    ' ───────────────────────────── Tastatur ─────────────────────────────

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Control Or Keys.S
                btnSpeichern.PerformClick() : Return True
            Case Keys.Control Or Keys.N
                NeuerArtikel() : Return True
            Case Keys.Control Or Keys.F
                txtSuche.Focus() : txtSuche.SelectAll() : Return True
            Case Keys.Escape
                Close() : Return True
            Case Keys.Enter
                ' Enter springt zum nächsten Feld (außer in der mehrzeiligen Beschreibung)
                Dim tb = TryCast(ActiveControl, TextBox)
                If tb IsNot Nothing AndAlso Not tb.Multiline AndAlso tb IsNot txtSuche Then
                    SelectNextControl(tb, True, True, True, True)
                    Return True
                End If
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class
