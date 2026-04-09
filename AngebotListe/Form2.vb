Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel1.AutoScroll = True

        For Each prop In GetType(Color).GetProperties()
            If prop.PropertyType Is GetType(Color) Then

                Dim c As Color = CType(prop.GetValue(Nothing), Color)

                ' Panel (renk kutusu + isim + hex)
                Dim p As New Panel With {
                    .Width = 250,
                    .Height = 30,
                    .BackColor = Color.White,
                    .Margin = New Padding(3)
                }

                ' Renk kutusu
                Dim colorBox As New Panel With {
                    .BackColor = c,
                    .Width = 30,
                    .Height = 30,
                    .Dock = DockStyle.Left
                }

                ' Renk adı + HEX
                Dim lbl As New Label With {
                    .Text = $"{prop.Name}   #{c.R:X2}{c.G:X2}{c.B:X2}",
                    .AutoSize = False,
                    .Width = 200,
                    .Height = 30,
                    .TextAlign = ContentAlignment.MiddleLeft,
                    .Dock = DockStyle.Right
                }

                p.Controls.Add(lbl)
                p.Controls.Add(colorBox)
                FlowLayoutPanel1.Controls.Add(p)

            End If
        Next

    End Sub

End Class