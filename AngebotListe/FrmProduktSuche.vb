Imports System.Data.SqlClient

Public Class FrmProduktSuche

    Public SelectedProduct As DataRow

    Private Sub txtSuche_TextChanged(sender As Object, e As EventArgs) Handles txtSuche.TextChanged
        Dim search As String = txtSuche.Text.Trim()

        Using con As New SqlConnection("Data Source=185.187.170.107;Initial Catalog= AngebotListe;Persist Security Info=True;User Id=salih;Password=Garahi1943;")
            con.Open()

            Dim cmd As New SqlCommand("SELECT ProduktId AS ArtNr, Name, Beschreibung, Preis, Kategorie, Bild FROM Products WHERE Name LIKE @s OR Beschreibung LIKE @s", con)


            cmd.Parameters.AddWithValue("@s", "%" & search & "%")

            Dim dt As New DataTable()
            dt.Load(cmd.ExecuteReader())

            dgvErgebnisse.DataSource = dt
        End Using

        ' -------------------------
        '  PROFESSIONELLE TABLO AYARLARI
        ' -------------------------
        If dgvErgebnisse.Columns.Count > 0 Then

            ' ID küçük ve sabit
            'dgvErgebnisse.Columns("ID").Width = 50
            'dgvErgebnisse.Columns("ID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            ' İçeriğe göre genişleyen kolonlar
            dgvErgebnisse.Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            dgvErgebnisse.Columns("Beschreibung").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            'dgvErgebnisse.Columns("BildPfad").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

            ' Preis sağa hizalı
            dgvErgebnisse.Columns("Preis").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvErgebnisse.Columns("Preis").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

            ' Satır yüksekliği
            dgvErgebnisse.RowTemplate.Height = 28

            ' Header font
            dgvErgebnisse.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            dgvErgebnisse.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        End If
        'dgvErgebnisse.Columns("ID").Visible = False


    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If dgvErgebnisse.SelectedRows.Count = 0 Then Exit Sub

        SelectedProduct = CType(dgvErgebnisse.SelectedRows(0).DataBoundItem, DataRowView).Row
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub FrmProduktSuche_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSuche_TextChanged(Nothing, Nothing)


        ' --- PROFESYONEL SATIR SEÇİMİ ---
        With dgvErgebnisse
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .EditMode = DataGridViewEditMode.EditProgrammatically

            ' Seçili satır rengi
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255)
            .DefaultCellStyle.SelectionForeColor = Color.White

            ' Header seçili rengi
            .ColumnHeadersDefaultCellStyle.SelectionBackColor = .ColumnHeadersDefaultCellStyle.BackColor
            .ColumnHeadersDefaultCellStyle.SelectionForeColor = .ColumnHeadersDefaultCellStyle.ForeColor

            ' EnableHeadersVisualStyles kapalı olmalı
            .EnableHeadersVisualStyles = False
        End With

        DATAGRİD_1_RENKLENDIR()

        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
        Button2.BackColor = Color.FromArgb(0, 120, 215)
        Button2.ForeColor = Color.White


        '==================================================

        With dgvErgebnisse
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            .Columns("ArtNr").Width = 60
            .Columns("Name").Width = 300
            .Columns("Name").HeaderText = "Artikelname"



            .Columns("Beschreibung").Width = 300
            .Columns("Preis").Width = 80
            .Columns("Kategorie").Width = 120
            If .Columns.Contains("Bild") Then .Columns("Bild").Visible = False
        End With

    End Sub


    Private Sub btnNeueProdukt_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub dgvErgebnisse_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvErgebnisse.CellContentClick

    End Sub


    Public Sub DATAGRİD_1_RENKLENDIR()

        dgvErgebnisse.RowsDefaultCellStyle.BackColor = Color.GhostWhite
        dgvErgebnisse.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro

        dgvErgebnisse.ColumnHeadersDefaultCellStyle.Font = New Font("SansSerif", 12, FontStyle.Bold)
        dgvErgebnisse.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro
        dgvErgebnisse.EnableHeadersVisualStyles = False

        dgvErgebnisse.RowHeadersVisible = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim artikelEinfügenForm As New ArtikelEinfügen()

        ' ArtikelEinfügen formunun konumunu tam üstüne ayarla
        artikelEinfügenForm.StartPosition = FormStartPosition.Manual
        artikelEinfügenForm.Location = Me.Location

        artikelEinfügenForm.Show()
    End Sub

    Private Sub dgvErgebnisse_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvErgebnisse.CellDoubleClick


        If e.RowIndex < 0 Then Exit Sub

        Dim drv As DataRowView = TryCast(dgvErgebnisse.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub

        SelectedProduct = drv.Row

        ' Açık FrmProductCard2 formunu bul
        Dim frmCard2 As FrmProductCard2 = Application.OpenForms.OfType(Of FrmProductCard2)().FirstOrDefault()
        If frmCard2 IsNot Nothing Then
            ' ⚠️ Grid’e ekleme
            frmCard2.DataGridView1.Rows.Add(
            False, ' Select checkbox
            If(IsDBNull(SelectedProduct("ID")), 0, SelectedProduct("ID")),
            If(IsDBNull(SelectedProduct("Name")), "", SelectedProduct("Name")),
            If(IsDBNull(SelectedProduct("Beschreibung")), "", SelectedProduct("Beschreibung")),
            If(IsDBNull(SelectedProduct("Preis")), 0D, SelectedProduct("Preis")),
            If(IsDBNull(SelectedProduct("BildPfad")), "", SelectedProduct("BildPfad"))
        )

            ' Yeni eklenen satırı seç
            Dim lastRowIndex As Integer = frmCard2.DataGridView1.Rows.Count - 1
            frmCard2.DataGridView1.ClearSelection()
            frmCard2.DataGridView1.Rows(lastRowIndex).Selected = True
            frmCard2.DataGridView1.CurrentCell = frmCard2.DataGridView1.Rows(lastRowIndex).Cells("ID")

            ' FlowLayoutPanel ile senkron
            For Each card As ProductCard In frmCard2.FlowLayoutPanel1.Controls
                If card.ProductID = If(IsDBNull(SelectedProduct("ID")), 0, SelectedProduct("ID")) Then
                    frmCard2.FlowLayoutPanel1.ScrollControlIntoView(card)
                    card.SetHighlight(True)
                Else
                    card.SetHighlight(False)
                End If
            Next
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
