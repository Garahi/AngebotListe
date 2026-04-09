Public Class SplashScreen1


    Private iprogessBarvalue As Integer = 1


    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 1000
        ProgressBar1.Value = 0

        Timer1.Enabled = True
        Timer1.Interval = 300
        Timer1.Start()


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        iprogessBarvalue += 1

        Select Case iprogessBarvalue
            Case 1, 3, 5, 7, 9

                ProgressBar1.ForeColor = Color.White
                ProgressBar1.Value = (iprogessBarvalue * 100)
            Case 2, 4, 6, 8, 10
                ProgressBar1.Value = (iprogessBarvalue * 100)

            Case 13
                Timer1.Stop()
                Timer1.Enabled = False


                'Form55.Show()



                ' Form42. Show()
                Me.Close()




        End Select










    End Sub

    Private Sub MainLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles MainLayoutPanel.Paint
        MainLayoutPanel.BackColor = ColorTranslator.FromHtml("#324A78")

        'MainLayoutPanel.BackColor = ColorTranslator.FromHtml("#1F2A44")
        'Me.BackColor = ColorTranslator.FromHtml("#1F2A44")
    End Sub

End Class