Module Program

    Sub Main()

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Splash Screen çalıştır
        Application.Run(New SplashScreen2())

        ' Splash kapandıktan sonra lisans kontrolü
        Dim hwid As String = GetHWID()
        Dim dt As DataTable = Database.GetLicenseByHWID(hwid, "YeniProgram")

        If dt.Rows.Count > 0 Then

            Dim row As DataRow = dt.Rows(0)

            If row("ProgramName").ToString() <> "YeniProgram" Then
                Application.Run(New LicenseForm())
                Return
            End If

            If Not CBool(row("IsActive")) Then
                Application.Run(New LicenseForm())
                Return
            End If

            If CDate(row("ExpireDate")) < Date.Now Then
                Application.Run(New LicenseForm())
                Return
            End If

            If row("BoundHWID").ToString() <> hwid Then
                Application.Run(New LicenseForm())
                Return
            End If

            ' ⭐ Lisans OK → Login Form göster
            Dim login As New LoginForm2()
            If login.ShowDialog() = DialogResult.OK Then
                Application.Run(New NeuesAngebotListeErstellen())
            End If
            Return

        End If

        Application.Run(New LicenseForm())

    End Sub

End Module