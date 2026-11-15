Imports System.Data.SqlClient
Imports System.IO

Public Class ProductCard
    Inherits UserControl
    Public ProductName As String
    Private _showActionButtons As Boolean = True
    Public Event CardFocused(productID As Integer)
    Public Property DisplayMode As String = "Normal"
    Public Property ParentPanel As FlowLayoutPanel
    Private CurrentProductCard As ProductCard
    Public Event PriceChanged(productID As Integer, newPrice As Decimal)
    Public ReadOnly baglan As String = Baglanti.AngebotListe
    Public Event CardDeleted(ByVal sender As ProductCard)
    Public Event CardRemoved As EventHandler
    Public Property IsDeleted As Boolean = False
    Public Property OwnerForm As NeuesAngebotListeErstellen
    Public Property FirmaAdi As String = "My Fresh Fruit Yeşil GmbH"
    Public Property FirmaAdi2 As String = "Avanit GmbH"
    Public Shared Property GlobalFirmaAdi As String = "My Fresh Fruit Yeşil GmbH"
    Public Property ProductID As Integer?
    Public Property AngebotID As Integer = 0

    ' 🔥 Ürün değişikliğini takip etmek için
    Public Property OriginalProductID As Integer

    ' 🔥 Kart düzenlendi mi?
    Public Property IsEdited As Boolean = False

    Public Property ImagePath As String = ""

    Private _produktName As String = ""
    Private _beschreibung As String = ""

    Public Property ProduktName As String
        Get
            Return _produktName
        End Get
        Set(value As String)
            _produktName = If(value, "")
            TexteAnzeigen()
        End Set
    End Property

    Public Property Beschreibung As String
        Get
            Return _beschreibung
        End Get
        Set(value As String)
            _beschreibung = If(value, "")
            TexteAnzeigen()
        End Set
    End Property

    ' Leere Karte: Hinweis anzeigen. Beschreibung ausblenden, wenn sie nur den Namen wiederholt.
    Private Sub TexteAnzeigen()
        If _produktName = "" Then
            lblName.Text = "Noch kein Produkt gewählt"
            lblName.ForeColor = Color.FromArgb(107, 114, 128)
        Else
            lblName.Text = _produktName
            lblName.ForeColor = Color.FromArgb(31, 41, 55)
        End If
        lblBeschreibung.Text = If(String.Equals(_beschreibung.Trim(), _produktName.Trim(), StringComparison.CurrentCultureIgnoreCase), "", _beschreibung)
    End Sub

    Private _preisValue As Decimal

    Public Property Preis As Decimal
        Get
            Return _preisValue
        End Get
        Set(value As Decimal)
            _preisValue = value

            ' Label güncelle
            lblPreis.Text = value.ToString("N2", Globalization.CultureInfo.GetCultureInfo("de-DE")) & " €"
            PictureBox2.Left = lblPreis.Right + 4
            ' ⭐ TextBox da güncellensin
            'txtPreisEdit.Text = value.ToString("0.##", Globalization.CultureInfo.GetCultureInfo("de-DE"))
        End Set
    End Property








    Public Property BildPfad As String
        Get
            Return ImagePath
        End Get
        Set(value As String)
            ImagePath = value
            'LoadImageSafe(value)
        End Set
    End Property

    ' ⭐ Resmi byte dizisi olarak verir (pbBild.Image içeriğinden)
    Public ReadOnly Property BildBytes As Byte()
        Get
            Return ImageToByteArray(pbBild.Image)
        End Get
    End Property

    ' ⭐ Byte dizisinden resmi pbBild'e yükler
    Public Sub LoadBildFromBytes(bytes As Byte())
        If bytes Is Nothing OrElse bytes.Length = 0 Then
            pbBild.Image = Nothing
            Return
        End If
        Try
            pbBild.Image = ByteArrayToImage(bytes)
        Catch
            pbBild.Image = Nothing
        End Try
    End Sub

    Public Sub New()
        InitializeComponent()
        pbBild.AllowDrop = True

        AddHandler pbBild.Click, AddressOf pbBild_Click
        AddHandler pbBild.DragEnter, AddressOf pbBild_DragEnter
        AddHandler pbBild.DragDrop, AddressOf pbBild_DragDrop
        AddHandler btnAction.Click, AddressOf btnAction_Click
        AddHandler btnAction.MouseEnter, AddressOf btnAction_MouseEnter
        AddHandler btnAction.MouseLeave, AddressOf btnAction_MouseLeave
        AddHandler pnlBottom.MouseEnter, AddressOf pnlBottom_MouseEnter
        AddHandler pnlBottom.MouseLeave, AddressOf pnlBottom_MouseLeave
    End Sub

    Public Sub LoadImageFromDatabase(productId As Integer)
        Using con As New SqlConnection(baglan)
            con.Open()

            ' Hem Bild BLOB'unu hem de BildPfad path'ini çek
            Dim cmd As New SqlCommand("SELECT Bild, BildPfad FROM Products WHERE ProduktId=@id", con)
            cmd.Parameters.AddWithValue("@id", productId)

            Dim bildBytes As Byte() = Nothing
            Dim bildPfad As String = Nothing
            Using rd = cmd.ExecuteReader()
                If rd.Read() Then
                    If Not IsDBNull(rd("Bild")) Then
                        bildBytes = CType(rd("Bild"), Byte())
                    End If
                    If Not IsDBNull(rd("BildPfad")) Then
                        bildPfad = rd("BildPfad").ToString()
                    End If
                End If
            End Using

            ' 1) BLOB varsa onu göster
            If bildBytes IsNot Nothing AndAlso bildBytes.Length > 0 Then
                pbBild.Image = ByteArrayToImage(bildBytes)
                Return
            End If

            ' 2) BLOB yok ama BildPfad var ve dosya bu PC'de mevcut → otomatik migration
            If Not String.IsNullOrWhiteSpace(bildPfad) AndAlso File.Exists(bildPfad) Then
                Try
                    Dim bytes As Byte() = File.ReadAllBytes(bildPfad)
                    pbBild.Image = ByteArrayToImage(bytes)

                    ' Bytes'ı SQL Bild kolonuna kaydet (bir daha path'e ihtiyaç yok)
                    Using up As New SqlCommand("UPDATE Products SET Bild = @img WHERE ProduktId = @id", con)
                        up.Parameters.Add("@img", SqlDbType.VarBinary).Value = bytes
                        up.Parameters.AddWithValue("@id", productId)
                        up.ExecuteNonQuery()
                    End Using
                Catch
                    pbBild.Image = Nothing
                End Try
                Return
            End If

            pbBild.Image = Nothing
        End Using
    End Sub

    Private Sub pbBild_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub pbBild_DragDrop(sender As Object, e As DragEventArgs)
        Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If files.Length = 0 Then Exit Sub

        Dim filePath As String = files(0)
        Try
            ' Resmi yükle ve göster
            pbBild.Image = Image.FromFile(filePath)
            ImagePath = filePath
        Catch ex As Exception
            MessageBox.Show("Bild konnte nicht geladen werden: " & ex.Message)
            Exit Sub
        End Try

        ' ⭐ Resmi anında SQL'e BLOB olarak kaydet
        If ProductID > 0 Then
            Dim bytes As Byte() = File.ReadAllBytes(filePath)
            UpdateBildInDatabase(ProductID, bytes)
        End If
    End Sub

    Private Sub pbBild_Click(sender As Object, e As EventArgs)
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = OpenFileDialog1.FileName
            Try
                pbBild.Image = Image.FromFile(filePath)
                ImagePath = filePath
            Catch ex As Exception
                MessageBox.Show("Bild konnte nicht geladen werden: " & ex.Message)
                Exit Sub
            End Try

            ' ⭐ Resmi anında SQL'e BLOB olarak kaydet
            If ProductID > 0 Then
                Dim bytes As Byte() = File.ReadAllBytes(filePath)
                UpdateBildInDatabase(ProductID, bytes)
            End If
        End If
    End Sub

    ' ⭐ Resmi binary BLOB olarak Products.Bild kolonuna yazar
    Private Sub UpdateBildInDatabase(id As Integer, bytes As Byte())
        If id <= 0 OrElse bytes Is Nothing Then Exit Sub

        Using con As New SqlConnection(baglan)
            con.Open()
            Dim cmd As New SqlCommand("UPDATE Products SET Bild = @img WHERE ProduktId = @id", con)
            cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = bytes
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub InsertProduct()
        Dim imageBytes As Byte() = ImageToByteArray(pbBild.Image)

        Using con As New SqlConnection(baglan)
            con.Open()
            Dim cmd As New SqlCommand("INSERT INTO Products (Name, Beschreibung, Preis, Bild, Aktiv, ErstelltAm) VALUES (@name, @beschreibung, @preis, @img, 1, GETDATE())", con)

            cmd.Parameters.AddWithValue("@name", ProduktName)
            cmd.Parameters.AddWithValue("@beschreibung", Beschreibung)
            cmd.Parameters.AddWithValue("@preis", Preis)
            cmd.Parameters.AddWithValue("@img", If(imageBytes, DBNull.Value))

            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Produkt eingefügt!")
    End Sub

    Public Sub UpdateProduct()
        If ProductID <= 0 Then Exit Sub

        Dim imageBytes As Byte() = ImageToByteArray(pbBild.Image)

        Using con As New SqlConnection(baglan)
            con.Open()
            Dim cmd As New SqlCommand("UPDATE Products SET Name = @name, Beschreibung = @beschreibung, Preis = @preis, Bild = @img WHERE ProduktId = @id", con)

            cmd.Parameters.AddWithValue("@name", ProduktName)
            cmd.Parameters.AddWithValue("@beschreibung", Beschreibung)
            cmd.Parameters.AddWithValue("@preis", Preis)
            cmd.Parameters.AddWithValue("@img", If(imageBytes, DBNull.Value))
            cmd.Parameters.AddWithValue("@id", ProductID)

            cmd.ExecuteNonQuery()
        End Using

        MessageBox.Show("Produkt aktualisiert!")
    End Sub

    Public Sub DeleteProduct()
        If ProductID <= 0 Then Exit Sub

        Using con As New SqlConnection(baglan)
            con.Open()
            Dim cmd As New SqlCommand("DELETE FROM Products WHERE ID = @id", con)
            cmd.Parameters.AddWithValue("@id", ProductID)
            cmd.ExecuteNonQuery()
        End Using

        Me.Parent.Controls.Remove(Me)
        MessageBox.Show("Produkt gelöscht!")
    End Sub

    Public Function GetShareImage() As Bitmap
        Dim sharePanel As New Panel()
        sharePanel.BackColor = Color.White
        sharePanel.Size = New Size(350, 520)

        Dim pic As New PictureBox()
        pic.Size = New Size(330, 230)
        pic.Location = New Point(10, 10)
        pic.SizeMode = PictureBoxSizeMode.Zoom
        If pbBild.Image IsNot Nothing Then
            pic.Image = CType(pbBild.Image.Clone(), Image)
        End If
        sharePanel.Controls.Add(pic)

        Dim lblName As New Label()
        lblName.Text = Me.ProduktName
        lblName.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblName.Location = New Point(10, pic.Bottom + 10)
        lblName.MaximumSize = New Size(330, 0)
        lblName.AutoSize = True
        sharePanel.Controls.Add(lblName)

        Dim lblP As New Label()
        lblP.Text = Preis.ToString("0.00 €")
        lblP.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        lblP.Location = New Point(10, lblName.Bottom + 8)
        lblP.AutoSize = True
        sharePanel.Controls.Add(lblP)

        Dim lblD As New Label()
        lblD.Text = Beschreibung
        lblD.Font = New Font("Segoe UI", 12)
        lblD.Location = New Point(10, lblP.Bottom + 8)
        lblD.MaximumSize = New Size(330, 0)
        lblD.AutoSize = True
        sharePanel.Controls.Add(lblD)

        Dim btnCart As New Button()
        btnCart.Text = Me.FirmaAdi
        btnCart.Text = Me.FirmaAdi2


        '========================================================
        'btnCart.Text = GlobalFirmaAdi
        btnAction.Text = GlobalFirmaAdi

        btnCart.Text = GlobalFirmaAdi
        btnAction.Text = GlobalFirmaAdi


        'btnCart.Text = "My Fresh Fruit Yeşil GmbH"
        btnCart.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnCart.Size = New Size(330, 45)
        btnCart.Location = New Point(10, lblD.Bottom + 15)
        btnCart.BackColor = Color.DarkGreen
        btnCart.ForeColor = Color.White
        sharePanel.Controls.Add(btnCart)

        sharePanel.Height = btnCart.Bottom + 15

        Dim bmp As New Bitmap(sharePanel.Width, sharePanel.Height)
        sharePanel.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

        Return bmp
    End Function

    ' F9 (Firmenname ändern) ruft Refresh auf: Firmenname auf der Karte aktualisieren
    Public Overrides Sub Refresh()
        btnAction.Text = GlobalFirmaAdi
        MyBase.Refresh()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Using pen As New Pen(Color.FromArgb(225, 229, 235))
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1)
        End Using
    End Sub

    Public Sub SetHighlight(active As Boolean)
        Me.BackColor = If(active, Color.FromArgb(232, 240, 254), Color.White)
    End Sub

    Private Sub ProductCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnAction.FlatStyle = FlatStyle.Flat
        btnAction.FlatAppearance.BorderSize = 0
        btnAction.BackColor = Color.FromArgb(59, 89, 152)
        btnAction.ForeColor = Color.White
        btnAction.Text = GlobalFirmaAdi   ' vorher fest "My Fresh Fruit Yeşil GmbH" – F9-Änderung wirkte nicht
        btnAction.Cursor = Cursors.Hand
        'Burasi neue angebotta görünmesin diye 
        If DisplayMode = "AngebotListe" Then
            btnEinfügen.Visible = False
            Button1.Visible = False
            btnDelete.Visible = False
            btnAction.Visible = False
            pnlBottom.Visible = False
            btnProduktSuchen.Visible = True
        Else

            '    btnEinfügen.Visible = True
            'Button1.Visible = True
            'btnDelete.Visible = True


            btnProduktSuchen.Visible = True
            btnAction.Visible = True
            pnlBottom.Visible = True
            Height = pnlBottom.Bottom   ' Firmenleiste unten anzeigen
        End If
        TexteAnzeigen()
        btnProduktSuchen.Text = If(ProductID.GetValueOrDefault() > 0, "Ändern", "Produkt wählen")
        If ProductID.GetValueOrDefault() = 0 Then
            btnProduktSuchen.BackColor = Color.FromArgb(26, 115, 232)
            btnProduktSuchen.ForeColor = Color.White
        End If


    End Sub

    Private Sub btnAction_Click(sender As Object, e As EventArgs)
        RaiseEvent CardFocused(Me.ProductID)
    End Sub

    Private Sub btnAction_MouseEnter(sender As Object, e As EventArgs)
        btnAction.BackColor = Color.FromArgb(40, 70, 130)
    End Sub

    Private Sub btnAction_MouseLeave(sender As Object, e As EventArgs)
        btnAction.BackColor = Color.FromArgb(59, 89, 152)
    End Sub

    Private Sub pnlBottom_MouseEnter(sender As Object, e As EventArgs)
        btnAction.BackColor = Color.FromArgb(40, 70, 130)
    End Sub

    Private Sub pnlBottom_MouseLeave(sender As Object, e As EventArgs)
        btnAction.BackColor = Color.FromArgb(59, 89, 152)
    End Sub

    Private Sub btnEinfügen_Click(sender As Object, e As EventArgs) Handles btnEinfügen.Click
        InsertProduct()
    End Sub
    Private Sub btnProduktSuchen_Click(sender As Object, e As EventArgs) Handles btnProduktSuchen.Click
        Dim parentForm As Form = Me.FindForm()
        Dim searchForm As New FrmProduktSuche()

        searchForm.StartPosition = FormStartPosition.CenterParent

        If searchForm.ShowDialog(parentForm) = DialogResult.OK AndAlso searchForm.SelectedProduct IsNot Nothing Then
            ProduktUebernehmen(searchForm.SelectedProduct)
        End If
    End Sub

    ' Füllt die Karte mit einem Produkt aus der Produktsuche (auch für "Produkte hinzufügen" mit Mehrfachauswahl)
    Public Sub ProduktUebernehmen(selectedProduct As DataRow)
        If selectedProduct Is Nothing Then Return
        btnProduktSuchen.Text = "Ändern"
        btnProduktSuchen.BackColor = Color.White
        btnProduktSuchen.ForeColor = Color.FromArgb(26, 115, 232)

            ' 🔍 Güvenli kolon bulma fonksiyonu
            Dim productIdColumn As String = GetExistingColumn(selectedProduct, {"ID", "ProduktID", "ProductID", "ArtikelID", "ArtNr"})

            ' 🆔 ID kolonu bulunduysa yükle, yoksa 0 ata
            If productIdColumn <> "" Then
                Me.ProductID = If(IsDBNull(selectedProduct(productIdColumn)), 0, Convert.ToInt32(selectedProduct(productIdColumn)))
            Else
                Me.ProductID = 0
            End If

            ' 🏷️ Diğer kolonlar güvenli şekilde okunuyor
            Me.ProduktName = SafeGet(selectedProduct, "Name")
            Me.Beschreibung = SafeGet(selectedProduct, "Beschreibung")

            ' ⭐ FİYAT BURADA DOĞRU OKUNUYOR
            Me.Preis = SafeGetDecimal(selectedProduct, "Preis")

            ' ⭐ FİYATI TEK BİR YERDE DOĞRU FORMATLA
            Dim preisText As String = Me.Preis.ToString("0.##", Globalization.CultureInfo.GetCultureInfo("de-DE"))

            lblPreis.Text = preisText & " €"
            txtPreisEdit.Text = preisText
            'lblPreis.Text = txtPreisEdit.Text
            lblPreis.Text = preisText & " €"
            ' 🖼️ Resim yükleme — önce Bild (BLOB), sonra fallback BildPfad
            If selectedProduct.Table.Columns.Contains("Bild") AndAlso Not IsDBNull(selectedProduct("Bild")) Then
                LoadBildFromBytes(CType(selectedProduct("Bild"), Byte()))
            ElseIf Me.ProductID > 0 Then
                ' Eğer DataRow'da Bild yoksa, ProductID üzerinden Products tablosundan oku
                LoadImageFromDatabase(Me.ProductID)
            End If

            ' ⭐⭐⭐ BURASI ÖNEMLİ ⭐⭐⭐
            Label1.Text = Me.ProductID.ToString()

            ' ❌ BU SATIR KESİNLİKLE SİLİNDİ
            ' txtPreisEdit.Text = SafeGetDecimal(selectedProduct, "Preis")

    End Sub

    ' ---------------------------------------------------------
    ' 🔧 Yardımcı Fonksiyonlar
    ' ---------------------------------------------------------

    ' Kolon var mı kontrol eder, varsa adını döner
    Private Function GetExistingColumn(row As DataRow, possibleNames As String()) As String
        For Each colName In possibleNames
            If row.Table.Columns.Contains(colName) Then
                Return colName
            End If
        Next
        Return ""
    End Function

    ' Güvenli string okuma
    Private Function SafeGet(row As DataRow, columnName As String) As String
        If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
            Return row(columnName).ToString()
        End If
        Return ""
    End Function

    ' Güvenli decimal okuma
    Private Function SafeGetDecimal(row As DataRow, columnName As String) As Decimal
        If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
            Return Convert.ToDecimal(row(columnName))
        End If
        Return 0D
    End Function





    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Sind Sie sicher, dass Sie dieses Produkt löschen möchten?", "Bestätigung", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            DeleteProduct()
        End If
    End Sub

    Private Sub txtPreisEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPreisEdit.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Escape Then
            e.SuppressKeyPress = True
            btnProduktSuchen.Focus()   ' löst Leave aus → Preis wird übernommen
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click, lblPreis.Click
        Dim raw As String = lblPreis.Text.Replace("€", "").Trim()

        Dim value As Decimal
        Decimal.TryParse(raw,
                 Globalization.NumberStyles.Any,
                 Globalization.CultureInfo.GetCultureInfo("de-DE"),
                 value)

        txtPreisEdit.Text = value.ToString("0.##", Globalization.CultureInfo.GetCultureInfo("de-DE"))
        txtPreisEdit.Text = Preis.ToString("0.00", Globalization.CultureInfo.GetCultureInfo("de-DE"))
        txtPreisEdit.Visible = True
        txtPreisEdit.BringToFront()
        txtPreisEdit.Focus()
        txtPreisEdit.SelectAll()

        lblPreis.Visible = False   ' düzenleme modunda gizle
        PictureBox2.Visible = False
    End Sub

    Public Property ShowActionButtons As Boolean
        Get
            Return _showActionButtons
        End Get
        Set(value As Boolean)
            _showActionButtons = value
            UpdateButtonsVisibility()
        End Set
    End Property
    Private Sub UpdateButtonsVisibility()
        ' Kartın içindeki butonları buraya yaz
        btnEinfügen.Visible = _showActionButtons
        btnDelete.Visible = _showActionButtons
        Button1.Visible = _showActionButtons
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            ImagePath = OpenFileDialog1.FileName
            pbBild.Image = Image.FromFile(ImagePath)

            ' ⭐ Resmi anında SQL'e BLOB olarak kaydet
            If Me.ProductID > 0 Then
                Dim bytes As Byte() = File.ReadAllBytes(ImagePath)
                UpdateBildInDatabase(Me.ProductID, bytes)
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.ProductID = 0 OrElse Me.AngebotID = 0 Then
            MessageBox.Show("ProduktID veya AngebotID hatalı.")
            Exit Sub
        End If

        Dim preis As Decimal = Me.Preis

        Using con As New SqlConnection(baglan)
            con.Open()
            Dim cmd As New SqlCommand("UPDATE AngebotProdukte SET Preis=@preis WHERE AngebotID=@aid AND ProduktID=@pid", con)
            cmd.Parameters.AddWithValue("@preis", preis)
            cmd.Parameters.AddWithValue("@aid", Me.AngebotID)
            cmd.Parameters.AddWithValue("@pid", Me.ProductID)
            Dim rowsAffected = cmd.ExecuteNonQuery()

            If rowsAffected = 0 Then
                MessageBox.Show("Hiç satır güncellenmedi. AngebotID veya ProduktID yanlış olabilir.")
            Else
                MessageBox.Show("Preis für " & Me.ProduktName & " erfolgreich aktualisiert.")
            End If
        End Using
    End Sub




    Private Sub txtPreisEdit_TextChanged(sender As Object, e As EventArgs) Handles txtPreisEdit.TextChanged
        If Not txtPreisEdit.Focused Then Return   ' nur echte Eingaben des Benutzers
        Dim newPreis As Decimal
        If Decimal.TryParse(txtPreisEdit.Text.Replace(".", ","), Globalization.NumberStyles.Number, Globalization.CultureInfo.GetCultureInfo("de-DE"), newPreis) Then
            Me.Preis = newPreis
            Me.IsEdited = True
            ' Ana forma bildir
            RaiseEvent PriceChanged(Me.ProductID, newPreis)
        End If
    End Sub

    Private Sub txtPreisEdit_Leave(sender As Object, e As EventArgs) Handles txtPreisEdit.Leave
        Dim value As Decimal

        If Decimal.TryParse(txtPreisEdit.Text.Replace(".", ","),
                        Globalization.NumberStyles.Number,
                        Globalization.CultureInfo.GetCultureInfo("de-DE"),
                        value) Then

            Me.Preis = value   ' hem label hem textbox güncellenir
        End If

        txtPreisEdit.Visible = False
        lblPreis.Visible = True   ' ⭐ LABEL GERİ GELİYOR
        PictureBox2.Visible = True
    End Sub

    'Public Event CardDeleted(ByVal sender As ProductCard)


    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        ' Kart silindi olarak işaretleniyor
        Me.IsDeleted = True

        ' --- OwnerForm ve DeletedCards güvenli kontrol ---
        If OwnerForm IsNot Nothing Then

            ' DeletedCards listesi yoksa otomatik oluştur
            If OwnerForm.DeletedCards Is Nothing Then
                OwnerForm.DeletedCards = New List(Of ProductCard)
            End If

            ' Kartı DeletedCards listesine ekle
            OwnerForm.DeletedCards.Add(Me)
        End If

        ' --- FlowLayoutPanel'den güvenli kaldırma ---
        Dim parentPanel As FlowLayoutPanel = TryCast(Me.Parent, FlowLayoutPanel)
        If parentPanel IsNot Nothing Then
            parentPanel.Controls.Remove(Me)
            ' ❌ Dispose yok — doğru
        End If

        ' --- Event tetikleme ---
        RaiseEvent CardRemoved(Me, EventArgs.Empty)
    End Sub


    Private Function ImageToByteArray(img As Image) As Byte()
        If img Is Nothing Then Return Nothing

        Try
            ' ⭐ Önce yeni 32bpp Bitmap'e kopyala — indexed format ve stream-bağımlılık sorununu çözer
            Using copy As New Bitmap(img.Width, img.Height, Imaging.PixelFormat.Format32bppArgb)
                Using g = Graphics.FromImage(copy)
                    g.DrawImage(img, 0, 0, img.Width, img.Height)
                End Using
                Using ms As New MemoryStream()
                    copy.Save(ms, Imaging.ImageFormat.Jpeg)
                    Return ms.ToArray()
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Private Function ByteArrayToImage(bytes As Byte()) As Image
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return Nothing

        Try
            Using ms As New MemoryStream(bytes)
                Using tmp As Image = Image.FromStream(ms)
                    Return New Bitmap(tmp)
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function
    Private Sub pbBild_Click_1(sender As Object, e As EventArgs) Handles pbBild.Click

    End Sub

    Private Sub btnAction_Click_1(sender As Object, e As EventArgs) Handles btnAction.Click

    End Sub
End Class