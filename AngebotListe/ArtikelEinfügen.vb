Imports System.Data.SqlClient

Public Class ArtikelEinfügen
    Dim myBaglan As String = "Data Source=185.187.170.107;Initial Catalog= AngebotListe;Persist Security Info=True;User Id=salih;Password=Garahi1943;"

    Private Sub ArtikelEinfügen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()

        ' Sürükle ve bırak işlemini etkinleştir
        PictureBox1.AllowDrop = True
        AddHandler PictureBox1.DragEnter, AddressOf PictureBox1_DragEnter
        AddHandler PictureBox1.DragDrop, AddressOf PictureBox1_DragDrop

        btnNeue.FlatStyle = FlatStyle.Flat
        btnNeue.FlatAppearance.BorderSize = 0
        btnNeue.BackColor = Color.FromArgb(0, 120, 215)
        btnNeue.ForeColor = Color.White

        btnEinfügen.FlatStyle = FlatStyle.Flat
        btnEinfügen.FlatAppearance.BorderSize = 0
        btnEinfügen.BackColor = Color.FromArgb(0, 120, 215)
        btnEinfügen.ForeColor = Color.White

        btnLöschen.FlatStyle = FlatStyle.Flat
        btnLöschen.FlatAppearance.BorderSize = 0
        btnLöschen.BackColor = Color.FromArgb(0, 120, 215)
        btnLöschen.ForeColor = Color.White

        btnAndern.FlatStyle = FlatStyle.Flat
        btnAndern.FlatAppearance.BorderSize = 0
        btnAndern.BackColor = Color.FromArgb(0, 120, 215)
        btnAndern.ForeColor = Color.White
        '=====================================
        Me.KeyPreview = True
        '========================================

        PictureBox1.AllowDrop = True

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Dim borderColor As Color = Color.FromArgb(180, 180, 180)
        Dim borderWidth As Integer = 1

        Using pen As New Pen(borderColor, borderWidth)
            e.Graphics.DrawRectangle(pen, 0, 0, Panel1.Width - 1, Panel1.Height - 1)
        End Using
    End Sub

    Public Sub DATAGRİD_1_RENKLENDIR()

        dgvProducts.RowsDefaultCellStyle.BackColor = Color.GhostWhite
        dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro

        dgvProducts.ColumnHeadersDefaultCellStyle.Font = New Font("SansSerif", 12, FontStyle.Bold)
        dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro
        dgvProducts.EnableHeadersVisualStyles = False

        dgvProducts.RowHeadersVisible = False

    End Sub
    Private Sub LoadGrid()
        dgvProducts.DataSource = LoadProducts()

        ' Gizlenecek kolonlar
        'dgvProducts.Columns("ID").Visible = False
        dgvProducts.Columns("ProduktId").Visible = False
        dgvProducts.Columns("AngebotID").Visible = False

        ' ✔ ProduktNr (ürün numarası)
        dgvProducts.Columns("ProduktNr").HeaderText = "ArtikelNr"
        dgvProducts.Columns("ProduktNr").Width = 70

        ' ✔ Name → Artikelbezeichnung
        dgvProducts.Columns("Name").HeaderText = "Artikelbezeichnung"
        dgvProducts.Columns("Name").Width = 165

        ' ✔ Beschreibung
        dgvProducts.Columns("Beschreibung").Width = 165

        ' ✔ Aktiv
        dgvProducts.Columns("Aktiv").Width = 50

        ' ✔ Preis
        dgvProducts.Columns("Preis").Width = 50
        dgvProducts.Columns("Preis").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DATAGRİD_1_RENKLENDIR()
    End Sub


    Public Function LoadProducts() As DataTable
        Dim dt As New DataTable()
        Dim sql As String =
    "SELECT ProduktId, ProduktNr, AngebotID, Name, Beschreibung, Preis, Kategorie, Aktiv, BildPfad, ErstelltAm FROM Products"

        Using con As New SqlConnection(myBaglan)
            Using da As New SqlDataAdapter(sql, con)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Public Sub Insert(p As Product)

        Dim sql As String =
    "INSERT INTO Products 
    (ProduktNr, Name, Beschreibung, Preis, Bild, Kategorie, Aktiv, ErstelltAm)
    VALUES
    (@ProduktNr, @Name, @Beschreibung, @Preis, @Bild, @Kategorie, @Aktiv, @ErstelltAm)"

        Using con As New SqlConnection(myBaglan)
            Using cmd As New SqlCommand(sql, con)

                cmd.Parameters.AddWithValue("@ProduktNr", p.ProduktNr)
                cmd.Parameters.AddWithValue("@Name", p.Name)
                cmd.Parameters.AddWithValue("@Beschreibung", p.Beschreibung)
                cmd.Parameters.AddWithValue("@Preis", p.Preis)
                cmd.Parameters.Add("@Bild", SqlDbType.VarBinary).Value = If(CType(p.Bild, Object), DBNull.Value) ' ✅ explicit type
                cmd.Parameters.AddWithValue("@Kategorie", p.Kategorie)
                cmd.Parameters.AddWithValue("@Aktiv", p.Aktiv)
                cmd.Parameters.AddWithValue("@ErstelltAm", p.ErstelltAm)

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Public Sub Update(p As Product)

        Dim sql As String =
    "UPDATE Products SET
        ProduktNr = @ProduktNr,
        Name = @Name,
        Beschreibung = @Beschreibung,
        Preis = @Preis,
        Bild = @Bild,
        Kategorie = @Kategorie,
        Aktiv = @Aktiv,
        AngebotID = @AngebotID,
        ErstelltAm = @ErstelltAm
     WHERE ProduktId = @ProduktId"

        Using con As New SqlConnection(myBaglan)
            Using cmd As New SqlCommand(sql, con)

                cmd.Parameters.AddWithValue("@ProduktNr", p.ProduktNr)
                cmd.Parameters.AddWithValue("@Name", p.Name)
                cmd.Parameters.AddWithValue("@Beschreibung", p.Beschreibung)
                cmd.Parameters.AddWithValue("@Preis", p.Preis)
                cmd.Parameters.Add("@Bild", SqlDbType.VarBinary).Value = If(CType(p.Bild, Object), DBNull.Value) ' ✅ explicit type
                cmd.Parameters.AddWithValue("@Kategorie", p.Kategorie)
                cmd.Parameters.AddWithValue("@Aktiv", p.Aktiv)
                cmd.Parameters.AddWithValue("@AngebotID", If(p.AngebotID, CType(DBNull.Value, Object)))
                cmd.Parameters.AddWithValue("@ErstelltAm", p.ErstelltAm)
                cmd.Parameters.AddWithValue("@ProduktId", p.ProduktId)

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(id As Integer)
        Dim sql As String = "DELETE FROM Products WHERE ProduktId=@ProduktId"

        Using con As New SqlConnection(myBaglan)
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@ProduktId", id)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function LoadAll() As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT * FROM Products ORDER BY ID DESC"

        Using con As New SqlConnection(myBaglan)
            Using da As New SqlDataAdapter(sql, con)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Private Sub dgvProducts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProducts.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = dgvProducts.Rows(e.RowIndex)

        txtId.Text = row.Cells("ProduktId").Value.ToString()        ' ✔ Anahtar
        txtProduktId.Text = row.Cells("ProduktNr").Value.ToString() ' ✔ Ürün numarası

        txtName.Text = row.Cells("Name").Value.ToString()
        txtBeschreibung.Text = row.Cells("Beschreibung").Value.ToString()
        txtPreis.Text = row.Cells("Preis").Value.ToString()
        If row.DataGridView.Columns.Contains("BildPfad") Then
            txtBildPfad.Text = If(row.Cells("BildPfad").Value Is Nothing, "", row.Cells("BildPfad").Value.ToString())
        End If
        cmbKategorie.Text = row.Cells("Kategorie").Value.ToString()
        chkAktiv.Checked = CBool(row.Cells("Aktiv").Value)
        txtErstelltAm.Text = row.Cells("ErstelltAm").Value.ToString()

        ' ⭐ Resmi yükle — önce SQL'deki Bild BLOB'tan, fallback BildPfad path
        PictureBox1.Image = Nothing
        Dim productId As Integer
        If Integer.TryParse(txtId.Text, productId) Then
            Try
                Using con As New SqlConnection(myBaglan)
                    con.Open()
                    Using cmd As New SqlCommand("SELECT Bild, BildPfad FROM Products WHERE ProduktId = @id", con)
                        cmd.Parameters.AddWithValue("@id", productId)
                        Using rd = cmd.ExecuteReader()
                            If rd.Read() Then
                                If Not IsDBNull(rd("Bild")) Then
                                    Dim bytes As Byte() = CType(rd("Bild"), Byte())
                                    Using ms As New IO.MemoryStream(bytes)
                                        PictureBox1.Image = Image.FromStream(ms)
                                    End Using
                                ElseIf Not IsDBNull(rd("BildPfad")) Then
                                    Dim pfad As String = rd("BildPfad").ToString()
                                    If IO.File.Exists(pfad) Then
                                        PictureBox1.Image = Image.FromFile(pfad)
                                    End If
                                End If
                            End If
                        End Using
                    End Using
                End Using
            Catch
                ' Resim yüklenemezse sessizce geç
            End Try
        End If
    End Sub


    'Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
    '    If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
    '        Dim ImagePath As String = OpenFileDialog1.FileName

    '        ' Resmi PictureBox1'e yükle
    '        PictureBox1.Image = Image.FromFile(ImagePath)

    '        ' Resim yolunu txtBildPfad metin kutusuna kaydet
    '        txtBildPfad.Text = ImagePath
    '    End If
    'End Sub

    Private Sub PictureBox1_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(sender As Object, e As DragEventArgs)
        Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If files.Length > 0 Then
            Dim filePath As String = files(0)

            ' Resmi PictureBox'a yükle
            Try
                PictureBox1.Image = Image.FromFile(filePath)
                txtBildPfad.Text = filePath  ' Resim yolunu güncelle
            Catch ex As Exception
                MessageBox.Show("Resim yüklenemedi: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnEinfügen_Click(sender As Object, e As EventArgs) Handles btnEinfügen.Click

        ' --- 1) Resim seçilmiş mi kontrol et ---
        If PictureBox1.Image Is Nothing Then
            MessageBox.Show("Bitte wählen Sie zuerst ein Bild aus.",
                        "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' --- 2) Fiyat doğru formatta mı? ---
        Dim preisValue As Decimal
        If Not Decimal.TryParse(txtPreis.Text, Globalization.NumberStyles.Any,
                                Globalization.CultureInfo.GetCultureInfo("de-DE"), preisValue) Then
            MessageBox.Show("Bitte geben Sie einen gültigen Preis ein.",
                        "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' --- 3) Ürün oluştur ---
        Dim newProduct As New Product With {
            .ProduktNr = txtProduktId.Text,
            .Name = txtName.Text,
            .Beschreibung = txtBeschreibung.Text,
            .Preis = preisValue,
            .Bild = ImageToByteArray(PictureBox1.Image), ' ✅ BURASI DEĞİŞTİ
            .Kategorie = cmbKategorie.Text,
            .Aktiv = chkAktiv.Checked,
            .ErstelltAm = DateTime.Now
        }

        ' --- 4) Veritabanına ekle ---
        Insert(newProduct)

        ' --- 5) Listeyi yenile ---
        LoadGrid()
        ClearFields()

        MessageBox.Show("Das Produkt wurde erfolgreich hinzugefügt.",
                    "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub btnAndern_Click(sender As Object, e As EventArgs) Handles btnAndern.Click

        Dim updatedProduct As New Product With {
        .ProduktId = Integer.Parse(txtId.Text),   ' ✔ ANAHTAR: ProduktId
        .ProduktNr = txtProduktId.Text,           ' ✔ Ürün numarası
        .Name = txtName.Text,
        .Beschreibung = txtBeschreibung.Text,
        .Preis = Decimal.Parse(txtPreis.Text),
        .Bild = ImageToByteArray(PictureBox1.Image), ' ✅ Resmi BLOB olarak SQL'e
        .Kategorie = cmbKategorie.Text,
        .Aktiv = chkAktiv.Checked,
        .ErstelltAm = DateTime.Parse(txtErstelltAm.Text)
    }

        Update(updatedProduct)
        LoadGrid()
        ClearFields()

    End Sub


    Private Sub ClearFields()
        ' Tüm metin kutularını ve diğer kontrolleri temizle
        txtId.Clear()
        txtName.Clear()
        txtBeschreibung.Clear()
        txtPreis.Clear()
        txtBildPfad.Clear()
        cmbKategorie.SelectedIndex = -1
        chkAktiv.Checked = False
        txtErstelltAm.Clear()
        PictureBox1.Image = Nothing
    End Sub

    Private Sub btnNeue_Click(sender As Object, e As EventArgs) Handles btnNeue.Click
        ' Yeni bir ürün oluştur
        Dim newProduct As New Product With {
        .Name = txtName.Text,
        .Beschreibung = txtBeschreibung.Text,
        .Preis = Decimal.Parse(txtPreis.Text),
        .Bild = ImageToByteArray(PictureBox1.Image), ' ✅ Resmi BLOB olarak SQL'e
        .Kategorie = cmbKategorie.Text,
        .Aktiv = chkAktiv.Checked,
        .ErstelltAm = DateTime.Now ' Veya istediğiniz başka bir tarih
    }

        ' Ürünü veri tabanına ekle
        Insert(newProduct)

        ' Listeyi yenile
        LoadGrid()

        ' Alanları temizle
        ResetFields()
    End Sub

    Private Sub ResetFields()
        ' Tüm metin kutularını ve diğer kontrolleri temizle
        txtId.Clear()
        txtName.Clear()
        txtBeschreibung.Clear()
        txtPreis.Clear()
        txtBildPfad.Clear()
        cmbKategorie.SelectedIndex = -1
        chkAktiv.Checked = False
        txtErstelltAm.Clear()
        PictureBox1.Image = Nothing
    End Sub

    Private Sub btnLöschen_Click(sender As Object, e As EventArgs) Handles btnLöschen.Click
        ' Seçili ürünün ID'sini al
        Dim productId As Integer

        If Integer.TryParse(txtId.Text, productId) Then
            ' Kullanıcıdan silme işlemi için onay al
            Dim result As DialogResult = MessageBox.Show("Sind Sie sicher, dass Sie dieses Produkt löschen möchten?", "Löschbestätigung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                ' Ürünü sil
                Delete(productId)

                ' Listeyi güncelle
                LoadGrid()

                ' Alanları temizle
                ResetFields()
                MessageBox.Show("Produkt wurde erfolgreich gelöscht.", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Ungültige Produkt-ID.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub dgvProducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProducts.CellContentClick

    End Sub

    Private Sub txtProduktId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProduktId.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Enter’ın ding sesi çıkmasın
            txtName.Focus()    ' Bir sonraki TextBox
        End If
    End Sub

    Private Sub txtProduktId_TextChanged(sender As Object, e As EventArgs) Handles txtProduktId.TextChanged

    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtName.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Enter’ın ding sesi çıkmasın
            txtBeschreibung.Focus()    ' Bir sonraki TextBox
        End If
    End Sub

    Private Sub txtBeschreibung_TextChanged(sender As Object, e As EventArgs) Handles txtBeschreibung.TextChanged

    End Sub

    Private Sub txtBeschreibung_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBeschreibung.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Enter’ın ding sesi çıkmasın
            txtPreis.Focus()    ' Bir sonraki TextBox
        End If
    End Sub

    Private Sub txtPreis_TextChanged(sender As Object, e As EventArgs) Handles txtPreis.TextChanged

    End Sub

    Private Sub txtPreis_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPreis.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Enter’ın ding sesi çıkmasın
            btnEinfügen.Focus()    ' Bir sonraki TextBox
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim ofd As New OpenFileDialog With {
        .Filter = "Bilddateien (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
        .Title = "Bild auswählen"
    }

        If ofd.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(ofd.FileName)
            txtBildPfad.Text = ofd.FileName
        End If
    End Sub

    Private Sub cmbKategorie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKategorie.SelectedIndexChanged

    End Sub




    'Private Sub NeuesAngebotListeErstellen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        e.SuppressKeyPress = True

    '        Dim ctrl As Control = Me.ActiveControl
    '        Me.SelectNextControl(ctrl, True, True, True, True)
    '    End If
    'End Sub

End Class