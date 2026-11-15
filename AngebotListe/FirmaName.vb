Public Class FirmaName
    Private Sub FirmaName_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ProductCard.GlobalFirmaAdi
        TextBox1.SelectAll()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim yeniFirmaAdi As String = TextBox1.Text.Trim()
        If yeniFirmaAdi = "" Then
            yeniFirmaAdi = "My Fresh Fruit Yeşil GmbH"
        End If

        ' Global firma adını değiştir
        ProductCard.GlobalFirmaAdi = yeniFirmaAdi

        ' Tüm açık formlardaki ProductCard'ları yenile.
        ' (Eskiden yalnızca bu küçük formun içinde aranıyordu; kartlar ana formda olduğu için hiçbiri yenilenmiyordu.)
        For Each frm As Form In Application.OpenForms
            If frm IsNot Me Then RefreshAllProductCards(frm)
        Next
        Me.DialogResult = DialogResult.OK
        ' Formu kapat
        Me.Close()
    End Sub

    Private Sub RefreshAllProductCards(parent As Control)
        For Each ctrl As Control In parent.Controls

            ' Eğer kontrol ProductCard ise yenile
            If TypeOf ctrl Is ProductCard Then
                ctrl.Refresh()
            End If

            ' İçinde başka kontroller varsa onları da tara
            If ctrl.HasChildren Then
                RefreshAllProductCards(ctrl)
            End If

        Next
    End Sub


End Class