Public Class FirmaName
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim yeniFirmaAdi As String = TextBox1.Text.Trim()
        If yeniFirmaAdi = "" Then
            yeniFirmaAdi = "My Fresh Fruit Yeşil GmbH"
        End If

        ' Global firma adını değiştir
        ProductCard.GlobalFirmaAdi = yeniFirmaAdi

        ' Formdaki tüm ProductCard’ları yenile
        RefreshAllProductCards(Me)
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