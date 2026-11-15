Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class FrmProductCard2

    Private Sub FrmProductCard2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()

        If Not DataGridView1.Columns.Contains("Select") Then
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.Name = "Select"
            chk.HeaderText = "✓"
            chk.Width = 40
            DataGridView1.Columns.Insert(0, chk)
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
    End Sub


    Private Sub LoadProducts()
        Dim dt As New DataTable()

        Using con As New SqlConnection(Baglanti.AngebotListe)
            Using cmd As New SqlCommand("SELECT * FROM Products WHERE Aktiv = 1", con)
                con.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        ' 🟢 DataGridView kolonları manuel ekle
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add(New DataGridViewCheckBoxColumn() With {.Name = "Select", .HeaderText = "✓", .Width = 40})
        DataGridView1.Columns.Add("ID", "ID")
        DataGridView1.Columns.Add("Name", "Name")
        DataGridView1.Columns.Add("Beschreibung", "Beschreibung")
        DataGridView1.Columns.Add("Preis", "Preis")
        DataGridView1.Columns.Add("BildPfad", "BildPfad")

        ' Satır ekle
        For Each row As DataRow In dt.Rows
            DataGridView1.Rows.Add(False,
                               If(IsDBNull(row("ID")), 0, row("ID")),
                               If(IsDBNull(row("Name")), "", row("Name")),
                               If(IsDBNull(row("Beschreibung")), "", row("Beschreibung")),
                               If(IsDBNull(row("Preis")), 0D, row("Preis")),
                               If(IsDBNull(row("BildPfad")), "", row("BildPfad")))
        Next

        ' FlowLayoutPanel doldur
        FlowLayoutPanel1.Controls.Clear()
        For Each row As DataRow In dt.Rows
            Dim card As New ProductCard()
            card.ProductID = If(IsDBNull(row("ID")), 0, CInt(row("ID")))
            card.ProduktName = If(IsDBNull(row("Name")), "", row("Name").ToString())
            card.Beschreibung = If(IsDBNull(row("Beschreibung")), "", row("Beschreibung").ToString())
            card.Preis = If(IsDBNull(row("Preis")), 0D, CDec(row("Preis")))
            card.BildPfad = If(IsDBNull(row("BildPfad")), "", row("BildPfad").ToString())

            AddHandler card.CardFocused, AddressOf Card_Focused
            FlowLayoutPanel1.Controls.Add(card)
        Next
    End Sub
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        If DataGridView1.IsCurrentCellDirty Then
            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

        ' Hiç satır seçili değilse → çık
        If DataGridView1.SelectedRows.Count = 0 Then Exit Sub

        Dim row = DataGridView1.SelectedRows(0)

        ' Satır boşsa → çık
        If row Is Nothing Then Exit Sub

        Dim rawID = row.Cells("ID").Value

        ' ID NULL ise → çık
        If rawID Is Nothing OrElse IsDBNull(rawID) Then Exit Sub

        ' ID sayı değilse → çık
        Dim selectedID As Integer
        If Not Integer.TryParse(rawID.ToString(), selectedID) Then Exit Sub

        ' Buraya gelmişse ID kesinlikle geçerli
        For Each card As ProductCard In FlowLayoutPanel1.Controls
            If card.ProductID = selectedID Then
                FlowLayoutPanel1.ScrollControlIntoView(card)
                card.SetHighlight(True)
            Else
                card.SetHighlight(False)
            End If
        Next

    End Sub



    Private Sub btnNeueProdukt_Click(sender As Object, e As EventArgs) Handles btnNeueProdukt.Click


        Dim newCard As New ProductCard()

        newCard.ProductID = 0
        newCard.ProduktName = ""
        newCard.Beschreibung = ""
        newCard.Preis = 0
        newCard.BildPfad = ""

        FlowLayoutPanel1.Controls.Add(newCard)
        ' Yeni eklenen kartı otomatik görünür yap
        FlowLayoutPanel1.ScrollControlIntoView(newCard)
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

    'Private Sub btnWhatsApp_Click(sender As Object, e As EventArgs) Handles btnWhatsApp.Click

    '    Dim selectedCards As New List(Of ProductCard)

    '    ' Seçili satırları bul
    '    For Each row As DataGridViewRow In DataGridView1.Rows
    '        If Convert.ToBoolean(row.Cells("Select").Value) = True Then
    '            Dim id As Integer = CInt(row.Cells("ID").Value)

    '            ' Kartı bul
    '            For Each card As ProductCard In FlowLayoutPanel1.Controls
    '                If card.ProductID = id Then
    '                    selectedCards.Add(card)
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '    Next

    '    If selectedCards.Count = 0 Then
    '        MessageBox.Show("Bitte Produkte auswählen.")
    '        Exit Sub
    '    End If

    '    ' Her kart için WhatsApp görseli oluştur
    '    Dim index As Integer = 1
    '    For Each card In selectedCards
    '        Dim img As Bitmap = card.GetShareImage()

    '        Dim savePath As String = $"C:\AngebotListe\WhatsapaYollanilan\share_{index}.png"
    '        img.Save(savePath, Imaging.ImageFormat.Png)

    '        Process.Start(savePath)
    '        index += 1
    '    Next

    'End Sub
    Private Sub btnWhatsApp_Click(sender As Object, e As EventArgs) Handles btnWhatsApp.Click

        Dim selectedCards As New List(Of ProductCard)

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Convert.ToBoolean(row.Cells("Select").Value) = True Then
                Dim id As Integer = CInt(row.Cells("ID").Value)

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

        Dim savePath As String = "C:\AngebotListe\WhatsapaYollanilan\Angebot.pdf"

        Dim doc As New Document(PageSize.A4, 20, 20, 20, 20)
        Dim writer = PdfWriter.GetInstance(doc, New IO.FileStream(savePath, IO.FileMode.Create))
        doc.Open()

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
    Private Sub DataGridView1_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.RowValidating

        Dim row = DataGridView1.Rows(e.RowIndex)

        If row.IsNewRow Then Exit Sub

        If row.Cells("Name").Value Is Nothing _
       OrElse String.IsNullOrWhiteSpace(row.Cells("Name").Value.ToString()) Then

            row.ErrorText = "Produktname darf nicht leer sein!"
            e.Cancel = True
        Else
            row.ErrorText = ""
        End If

    End Sub
    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        e.ThrowException = False
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub NeuesAngebotListeErstellen_Click(sender As Object, e As EventArgs) Handles NeuesAngebotListeErstellen.Click



        ' 1️⃣ Otomatik AngebotNr üret
        Dim angebotNr As String
        Using con As New SqlConnection(Baglanti.AngebotListe)
            con.Open()
            Dim year As String = DateTime.Now.Year.ToString()
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM AngebotListe WHERE YEAR(Datum) = YEAR(GETDATE())", con)
            Dim count As Integer = CInt(cmd.ExecuteScalar()) + 1
            angebotNr = $"ANG-{year}-{count.ToString("0000")}"
        End Using

        ' 2️⃣ NeuesAngebotListeErstellen formunu aç
        Dim frm As New NeuesAngebotListeErstellen()
        frm.txtAngebotNr.Text = angebotNr
        frm.ShowDialog()   ' Modal olarak açabiliriz

    End Sub
End Class

