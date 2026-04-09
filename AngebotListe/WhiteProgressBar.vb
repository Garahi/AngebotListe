Imports System.Drawing
Imports System.Windows.Forms

Public Class WhiteProgressBar
    Inherits ProgressBar

    Public Sub New()
        Me.SetStyle(ControlStyles.UserPaint, True)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim rect As Rectangle = Me.ClientRectangle
        Dim g As Graphics = e.Graphics

        ' Arka plan (koyu)
        g.FillRectangle(New SolidBrush(Color.FromArgb(25, 25, 25)), rect)

        ' Dolan kısım (beyaz)
        Dim fillWidth As Integer = CInt(rect.Width * (Me.Value / Me.Maximum))
        g.FillRectangle(Brushes.White, 0, 0, fillWidth, rect.Height)

        ' Kenarlık
        g.DrawRectangle(Pens.White, 0, 0, rect.Width - 1, rect.Height - 1)
    End Sub
End Class




