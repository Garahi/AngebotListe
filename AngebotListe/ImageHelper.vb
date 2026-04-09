Imports System.IO

Public Module ImageHelper

    Public Function ByteArrayToImage(bytes As Byte()) As Image
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return Nothing

        ' ⭐ Stream'i kapatmadan Image dönmek bozulmaya yol açar.
        ' Önce Image.FromStream ile yükle, sonra yeni bir Bitmap'e kopyala.
        Try
            Using ms As New MemoryStream(bytes)
                Using tmp As Image = Image.FromStream(ms)
                    Return New Bitmap(tmp)
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Public Function ImageToByteArray(img As Image) As Byte()
        If img Is Nothing Then Return Nothing

        Try
            ' ⭐ Önce yeni 32bpp Bitmap'e kopyala — indexed format ve stream-bağımlılık sorununu çözer
            Using copy As New Bitmap(img.Width, img.Height, Imaging.PixelFormat.Format32bppArgb)
                Using g = Graphics.FromImage(copy)
                    g.DrawImage(img, 0, 0, img.Width, img.Height)
                End Using
                Using ms As New MemoryStream()
                    copy.Save(ms, Imaging.ImageFormat.Jpeg)
                    Return ms.ToArray()
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function

End Module