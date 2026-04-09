Imports System.Drawing.Drawing2D

Public Class RoundedButton
    Inherits Button

    Public Property BorderRadius As Integer = 10

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)

        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
        Dim path As New GraphicsPath()

        path.AddArc(rect.X, rect.Y, BorderRadius, BorderRadius, 180, 90)
        path.AddArc(rect.Right - BorderRadius, rect.Y, BorderRadius, BorderRadius, 270, 90)
        path.AddArc(rect.Right - BorderRadius, rect.Bottom - BorderRadius, BorderRadius, BorderRadius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - BorderRadius, BorderRadius, BorderRadius, 90, 90)
        path.CloseFigure()

        Me.Region = New Region(path)
    End Sub
End Class
