Imports System.Data.SqlClient

Public Class LoginForm2

    Public Property LoggedInUserId As Integer = 0
    Public Property LoggedInUsername As String = ""
    Public Property LoggedInRole As String = ""

    Private ReadOnly connectionString As String = Baglanti.AngebotListe

    ' Zuletzt verwendeter Benutzername (das Passwort wird nie gespeichert)
    Private Shared ReadOnly LetzterBenutzerDatei As String =
        IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AngebotListe", "letzter_benutzer.txt")

    Private Sub LoginForm2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If IO.File.Exists(LetzterBenutzerDatei) Then txtUsername.Text = IO.File.ReadAllText(LetzterBenutzerDatei).Trim()
        Catch
        End Try
    End Sub

    Private Sub LoginForm2_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If txtUsername.Text = "" Then txtUsername.Focus() Else txtPassword.Focus()
    End Sub

    Private Sub BenutzernameMerken(name As String)
        Try
            IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(LetzterBenutzerDatei))
            IO.File.WriteAllText(LetzterBenutzerDatei, name)
        Catch
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        lblStatus.Text = ""

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text

        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
            lblStatus.Text = "Bitte Benutzername und Passwort eingeben."
            Return
        End If

        btnLogin.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Using cmd As New SqlCommand("SELECT TOP 1 Id, Username, PasswordHash, Role, IsActive FROM Users WHERE Username = @u", con)
                    cmd.Parameters.AddWithValue("@u", username)
                    Using rd = cmd.ExecuteReader()
                        If Not rd.Read() Then
                            lblStatus.Text = "Benutzername oder Passwort falsch."
                            Return
                        End If

                        Dim isActive As Boolean = (rd("IsActive") IsNot DBNull.Value AndAlso CBool(rd("IsActive")))
                        If Not isActive Then
                            lblStatus.Text = "Dieser Benutzer ist deaktiviert."
                            Return
                        End If

                        Dim storedHash As String = If(rd("PasswordHash") Is DBNull.Value, "", rd("PasswordHash").ToString())

                        ' BCrypt mı düz metin mi kontrol et
                        Dim ok As Boolean = False
                        If storedHash.StartsWith("$2a$") OrElse storedHash.StartsWith("$2b$") OrElse storedHash.StartsWith("$2y$") Then
                            ' BCrypt — şu an VB.NET'te BCrypt yok, plain compare yapıyoruz
                            ' İleride BCrypt.Net-Next eklenirse buraya BCrypt.Verify gelecek
                            ok = (storedHash = password)
                        Else
                            ok = (storedHash = password)
                        End If

                        If Not ok Then
                            lblStatus.Text = "Benutzername oder Passwort falsch."
                            Return
                        End If

                        ' Başarılı
                        Me.LoggedInUserId = CInt(rd("Id"))
                        Me.LoggedInUsername = If(rd("Username") Is DBNull.Value, "", rd("Username").ToString())
                        Me.LoggedInRole = If(rd("Role") Is DBNull.Value, "User", rd("Role").ToString())
                        BenutzernameMerken(Me.LoggedInUsername)
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    End Using
                End Using
            End Using
        Catch ex As SqlException
            lblStatus.Text = "Keine Verbindung zum Server. Bitte Internetverbindung prüfen."
        Catch ex As Exception
            lblStatus.Text = "Fehler: " & ex.Message
        Finally
            btnLogin.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

End Class
