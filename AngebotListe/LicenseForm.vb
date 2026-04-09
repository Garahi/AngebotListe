Public Class LicenseForm
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click

        Dim key As String = txtKey.Text.Trim()

        If key = "" Then
            MessageBox.Show("Lisans anahtarı giriniz.")
            Return
        End If

        Dim dt As DataTable = Database.GetLicense(key)

        ' 🔥 TEST SATIRI — SQL’den GERÇEK ProgramName’i gösterir
        If dt.Rows.Count > 0 Then
            'MessageBox.Show("SQL'den gelen ProgramName: " & dt.Rows(0)("ProgramName").ToString())
        End If

        If dt.Rows.Count = 0 Then
            MessageBox.Show("Geçersiz lisans anahtarı.")
            Return
        End If

        Dim row As DataRow = dt.Rows(0)

        ' 1) Lisans aktif mi?
        If Not CBool(row("IsActive")) Then
            MessageBox.Show("Bu lisans pasif durumda.")
            Return
        End If

        ' 2) ProgramName eşleşiyor mu?
        If row("ProgramName").ToString() <> "YeniProgram" Then
            MessageBox.Show("Bu lisans bu program için geçerli değil.")
            Return
        End If

        ' 3) Lisans süresi dolmuş mu?
        Dim expire As Date = CDate(row("ExpireDate"))
        If expire < Date.Now Then
            MessageBox.Show("Lisans süresi dolmuş.")
            Return
        End If

        ' 4) HWID kontrolü
        Dim currentHWID As String = GetHWID()

        If Not IsDBNull(row("BoundHWID")) Then
            Dim bound As String = row("BoundHWID").ToString()

            If bound <> "" AndAlso bound <> currentHWID Then
                MessageBox.Show("Bu lisans başka bir bilgisayara bağlı.")
                Return
            End If
        End If

        ' 5) Lisans geçerli → programı aç
        Dim f As New NeuesAngebotListeErstellen()
        f.Show()
        Me.Hide()

        ' 6) HWID bağlama (ilk kullanımda)
        Database.BindHWID(key, currentHWID)

    End Sub

    Private Sub LicenseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MessageBox.Show("LicenseForm açıldı!")
    End Sub

End Class
