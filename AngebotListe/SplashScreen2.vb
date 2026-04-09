Public NotInheritable Class SplashScreen2

    Private iprogessBarvalue As Integer = 0
    Private WithEvents MyTimer As New Timer

    ' Hier deine Wunschfarbe einstellen
    Private MeineFarbe As Color = Color.Orange ' <-- gewünschte Farbe

    Private Sub SplashScreen2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.CenterToScreen()   ' <-- garanti ortalama

        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 1000
        ProgressBar1.Value = 0
        ProgressBar1.Style = ProgressBarStyle.Continuous

        MyTimer.Interval = 300
        MyTimer.Start()
    End Sub


    Private Sub MyTimer_Tick(sender As Object, e As EventArgs) Handles MyTimer.Tick
        iprogessBarvalue += 1

        If iprogessBarvalue <= 10 Then
            ProgressBar1.Value = iprogessBarvalue * 100
            ProgressBar1.Invalidate() ' erzwingt Neuzeichnen
        End If

        If iprogessBarvalue >= 10 Then
            MyTimer.Stop()
            Me.Close()
        End If
    End Sub

    ' ProgressBar überschreiben, um eigene Farbe zu zeichnen
    Private Sub ProgressBar1_Paint(sender As Object, e As PaintEventArgs) Handles ProgressBar1.Paint
        Dim g As Graphics = e.Graphics
        Dim brush As New SolidBrush(MeineFarbe)
        Dim width As Integer = CInt((ProgressBar1.Value / ProgressBar1.Maximum) * ProgressBar1.Width)
        g.FillRectangle(brush, 0, 0, width, ProgressBar1.Height)
    End Sub

End Class
