Imports System.Data.SqlClient

' Benutzer verwalten (nur Admin). Benutzer werden nicht gelöscht, sondern deaktiviert,
' damit Zuordnungen in alten Daten erhalten bleiben.
' Hinweis: Die Passwörter liegen wie bisher im Klartext in der Spalte PasswordHash,
' weil Windows-Programm UND Mobil-App so prüfen. Eine Umstellung auf Hash muss in beiden gleichzeitig erfolgen.
Public Class FrmBenutzer

    Private ReadOnly cs As String = Baglanti.AngebotListe
    Private aktuelleId As Integer = 0
    Private ladeLaeuft As Boolean = False

    Private Sub FrmBenutzer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Sitzung.IstAdmin Then
            MessageBox.Show("Nur Administratoren dürfen Benutzer verwalten.", "Benutzer verwalten",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            BeginInvoke(New Action(AddressOf Close))
            Return
        End If
        Tema.GridStil(dgvBenutzer)
        If LadeBenutzer() Then NeuerBenutzer()
        BeginInvoke(New Action(AddressOf NachDemAnzeigen))
    End Sub

    ' (per BeginInvoke aus Load, da Shown bei eingebetteten Formularen nicht ausgelöst wird)
    Private Sub NachDemAnzeigen()
        If aktuelleId = 0 Then AuswahlAufheben()
        txtBenutzer.Focus()
    End Sub

    Private Function LadeBenutzer(Optional auswahlId As Integer = 0) As Boolean
        Dim dt As New DataTable()
        Try
            Using con As New SqlConnection(cs)
                Using da As New SqlDataAdapter(
                    "SELECT Id, Username, Role, CAST(ISNULL(IsActive, 0) AS bit) AS IsActive FROM Users ORDER BY Username", con)
                    da.Fill(dt)
                End Using
            End Using
        Catch ex As SqlException
            MessageBox.Show("Die Benutzer konnten nicht geladen werden: " & ex.Message, "Benutzer verwalten",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try

        ladeLaeuft = True
        dgvBenutzer.DataSource = dt
        With dgvBenutzer
            .Columns("Id").Visible = False
            .Columns("Username").HeaderText = "Benutzername"
            .Columns("Username").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns("Role").HeaderText = "Rolle"
            .Columns("Role").Width = 110
            .Columns("IsActive").HeaderText = "Aktiv"
            .Columns("IsActive").Width = 60
        End With
        AuswahlAufheben()
        ladeLaeuft = False

        If auswahlId > 0 Then
            For Each r As DataGridViewRow In dgvBenutzer.Rows
                If CInt(r.Cells("Id").Value) = auswahlId Then
                    dgvBenutzer.CurrentCell = r.Cells("Username")
                    r.Selected = True
                    Exit For
                End If
            Next
        End If
        Return True
    End Function

    Private Sub AuswahlAufheben()
        Dim vorher = ladeLaeuft
        ladeLaeuft = True
        dgvBenutzer.ClearSelection()
        dgvBenutzer.CurrentCell = Nothing
        ladeLaeuft = vorher
    End Sub

    Private Sub dgvBenutzer_SelectionChanged(sender As Object, e As EventArgs) Handles dgvBenutzer.SelectionChanged
        If ladeLaeuft OrElse dgvBenutzer.SelectedRows.Count = 0 Then Return
        Dim r = dgvBenutzer.SelectedRows(0)
        aktuelleId = CInt(r.Cells("Id").Value)
        txtBenutzer.Text = Convert.ToString(r.Cells("Username").Value)
        txtPasswort.Clear()
        RolleSetzen(Convert.ToString(r.Cells("Role").Value))
        chkAktiv.Checked = CBool(r.Cells("IsActive").Value)
        lblDetailTitel.Text = "Benutzer bearbeiten"
        lblInfo.Text = If(aktuelleId = Sitzung.BenutzerId, "Das sind Sie selbst.", "")
    End Sub

    Private Sub RolleSetzen(rolle As String)
        If rolle = "" Then rolle = "User"
        For Each item In cmbRolle.Items
            If String.Equals(item.ToString(), rolle, StringComparison.OrdinalIgnoreCase) Then
                cmbRolle.SelectedItem = item
                Return
            End If
        Next
        cmbRolle.Items.Add(rolle)      ' unbekannte Rolle aus der Datenbank trotzdem anzeigen
        cmbRolle.SelectedItem = rolle
    End Sub

    Private Sub NeuerBenutzer()
        aktuelleId = 0
        AuswahlAufheben()
        txtBenutzer.Clear()
        txtPasswort.Clear()
        RolleSetzen("User")
        chkAktiv.Checked = True
        lblDetailTitel.Text = "Neuer Benutzer"
        lblInfo.Text = "Noch nicht gespeichert"
        txtBenutzer.Focus()
    End Sub

    Private Sub btnNeu_Click(sender As Object, e As EventArgs) Handles btnNeu.Click
        NeuerBenutzer()
    End Sub

    Private Sub btnSpeichern_Click(sender As Object, e As EventArgs) Handles btnSpeichern.Click
        Dim name As String = txtBenutzer.Text.Trim()
        Dim pw As String = txtPasswort.Text
        Dim rolle As String = cmbRolle.Text.Trim()
        Dim aktiv As Boolean = chkAktiv.Checked

        If name = "" Then
            Hinweis("Bitte einen Benutzernamen eingeben.") : txtBenutzer.Focus() : Return
        End If
        If aktuelleId = 0 AndAlso pw = "" Then
            Hinweis("Bitte für den neuen Benutzer ein Passwort eingeben.") : txtPasswort.Focus() : Return
        End If
        If pw <> "" AndAlso pw.Length < 4 Then
            Hinweis("Das Passwort muss mindestens 4 Zeichen lang sein.") : txtPasswort.Focus() : Return
        End If
        If rolle = "" Then rolle = "User"

        ' Selbstschutz: eigenes Konto nicht deaktivieren und sich nicht selbst die Admin-Rolle nehmen
        If aktuelleId = Sitzung.BenutzerId AndAlso
           (Not aktiv OrElse Not String.Equals(rolle, "Admin", StringComparison.OrdinalIgnoreCase)) Then
            Hinweis("Sie können Ihr eigenes Konto nicht deaktivieren oder sich selbst die Admin-Rolle entziehen.")
            Return
        End If

        Try
            Using con As New SqlConnection(cs)
                con.Open()
                Using chk As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @u AND Id <> @id", con)
                    chk.Parameters.AddWithValue("@u", name)
                    chk.Parameters.AddWithValue("@id", aktuelleId)
                    If CInt(chk.ExecuteScalar()) > 0 Then
                        Hinweis($"Der Benutzername '{name}' ist schon vergeben.") : txtBenutzer.Focus() : Return
                    End If
                End Using

                Dim sql As String
                If aktuelleId = 0 Then
                    sql = "INSERT INTO Users (Username, PasswordHash, Role, IsActive) VALUES (@u, @pw, @r, @a); " &
                          "SELECT CAST(SCOPE_IDENTITY() AS int);"
                Else
                    sql = "UPDATE Users SET Username = @u, Role = @r, IsActive = @a" &
                          If(pw <> "", ", PasswordHash = @pw", "") & " WHERE Id = @id; SELECT @id;"
                End If
                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@u", name)
                    cmd.Parameters.AddWithValue("@pw", pw)
                    cmd.Parameters.AddWithValue("@r", rolle)
                    cmd.Parameters.AddWithValue("@a", aktiv)
                    cmd.Parameters.AddWithValue("@id", aktuelleId)
                    aktuelleId = CInt(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As SqlException
            MessageBox.Show("Speichern fehlgeschlagen: " & ex.Message, "Benutzer verwalten", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If aktuelleId = Sitzung.BenutzerId Then Sitzung.Anmelden(aktuelleId, name, rolle)
        LadeBenutzer(aktuelleId)
        txtPasswort.Clear()
        lblInfo.Text = "Gespeichert ✓"
    End Sub

    Private Sub Hinweis(text As String)
        MessageBox.Show(text, "Benutzer verwalten", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.S) Then btnSpeichern.PerformClick() : Return True
        If keyData = Keys.Escape Then Close() : Return True
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class
