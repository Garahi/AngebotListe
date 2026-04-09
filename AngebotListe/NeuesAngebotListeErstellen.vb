Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports ImageMagick
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI

Public Class NeuesAngebotListeErstellen

    ' ── Win32 API: WhatsApp penceresini öne getirmek için ──
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function EnumWindows(lpEnumFunc As EnumWindowsProc, lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetWindowText(hWnd As IntPtr, lpString As System.Text.StringBuilder, nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function IsWindowVisible(hWnd As IntPtr) As Boolean
    End Function

    Private Delegate Function EnumWindowsProc(hWnd As IntPtr, lParam As IntPtr) As Boolean
    Private Const SW_RESTORE As Integer = 9

    ' WhatsApp penceresini bul (Desktop veya Browser sekmesi)
    Private Function FindWhatsAppWindow() As IntPtr
        Dim found As IntPtr = IntPtr.Zero
        EnumWindows(Function(hWnd, lParam)
                        If Not IsWindowVisible(hWnd) Then Return True
                        Dim sb As New System.Text.StringBuilder(256)
                        GetWindowText(hWnd, sb, sb.Capacity)
                        Dim title As String = sb.ToString()
                        If title.IndexOf("WhatsApp", StringComparison.OrdinalIgnoreCase) >= 0 Then
                            found = hWnd
                            Return False ' bul ve dur
                        End If
                        Return True
                    End Function, IntPtr.Zero)
        Return found
    End Function

    Private Sub BringWhatsAppToFront()
        Dim h = FindWhatsAppWindow()
        If h <> IntPtr.Zero Then
            ShowWindow(h, SW_RESTORE)
            SetForegroundWindow(h)
        End If
    End Sub
    Private isInternalUpdate As Boolean = False
    Public DeletedCards As New List(Of ProductCard)
    Private selectedCards As New List(Of ProductCard)
    Private connectionString As String = "Server=185.187.170.107;Database=AngebotListe;User Id=salih;Password=Garahi1943;"
    Private headerCheckBox As New CheckBox()
    Private headerCheckBoxState As Boolean = False
    Public AngebetNr As String

    Private Sub NeuesAngebotListeErstellen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isInternalUpdate = True   ' 🔥 Event geçici olarak kapalı
        LoadAngebotNumbers() ' Teklif numarasını yükle
        LoadAngebotNrList()      ' ComboBox doldur
        cmbAngebotNr.SelectedIndex = -1   ' 🔥 Hiçbir şey seçili olmasın
        isInternalUpdate = False  ' 🔥 Event tekrar aktif
        dtpDatum.Format = DateTimePickerFormat.Short

        ' DataGridView için sütunları tanımlayın
        If Not DataGridView1.Columns.Contains("Select") Then
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.Name = "Select"
            chk.HeaderText = "✓"
            chk.Width = 40
            chk.SortMode = DataGridViewColumnSortMode.NotSortable ' 🔴 BUNU EKLE
            DataGridView1.Columns.Insert(0, chk)
            DataGridView1.Columns("Select").Width = 15
        End If

        ' DataGridView genel olarak kilitli
        DataGridView1.ReadOnly = False

        ' Sadece Select kolonunu yazılabilir yap
        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Name = "Select" Then
                col.ReadOnly = False
            Else
                col.ReadOnly = True
            End If
        Next
        '=========================
        'formun büyümemesi icin 

        Me.WindowState = FormWindowState.Normal
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        ' Formu tamamen sabitle
        Dim f As Size = Me.Size
        Me.MinimumSize = f
        Me.MaximumSize = f
        DATAGRİD_1_RENKLENDIR()
        '================================= ortala =========================
        Me.StartPosition = FormStartPosition.Manual

        Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height

        Dim formWidth As Integer = Me.Width
        Dim formHeight As Integer = Me.Height

        Me.Location = New Point(
            (screenWidth - formWidth) \ 2,
            (screenHeight - formHeight) \ 2
        )
        ' bunu yapiyorum ki  f9 ile firmaname formunu acabilmek icin '
        Me.KeyPreview = True
        Me.Icon = New Icon("C:\AngebotListe\İcon\SalihS48x48.ico")


    End Sub
    Public Sub New()
        InitializeComponent()

        Dim exePath As String = Application.ExecutablePath
        Me.Icon = Icon.ExtractAssociatedIcon(exePath)
    End Sub
    Private Sub NeuesAngebotListeErstellen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F9 Then

            Dim frm As New FirmaName()

            ' Formu her zaman ortada aç
            frm.StartPosition = FormStartPosition.CenterParent

            ' Bu formun önünde açılması için System.InvalidCaSystem.InvalidCastException: stException: ShowDialog kullan
            frm.ShowDialog(Me)

        End If
    End Sub

    Public Sub DATAGRİD_1_RENKLENDIR()

        DataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro

        DataGridView1.ColumnHeadersDefaultCellStyle.Font =
    New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)

        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro
        DataGridView1.EnableHeadersVisualStyles = False

        DataGridView1.RowHeadersVisible = False

    End Sub
    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint
        Dim borderColor As Color = Color.FromArgb(180, 180, 180)
        Dim borderWidth As Integer = 1

        Using pen As New Pen(borderColor, borderWidth)
            e.Graphics.DrawRectangle(pen, 0, 0, FlowLayoutPanel1.Width - 1, FlowLayoutPanel1.Height - 1)
        End Using
    End Sub


    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        ' Sadece "Select" sütunu
        If DataGridView1.Columns(e.ColumnIndex).Name = "Select" Then

            ' Hücrelerdeki değişiklikleri commit et (sol veya sağ farketmez)
            If DataGridView1.IsCurrentCellDirty Then
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If

            Dim allChecked As Boolean = True

            ' Tüm satırların seçili olup olmadığını kontrol et
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim cellValue = row.Cells("Select").Value
                If cellValue Is Nothing OrElse cellValue Is DBNull.Value OrElse Not Convert.ToBoolean(cellValue) Then
                    allChecked = False
                    Exit For
                End If
            Next

            ' Eğer hepsi seçili ise boşalt, değilse hepsini seç
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.Cells("Select").Value = Not allChecked
            Next
        End If
    End Sub
    ' Teklif numaralarını yükle
    Private Sub LoadAngebotNumbers()

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' Sadece bugünün numaralarını çek
            Dim today As String = DateTime.Now.ToString("yyyyMMdd")
            Dim prefix As String = "ANG-" & today & "-"

            Dim cmd As New SqlCommand("SELECT TOP 1 AngebotNr FROM AngebotListe WHERE AngebotNr LIKE @Prefix + '%' ORDER BY AngebotNr DESC", con)

            cmd.Parameters.AddWithValue("@Prefix", prefix)

            Dim lastNrObj As Object = cmd.ExecuteScalar()

            Dim maxNum As Integer = 0

            If lastNrObj IsNot Nothing Then
                Dim lastNr As String = lastNrObj.ToString()
                Dim parts = lastNr.Split("-"c)
                maxNum = Integer.Parse(parts(2))
            End If

            ' Yeni numara = max + 1
            Dim newNum As Integer = maxNum + 1
            Dim newNr As String = prefix & newNum.ToString("D4")

            txtAngebotNr.Text = newNr
            AngebetNr = newNr
        End Using

    End Sub


    Private Function GetNextAngebotNr() As String
        Dim nextNr As String = ""
        Using con As New SqlConnection(connectionString)
            con.Open()
            ' En son AngebotNr'yi al (yıl bazlı veya genel)
            Dim cmd As New SqlCommand("SELECT TOP 1 AngebotNr FROM [AngebotListe] ORDER BY AngebotID DESC", con)
            Dim lastNrObj = cmd.ExecuteScalar()

            If lastNrObj Is Nothing Then
                ' Eğer hiç yoksa ilk Nr
                Dim year As String = DateTime.Now.Year.ToString()
                nextNr = $"ANG-{year}-0001"
            Else
                Dim lastNr As String = lastNrObj.ToString()
                ' Format: "ANG-2026-0005"
                Dim parts = lastNr.Split("-"c)
                Dim year As String = parts(1)
                Dim num As Integer = Convert.ToInt32(parts(2)) + 1
                nextNr = $"ANG-{year}-{num:0000}"
            End If
        End Using
        Return nextNr
    End Function

    Private Sub cmbAngebotNr_SelectedIndexChanged(sender As Object, e As EventArgs) _
Handles cmbAngebotNr.SelectedIndexChanged

        If isInternalUpdate Then Exit Sub   ' 🔥 INSERT sırasında çalışmasın

        If cmbAngebotNr.SelectedItem Is Nothing Then Exit Sub

        Dim drv As DataRowView = TryCast(cmbAngebotNr.SelectedItem, DataRowView)
        Dim selectedAngebotNr As String

        If drv IsNot Nothing Then
            selectedAngebotNr = drv("AngebotNr").ToString()
        Else
            selectedAngebotNr = cmbAngebotNr.SelectedItem.ToString()
        End If

        LoadProductsBasedOnAngebotNr(selectedAngebotNr)

        If DataGridView1.Columns.Contains("Select") Then
            DataGridView1.Columns("Select").Width = 25
        End If
        UpdateProduktCount()
    End Sub

    Private Sub LoadAngebotNrList()

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim dt As New DataTable()

            Dim cmd As New SqlCommand("SELECT DISTINCT AngebotNr FROM AngebotListe ORDER BY AngebotNr DESC", con)

            dt.Load(cmd.ExecuteReader())

            cmbAngebotNr.DisplayMember = "AngebotNr"
            cmbAngebotNr.ValueMember = "AngebotNr"
            cmbAngebotNr.DataSource = dt
        End Using

    End Sub


    Private Sub LoadAngebotDetails(angebotNr As String)
        Dim dt As New DataTable()

        ' Teklif numarasını veritabanından alarak ID'sini bulma
        Dim angebotID As Integer
        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr = @AngebotNr", con)
            cmd.Parameters.AddWithValue("@AngebotNr", angebotNr)

            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                angebotID = Convert.ToInt32(result)
            End If
        End Using

        ' Ürünleri çekme
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("SELECT ProduktID, ProduktName AS Name, Beschreibung, Preis, Bild FROM AngebotProdukte WHERE AngebotID = @AngebotID", con)

                cmd.Parameters.AddWithValue("@AngebotID", angebotID)
                con.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        ' ⭐ DataGridView için Bild kolonunu kaldır (binary VarBinary binding sorunlu)
        Dim bildBytesByProductId As New Dictionary(Of Integer, Byte())
        If dt.Columns.Contains("Bild") AndAlso dt.Columns.Contains("ProduktID") Then
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Bild")) AndAlso Not IsDBNull(row("ProduktID")) Then
                    bildBytesByProductId(Convert.ToInt32(row("ProduktID"))) = CType(row("Bild"), Byte())
                End If
            Next
            dt.Columns.Remove("Bild")
        End If
        'Using con As New SqlConnection(connectionString)
        '    Using cmd As New SqlCommand("SELECT ProduktId, Name, Beschreibung, Preis, BildPfad FROM [AngebotListe].[dbo].[Products] WHERE Aktiv = 1;", con) ' BildPfad eklendi
        '        con.Open()
        '        dt.Load(cmd.ExecuteReader())
        '    End Using
        'End Using

        ' DataGridView doldur
        DataGridView1.DataSource = dt

        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .RowTemplate.Height = 32
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, FontStyle.Bold)
            .DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ReadOnly = True
        End With

        ' FlowLayoutPanel doldur
        FlowLayoutPanel1.Controls.Clear()

        For Each row As DataRow In dt.Rows
            Dim card As New ProductCard()

            ' Kontrol yapın ve boş değerlerle ilgili hatalardan kaçının
            If Not IsDBNull(row("ProduktId")) Then
                card.ProductID = CInt(row("ProduktId"))
            End If

            If Not IsDBNull(row("Name")) Then
                card.ProduktName = row("Name").ToString()
            End If

            If Not IsDBNull(row("Beschreibung")) Then
                card.Beschreibung = row("Beschreibung").ToString()
            End If

            If Not IsDBNull(row("Preis")) Then
                card.Preis = CDec(row("Preis"))
            End If

            ' ⭐ Bild — dictionary'den (dt'den çıkardık) ya da row'dan
            If bildBytesByProductId.ContainsKey(card.ProductID) Then
                card.LoadBildFromBytes(bildBytesByProductId(card.ProductID))
            ElseIf row.Table.Columns.Contains("Bild") AndAlso Not IsDBNull(row("Bild")) Then
                card.LoadBildFromBytes(CType(row("Bild"), Byte()))
            End If

            ' Event handler ekleyin
            AddHandler card.CardFocused, AddressOf Card_Focused


            FlowLayoutPanel1.Controls.Add(card)
        Next
    End Sub
    Private Sub Card_Focused(productID As Integer)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If CInt(row.Cells("ID").Value) = productID Then
                row.Selected = True
                DataGridView1.CurrentCell = row.Cells("ID")
                Exit For
            End If
        Next
    End Sub

    Private Sub LoadProductsBasedOnAngebotNr(angebotNr As String)
        FlowLayoutPanel1.Controls.Clear()

        Dim angebotID As Integer = -1
        Dim dt As New DataTable()

        ' 1) AngebotID al
        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr = @AngebotNr", con)
            cmd.Parameters.AddWithValue("@AngebotNr", angebotNr)

            Dim result = cmd.ExecuteScalar()
            If result Is Nothing Then
                MessageBox.Show("Seçilen AngebotNr için teklif bulunamadı.")
                Exit Sub
            End If

            angebotID = Convert.ToInt32(result)
        End Using

        ' 2) Ürünleri çek
        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT ProduktID, ProduktName, Beschreibung, Preis, Bild FROM AngebotProdukte WHERE AngebotID = @AngebotID", con)

            cmd.Parameters.AddWithValue("@AngebotID", angebotID)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            dt.Load(reader)

            ' ⭐ ÖNCE FlowLayoutPanel kartlarını doldur (Bild bytes'ı dt'den oku)
            ' ─────────────────────────────────────────────────────────────────
            For Each row As DataRow In dt.Rows

                Dim productCard As New ProductCard()
                productCard.OwnerForm = Me

                ' ProduktID
                If Not IsDBNull(row("ProduktID")) Then
                    productCard.ProductID = Convert.ToInt32(row("ProduktID"))
                    productCard.Label1.Text = productCard.ProductID.ToString()
                Else
                    productCard.ProductID = 0
                    productCard.Label1.Text = ""
                End If

                productCard.OriginalProductID = productCard.ProductID

                ' Name
                If Not IsDBNull(row("ProduktName")) Then
                    productCard.ProduktName = row("ProduktName").ToString()
                End If

                ' Beschreibung
                If Not IsDBNull(row("Beschreibung")) Then
                    productCard.Beschreibung = row("Beschreibung").ToString()
                End If

                ' ⭐ Fiyat — doğru kültürle parse
                If Not IsDBNull(row("Preis")) Then
                    productCard.Preis =
                    Convert.ToDecimal(row("Preis"), Globalization.CultureInfo.InvariantCulture)
                End If

                ' ⭐ Bild — BLOB bytes'tan yükle
                If row.Table.Columns.Contains("Bild") AndAlso Not IsDBNull(row("Bild")) Then
                    productCard.LoadBildFromBytes(CType(row("Bild"), Byte()))
                End If

                ' DisplayMode
                productCard.DisplayMode = "AngebotListe"

                ' ⭐ BUNU EKLE
                AddHandler productCard.CardRemoved, AddressOf UpdateProduktCount
                ' ⭐ DisplayMode textbox’ı bozduğu için EN SON tekrar yazıyoruz
                productCard.txtPreisEdit.Text =
                productCard.Preis.ToString("0.##", Globalization.CultureInfo.GetCultureInfo("de-DE"))

                AddHandler productCard.CardFocused, AddressOf Card_Focused

                FlowLayoutPanel1.Controls.Add(productCard)
            Next

            ' ⭐ DataGridView için Bild kolonunu kaldır (binary data binding sorunlu)
            If dt.Columns.Contains("Bild") Then
                dt.Columns.Remove("Bild")
            End If

            ' DataGridView doldur
            DataGridView1.DataSource = dt
            With DataGridView1
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .RowTemplate.Height = 32
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ReadOnly = True
            End With
        End Using
    End Sub




    ' Kaydetme işlemi
    Private Sub btnSpeichern_Click(sender As Object, e As EventArgs) Handles btnSpeichern.Click

        Dim angebotNr As String = txtAngebotNr.Text.Trim()
        Dim angebotID As Integer

        If angebotNr = "" Then
            MessageBox.Show("AngebotNummer bulunamadı!")
            Exit Sub
        End If

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' ⭐ Önce bu AngebotNr var mı kontrol et
            Dim cmdCheck As New SqlCommand("SELECT TOP 1 AngebotID FROM AngebotListe WHERE AngebotNr=@nr ORDER BY AngebotID DESC", con)
            cmdCheck.Parameters.AddWithValue("@nr", angebotNr)
            Dim existing = cmdCheck.ExecuteScalar()

            If existing IsNot Nothing AndAlso Not IsDBNull(existing) Then
                ' Var → mevcut ID kullan, eski ürünleri sil
                angebotID = Convert.ToInt32(existing)
                Dim cmdDelOld As New SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con)
                cmdDelOld.Parameters.AddWithValue("@aid", angebotID)
                cmdDelOld.ExecuteNonQuery()
            Else
                ' Yok → yeni INSERT
                Dim cmdAngebot As New SqlCommand("INSERT INTO AngebotListe (Datum, Beschreibung, PdfPfad, AngebotNr) VALUES (@Datum,@Beschreibung,@PdfPfad,@AngebotNr); SELECT SCOPE_IDENTITY();", con)
                cmdAngebot.Parameters.AddWithValue("@Datum", dtpDatum.Value)
                cmdAngebot.Parameters.AddWithValue("@Beschreibung", "Açıklama burada")
                cmdAngebot.Parameters.AddWithValue("@PdfPfad", DBNull.Value)
                cmdAngebot.Parameters.AddWithValue("@AngebotNr", angebotNr)
                angebotID = Convert.ToInt32(cmdAngebot.ExecuteScalar())
            End If

            ' ✔ Ürün sayacı
            Dim insertedCount As Integer = 0

            ' ✔ FlowLayoutPanel ürünleri ekle
            For Each card As ProductCard In FlowLayoutPanel1.Controls.OfType(Of ProductCard)

                ' Boş kartları atla
                If card.ProductID = 0 Then Continue For

                ' 🔥 Fiyatı update’teki gibi parse et
                Dim preis As Decimal = 0D

                If card.txtPreisEdit IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(card.txtPreisEdit.Text) Then
                    Dim rawText As String = card.txtPreisEdit.Text.Replace(".", ",").Trim()
                    If Not Decimal.TryParse(rawText, preis) Then
                        MessageBox.Show("Ungültiger Preis: " & rawText)
                        Continue For
                    End If
                Else
                    preis = card.Preis
                End If

                Dim cmdInsert As New SqlCommand("
                INSERT INTO AngebotProdukte
                (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
                VALUES (@AngebotID,@ProduktID,@ProduktName,@Beschreibung,@Preis,@Bild)", con)

                cmdInsert.Parameters.AddWithValue("@AngebotID", angebotID)
                cmdInsert.Parameters.AddWithValue("@ProduktID", card.ProductID)
                cmdInsert.Parameters.AddWithValue("@ProduktName", If(card.ProduktName, ""))
                cmdInsert.Parameters.AddWithValue("@Beschreibung", If(card.Beschreibung, ""))
                cmdInsert.Parameters.AddWithValue("@Preis", preis)
                cmdInsert.Parameters.Add("@Bild", SqlDbType.VarBinary).Value = If(CType(card.BildBytes, Object), DBNull.Value)

                insertedCount += cmdInsert.ExecuteNonQuery()

                ' 🔥 Update mantığı için başlangıç state’i kur
                card.OriginalProductID = card.ProductID
                card.Preis = preis
                card.IsEdited = False
            Next

            If insertedCount = 0 Then
                MessageBox.Show("Hiç geçerli ürün yok! Lütfen ürün seç.")
                Exit Sub
            End If

        End Using

        LoadAngebotCombo()
        cmbAngebotNr.SelectedItem = angebotNr

        LoadProductsBasedOnAngebotNr(angebotNr)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                row.Cells("Select").Value = True
            End If
        Next

        CreatePdfFromSelectedCards2(angebotNr)

        MessageBox.Show("✔ Angebot erstellt und PDF erzeugt.")

    End Sub


    Private Sub LoadAngebotCombo()
        Dim dt As New DataTable()

        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT AngebotNr FROM [AngebotListe] ORDER BY Datum DESC", con)
            dt.Load(cmd.ExecuteReader())
        End Using

        cmbAngebotNr.DataSource = dt
        cmbAngebotNr.DisplayMember = "AngebotNr"
        cmbAngebotNr.ValueMember = "AngebotNr"
    End Sub
    Private Function GetNextAngebotID() As Integer
        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT ISNULL(MAX(AngebotID),0)+1 FROM [AngebotListe].[dbo].[AngebotListe]", con)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function
    Private Sub btnNeueProdukt_Click(sender As Object, e As EventArgs) Handles btnNeueProdukt.Click

        Dim newCard As New ProductCard()

        newCard.ProductID = 0
        newCard.ProduktName = ""
        newCard.Beschreibung = ""
        newCard.Preis = 0
        newCard.BildPfad = ""

        newCard.DisplayMode = "AngebotListe"

        AddHandler newCard.PriceChanged, AddressOf UpdatePriceFromCard
        AddHandler newCard.CardRemoved, AddressOf UpdateProduktCount   ' ⭐ EKLENMESİ GEREKEN SATIR

        FlowLayoutPanel1.Controls.Add(newCard)
        FlowLayoutPanel1.ScrollControlIntoView(newCard)
        UpdateProduktCount()
    End Sub





    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Dim selectedCards As New List(Of ProductCard)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso Convert.ToBoolean(row.Cells("Select").Value) = True Then
                Dim id As Integer = CInt(row.Cells("ProduktId").Value)

                For Each card As ProductCard In FlowLayoutPanel1.Controls
                    If card.ProductID = id Then
                        selectedCards.Add(card)
                        Exit For
                    End If
                Next
            End If
        Next

        If selectedCards.Count = 0 Then
            MessageBox.Show("Bitte Produkte auswählen.")
            Exit Sub
        End If

        CreatePdfFromSelectedCards(selectedCards)
    End Sub

    Private Sub CreatePdfFromSelectedCards(selectedCards As List(Of ProductCard))
        Dim angebotNr As String = cmbAngebotNr.SelectedItem.ToString()
        Dim angebotDatum As String = DateTime.Now.ToString("dd.MM.yyyy")
        Dim savePath As String = $"C:\AngebotListe\WhatsapaYollanilan\{angebotNr}.pdf"

        Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
        Dim writer = PdfWriter.GetInstance(doc, New IO.FileStream(savePath, IO.FileMode.Create))
        doc.Open()

        Dim fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
        Dim paragraph As New Paragraph($"Angebot: {angebotNr}   Datum: {angebotDatum}", fontTitle)
        paragraph.Alignment = Element.ALIGN_CENTER
        paragraph.SpacingAfter = 20
        doc.Add(paragraph)

        Dim table As New PdfPTable(2)
        table.WidthPercentage = 100
        table.HorizontalAlignment = Element.ALIGN_CENTER
        table.DefaultCell.Border = Rectangle.NO_BORDER
        table.SpacingBefore = 10
        table.SpacingAfter = 10

        For Each card In selectedCards
            Dim img As Bitmap = card.GetShareImage()
            Dim ms As New IO.MemoryStream()
            img.Save(ms, Imaging.ImageFormat.Png)
            Dim pdfImg = Image.GetInstance(ms.ToArray())

            pdfImg.ScaleToFit(250, 350)

            Dim cell As New PdfPCell(pdfImg)
            cell.Border = Rectangle.NO_BORDER
            cell.Padding = 10
            cell.HorizontalAlignment = Element.ALIGN_CENTER

            table.AddCell(cell)
        Next

        If selectedCards.Count Mod 2 = 1 Then
            table.AddCell(New PdfPCell() With {.Border = Rectangle.NO_BORDER})
        End If

        doc.Add(table)
        doc.Close()

        Process.Start(savePath)
    End Sub

    Private Function GetSelectedProductCardsFromUI() As List(Of ProductCard)
        Dim list As New List(Of ProductCard)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso CBool(row.Cells("Select").Value) = True Then

                Dim card As ProductCard = CType(row.Cells("CardObject").Value, ProductCard)
                list.Add(card)

            End If
        Next

        Return list
    End Function

    Private Sub CreatePdfFromGridSelection()
        Dim angebotNr As String = cmbAngebotNr.SelectedItem.ToString()
        Dim angebotDatum As String = DateTime.Now.ToString("dd.MM.yyyy")
        Dim savePath As String = $"C:\AngebotListe\WhatsapaYollanilan\{angebotNr}.pdf"

        Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
        PdfWriter.GetInstance(doc, New IO.FileStream(savePath, IO.FileMode.Create))
        doc.Open()

        ' BAŞLIK
        Dim fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
        Dim header As New Paragraph($"Angebot: {angebotNr}     Datum: {angebotDatum}", fontTitle)
        header.Alignment = Element.ALIGN_CENTER
        header.SpacingAfter = 20
        doc.Add(header)

        ' 2 SÜTUNLU ANA TABLO
        Dim mainTable As New PdfPTable(2)
        mainTable.WidthPercentage = 100
        mainTable.DefaultCell.Border = Rectangle.NO_BORDER

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso Convert.ToBoolean(row.Cells("Select").Value) = True Then

                Dim produktName = row.Cells("ProduktName").Value.ToString()
                Dim beschreibung = row.Cells("Beschreibung").Value.ToString()
                Dim preis = Convert.ToDecimal(row.Cells("Preis").Value)
                Dim bildPfad = row.Cells("BildPfad").Value.ToString()

                ' ÜRÜN KARTI (tek hücre)
                Dim cardCell As New PdfPCell()
                cardCell.Border = Rectangle.NO_BORDER
                cardCell.Padding = 5
                cardCell.FixedHeight = 360
                cardCell.HorizontalAlignment = Element.ALIGN_LEFT

                ' Dikey layout
                Dim stack As New Paragraph()
                stack.Alignment = Element.ALIGN_LEFT

                ' 1) RESİM KUTUSU (sabit yükseklik)
                Dim imgBox As New PdfPTable(1)
                imgBox.WidthPercentage = 100
                Dim imgBoxCell As New PdfPCell()
                imgBoxCell.Border = Rectangle.NO_BORDER
                imgBoxCell.FixedHeight = 160
                imgBoxCell.HorizontalAlignment = Element.ALIGN_LEFT
                imgBoxCell.VerticalAlignment = Element.ALIGN_MIDDLE

                If IO.File.Exists(bildPfad) Then
                    Dim pdfImg = Image.GetInstance(bildPfad)
                    pdfImg.ScaleToFit(150, 150)
                    pdfImg.Alignment = Element.ALIGN_LEFT
                    imgBoxCell.AddElement(pdfImg)
                End If

                imgBox.AddCell(imgBoxCell)
                cardCell.AddElement(imgBox)

                ' 2) METİN KUTUSU (sabit yükseklik)
                Dim textBox As New PdfPTable(1)
                textBox.WidthPercentage = 100
                Dim textCell As New PdfPCell()
                textCell.Border = Rectangle.NO_BORDER
                textCell.FixedHeight = 120
                textCell.HorizontalAlignment = Element.ALIGN_LEFT

                Dim fontName = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13)
                Dim fontPrice = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)
                Dim fontDesc = FontFactory.GetFont(FontFactory.HELVETICA, 10)

                textCell.AddElement(New Paragraph(produktName, fontName))
                textCell.AddElement(New Paragraph(preis.ToString("C2"), fontPrice))
                textCell.AddElement(New Paragraph(beschreibung, fontDesc))

                textBox.AddCell(textCell)
                cardCell.AddElement(textBox)

                ' 3) BUTON KUTUSU (sabit yükseklik)
                Dim buttonBox As New PdfPTable(1)
                buttonBox.WidthPercentage = 40

                'Dim buttonText = New Paragraph("ADD TO CART", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.White))
                Dim buttonText = New Paragraph("My Fresh Fruit Yeşil GmbH", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.White))
                buttonText.Alignment = Element.ALIGN_CENTER

                Dim buttonBg As New PdfPCell(buttonText)
                buttonBg.BackgroundColor = New BaseColor(0, 128, 0)
                buttonBg.Border = Rectangle.NO_BORDER
                buttonBg.Padding = 6
                buttonBg.HorizontalAlignment = Element.ALIGN_CENTER

                buttonBox.AddCell(buttonBg)
                cardCell.AddElement(buttonBox)

                ' ANA TABLOYA EKLE
                mainTable.AddCell(cardCell)
            End If
        Next

        ' TEK ÜRÜN KALIRSA BOŞ HÜCRE EKLE
        If mainTable.Rows.Count Mod 2 = 1 Then
            mainTable.AddCell(New PdfPCell() With {.Border = Rectangle.NO_BORDER})
        End If

        doc.Add(mainTable)
        doc.Close()
        Process.Start(savePath)
    End Sub





    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        ' 1) Header tıklanmışsa çık
        If e.RowIndex < 0 Then Exit Sub

        ' 2) Select sütunu değilse çık
        If e.ColumnIndex <> DataGridView1.Columns("Select").Index Then Exit Sub

        ' 3) Normal işlem
        Dim row = DataGridView1.Rows(e.RowIndex)
        row.Cells("Select").Value = Not CBool(row.Cells("Select").Value)

    End Sub



    'Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick


    '    For Each row As DataGridViewRow In DataGridView1.Rows
    '        If Not row.IsNewRow Then
    '            row.Cells("Select").Value = True
    '        End If
    '    Next
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CreatePdfFromGridSelection()
    End Sub
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) _
Handles DataGridView1.SelectionChanged

        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub

        Dim row = DataGridView1.SelectedRows(0)
        If row Is Nothing Then Exit Sub

        Dim rawID = row.Cells("ProduktID").Value
        If rawID Is Nothing OrElse IsDBNull(rawID) Then Exit Sub

        Dim selectedID As Integer
        If Not Integer.TryParse(rawID.ToString(), selectedID) Then Exit Sub

        For Each card As ProductCard In FlowLayoutPanel1.Controls
            If card.ProductID = selectedID Then
                FlowLayoutPanel1.ScrollControlIntoView(card)
                card.SetHighlight(True)
            Else
                card.SetHighlight(False)
            End If
        Next

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim selectedRows As New List(Of DataGridViewRow)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso Convert.ToBoolean(row.Cells("Select").Value) = True Then
                selectedRows.Add(row)
            End If
        Next

        If selectedRows.Count = 0 Then
            MessageBox.Show("Bitte Produkte auswählen.")
            Exit Sub
        End If

        CreatePdfFromGridSelection(selectedRows)
    End Sub
    Private Sub CreatePdfFromGridSelection(selectedRows As List(Of DataGridViewRow))
        Dim angebotNr As String = cmbAngebotNr.SelectedItem.ToString()
        Dim angebotDatum As String = DateTime.Now.ToString("dd.MM.yyyy")
        Dim savePath As String = $"C:\AngebotListe\WhatsapaYollanilan\{angebotNr}.pdf"

        Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
        Dim writer = PdfWriter.GetInstance(doc, New IO.FileStream(savePath, IO.FileMode.Create))
        doc.Open()

        Dim fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
        Dim paragraph As New Paragraph($"Angebot: {angebotNr}   Datum: {angebotDatum}", fontTitle)
        paragraph.Alignment = Element.ALIGN_CENTER
        paragraph.SpacingAfter = 20
        doc.Add(paragraph)

        Dim table As New PdfPTable(2)
        table.WidthPercentage = 100
        table.HorizontalAlignment = Element.ALIGN_CENTER
        table.DefaultCell.Border = Rectangle.NO_BORDER
        table.SpacingBefore = 10
        table.SpacingAfter = 10

        For Each row In selectedRows

            Dim produktName As String = row.Cells("ProduktName").Value.ToString()
            Dim beschreibung As String = row.Cells("Beschreibung").Value.ToString()
            Dim preis As String = row.Cells("Preis").Value.ToString()
            Dim bildPfad As String = row.Cells("BildPfad").Value.ToString()

            ' ÜRÜN KARTI
            Dim cardTable As New PdfPTable(1)
            cardTable.WidthPercentage = 100

            ' --- RESİM ---
            Dim imgCell As New PdfPCell()
            imgCell.Border = Rectangle.NO_BORDER
            imgCell.HorizontalAlignment = Element.ALIGN_LEFT

            If IO.File.Exists(bildPfad) Then
                Dim pdfImg = Image.GetInstance(bildPfad)
                pdfImg.ScaleToFit(180, 180)
                pdfImg.Alignment = Element.ALIGN_LEFT
                imgCell.AddElement(pdfImg)
            End If

            cardTable.AddCell(imgCell)

            ' --- METİN ---
            Dim textCell As New PdfPCell()
            textCell.Border = Rectangle.NO_BORDER
            textCell.HorizontalAlignment = Element.ALIGN_LEFT

            textCell.AddElement(New Paragraph(produktName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)))
            textCell.AddElement(New Paragraph(preis & " €", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11)))
            textCell.AddElement(New Paragraph(beschreibung, FontFactory.GetFont(FontFactory.HELVETICA, 10)))

            cardTable.AddCell(textCell)

            ' --- BUTON ---
            Dim buttonCell As New PdfPCell()
            buttonCell.Border = Rectangle.NO_BORDER
            buttonCell.HorizontalAlignment = Element.ALIGN_LEFT

            'Dim buttonText As New Paragraph("ADD TO CART", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.White))
            Dim buttonText As New Paragraph("My Fresh Fruit Yeşil GmbH", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.White))
            buttonText.Alignment = Element.ALIGN_CENTER

            Dim buttonBg As New PdfPCell(buttonText)
            buttonBg.BackgroundColor = New BaseColor(0, 128, 0)
            buttonBg.Border = Rectangle.NO_BORDER
            buttonBg.Padding = 6
            buttonBg.HorizontalAlignment = Element.ALIGN_CENTER

            Dim buttonTable As New PdfPTable(1)
            buttonTable.WidthPercentage = 40
            buttonTable.AddCell(buttonBg)

            buttonCell.AddElement(buttonTable)
            cardTable.AddCell(buttonCell)

            ' ANA TABLOYA EKLE
            table.AddCell(cardTable)
        Next

        If selectedRows.Count Mod 2 = 1 Then
            table.AddCell(New PdfPCell() With {.Border = Rectangle.NO_BORDER})
        End If

        doc.Add(table)
        doc.Close()

        Process.Start(savePath)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ' 1️⃣ Angebot kayıtlı değilse otomatik kaydet
        If cmbAngebotNr.SelectedItem Is Nothing OrElse AngebotKayitVarMi(cmbAngebotNr.SelectedItem.ToString()) = False Then

            ' 🔥 SelectedIndexChanged tetiklenmesin
            isInternalUpdate = True

            btnSpeichern.PerformClick()

            ' 🔥 INSERT bitti, artık event tekrar çalışabilir
            isInternalUpdate = False

            ' 1.1️⃣ INSERT sonrası DataGridView’i yeniden yükle
            LoadProductsBasedOnAngebotNr(cmbAngebotNr.SelectedItem.ToString())

            ' 1.2️⃣ Tüm ürünleri otomatik seç
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    row.Cells("Select").Value = True
                End If
            Next
        End If


        ' 2️⃣ DataGridView'den seçilen ürünleri al
        Dim selectedCards As New List(Of ProductCard)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso Convert.ToBoolean(row.Cells("Select").Value) = True Then

                Dim id As Integer = CInt(row.Cells("ProduktId").Value)

                For Each card As ProductCard In FlowLayoutPanel1.Controls
                    If card.ProductID = id Then
                        selectedCards.Add(card)
                        Exit For
                    End If
                Next
            End If
        Next

        If selectedCards.Count = 0 Then
            MessageBox.Show("Bitte Produkte auswählen.")
            Exit Sub
        End If

        ' 3️⃣ PDF oluştur
        CreatePdfFromSelectedCards2()

    End Sub

    Private Function AngebotKayitVarMi(angebotNr As String) As Boolean
        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM AngebotListe WHERE AngebotNr = @nr", con)
            cmd.Parameters.AddWithValue("@nr", angebotNr)
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Sub CreatePdfFromSelectedCards2(Optional angebotNr As String = Nothing)

        ' ✔ AngebotNummer her zaman txtAngebotNr.Text'ten alınacak
        If angebotNr Is Nothing OrElse angebotNr.Trim() = "" Then
            angebotNr = txtAngebotNr.Text
        End If

        If angebotNr.Trim() = "" Then
            MessageBox.Show("AngebotNummer bulunamadı!")
            Exit Sub
        End If

        Dim angebotDatum As String = DateTime.Now.ToString("dd.MM.yyyy")
        Dim savePath As String = $"C:\AngebotListe\WhatsapaYollanilan\{angebotNr}.pdf"

        Dim selectedCards As New List(Of ProductCard)

        ' ✔ DataGridView'de seçili ProductID'leri al
        Dim selectedIds As New List(Of Integer)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For
            If Not DataGridView1.Columns.Contains("Select") Then Exit For
            Dim selVal = row.Cells("Select").Value
            If selVal IsNot Nothing AndAlso CBool(selVal) = True Then
                If row.DataGridView.Columns.Contains("ProduktID") AndAlso row.Cells("ProduktID").Value IsNot Nothing Then
                    selectedIds.Add(Convert.ToInt32(row.Cells("ProduktID").Value))
                End If
            End If
        Next

        ' Hiç seçim yoksa → tüm kartları al
        Dim takeAll As Boolean = (selectedIds.Count = 0)

        ' ⭐ FlowLayoutPanel'deki gerçek kartları kullan (Bild dolu)
        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If TypeOf ctrl Is ProductCard Then
                Dim c As ProductCard = CType(ctrl, ProductCard)
                If takeAll OrElse selectedIds.Contains(CInt(c.ProductID)) Then
                    selectedCards.Add(c)
                End If
            End If
        Next

        ' ✔ PDF OLUŞTURMA
        Using fs As New IO.FileStream(savePath, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
            Using doc As New Document(PageSize.A4, 20, 20, 20, 20)
                Using writer = PdfWriter.GetInstance(doc, fs)

                    doc.Open()

                    Dim fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
                    Dim paragraph As New Paragraph($"Angebot: {angebotNr}   Datum: {angebotDatum}", fontTitle)
                    paragraph.Alignment = Element.ALIGN_CENTER
                    paragraph.SpacingAfter = 20
                    doc.Add(paragraph)

                    Dim table As New PdfPTable(2)
                    table.WidthPercentage = 100
                    table.DefaultCell.Border = Rectangle.NO_BORDER
                    table.HorizontalAlignment = Element.ALIGN_CENTER

                    For Each card In selectedCards
                        Using img As Bitmap = card.GetShareImage()
                            Using ms As New IO.MemoryStream()
                                img.Save(ms, Imaging.ImageFormat.Png)

                                Dim pdfImg = Image.GetInstance(ms.ToArray())
                                pdfImg.ScaleToFit(250, 350)

                                Dim cell As New PdfPCell(pdfImg)
                                cell.Border = Rectangle.NO_BORDER
                                cell.Padding = 10
                                cell.HorizontalAlignment = Element.ALIGN_CENTER

                                table.AddCell(cell)
                            End Using
                        End Using
                    Next

                    If selectedCards.Count Mod 2 = 1 Then
                        table.AddCell(New PdfPCell() With {.Border = Rectangle.NO_BORDER})
                    End If

                    doc.Add(table)
                    doc.Close()

                End Using
            End Using
        End Using


        ' ✔ PDF'yi aç
        Dim psi As New ProcessStartInfo()
        psi.FileName = savePath
        psi.UseShellExecute = True
        Process.Start(psi)

        ' ⭐ PDF üretildi → WhatsApp PDF Senden Helper'ı aç
        If File.Exists(savePath) Then
            ZeigeWhatsAppPdfHelper(savePath, angebotNr)
        End If

    End Sub






    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If cmbAngebotNr.SelectedItem Is Nothing Then
            MessageBox.Show("Bitte ein Angebot auswählen.")
            Exit Sub
        End If

        Dim angebotNr As String = cmbAngebotNr.Text
        Dim angebotID As Integer

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' Seçilen AngebotID al
            Dim cmdID As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdID.Parameters.AddWithValue("@nr", angebotNr)
            Dim result = cmdID.ExecuteScalar()
            If result Is Nothing Then
                MessageBox.Show("Angebot nicht gefunden")
                Exit Sub
            End If

            angebotID = Convert.ToInt32(result)

            ' FlowLayoutPanel içindeki tüm ProductCard’ları güncelle
            For Each card As ProductCard In FlowLayoutPanel1.Controls.OfType(Of ProductCard)
                If Not card.IsEdited Then Continue For ' sadece düzenlenenleri al

                Dim neuerPreis As Decimal
                If Decimal.TryParse(card.txtPreisEdit.Text, neuerPreis) Then
                    card.Preis = neuerPreis
                Else
                    Continue For
                End If

                Dim cmdUpdate As New SqlCommand("
                UPDATE AngebotProdukte
                SET Preis=@preis
                WHERE AngebotID=@aid AND ProduktID=@pid", con)

                cmdUpdate.Parameters.AddWithValue("@preis", card.Preis)
                cmdUpdate.Parameters.AddWithValue("@aid", angebotID)
                cmdUpdate.Parameters.AddWithValue("@pid", card.ProductID)
                cmdUpdate.ExecuteNonQuery()

                card.IsEdited = False ' iş tamamlandı, flag sıfırla
            Next
        End Using

        ' 🔥 Fiyat güncellemeden sonra DataGridView ve kartları yeniden yükle
        LoadProductsBasedOnAngebotNr(angebotNr)

        MessageBox.Show("Preise erfolgreich aktualisiert.")

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        ' --- 1) AngebotNr seçilmiş mi? ---
        If cmbAngebotNr.SelectedItem Is Nothing Then
            MessageBox.Show("Bitte wählen Sie eine AngebotNr aus.",
                        "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' --- 2) Silme onayı ---
        Dim confirm = MessageBox.Show(
        "Sind Sie sicher, dass Sie dieses Angebot vollständig löschen möchten?" & vbCrLf &
        "Dieser Vorgang kann nicht rückgängig gemacht werden.",
        "Löschbestätigung",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning)

        If confirm = DialogResult.No Then
            Exit Sub
        End If

        ' --- 3) AngebotNr al ---
        Dim angebotNr As String
        Dim drv As DataRowView = TryCast(cmbAngebotNr.SelectedItem, DataRowView)

        If drv IsNot Nothing Then
            angebotNr = drv("AngebotNr").ToString().Trim()
        Else
            angebotNr = cmbAngebotNr.SelectedItem.ToString().Trim()
        End If

        Dim angebotID As Integer = -1

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' --- 4) AngebotID bul ---
            Dim cmdGetID As New SqlCommand(
            "SELECT AngebotID FROM AngebotListe WHERE AngebotNr = @AngebotNr", con)
            cmdGetID.Parameters.AddWithValue("@AngebotNr", angebotNr)

            Dim result = cmdGetID.ExecuteScalar()

            If result Is Nothing Then
                MessageBox.Show("Die ausgewählte AngebotNr wurde nicht gefunden.",
                            "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            angebotID = Convert.ToInt32(result)

            ' --- 5) Ürünleri sil ---
            Dim cmdDeleteProdukte As New SqlCommand(
            "DELETE FROM AngebotProdukte WHERE AngebotID=@AngebotID", con)
            cmdDeleteProdukte.Parameters.AddWithValue("@AngebotID", angebotID)
            cmdDeleteProdukte.ExecuteNonQuery()

            ' --- 6) Teklifi sil ---
            Dim cmdDeleteAngebot As New SqlCommand(
            "DELETE FROM AngebotListe WHERE AngebotID=@AngebotID", con)
            cmdDeleteAngebot.Parameters.AddWithValue("@AngebotID", angebotID)
            cmdDeleteAngebot.ExecuteNonQuery()

        End Using

        ' --- 7) UI güncelle ---
        FlowLayoutPanel1.Controls.Clear()
        DataGridView1.DataSource = Nothing

        ' ⭐ ComboBox DataSource'lı olduğu için Items.Remove çalışmaz —
        '    LoadAngebotNrList ile DB'den taze yükle
        isInternalUpdate = True
        Try
            LoadAngebotNrList()
            cmbAngebotNr.SelectedIndex = -1
        Finally
            isInternalUpdate = False
        End Try

        LoadAngebotNumbers() ' txtAngebotNr için yeni numara

        MessageBox.Show("Das Angebot wurde erfolgreich gelöscht.",
                    "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        ' 0) Eğer teklif kaydedilmemişse önce kaydet
        If cmbAngebotNr.SelectedItem Is Nothing OrElse
       AngebotKayitVarMi(cmbAngebotNr.SelectedItem.ToString()) = False Then
            btnSpeichern.PerformClick()
        End If

        ' 1) Fiyatları SQL'e kaydet
        SaveManualPricesToSQL()

        ' 2) AngebotNr al
        'Dim angebotNr As String = cmbAngebotNr.SelectedItem.ToString()
        Dim angebotNr As String = txtAngebotNr.Text.Trim()

        ' 3) DataGridView'den seçili ürünleri al
        Dim productList As New List(Of ProductCard)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For

            ' sadece seçili olanları al
            If row.Cells("Select").Value Is Nothing OrElse
           CBool(row.Cells("Select").Value) = False Then
                Continue For
            End If

            Dim card As New ProductCard()
            card.ProductID = Convert.ToInt32(row.Cells("ProduktID").Value)
            card.ProduktName = If(row.Cells("ProduktName").Value IsNot Nothing, row.Cells("ProduktName").Value.ToString(), "")
            card.Beschreibung = If(row.Cells("Beschreibung").Value IsNot Nothing, row.Cells("Beschreibung").Value.ToString(), "")
            card.Preis = If(row.Cells("Preis").Value IsNot Nothing, Convert.ToDecimal(row.Cells("Preis").Value), 0)
            ' ⭐ Bild — BLOB bytes'tan yükle
            If row.DataGridView.Columns.Contains("Bild") Then
                Dim bildVal2 = row.Cells("Bild").Value
                If bildVal2 IsNot Nothing AndAlso Not IsDBNull(bildVal2) AndAlso TypeOf bildVal2 Is Byte() Then
                    card.LoadBildFromBytes(CType(bildVal2, Byte()))
                End If
            End If

            productList.Add(card)
        Next

        If productList.Count = 0 Then
            MessageBox.Show("PDF için ürün seçilmedi.")
            Exit Sub
        End If

        ' 4) SQL Güncelleme
        Using con As New SqlConnection(connectionString)
            con.Open()

            ' Teklif ID al
            Dim cmdId As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdId.Parameters.AddWithValue("@nr", angebotNr)
            Dim angebotId As Integer = CInt(cmdId.ExecuteScalar())

            ' Eski ürünleri sil
            Dim cmdDelete As New SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con)
            cmdDelete.Parameters.AddWithValue("@aid", angebotId)
            cmdDelete.ExecuteNonQuery()

            ' Yeni ürünleri ekle
            Dim cmdInsert As New SqlCommand("
            INSERT INTO AngebotProdukte
            (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
            VALUES (@aid, @pid, @pname, @desc, @preis, @bild)
        ", con)

            ' Parametreleri ekle
            cmdInsert.Parameters.Add("@aid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pname", SqlDbType.NVarChar, 200)
            cmdInsert.Parameters.Add("@desc", SqlDbType.NVarChar, 500)
            cmdInsert.Parameters.Add("@preis", SqlDbType.Decimal)
            cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary)

            ' Insert işlemini döngü ile yap
            For Each card In productList
                cmdInsert.Parameters("@aid").Value = angebotId
                cmdInsert.Parameters("@pid").Value = card.ProductID
                cmdInsert.Parameters("@pname").Value = card.ProduktName
                cmdInsert.Parameters("@desc").Value = card.Beschreibung
                cmdInsert.Parameters("@preis").Value = card.Preis
                cmdInsert.Parameters("@bild").Value = If(CType(card.BildBytes, Object), DBNull.Value)
                cmdInsert.ExecuteNonQuery()
            Next
        End Using

        ' 5) PDF oluştur
        CreatePdfFromSelectedCards2(angebotNr)

        'MessageBox.Show("PDF başarıyla oluşturuldu.")

    End Sub


    Private Sub SaveManualPricesToSQL()

        If DataGridView1 Is Nothing OrElse DataGridView1.Rows.Count = 0 Then Exit Sub

        Dim angebotNr As String = GetLastCreatedAngebotNr()
        If angebotNr Is Nothing Then Exit Sub

        Using con As New SqlConnection(connectionString)
            con.Open()

            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.IsNewRow Then Continue For

                Dim pid As Integer = Convert.ToInt32(row.Cells("ProduktID").Value)
                Dim preis As Decimal = Convert.ToDecimal(row.Cells("Preis").Value)

                Dim cmd As New SqlCommand("
                UPDATE AngebotProdukte
                SET Preis = @Preis
                WHERE ProduktID = @PID AND AngebotID = (
                    SELECT AngebotID FROM AngebotListe WHERE AngebotNr = @AngebotNr
                )", con)

                cmd.Parameters.AddWithValue("@Preis", preis)
                cmd.Parameters.AddWithValue("@PID", pid)
                cmd.Parameters.AddWithValue("@AngebotNr", angebotNr)

                cmd.ExecuteNonQuery()
            Next

        End Using

    End Sub

    Private Sub UpdateManualPrice()
        ' TextBox yoksa çık
        If Not Me.Controls.ContainsKey("txtPreisEdit") Then Exit Sub

        Dim txt As TextBox = CType(Me.Controls("txtPreisEdit"), TextBox)

        If String.IsNullOrWhiteSpace(txt.Text) Then Exit Sub

        Dim newPreis As Decimal
        If Not Decimal.TryParse(txt.Text, newPreis) Then Exit Sub

        ' DataGridView yoksa çık
        If DataGridView1 Is Nothing OrElse DataGridView1.CurrentRow Is Nothing Then Exit Sub

        ' Düzenlenen ürünün ID’si
        Dim produktID As Integer = Convert.ToInt32(DataGridView1.CurrentRow.Cells("ProduktID").Value)

        ' Angebot numarası
        Dim angebotNr As String = GetLastCreatedAngebotNr()
        If angebotNr Is Nothing Then Exit Sub

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("UPDATE AngebotProdukte SET Preis = @Preis WHERE ProduktID = @PID AND AngebotID = (SELECT TOP 1 AngebotID FROM AngebotListe WHERE AngebotNr = @AngebotNr ORDER BY AngebotID DESC)", con)

            cmd.Parameters.AddWithValue("@Preis", newPreis)
            cmd.Parameters.AddWithValue("@PID", produktID)
            cmd.Parameters.AddWithValue("@AngebotNr", angebotNr)

            cmd.ExecuteNonQuery()
        End Using
    End Sub


    'Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    '    ' 🔥 1) En son oluşturulan AngebotNr’yi al
    '    Dim angebotNr As String = GetLastCreatedAngebotNr()

    '    If angebotNr Is Nothing Then
    '        MessageBox.Show("Henüz hiç Angebot oluşturulmamış!")
    '        Exit Sub
    '    End If

    '    ' 🔥 2) DataGridView’i bu Angebot’a göre doldur
    '    LoadProductsBasedOnAngebotNr(angebotNr)

    '    ' 🔥 3) Tüm checkbox’ları otomatik seç
    '    For Each row As DataGridViewRow In DataGridView1.Rows
    '        If Not row.IsNewRow Then
    '            row.Cells("Select").Value = True
    '        End If
    '    Next

    '    ' 🔥 4) PDF oluştur
    '    CreatePdfFromSelectedCards2(angebotNr)

    'End Sub

    'Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    '    Dim angebotNr As String = Nothing

    '    ' 🔥 1) Angebot kayıtlı değilse otomatik kaydet
    '    If cmbAngebotNr.SelectedItem Is Nothing OrElse AngebotKayitVarMi(cmbAngebotNr.SelectedItem.ToString()) = False Then

    '        isInternalUpdate = True
    '        btnSpeichern.PerformClick()
    '        isInternalUpdate = False

    '        ' 🔥 1.1) Yeni oluşturulan AngebotNr’yi DB’den çek
    '        angebotNr = GetLastCreatedAngebotNr()

    '        ' 🔥 1.2) ComboBox’a set et
    '        cmbAngebotNr.SelectedItem = angebotNr

    '    Else
    '        angebotNr = cmbAngebotNr.SelectedItem.ToString()
    '    End If

    '    ' ❗ Ürünleri yeniden yükleme — PDF’i bozuyordu
    '    'LoadProductsBasedOnAngebotNr(angebotNr)

    '    ' 🔥 4) Tüm checkbox’ları otomatik seç
    '    For Each row As DataGridViewRow In DataGridView1.Rows
    '        If Not row.IsNewRow Then
    '            row.Cells("Select").Value = True
    '        End If
    '    Next

    '    ' 🔥 5) PDF oluştur
    '    CreatePdfFromSelectedCards2()

    'End Sub
    Private Function GetLastCreatedAngebotNr() As String
        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("
            SELECT TOP 1 AngebotNr 
            FROM AngebotListe 
            ORDER BY AngebotID DESC", con)

            Dim result = cmd.ExecuteScalar()

            If result IsNot Nothing Then
                Return result.ToString()
            End If
        End Using

        Return Nothing
    End Function
    Private Sub UpdatePriceFromCard(productID As Integer, newPrice As Decimal)

        ' DataGridView güncelle
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Convert.ToInt32(row.Cells("ProduktID").Value) = productID Then
                row.Cells("Preis").Value = newPrice
                Exit For
            End If
        Next

        ' SQL güncelle
        Dim angebotNr As String = GetLastCreatedAngebotNr()
        If angebotNr Is Nothing Then Exit Sub

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim cmd As New SqlCommand("UPDATE AngebotProdukte SET Preis = @Preis WHERE ProduktID = @PID AND AngebotID = (SELECT TOP 1 AngebotID FROM AngebotListe WHERE AngebotNr = @AngebotNr ORDER BY AngebotID DESC)", con)

            cmd.Parameters.AddWithValue("@Preis", newPrice)
            cmd.Parameters.AddWithValue("@PID", productID)
            cmd.Parameters.AddWithValue("@AngebotNr", angebotNr)

            cmd.ExecuteNonQuery()
        End Using

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim angebotNr As String = txtAngebotNr.Text.Trim()
        Dim angebotID As Integer

        If angebotNr = "" Then
            MessageBox.Show("AngebotNummer bulunamadı!")
            Exit Sub
        End If

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' ⭐ Önce bu AngebotNr var mı kontrol et
            Dim cmdCheck As New SqlCommand("SELECT TOP 1 AngebotID FROM AngebotListe WHERE AngebotNr=@nr ORDER BY AngebotID DESC", con)
            cmdCheck.Parameters.AddWithValue("@nr", angebotNr)
            Dim existing = cmdCheck.ExecuteScalar()

            If existing IsNot Nothing AndAlso Not IsDBNull(existing) Then
                angebotID = Convert.ToInt32(existing)
                Dim cmdDelOld As New SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con)
                cmdDelOld.Parameters.AddWithValue("@aid", angebotID)
                cmdDelOld.ExecuteNonQuery()
            Else
                Dim cmdAngebot As New SqlCommand("INSERT INTO AngebotListe (Datum, Beschreibung, PdfPfad, AngebotNr) VALUES (@Datum,@Beschreibung,@PdfPfad,@AngebotNr); SELECT SCOPE_IDENTITY();", con)
                cmdAngebot.Parameters.AddWithValue("@Datum", DateTime.Now)
                cmdAngebot.Parameters.AddWithValue("@Beschreibung", "Açıklama burada")
                cmdAngebot.Parameters.AddWithValue("@PdfPfad", DBNull.Value)
                cmdAngebot.Parameters.AddWithValue("@AngebotNr", angebotNr)
                angebotID = Convert.ToInt32(cmdAngebot.ExecuteScalar())
            End If

            For Each ctrl As Control In FlowLayoutPanel1.Controls
                If TypeOf ctrl Is ProductCard Then

                    Dim card As ProductCard = CType(ctrl, ProductCard)

                    Dim cmdInsert As New SqlCommand("
                    INSERT INTO AngebotProdukte
                    (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
                    VALUES (@AngebotID,@ProduktID,@ProduktName,@Beschreibung,@Preis,@Bild)", con)

                    cmdInsert.Parameters.AddWithValue("@AngebotID", angebotID)
                    cmdInsert.Parameters.AddWithValue("@ProduktID", card.ProductID)
                    cmdInsert.Parameters.AddWithValue("@ProduktName", card.ProduktName)
                    cmdInsert.Parameters.AddWithValue("@Beschreibung", card.Beschreibung)
                    cmdInsert.Parameters.AddWithValue("@Preis", card.Preis)
                    cmdInsert.Parameters.Add("@Bild", SqlDbType.VarBinary).Value = If(CType(card.BildBytes, Object), DBNull.Value)

                    cmdInsert.ExecuteNonQuery()
                End If
            Next

        End Using

        LoadAngebotCombo()
        cmbAngebotNr.SelectedItem = angebotNr

        LoadProductsBasedOnAngebotNr(angebotNr)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                row.Cells("Select").Value = True
            End If
        Next

        CreatePdfFromSelectedCards2(angebotNr)

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        If cmbAngebotNr.SelectedItem Is Nothing Then
            MessageBox.Show("Bitte ein Angebot auswählen.")
            Exit Sub
        End If

        Dim angebotNr As String = cmbAngebotNr.Text
        Dim angebotID As Integer

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' Seçilen AngebotID al
            Dim cmdID As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdID.Parameters.AddWithValue("@nr", angebotNr)
            Dim result = cmdID.ExecuteScalar()
            If result Is Nothing Then
                MessageBox.Show("Angebot nicht gefunden")
                Exit Sub
            End If

            angebotID = Convert.ToInt32(result)

            ' FlowLayoutPanel içindeki tüm ProductCard’ları güncelle
            For Each card As ProductCard In FlowLayoutPanel1.Controls.OfType(Of ProductCard)
                If Not card.IsEdited Then Continue For ' sadece düzenlenenleri al

                Dim neuerPreis As Decimal
                If Decimal.TryParse(card.txtPreisEdit.Text, neuerPreis) Then
                    card.Preis = neuerPreis
                Else
                    Continue For
                End If

                Dim cmdUpdate As New SqlCommand("
        UPDATE AngebotProdukte
        SET Preis=@preis
        WHERE AngebotID=@aid AND ProduktID=@pid", con)

                cmdUpdate.Parameters.AddWithValue("@preis", card.Preis)
                cmdUpdate.Parameters.AddWithValue("@aid", angebotID)
                cmdUpdate.Parameters.AddWithValue("@pid", card.ProductID)
                cmdUpdate.ExecuteNonQuery()

                card.IsEdited = False ' iş tamamlandı, flag sıfırla
            Next
        End Using

        MessageBox.Show("Preise erfolgreich aktualisiert.")

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        If cmbAngebotNr.SelectedItem Is Nothing Then
            MessageBox.Show("Bitte ein Angebot auswählen.")
            Exit Sub
        End If

        Dim angebotNr As String = cmbAngebotNr.Text
        Dim angebotID As Integer

        ' DB bağlantısı
        Using con As New SqlConnection(connectionString)
            con.Open()

            ' Seçilen AngebotID al
            Dim cmdID As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdID.Parameters.AddWithValue("@nr", angebotNr)
            Dim result = cmdID.ExecuteScalar()
            If result Is Nothing Then
                MessageBox.Show("Angebot nicht gefunden")
                Exit Sub
            End If
            angebotID = Convert.ToInt32(result)

            ' Sadece düzenlenen ProductCard’ları güncelle
            For Each card As ProductCard In FlowLayoutPanel1.Controls.OfType(Of ProductCard)
                If Not card.IsEdited Then Continue For

                Dim neuerPreis As Decimal
                If Decimal.TryParse(card.txtPreisEdit.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, neuerPreis) Then
                    card.Preis = neuerPreis
                Else
                    Continue For
                End If

                ' DB güncelle
                Dim cmdUpdate As New SqlCommand("
                UPDATE AngebotProdukte
                SET Preis=@preis
                WHERE AngebotID=@aid AND ProduktID=@pid", con)
                cmdUpdate.Parameters.AddWithValue("@preis", card.Preis)
                cmdUpdate.Parameters.AddWithValue("@aid", angebotID)
                cmdUpdate.Parameters.AddWithValue("@pid", card.ProductID)

                Dim rowsAffected = cmdUpdate.ExecuteNonQuery()
                If rowsAffected = 0 Then
                    MessageBox.Show("Güncellenmedi: " & card.ProduktName)
                End If

                ' Flag sıfırla
                card.IsEdited = False
            Next
        End Using

        MessageBox.Show("Sadece düzenlenen ürünler güncellendi.")

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Form2.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click


        ' 1) Angebot yoksa kaydet
        If cmbAngebotNr.SelectedItem Is Nothing OrElse
       Not AngebotKayitVarMi(cmbAngebotNr.SelectedItem.ToString()) Then

            btnSpeichern.PerformClick()
        End If

        ' 2) AngebotNr al
        Dim angebotNr As String = txtAngebotNr.Text.Trim()

        If angebotNr = "" Then
            MessageBox.Show("AngebotNr boş!")
            Exit Sub
        End If

        ' 3) FlowLayoutPanel'den ürünleri al (🔥 DOĞRU YER BURASI)
        Dim productList As New List(Of ProductCard)

        For Each ctrl As Control In FlowLayoutPanel1.Controls
            Dim card As ProductCard = TryCast(ctrl, ProductCard)
            If card Is Nothing Then Continue For

            ' boş ürünleri alma
            If card.ProductID Is Nothing Then Continue For

            productList.Add(card)
        Next

        If productList.Count = 0 Then
            MessageBox.Show("Hiç ürün yok!")
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' 4) AngebotID al
                Dim angebotId As Integer
                Using cmdId As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
                    cmdId.Parameters.AddWithValue("@nr", angebotNr)

                    Dim result = cmdId.ExecuteScalar()
                    If result Is Nothing Then
                        MessageBox.Show("AngebotID bulunamadı!")
                        Exit Sub
                    End If

                    angebotId = CInt(result)
                End Using

                ' 5) eski ürünleri sil
                Using cmdDelete As New SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con)
                    cmdDelete.Parameters.AddWithValue("@aid", angebotId)
                    cmdDelete.ExecuteNonQuery()
                End Using

                ' 6) INSERT
                Using cmdInsert As New SqlCommand("
                INSERT INTO AngebotProdukte
                (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
                VALUES (@aid, @pid, @pname, @desc, @preis, @bild)", con)

                    cmdInsert.Parameters.Add("@aid", SqlDbType.Int)
                    cmdInsert.Parameters.Add("@pid", SqlDbType.Int)
                    cmdInsert.Parameters.Add("@pname", SqlDbType.NVarChar, 200)
                    cmdInsert.Parameters.Add("@desc", SqlDbType.NVarChar, 500)
                    cmdInsert.Parameters.Add("@preis", SqlDbType.Decimal).Precision = 18
                    cmdInsert.Parameters("@preis").Scale = 2
                    cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary)

                    For Each card In productList

                        cmdInsert.Parameters("@aid").Value = angebotId
                        cmdInsert.Parameters("@pid").Value = card.ProductID
                        cmdInsert.Parameters("@pname").Value = If(card.ProduktName, "")
                        cmdInsert.Parameters("@desc").Value = If(card.Beschreibung, "")
                        cmdInsert.Parameters("@preis").Value = card.Preis
                        cmdInsert.Parameters("@bild").Value = If(CType(card.BildBytes, Object), DBNull.Value)

                        Dim inserted As Integer = cmdInsert.ExecuteNonQuery()

                        If inserted = 0 Then
                            MessageBox.Show("Insert başarısız!")
                        End If
                    Next
                End Using
            End Using

            MessageBox.Show("✔ Ürünler kaydedildi")

            ' 7) PDF
            CreatePdfFromSelectedCards2(angebotNr)

        Catch ex As Exception
            MessageBox.Show("HATA: " & ex.Message)
        End Try

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Dim angebotNr As String = txtAngebotNr.Text.Trim()
        If angebotNr = "" Then
            MessageBox.Show("AngebotNummer bulunamadı!")
            Exit Sub
        End If

        ' Eğer teklif daha önce kaydedilmemişse önce kaydet
        If Not AngebotKayitVarMi(angebotNr) Then
            btnSpeichern.PerformClick()
        End If

        ' FlowLayoutPanel içindeki ProductCard’ları topla
        Dim productList As New List(Of ProductCard)

        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If TypeOf ctrl Is ProductCard Then

                Dim card As ProductCard = CType(ctrl, ProductCard)

                ' ProductID Label1'den okunacaksa:
                Dim pid As Integer = 0
                If Integer.TryParse(card.Label1.Text, pid) Then
                    card.ProductID = pid
                End If

                ' ProductID 0 ise kaydetme
                If card.ProductID = 0 Then Continue For

                productList.Add(card)
            End If
        Next

        If productList.Count = 0 Then
            MessageBox.Show("Kaydedilecek ürün bulunamadı.")
            Exit Sub
        End If

        ' SQL Güncelleme
        Using con As New SqlConnection(connectionString)
            con.Open()

            ' AngebotID al
            Dim cmdId As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdId.Parameters.AddWithValue("@nr", angebotNr)
            Dim angebotId As Integer = CInt(cmdId.ExecuteScalar())

            ' Eski ürünleri sil
            Dim cmdDelete As New SqlCommand("DELETE FROM AngebotProdukte WHERE AngebotID=@aid", con)
            cmdDelete.Parameters.AddWithValue("@aid", angebotId)
            cmdDelete.ExecuteNonQuery()

            ' Yeni ürünleri ekle
            Dim cmdInsert As New SqlCommand("
            INSERT INTO AngebotProdukte
            (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
            VALUES (@aid, @pid, @pname, @desc, @preis, @bild)
        ", con)

            cmdInsert.Parameters.Add("@aid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pname", SqlDbType.NVarChar, 200)
            cmdInsert.Parameters.Add("@desc", SqlDbType.NVarChar, 500)
            cmdInsert.Parameters.Add("@preis", SqlDbType.Decimal)
            cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary)

            For Each card In productList
                cmdInsert.Parameters("@aid").Value = angebotId
                cmdInsert.Parameters("@pid").Value = card.ProductID
                cmdInsert.Parameters("@pname").Value = card.ProduktName
                cmdInsert.Parameters("@desc").Value = card.Beschreibung
                cmdInsert.Parameters("@preis").Value = card.Preis
                cmdInsert.Parameters("@bild").Value = If(CType(card.BildBytes, Object), DBNull.Value)
                cmdInsert.ExecuteNonQuery()
            Next
        End Using

        MessageBox.Show("Ürünler başarıyla kaydedildi.")

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        Dim angebotNr As String = txtAngebotNr.Text.Trim()
        If angebotNr = "" Then
            MessageBox.Show("AngebotNummer bulunamadı!")
            Exit Sub
        End If

        ' Eğer teklif daha önce kaydedilmemişse önce kaydet
        If Not AngebotKayitVarMi(angebotNr) Then
            btnSpeichern.PerformClick()
        End If

        ' FlowLayoutPanel içindeki ProductCard’ları topla
        Dim productList As New List(Of ProductCard)

        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If TypeOf ctrl Is ProductCard Then

                Dim card As ProductCard = CType(ctrl, ProductCard)

                ' Label1 → ProductID içeriyor
                Dim pid As Integer = 0
                Integer.TryParse(card.Label1.Text, pid)
                card.ProductID = pid

                If card.ProductID = 0 Then Continue For

                productList.Add(card)
            End If
        Next

        If productList.Count = 0 Then
            MessageBox.Show("Güncellenecek ürün bulunamadı.")
            Exit Sub
        End If

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' AngebotID al
            Dim cmdId As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdId.Parameters.AddWithValue("@nr", angebotNr)
            Dim angebotId As Integer = CInt(cmdId.ExecuteScalar())

            ' 1) Mevcut ürünleri çek
            Dim existing As New DataTable()
            Dim cmdGet As New SqlCommand("SELECT ProduktID FROM AngebotProdukte WHERE AngebotID=@aid", con)
            cmdGet.Parameters.AddWithValue("@aid", angebotId)
            existing.Load(cmdGet.ExecuteReader())

            ' 2) UPDATE komutu
            Dim cmdUpdate As New SqlCommand("UPDATE AngebotProdukte SET ProduktName=@pname, Beschreibung=@desc, Preis=@preis, Bild=@bild WHERE AngebotID=@aid AND ProduktID=@pid", con)

            cmdUpdate.Parameters.Add("@aid", SqlDbType.Int)
            cmdUpdate.Parameters.Add("@pid", SqlDbType.Int)
            cmdUpdate.Parameters.Add("@pname", SqlDbType.NVarChar, 200)
            cmdUpdate.Parameters.Add("@desc", SqlDbType.NVarChar, 500)
            cmdUpdate.Parameters.Add("@preis", SqlDbType.Decimal)
            cmdUpdate.Parameters.Add("@bild", SqlDbType.VarBinary)

            ' 3) INSERT komutu
            Dim cmdInsert As New SqlCommand("
            INSERT INTO AngebotProdukte
            (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
            VALUES (@aid, @pid, @pname, @desc, @preis, @bild)
        ", con)

            cmdInsert.Parameters.Add("@aid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pname", SqlDbType.NVarChar, 200)
            cmdInsert.Parameters.Add("@desc", SqlDbType.NVarChar, 500)
            cmdInsert.Parameters.Add("@preis", SqlDbType.Decimal)
            cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary)

            ' 4) DELETE komutu
            Dim cmdDelete As New SqlCommand("
            DELETE FROM AngebotProdukte 
            WHERE AngebotID=@aid AND ProduktID=@pid
        ", con)

            cmdDelete.Parameters.Add("@aid", SqlDbType.Int)
            cmdDelete.Parameters.Add("@pid", SqlDbType.Int)

            ' 5) UPDATE / INSERT işlemleri
            For Each card In productList

                Dim exists As Boolean =
                    existing.AsEnumerable().Any(Function(r) r.Field(Of Integer)("ProduktID") = card.ProductID)

                If exists Then
                    ' UPDATE
                    cmdUpdate.Parameters("@aid").Value = angebotId
                    cmdUpdate.Parameters("@pid").Value = card.ProductID
                    cmdUpdate.Parameters("@pname").Value = card.ProduktName
                    cmdUpdate.Parameters("@desc").Value = card.Beschreibung
                    cmdUpdate.Parameters("@preis").Value = card.Preis
                    cmdUpdate.Parameters("@bild").Value = If(CType(card.BildBytes, Object), DBNull.Value)
                    cmdUpdate.ExecuteNonQuery()
                Else
                    ' INSERT
                    cmdInsert.Parameters("@aid").Value = angebotId
                    cmdInsert.Parameters("@pid").Value = card.ProductID
                    cmdInsert.Parameters("@pname").Value = card.ProduktName
                    cmdInsert.Parameters("@desc").Value = card.Beschreibung
                    cmdInsert.Parameters("@preis").Value = card.Preis
                    cmdInsert.Parameters("@bild").Value = If(CType(card.BildBytes, Object), DBNull.Value)
                    cmdInsert.ExecuteNonQuery()
                End If
            Next

            ' 6) Artık olmayan ürünleri sil
            For Each row As DataRow In existing.Rows
                Dim pid As Integer = row("ProduktID")
                Dim stillExists As Boolean =
                    productList.Any(Function(c) c.ProductID = pid)

                If Not stillExists Then
                    cmdDelete.Parameters("@aid").Value = angebotId
                    cmdDelete.Parameters("@pid").Value = pid
                    cmdDelete.ExecuteNonQuery()
                End If
            Next

        End Using
        ' 🔥 Fiyat güncellemeden sonra DataGridView ve kartları yeniden yükle
        LoadProductsBasedOnAngebotNr(angebotNr)

        MessageBox.Show("Preise erfolgreich aktualisiert.")



    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        ' eger datagrid check box secili degilse secsindiye  
        ' ✔ DataGridView’de checkbox var mı kontrol et
        If DataGridView1.Columns.Contains("Select") Then

            Dim anyChecked As Boolean = False

            ' ✔ Her satırı kontrol et
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Convert.ToBoolean(row.Cells("Select").Value) = True Then
                    anyChecked = True
                    Exit For
                End If
            Next

            ' ✔ Eğer hiçbiri seçili değilse → hepsini otomatik seç
            If Not anyChecked Then
                For Each row As DataGridViewRow In DataGridView1.Rows
                    row.Cells("Select").Value = True
                Next
            End If

        End If
        '===============================================================================

        ' ✔ Seçilen teklif numarası TextBox yerine ComboBox’tan alınmalı
        Dim angebotNr As String = cmbAngebotNr.Text.Trim()
        If angebotNr = "" Then
            MessageBox.Show("Bitte ein Angebot auswählen.")
            Exit Sub
        End If

        Dim angebotId As Integer = -1

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' 1) AngebotListe tablosunda bu numara var mı?
            Dim cmdCheck As New SqlCommand("
            SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr
        ", con)
            cmdCheck.Parameters.AddWithValue("@nr", angebotNr)

            Dim result = cmdCheck.ExecuteScalar()

            If result Is Nothing OrElse IsDBNull(result) Then
                ' ✔ Kayıt yoksa → aynı numarayla yeni kayıt oluştur
                Dim cmdInsertAngebot As New SqlCommand("
                INSERT INTO AngebotListe (AngebotNr, AngebotDatum)
                VALUES (@nr, GETDATE());
                SELECT SCOPE_IDENTITY();
            ", con)
                cmdInsertAngebot.Parameters.AddWithValue("@nr", angebotNr)

                angebotId = Convert.ToInt32(cmdInsertAngebot.ExecuteScalar())
            Else
                angebotId = Convert.ToInt32(result)
            End If

            ' 2) FlowLayoutPanel içindeki ProductCard’ları topla
            Dim productList As New List(Of ProductCard)

            For Each ctrl As Control In FlowLayoutPanel1.Controls
                If TypeOf ctrl Is ProductCard Then

                    Dim card As ProductCard = CType(ctrl, ProductCard)

                    Dim pid As Integer = 0
                    Integer.TryParse(card.Label1.Text, pid)
                    card.ProductID = pid

                    If card.ProductID > 0 Then
                        productList.Add(card)
                    End If
                End If
            Next

            If productList.Count = 0 Then
                MessageBox.Show("Keine Produkte ausgewählt.")
                Exit Sub
            End If

            ' 3) Eski ürünleri sil
            Dim cmdDelete As New SqlCommand("
            DELETE FROM AngebotProdukte WHERE AngebotID=@aid
        ", con)
            cmdDelete.Parameters.AddWithValue("@aid", angebotId)
            cmdDelete.ExecuteNonQuery()

            ' 4) Yeni ürünleri ekle
            Dim cmdInsert As New SqlCommand("
            INSERT INTO AngebotProdukte
            (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
            VALUES (@aid, @pid, @pname, @desc, @preis, @bild)
        ", con)

            cmdInsert.Parameters.Add("@aid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pid", SqlDbType.Int)
            cmdInsert.Parameters.Add("@pname", SqlDbType.NVarChar, 200)
            cmdInsert.Parameters.Add("@desc", SqlDbType.NVarChar, 500)
            cmdInsert.Parameters.Add("@preis", SqlDbType.Decimal)
            cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary)

            For Each card In productList
                cmdInsert.Parameters("@aid").Value = angebotId
                cmdInsert.Parameters("@pid").Value = card.ProductID
                cmdInsert.Parameters("@pname").Value = card.ProduktName
                cmdInsert.Parameters("@desc").Value = card.Beschreibung
                cmdInsert.Parameters("@preis").Value = card.Preis
                cmdInsert.Parameters("@bild").Value = If(CType(card.BildBytes, Object), DBNull.Value)
                cmdInsert.ExecuteNonQuery()
            Next

        End Using

        ' 5) PDF oluştur
        CreatePdfFromSelectedCards2(angebotNr)

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click

        If cmbAngebotNr.SelectedItem Is Nothing Then
            MessageBox.Show("Bitte ein Angebot auswählen.")
            Exit Sub
        End If

        Dim angebotNr As String = cmbAngebotNr.Text
        Dim angebotID As Integer

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' AngebotID al
            Dim cmdID As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdID.Parameters.AddWithValue("@nr", angebotNr)
            Dim result = cmdID.ExecuteScalar()

            If result Is Nothing Then
                MessageBox.Show("Angebot nicht gefunden.")
                Exit Sub
            End If

            angebotID = Convert.ToInt32(result)

            ' ⭐ 0) SİLİNEN KARTLARI DB’DEN SİL (FlowLayoutPanel DEĞİL → DeletedCards LİSTESİ)
            For Each deletedCard As ProductCard In DeletedCards

                Dim cmdDelete As New SqlCommand("
                DELETE FROM AngebotProdukte
                WHERE AngebotID=@aid AND ProduktID=@pid
            ", con)

                cmdDelete.Parameters.AddWithValue("@aid", angebotID)
                cmdDelete.Parameters.AddWithValue("@pid", deletedCard.OriginalProductID)

                cmdDelete.ExecuteNonQuery()
            Next

            ' ⭐ Silinen kart listesini temizle
            DeletedCards.Clear()


            ' ⭐ 1) KALAN KARTLARI INSERT/UPDATE ET
            For Each card As ProductCard In FlowLayoutPanel1.Controls.OfType(Of ProductCard)

                ' Fiyat parse
                Dim neuerPreis As Decimal
                Dim rawText As String = card.txtPreisEdit.Text.Trim()

                If Decimal.TryParse(rawText,
                                Globalization.NumberStyles.Number,
                                Globalization.CultureInfo.GetCultureInfo("de-DE"),
                                neuerPreis) Then

                    card.Preis = neuerPreis
                Else
                    MessageBox.Show("Ungültiger Preis: " & rawText)
                    Continue For
                End If


                ' ⭐ YENİ ÜRÜN → INSERT
                If card.OriginalProductID = 0 Then

                    Dim cmdInsert As New SqlCommand("
                    INSERT INTO AngebotProdukte
                    (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
                    VALUES (@aid, @pid, @pname, @desc, @preis, @bild)
                ", con)

                    cmdInsert.Parameters.AddWithValue("@aid", angebotID)
                    cmdInsert.Parameters.AddWithValue("@pid", card.ProductID)
                    cmdInsert.Parameters.AddWithValue("@pname", card.ProduktName)
                    cmdInsert.Parameters.AddWithValue("@desc", card.Beschreibung)
                    cmdInsert.Parameters.AddWithValue("@preis", card.Preis)
                    cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary).Value = If(CType(card.BildBytes, Object), DBNull.Value)

                    cmdInsert.ExecuteNonQuery()

                    card.OriginalProductID = card.ProductID
                    card.IsEdited = False

                    Continue For
                End If


                ' ⭐ ESKİ ÜRÜN → UPDATE
                If card.IsEdited Then

                    Dim cmdUpdate As New SqlCommand("UPDATE AngebotProdukte SET ProduktID=@newPid, ProduktName=@pname, Beschreibung=@desc, Preis=@preis, Bild=@bild WHERE AngebotID=@aid AND ProduktID=@oldPid", con)

                    cmdUpdate.Parameters.AddWithValue("@newPid", card.ProductID)
                    cmdUpdate.Parameters.AddWithValue("@oldPid", card.OriginalProductID)
                    cmdUpdate.Parameters.AddWithValue("@pname", card.ProduktName)
                    cmdUpdate.Parameters.AddWithValue("@desc", card.Beschreibung)
                    cmdUpdate.Parameters.AddWithValue("@preis", card.Preis)
                    cmdUpdate.Parameters.Add("@bild", SqlDbType.VarBinary).Value = If(CType(card.BildBytes, Object), DBNull.Value)
                    cmdUpdate.Parameters.AddWithValue("@aid", angebotID)

                    cmdUpdate.ExecuteNonQuery()

                    card.OriginalProductID = card.ProductID
                    card.IsEdited = False
                End If

            Next

        End Using

        LoadProductsBasedOnAngebotNr(angebotNr)
        MessageBox.Show("Produkt erfolgreich aktualisiert.")
    End Sub



    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        ResetAngebotForm()
    End Sub

    Private Sub ResetAngebotForm()

        ' Yeni numara üret
        LoadAngebotNumbers()

        ' Tarihi bugüne çek
        dtpDatum.Value = DateTime.Now

        ' Ürün kartlarını temizle
        FlowLayoutPanel1.Controls.Clear()

        ' DataGrid temizle (DOĞRU YÖNTEM)
        DataGridView1.DataSource = Nothing

        ' Form üzerindeki alanları sıfırla
        Label1.Text = ""

        ' Ürün sayısını güncelle
        UpdateProduktCount()

    End Sub

    Public Sub UpdateProduktCount()
        Label3.Text = "Produkte: " & FlowLayoutPanel1.Controls.Count.ToString()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

        If cmbAngebotNr.SelectedItem Is Nothing Then
            MessageBox.Show("Bitte ein Angebot auswählen.")
            Exit Sub
        End If

        Dim angebotNr As String = cmbAngebotNr.Text
        Dim angebotID As Integer

        Using con As New SqlConnection(connectionString)
            con.Open()

            ' AngebotID al
            Dim cmdID As New SqlCommand("SELECT AngebotID FROM AngebotListe WHERE AngebotNr=@nr", con)
            cmdID.Parameters.AddWithValue("@nr", angebotNr)
            Dim result = cmdID.ExecuteScalar()

            If result Is Nothing Then
                MessageBox.Show("Angebot nicht gefunden.")
                Exit Sub
            End If

            angebotID = Convert.ToInt32(result)

            ' FlowLayoutPanel içindeki tüm ProductCard’ları kontrol et
            For Each card As ProductCard In FlowLayoutPanel1.Controls.OfType(Of ProductCard)

                ' ⭐ Fiyatı doğru kültürle parse et
                Dim neuerPreis As Decimal
                Dim rawText As String = card.txtPreisEdit.Text.Trim()

                If Decimal.TryParse(rawText,
                                    Globalization.NumberStyles.Number,
                                    Globalization.CultureInfo.GetCultureInfo("de-DE"),
                                    neuerPreis) Then

                    card.Preis = neuerPreis
                Else
                    MessageBox.Show("Ungültiger Preis: " & rawText)
                    Continue For
                End If

                ' ⭐ 1) YENİ ÜRÜN MÜ? (INSERT)
                If card.OriginalProductID = 0 Then

                    Dim cmdInsert As New SqlCommand("
                    INSERT INTO AngebotProdukte
                    (AngebotID, ProduktID, ProduktName, Beschreibung, Preis, Bild)
                    VALUES (@aid, @pid, @pname, @desc, @preis, @bild)
                ", con)

                    cmdInsert.Parameters.AddWithValue("@aid", angebotID)
                    cmdInsert.Parameters.AddWithValue("@pid", card.ProductID)
                    cmdInsert.Parameters.AddWithValue("@pname", card.ProduktName)
                    cmdInsert.Parameters.AddWithValue("@desc", card.Beschreibung)
                    cmdInsert.Parameters.AddWithValue("@preis", card.Preis)
                    cmdInsert.Parameters.Add("@bild", SqlDbType.VarBinary).Value = If(CType(card.BildBytes, Object), DBNull.Value)

                    cmdInsert.ExecuteNonQuery()

                    card.OriginalProductID = card.ProductID
                    card.IsEdited = False

                    Continue For
                End If

                ' ⭐ 2) ESKİ ÜRÜN MÜ? (UPDATE)
                If card.IsEdited Then

                    Dim cmdUpdate As New SqlCommand("UPDATE AngebotProdukte SET ProduktID=@newPid, ProduktName=@pname, Beschreibung=@desc, Preis=@preis, Bild=@bild WHERE AngebotID=@aid AND ProduktID=@oldPid", con)

                    cmdUpdate.Parameters.AddWithValue("@newPid", card.ProductID)
                    cmdUpdate.Parameters.AddWithValue("@oldPid", card.OriginalProductID)
                    cmdUpdate.Parameters.AddWithValue("@pname", card.ProduktName)
                    cmdUpdate.Parameters.AddWithValue("@desc", card.Beschreibung)
                    cmdUpdate.Parameters.AddWithValue("@preis", card.Preis)
                    cmdUpdate.Parameters.Add("@bild", SqlDbType.VarBinary).Value = If(CType(card.BildBytes, Object), DBNull.Value)
                    cmdUpdate.Parameters.AddWithValue("@aid", angebotID)

                    cmdUpdate.ExecuteNonQuery()

                    card.OriginalProductID = card.ProductID
                    card.IsEdited = False
                End If

            Next

        End Using

        LoadProductsBasedOnAngebotNr(angebotNr)
        MessageBox.Show("Produkt erfolgreich aktualisiert.")

    End Sub























    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

        Dim folder As String = "C:\AngebotListe\WhatsapaYollanilan\"
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)

        Dim angebotNr As String = txtAngebotNr.Text.Trim()
        If String.IsNullOrEmpty(angebotNr) Then angebotNr = "ANG-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")

        '-----------------------------------------
        ' 1) Gönderilecek kartları topla
        '    Önce DataGridView'de "Select" işaretli olanlar,
        '    yoksa FlowLayoutPanel'deki tüm kartlar.
        '-----------------------------------------
        Dim cardsToSend As New List(Of ProductCard)

        Dim selectedIDs As New List(Of Integer)
        If DataGridView1.Columns.Contains("Select") Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.IsNewRow Then Continue For
                Dim selVal = row.Cells("Select").Value
                If selVal IsNot Nothing AndAlso CBool(selVal) = True Then
                    If row.DataGridView.Columns.Contains("ProduktID") AndAlso row.Cells("ProduktID").Value IsNot Nothing Then
                        selectedIDs.Add(Convert.ToInt32(row.Cells("ProduktID").Value))
                    End If
                End If
            Next
        End If

        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If TypeOf ctrl Is ProductCard Then
                Dim c As ProductCard = CType(ctrl, ProductCard)
                If selectedIDs.Count = 0 OrElse selectedIDs.Contains(CInt(c.ProductID)) Then
                    cardsToSend.Add(c)
                End If
            End If
        Next

        If cardsToSend.Count = 0 Then
            MsgBox("Gönderilecek ürün bulunamadı.")
            Exit Sub
        End If

        '-----------------------------------------
        ' 2) Her kart için ayrı PNG resim üret + diske kaydet
        '-----------------------------------------
        Dim imagePaths As New List(Of String)
        Dim idx As Integer = 0
        For Each card In cardsToSend
            idx += 1
            Dim outPath As String = Path.Combine(folder, $"{angebotNr}_{idx:000}.png")
            Try
                Using bmp As Bitmap = card.GetShareImage()
                    bmp.Save(outPath, Imaging.ImageFormat.Png)
                End Using
                imagePaths.Add(outPath)
            Catch ex As Exception
                MsgBox("Bild für '" & card.ProduktName & "' konnte nicht erzeugt werden: " & ex.Message)
            End Try
        Next

        If imagePaths.Count = 0 Then
            MsgBox("Hiçbir resim üretilemedi.")
            Exit Sub
        End If

        '-----------------------------------------
        ' 3) WhatsApp Senden Helper — kullanıcı manuel chat seçer, biz yapıştırırız
        '    Birden fazla chat'e art arda gönderebilir.
        '-----------------------------------------
        ZeigeWhatsAppHelper(imagePaths, folder)

    End Sub

    ' ═══════════════════════════════════════════════════════════════════
    ' WhatsApp Senden Helper — TopMost mini pencere
    ' Kullanıcı: WhatsApp Web'de mouse ile chat seçer → "Senden" butonuna basar
    ' App: form'u gizler, son aktif pencereye (WhatsApp) resimleri yapıştırır
    ' ═══════════════════════════════════════════════════════════════════
    Private Sub ZeigeWhatsAppHelper(imagePaths As List(Of String), folder As String)
        Dim helper As New Form()
        helper.Text = "WhatsApp Senden Helper"
        helper.Size = New Size(420, 320)
        helper.FormBorderStyle = FormBorderStyle.FixedToolWindow
        helper.StartPosition = FormStartPosition.Manual
        helper.Location = New Point(Screen.PrimaryScreen.WorkingArea.Right - 440, 60)
        helper.TopMost = True
        helper.BackColor = Color.White

        Dim lblTitle As New Label()
        lblTitle.Text = "📤 WhatsApp Senden"
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(37, 211, 102)
        lblTitle.Location = New Point(15, 12)
        lblTitle.AutoSize = True
        helper.Controls.Add(lblTitle)

        Dim lblInfo As New Label()
        lblInfo.Text = $"{imagePaths.Count} Bild(er) bereit." & vbCrLf & vbCrLf &
                       "ANLEITUNG:" & vbCrLf &
                       "1) Im WhatsApp Web → Chat anklicken" & vbCrLf &
                       "2) Hier auf '📤 Senden' klicken" & vbCrLf &
                       "3) Beliebig oft wiederholbar (mehrere Chats)"
        lblInfo.Font = New System.Drawing.Font("Segoe UI", 9)
        lblInfo.Location = New Point(15, 45)
        lblInfo.Size = New Size(390, 100)
        helper.Controls.Add(lblInfo)

        Dim lblStatus As New Label()
        lblStatus.Text = "Bereit. Wählen Sie einen Chat aus."
        lblStatus.Font = New System.Drawing.Font("Segoe UI", 9, FontStyle.Italic)
        lblStatus.ForeColor = Color.Gray
        lblStatus.Location = New Point(15, 150)
        lblStatus.Size = New Size(390, 20)
        helper.Controls.Add(lblStatus)

        Dim btnSenden As New Button()
        btnSenden.Text = "📤 An ausgewählten Chat senden"
        btnSenden.Location = New Point(15, 180)
        btnSenden.Size = New Size(390, 50)
        btnSenden.BackColor = Color.FromArgb(37, 211, 102)
        btnSenden.ForeColor = Color.White
        btnSenden.Font = New System.Drawing.Font("Segoe UI", 11, FontStyle.Bold)
        btnSenden.FlatStyle = FlatStyle.Flat
        btnSenden.FlatAppearance.BorderSize = 0
        helper.Controls.Add(btnSenden)

        Dim btnFertig As New Button()
        btnFertig.Text = "✗ Fertig / Schließen"
        btnFertig.Location = New Point(15, 240)
        btnFertig.Size = New Size(390, 35)
        btnFertig.BackColor = Color.LightGray
        btnFertig.Font = New System.Drawing.Font("Segoe UI", 10)
        btnFertig.FlatStyle = FlatStyle.Flat
        helper.Controls.Add(btnFertig)

        Dim totalSent As Integer = 0
        Dim totalRuns As Integer = 0

        AddHandler btnSenden.Click,
            Sub(s, ev)
                btnSenden.Enabled = False
                btnFertig.Enabled = False
                lblStatus.Text = "Wird gesendet..."
                lblStatus.ForeColor = Color.Blue
                helper.Refresh()

                ' Helper'ı gizle → fokus otomatik son aktif pencereye (WhatsApp) geçer
                helper.Hide()
                Application.DoEvents()
                Threading.Thread.Sleep(600)

                Dim sent As Integer = 0
                For Each path In imagePaths
                    Try
                        Using bmp As New Bitmap(path)
                            Clipboard.SetImage(bmp)
                        End Using
                        Threading.Thread.Sleep(400)

                        SendKeys.SendWait("^v")
                        Threading.Thread.Sleep(2500)

                        SendKeys.SendWait("{ENTER}")
                        Threading.Thread.Sleep(3000)
                        sent += 1
                    Catch
                    End Try
                Next

                totalSent += sent
                totalRuns += 1

                ' Helper'ı tekrar göster
                helper.Show()
                helper.BringToFront()
                btnSenden.Enabled = True
                btnFertig.Enabled = True
                lblStatus.Text = $"✓ {sent} / {imagePaths.Count} gesendet (Runde {totalRuns}, gesamt {totalSent})"
                lblStatus.ForeColor = Color.Green
            End Sub

        AddHandler btnFertig.Click,
            Sub(s, ev)
                helper.Close()
            End Sub

        AddHandler helper.FormClosed,
            Sub(s, ev)
                If totalRuns > 0 Then
                    MessageBox.Show(
                        $"Insgesamt {totalSent} Bild(er) in {totalRuns} Chat(s) gesendet." & vbCrLf & vbCrLf &
                        "Backup im Ordner:" & vbCrLf & folder,
                        "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Sub

        helper.Show(Me)
    End Sub

    ' ═══════════════════════════════════════════════════════════════════
    ' WhatsApp PDF Senden Helper — PDF dosyasını clipboard'a koyup yapıştırır
    ' Kullanıcı WhatsApp Web'de chat'i mouse ile seçer → "PDF Senden" butonu
    ' ═══════════════════════════════════════════════════════════════════
    Private Sub ZeigeWhatsAppPdfHelper(pdfPath As String, angebotNr As String)
        If Not File.Exists(pdfPath) Then
            MsgBox("PDF nicht gefunden: " & pdfPath)
            Return
        End If

        Dim helper As New Form()
        helper.Text = "WhatsApp PDF Senden"
        helper.Size = New Size(420, 320)
        helper.FormBorderStyle = FormBorderStyle.FixedToolWindow
        helper.StartPosition = FormStartPosition.Manual
        helper.Location = New Point(Screen.PrimaryScreen.WorkingArea.Right - 440, 60)
        helper.TopMost = True
        helper.BackColor = Color.White

        Dim lblTitle As New Label()
        lblTitle.Text = "📄 WhatsApp PDF Senden"
        lblTitle.Font = New System.Drawing.Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(37, 211, 102)
        lblTitle.Location = New Point(15, 12)
        lblTitle.AutoSize = True
        helper.Controls.Add(lblTitle)

        Dim lblInfo As New Label()
        lblInfo.Text = $"PDF: {Path.GetFileName(pdfPath)}" & vbCrLf & vbCrLf &
                       "ANLEITUNG:" & vbCrLf &
                       "1) Im WhatsApp Web → Chat anklicken" & vbCrLf &
                       "2) Hier auf '📄 PDF Senden' klicken" & vbCrLf &
                       "3) Beliebig oft wiederholbar (mehrere Chats)"
        lblInfo.Font = New System.Drawing.Font("Segoe UI", 9)
        lblInfo.Location = New Point(15, 45)
        lblInfo.Size = New Size(390, 100)
        helper.Controls.Add(lblInfo)

        Dim lblStatus As New Label()
        lblStatus.Text = "Bereit. Wählen Sie einen Chat aus."
        lblStatus.Font = New System.Drawing.Font("Segoe UI", 9, FontStyle.Italic)
        lblStatus.ForeColor = Color.Gray
        lblStatus.Location = New Point(15, 150)
        lblStatus.Size = New Size(390, 20)
        helper.Controls.Add(lblStatus)

        Dim btnSenden As New Button()
        btnSenden.Text = "📄 PDF an ausgewählten Chat senden"
        btnSenden.Location = New Point(15, 180)
        btnSenden.Size = New Size(390, 50)
        btnSenden.BackColor = Color.FromArgb(37, 211, 102)
        btnSenden.ForeColor = Color.White
        btnSenden.Font = New System.Drawing.Font("Segoe UI", 11, FontStyle.Bold)
        btnSenden.FlatStyle = FlatStyle.Flat
        btnSenden.FlatAppearance.BorderSize = 0
        helper.Controls.Add(btnSenden)

        Dim btnFertig As New Button()
        btnFertig.Text = "✗ Fertig / Schließen"
        btnFertig.Location = New Point(15, 240)
        btnFertig.Size = New Size(390, 35)
        btnFertig.BackColor = Color.LightGray
        btnFertig.Font = New System.Drawing.Font("Segoe UI", 10)
        btnFertig.FlatStyle = FlatStyle.Flat
        helper.Controls.Add(btnFertig)

        Dim totalRuns As Integer = 0

        AddHandler btnSenden.Click,
            Sub(s, ev)
                btnSenden.Enabled = False
                btnFertig.Enabled = False
                lblStatus.Text = "Wird gesendet..."
                lblStatus.ForeColor = Color.Blue
                helper.Refresh()

                Try
                    Dim files As New System.Collections.Specialized.StringCollection()
                    files.Add(pdfPath)
                    Clipboard.SetFileDropList(files)
                Catch ex As Exception
                    lblStatus.Text = "Clipboard-Fehler: " & ex.Message
                    lblStatus.ForeColor = Color.Red
                    btnSenden.Enabled = True
                    btnFertig.Enabled = True
                    Return
                End Try

                helper.Hide()
                Application.DoEvents()
                Threading.Thread.Sleep(700)

                SendKeys.SendWait("^v")
                Threading.Thread.Sleep(3000)
                SendKeys.SendWait("{ENTER}")
                Threading.Thread.Sleep(2500)

                totalRuns += 1

                helper.Show()
                helper.BringToFront()
                btnSenden.Enabled = True
                btnFertig.Enabled = True
                lblStatus.Text = $"✓ PDF gesendet (Runde {totalRuns})"
                lblStatus.ForeColor = Color.Green
            End Sub

        AddHandler btnFertig.Click,
            Sub(s, ev)
                helper.Close()
            End Sub

        AddHandler helper.FormClosed,
            Sub(s, ev)
                If totalRuns > 0 Then
                    MessageBox.Show(
                        $"PDF an {totalRuns} Chat(s) gesendet." & vbCrLf & vbCrLf &
                        "Datei:" & vbCrLf & pdfPath,
                        "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Sub

        helper.Show(Me)
    End Sub

    ' ═══════════════════════════════════════════════════════════════════
    ' Multi-Select Kontakt listesi — birden fazla kişi/grup seçilir
    ' Aynı txt dosyasını kullanır: Name|Telefon
    ' ═══════════════════════════════════════════════════════════════════
    Private Function ZeigeKontaktAuswahlMulti() As List(Of Tuple(Of String, String))
        Dim kontaktFile As String = "C:\AngebotListe\whatsapp_kontakte.txt"
        If Not Directory.Exists("C:\AngebotListe\") Then Directory.CreateDirectory("C:\AngebotListe\")
        If Not File.Exists(kontaktFile) Then
            File.WriteAllText(kontaktFile, "Test|4915734412344" & Environment.NewLine)
        End If

        Dim ladeKontakte As Func(Of List(Of Tuple(Of String, String))) =
            Function()
                Dim list As New List(Of Tuple(Of String, String))
                For Each line In File.ReadAllLines(kontaktFile)
                    If String.IsNullOrWhiteSpace(line) Then Continue For
                    Dim parts = line.Split("|"c)
                    Dim nm As String = parts(0).Trim()
                    Dim ph As String = If(parts.Length > 1, parts(1).Trim(), "")
                    list.Add(Tuple.Create(nm, ph))
                Next
                Return list
            End Function

        Dim kontakte = ladeKontakte()

        Dim f As New Form()
        f.Text = "Mehrere Kontakte auswählen"
        f.Size = New Size(480, 580)
        f.StartPosition = FormStartPosition.CenterParent
        f.FormBorderStyle = FormBorderStyle.FixedDialog
        f.MinimizeBox = False
        f.MaximizeBox = False

        Dim lblInfo As New Label()
        lblInfo.Text = "Wählen Sie mehrere Kontakte / Gruppen aus:" & vbCrLf &
                       "(WhatsApp Web muss bereits eingeloggt sein)"
        lblInfo.Dock = DockStyle.Top
        lblInfo.Height = 40
        lblInfo.Padding = New Padding(8, 8, 8, 0)
        f.Controls.Add(lblInfo)

        Dim clb As New CheckedListBox()
        clb.Dock = DockStyle.Fill
        clb.Font = New System.Drawing.Font("Segoe UI", 10)
        clb.IntegralHeight = False
        clb.CheckOnClick = True
        For Each k In kontakte
            clb.Items.Add($"{k.Item1}  ({k.Item2})", False)
        Next
        f.Controls.Add(clb)

        Dim pnlBtn As New Panel()
        pnlBtn.Dock = DockStyle.Bottom
        pnlBtn.Height = 130
        f.Controls.Add(pnlBtn)

        Dim btnAdd As New Button()
        btnAdd.Text = "+ Hinzufügen"
        btnAdd.Location = New Point(8, 8)
        btnAdd.Size = New Size(140, 30)
        btnAdd.BackColor = Color.LightGreen
        AddHandler btnAdd.Click,
            Sub()
                Dim nm As String = InputBox("Name (Anzeige):", "Kontakt hinzufügen", "")
                If String.IsNullOrWhiteSpace(nm) Then Return
                Dim ph As String = InputBox("Telefonnummer mit Ländercode (ohne +):" & vbCrLf & "Beispiel: 4915734412344",
                                            "Telefonnummer", "")
                If String.IsNullOrWhiteSpace(ph) Then Return
                ph = ph.Trim().Replace("+", "").Replace(" ", "").Replace("-", "")
                File.AppendAllText(kontaktFile, $"{nm.Trim()}|{ph}" & Environment.NewLine)
                clb.Items.Add($"{nm.Trim()}  ({ph})", False)
            End Sub
        pnlBtn.Controls.Add(btnAdd)

        Dim btnDel As New Button()
        btnDel.Text = "🗑 Löschen"
        btnDel.Location = New Point(160, 8)
        btnDel.Size = New Size(120, 30)
        btnDel.BackColor = Color.MistyRose
        AddHandler btnDel.Click,
            Sub()
                If clb.SelectedIndex < 0 Then Return
                Dim idx = clb.SelectedIndex
                clb.Items.RemoveAt(idx)
                Dim allLines = File.ReadAllLines(kontaktFile).Where(Function(s) Not String.IsNullOrWhiteSpace(s)).ToList()
                If idx < allLines.Count Then
                    allLines.RemoveAt(idx)
                    File.WriteAllLines(kontaktFile, allLines)
                End If
            End Sub
        pnlBtn.Controls.Add(btnDel)

        Dim btnAll As New Button()
        btnAll.Text = "✓ Alle"
        btnAll.Location = New Point(290, 8)
        btnAll.Size = New Size(80, 30)
        AddHandler btnAll.Click,
            Sub()
                For i As Integer = 0 To clb.Items.Count - 1
                    clb.SetItemChecked(i, True)
                Next
            End Sub
        pnlBtn.Controls.Add(btnAll)

        Dim btnNone As New Button()
        btnNone.Text = "✗ Keine"
        btnNone.Location = New Point(376, 8)
        btnNone.Size = New Size(80, 30)
        AddHandler btnNone.Click,
            Sub()
                For i As Integer = 0 To clb.Items.Count - 1
                    clb.SetItemChecked(i, False)
                Next
            End Sub
        pnlBtn.Controls.Add(btnNone)

        Dim btnOk As New Button()
        btnOk.Text = "📤 An ausgewählte senden"
        btnOk.Location = New Point(8, 50)
        btnOk.Size = New Size(220, 60)
        btnOk.BackColor = Color.FromArgb(37, 211, 102)
        btnOk.ForeColor = Color.White
        btnOk.Font = New System.Drawing.Font("Segoe UI", 11, FontStyle.Bold)
        btnOk.DialogResult = DialogResult.OK
        pnlBtn.Controls.Add(btnOk)

        Dim btnCancel As New Button()
        btnCancel.Text = "Abbrechen"
        btnCancel.Location = New Point(240, 50)
        btnCancel.Size = New Size(216, 60)
        btnCancel.DialogResult = DialogResult.Cancel
        pnlBtn.Controls.Add(btnCancel)

        f.AcceptButton = btnOk
        f.CancelButton = btnCancel

        If f.ShowDialog(Me) <> DialogResult.OK Then Return Nothing

        Dim sel = ladeKontakte()
        Dim picked As New List(Of Tuple(Of String, String))
        For i As Integer = 0 To clb.Items.Count - 1
            If clb.GetItemChecked(i) AndAlso i < sel.Count Then
                picked.Add(sel(i))
            End If
        Next
        Return picked
    End Function

    ' ═══════════════════════════════════════════════════════════════════
    ' Kontakt seçim penceresi — Name|Telefon formatında local txt
    ' Format her satır: "Anzeigename|49xxxxxxxxxx"
    ' ═══════════════════════════════════════════════════════════════════
    Private Function ZeigeKontaktAuswahl() As Tuple(Of String, String)
        Dim kontaktFile As String = "C:\AngebotListe\whatsapp_kontakte.txt"
        If Not Directory.Exists("C:\AngebotListe\") Then Directory.CreateDirectory("C:\AngebotListe\")
        If Not File.Exists(kontaktFile) Then
            File.WriteAllText(kontaktFile, "Test|4915734412344" & Environment.NewLine)
        End If

        ' Yükle: Name|Phone
        Dim ladeKontakte As Func(Of List(Of Tuple(Of String, String))) =
            Function()
                Dim list As New List(Of Tuple(Of String, String))
                For Each line In File.ReadAllLines(kontaktFile)
                    If String.IsNullOrWhiteSpace(line) Then Continue For
                    Dim parts = line.Split("|"c)
                    Dim nm As String = parts(0).Trim()
                    Dim ph As String = If(parts.Length > 1, parts(1).Trim(), "")
                    list.Add(Tuple.Create(nm, ph))
                Next
                Return list
            End Function

        Dim kontakte = ladeKontakte()

        Dim f As New Form()
        f.Text = "WhatsApp Kontakt wählen"
        f.Size = New Size(460, 520)
        f.StartPosition = FormStartPosition.CenterParent
        f.FormBorderStyle = FormBorderStyle.FixedDialog
        f.MinimizeBox = False
        f.MaximizeBox = False

        Dim lblInfo As New Label()
        lblInfo.Text = "Wählen Sie einen Kontakt:" & vbCrLf &
                       "(WhatsApp Web muss im Browser bereits eingeloggt sein)"
        lblInfo.Dock = DockStyle.Top
        lblInfo.Height = 40
        lblInfo.Padding = New Padding(8, 8, 8, 0)
        f.Controls.Add(lblInfo)

        Dim lb As New ListBox()
        lb.Dock = DockStyle.Fill
        lb.Font = New System.Drawing.Font("Segoe UI", 11)
        lb.IntegralHeight = False
        For Each k In kontakte
            lb.Items.Add($"{k.Item1}  ({k.Item2})")
        Next
        f.Controls.Add(lb)

        Dim pnlBtn As New Panel()
        pnlBtn.Dock = DockStyle.Bottom
        pnlBtn.Height = 100
        f.Controls.Add(pnlBtn)

        Dim btnAdd As New Button()
        btnAdd.Text = "+ Hinzufügen"
        btnAdd.Location = New Point(8, 8)
        btnAdd.Size = New Size(140, 32)
        btnAdd.BackColor = Color.LightGreen
        AddHandler btnAdd.Click,
            Sub()
                Dim nm As String = InputBox("Name (Anzeige):", "Kontakt hinzufügen", "")
                If String.IsNullOrWhiteSpace(nm) Then Return
                Dim ph As String = InputBox("Telefonnummer mit Ländercode (ohne + ohne Leerzeichen):" & vbCrLf &
                                            "Beispiel: 4915734412344",
                                            "Telefonnummer", "")
                If String.IsNullOrWhiteSpace(ph) Then Return
                ph = ph.Trim().Replace("+", "").Replace(" ", "").Replace("-", "")
                File.AppendAllText(kontaktFile, $"{nm.Trim()}|{ph}" & Environment.NewLine)
                lb.Items.Add($"{nm.Trim()}  ({ph})")
            End Sub
        pnlBtn.Controls.Add(btnAdd)

        Dim btnDel As New Button()
        btnDel.Text = "🗑 Löschen"
        btnDel.Location = New Point(160, 8)
        btnDel.Size = New Size(120, 32)
        btnDel.BackColor = Color.MistyRose
        AddHandler btnDel.Click,
            Sub()
                If lb.SelectedIndex < 0 Then Return
                Dim idx = lb.SelectedIndex
                lb.Items.RemoveAt(idx)
                Dim allLines = File.ReadAllLines(kontaktFile).Where(Function(s) Not String.IsNullOrWhiteSpace(s)).ToList()
                If idx < allLines.Count Then
                    allLines.RemoveAt(idx)
                    File.WriteAllLines(kontaktFile, allLines)
                End If
            End Sub
        pnlBtn.Controls.Add(btnDel)

        Dim btnOk As New Button()
        btnOk.Text = "✓ Senden"
        btnOk.Location = New Point(8, 52)
        btnOk.Size = New Size(200, 38)
        btnOk.BackColor = Color.FromArgb(37, 211, 102) ' WhatsApp green
        btnOk.ForeColor = Color.White
        btnOk.Font = New System.Drawing.Font("Segoe UI", 11, FontStyle.Bold)
        btnOk.DialogResult = DialogResult.OK
        pnlBtn.Controls.Add(btnOk)

        Dim btnCancel As New Button()
        btnCancel.Text = "Abbrechen"
        btnCancel.Location = New Point(220, 52)
        btnCancel.Size = New Size(200, 38)
        btnCancel.DialogResult = DialogResult.Cancel
        pnlBtn.Controls.Add(btnCancel)

        f.AcceptButton = btnOk
        f.CancelButton = btnCancel

        If f.ShowDialog(Me) = DialogResult.OK AndAlso lb.SelectedIndex >= 0 Then
            Dim sel = ladeKontakte()
            If lb.SelectedIndex < sel.Count Then
                Return sel(lb.SelectedIndex)
            End If
        End If

        Return Nothing
    End Function






    Private Sub CreateSingleProductPdf(card As ProductCard, pdfPath As String)

        Dim doc As New iTextSharp.text.Document()
        PdfWriter.GetInstance(doc, New FileStream(pdfPath, FileMode.Create))
        doc.Open()

        doc.Add(New Paragraph("Produkt: " & card.ProduktName))
        doc.Add(New Paragraph("Beschreibung: " & card.Beschreibung))
        doc.Add(New Paragraph("Preis: " & card.Preis.ToString("0.00") & " €"))
        doc.Add(New Paragraph(" "))

        ' Ürün resmi varsa ekle — BLOB bytes'tan
        Dim cardBytes As Byte() = card.BildBytes
        If cardBytes IsNot Nothing AndAlso cardBytes.Length > 0 Then
            Dim img = iTextSharp.text.Image.GetInstance(cardBytes)
            img.ScaleToFit(300, 300)
            doc.Add(img)
        End If

        doc.Close()

    End Sub

End Class