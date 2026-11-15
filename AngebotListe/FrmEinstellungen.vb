Imports System.IO

Public Class FrmEinstellungen

    Private Sub FrmEinstellungen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tema.GridStil(dgvKontakte)
        dgvKontakte.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvKontakte.DefaultCellStyle.SelectionBackColor = Tema.AuswahlHell

        txtFirma.Text = Einstellungen.Firmenname
        txtPdfOrdner.Text = Einstellungen.PdfOrdner

        Try
            If File.Exists(Einstellungen.KontaktDatei) Then
                For Each zeile In File.ReadAllLines(Einstellungen.KontaktDatei)
                    If String.IsNullOrWhiteSpace(zeile) Then Continue For
                    Dim t = zeile.Split("|"c)
                    dgvKontakte.Rows.Add(t(0).Trim(), If(t.Length > 1, t(1).Trim(), ""))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Die Kontaktliste konnte nicht gelesen werden: " & ex.Message, "Einstellungen",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnDurchsuchen_Click(sender As Object, e As EventArgs) Handles btnDurchsuchen.Click
        If Directory.Exists(txtPdfOrdner.Text) Then dlgOrdner.SelectedPath = txtPdfOrdner.Text
        If dlgOrdner.ShowDialog(Me) = DialogResult.OK Then txtPdfOrdner.Text = dlgOrdner.SelectedPath
    End Sub

    Private Sub btnSpeichern_Click(sender As Object, e As EventArgs) Handles btnSpeichern.Click
        Dim firma As String = txtFirma.Text.Trim()
        Dim ordner As String = txtPdfOrdner.Text.Trim()
        If firma = "" Then
            MessageBox.Show("Bitte einen Firmennamen eingeben.", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtFirma.Focus() : Return
        End If
        If ordner = "" Then
            MessageBox.Show("Bitte einen Ordner für die PDF-Dateien wählen.", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPdfOrdner.Focus() : Return
        End If

        ' Kontakte prüfen: Telefon nur Ziffern
        Dim zeilen As New List(Of String)
        dgvKontakte.EndEdit()
        For Each r As DataGridViewRow In dgvKontakte.Rows
            If r.IsNewRow Then Continue For
            Dim name As String = Convert.ToString(r.Cells(0).Value).Trim().Replace("|", "/")
            Dim tel As String = New String(Convert.ToString(r.Cells(1).Value).Where(AddressOf Char.IsDigit).ToArray())
            If name = "" AndAlso tel = "" Then Continue For
            If name = "" OrElse tel.Length < 7 Then
                dgvKontakte.CurrentCell = r.Cells(If(name = "", 0, 1))
                MessageBox.Show("Bitte bei jedem Kontakt einen Namen und eine gültige Telefonnummer eintragen.",
                                "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            zeilen.Add(name & "|" & tel)
        Next

        Try
            Directory.CreateDirectory(ordner)
            Directory.CreateDirectory(Path.GetDirectoryName(Einstellungen.KontaktDatei))
            File.WriteAllLines(Einstellungen.KontaktDatei, zeilen)
            Einstellungen.Firmenname = firma
            Einstellungen.PdfOrdner = ordner
            Einstellungen.Speichern()
        Catch ex As Exception
            MessageBox.Show("Speichern fehlgeschlagen: " & ex.Message, "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        DialogResult = DialogResult.OK
        Close()
    End Sub

    ' Entf löscht die markierte Kontaktzeile
    Private Sub dgvKontakte_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvKontakte.KeyDown
        If e.KeyCode = Keys.Delete AndAlso dgvKontakte.CurrentRow IsNot Nothing AndAlso Not dgvKontakte.CurrentRow.IsNewRow Then
            dgvKontakte.Rows.Remove(dgvKontakte.CurrentRow)
            e.Handled = True
        End If
    End Sub

    Private Sub btnAbbrechen_Click(sender As Object, e As EventArgs) Handles btnAbbrechen.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class
