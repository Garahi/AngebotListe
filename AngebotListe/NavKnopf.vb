Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

' Eintrag der linken Navigationsleiste: Symbol (Segoe Fluent Icons / MDL2) + Text.
' Erscheint nach dem Erstellen in der Toolbox und ist im Designer verschiebbar.
<DefaultEvent("Click")>
Public Class NavKnopf
    Inherits Control

    Private _aktiv As Boolean
    Private _symbolCode As String = "E8A5"
    Private _hover As Boolean

    Private Shared ReadOnly SymbolSchrift As String = ErmittleSymbolSchrift()

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
        Cursor = Cursors.Hand
        Font = New Font("Segoe UI", 10.0F)
        ForeColor = Color.FromArgb(176, 186, 205)
        BackColor = Color.Transparent
        Size = New Size(186, 40)
    End Sub

    ' Unicode-Codepunkt des Symbols als Hex, z. B. E8A5 (Dokument), E7B8 (Paket), E716 (Personen), E713 (Zahnrad)
    <Category("Darstellung"), DefaultValue("E8A5")>
    Public Property SymbolCode As String
        Get
            Return _symbolCode
        End Get
        Set(value As String)
            _symbolCode = value
            Invalidate()
        End Set
    End Property

    ' Markiert den aktuell geöffneten Bereich
    <Category("Darstellung"), DefaultValue(False)>
    Public Property Aktiv As Boolean
        Get
            Return _aktiv
        End Get
        Set(value As Boolean)
            _aktiv = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        _hover = True : Invalidate() : MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _hover = False : Invalidate() : MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        Invalidate() : MyBase.OnTextChanged(e)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        Dim hinter As Color = Color.Empty
        If _aktiv Then
            hinter = Color.FromArgb(26, 115, 232)
        ElseIf _hover Then
            hinter = Color.FromArgb(44, 57, 88)
        End If
        If hinter <> Color.Empty Then
            Using pfad = AbgerundetesRechteck(New Rectangle(0, 0, Width - 1, Height - 1), 8),
                  b As New SolidBrush(hinter)
                g.FillPath(b, pfad)
            End Using
        End If

        Dim farbe As Color = If(_aktiv OrElse _hover, Color.White, ForeColor)

        ' Symbol
        Dim zeichen As String = ""
        Dim code As Integer
        If Integer.TryParse(_symbolCode, Globalization.NumberStyles.HexNumber, Nothing, code) Then zeichen = ChrW(code)
        If zeichen <> "" Then
            Using f As New Font(SymbolSchrift, 12.0F), b As New SolidBrush(farbe)
                Dim r As New Rectangle(12, 0, 24, Height)
                TextRenderer.DrawText(g, zeichen, f, r, farbe,
                                      TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding)
            End Using
        End If

        ' Text
        Using f As New Font(Font, If(_aktiv, FontStyle.Bold, FontStyle.Regular))
            TextRenderer.DrawText(g, Text, f, New Rectangle(46, 0, Width - 50, Height), farbe,
                                  TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis)
        End Using
    End Sub

    Private Shared Function AbgerundetesRechteck(r As Rectangle, radius As Integer) As GraphicsPath
        Dim p As New GraphicsPath()
        Dim d = radius * 2
        p.AddArc(r.X, r.Y, d, d, 180, 90)
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        p.CloseFigure()
        Return p
    End Function

    ' Windows 11: "Segoe Fluent Icons", Windows 10: "Segoe MDL2 Assets" (gleiche Codepunkte)
    Private Shared Function ErmittleSymbolSchrift() As String
        Using fc As New InstalledFontCollection()
            For Each schrift As String In {"Segoe Fluent Icons", "Segoe MDL2 Assets"}
                If fc.Families.Any(Function(f) f.Name = schrift) Then Return schrift
            Next
        End Using
        Return "Segoe UI Symbol"
    End Function

End Class
