<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBenutzer
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlListe = New System.Windows.Forms.Panel()
        Me.dgvBenutzer = New System.Windows.Forms.DataGridView()
        Me.pnlTrenner = New System.Windows.Forms.Panel()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.lblDetailTitel = New System.Windows.Forms.Label()
        Me.lblBenutzername = New System.Windows.Forms.Label()
        Me.txtBenutzer = New System.Windows.Forms.TextBox()
        Me.lblPasswort = New System.Windows.Forms.Label()
        Me.txtPasswort = New System.Windows.Forms.TextBox()
        Me.lblPasswortHinweis = New System.Windows.Forms.Label()
        Me.lblRolle = New System.Windows.Forms.Label()
        Me.cmbRolle = New System.Windows.Forms.ComboBox()
        Me.chkAktiv = New System.Windows.Forms.CheckBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlDetailFuss = New System.Windows.Forms.Panel()
        Me.pnlDetailFussLinie = New System.Windows.Forms.Panel()
        Me.btnNeu = New System.Windows.Forms.Button()
        Me.btnSpeichern = New System.Windows.Forms.Button()
        Me.pnlKopf = New System.Windows.Forms.Panel()
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblUntertitel = New System.Windows.Forms.Label()
        Me.pnlListe.SuspendLayout()
        Me.pnlTrenner.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        Me.pnlDetailFuss.SuspendLayout()
        Me.pnlDetailFussLinie.SuspendLayout()
        Me.pnlKopf.SuspendLayout()
        CType(Me.dgvBenutzer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlListe
        '
        Me.pnlListe.Controls.Add(Me.dgvBenutzer)
        Me.pnlListe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlListe.Location = New System.Drawing.Point(0, 64)
        Me.pnlListe.Padding = New System.Windows.Forms.Padding(16, 16, 16, 16)
        Me.pnlListe.Size = New System.Drawing.Size(519, 456)
        Me.pnlListe.TabIndex = 1
        Me.pnlListe.Name = "pnlListe"
        '
        'dgvBenutzer
        '
        Me.dgvBenutzer.AllowUserToAddRows = False
        Me.dgvBenutzer.AllowUserToDeleteRows = False
        Me.dgvBenutzer.AllowUserToResizeRows = False
        Me.dgvBenutzer.BackgroundColor = System.Drawing.Color.White
        Me.dgvBenutzer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvBenutzer.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvBenutzer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvBenutzer.ColumnHeadersHeight = 36
        Me.dgvBenutzer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvBenutzer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBenutzer.EnableHeadersVisualStyles = False
        Me.dgvBenutzer.GridColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgvBenutzer.Location = New System.Drawing.Point(16, 16)
        Me.dgvBenutzer.MultiSelect = False
        Me.dgvBenutzer.ReadOnly = True
        Me.dgvBenutzer.RowHeadersVisible = False
        Me.dgvBenutzer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBenutzer.Size = New System.Drawing.Size(487, 424)
        Me.dgvBenutzer.TabIndex = 0
        Me.dgvBenutzer.Name = "dgvBenutzer"
        '
        'pnlTrenner
        '
        Me.pnlTrenner.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlTrenner.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlTrenner.Location = New System.Drawing.Point(519, 64)
        Me.pnlTrenner.Size = New System.Drawing.Size(1, 456)
        Me.pnlTrenner.TabIndex = 2
        Me.pnlTrenner.Name = "pnlTrenner"
        '
        'pnlDetail
        '
        Me.pnlDetail.Controls.Add(Me.lblDetailTitel)
        Me.pnlDetail.Controls.Add(Me.lblBenutzername)
        Me.pnlDetail.Controls.Add(Me.txtBenutzer)
        Me.pnlDetail.Controls.Add(Me.lblPasswort)
        Me.pnlDetail.Controls.Add(Me.txtPasswort)
        Me.pnlDetail.Controls.Add(Me.lblPasswortHinweis)
        Me.pnlDetail.Controls.Add(Me.lblRolle)
        Me.pnlDetail.Controls.Add(Me.cmbRolle)
        Me.pnlDetail.Controls.Add(Me.chkAktiv)
        Me.pnlDetail.Controls.Add(Me.lblInfo)
        Me.pnlDetail.Controls.Add(Me.pnlDetailFuss)
        Me.pnlDetail.BackColor = System.Drawing.Color.White
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDetail.Location = New System.Drawing.Point(520, 64)
        Me.pnlDetail.Size = New System.Drawing.Size(300, 456)
        Me.pnlDetail.TabIndex = 3
        Me.pnlDetail.Name = "pnlDetail"
        '
        'lblDetailTitel
        '
        Me.lblDetailTitel.AutoSize = True
        Me.lblDetailTitel.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailTitel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.lblDetailTitel.Location = New System.Drawing.Point(18, 14)
        Me.lblDetailTitel.Size = New System.Drawing.Size(120, 21)
        Me.lblDetailTitel.TabIndex = 0
        Me.lblDetailTitel.Text = "Neuer Benutzer"
        Me.lblDetailTitel.Name = "lblDetailTitel"
        '
        'lblBenutzername
        '
        Me.lblBenutzername.AutoSize = True
        Me.lblBenutzername.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBenutzername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblBenutzername.Location = New System.Drawing.Point(20, 52)
        Me.lblBenutzername.Size = New System.Drawing.Size(200, 13)
        Me.lblBenutzername.TabIndex = 1
        Me.lblBenutzername.Text = "BENUTZERNAME *"
        Me.lblBenutzername.Name = "lblBenutzername"
        '
        'txtBenutzer
        '
        Me.txtBenutzer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBenutzer.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBenutzer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtBenutzer.Location = New System.Drawing.Point(20, 70)
        Me.txtBenutzer.Size = New System.Drawing.Size(260, 25)
        Me.txtBenutzer.TabIndex = 2
        Me.txtBenutzer.Name = "txtBenutzer"
        '
        'lblPasswort
        '
        Me.lblPasswort.AutoSize = True
        Me.lblPasswort.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPasswort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblPasswort.Location = New System.Drawing.Point(20, 110)
        Me.lblPasswort.Size = New System.Drawing.Size(200, 13)
        Me.lblPasswort.TabIndex = 3
        Me.lblPasswort.Text = "PASSWORT"
        Me.lblPasswort.Name = "lblPasswort"
        '
        'txtPasswort
        '
        Me.txtPasswort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPasswort.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPasswort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtPasswort.Location = New System.Drawing.Point(20, 128)
        Me.txtPasswort.Size = New System.Drawing.Size(260, 25)
        Me.txtPasswort.TabIndex = 4
        Me.txtPasswort.UseSystemPasswordChar = True
        Me.txtPasswort.Name = "txtPasswort"
        '
        'lblPasswortHinweis
        '
        Me.lblPasswortHinweis.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPasswortHinweis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblPasswortHinweis.Location = New System.Drawing.Point(18, 156)
        Me.lblPasswortHinweis.Size = New System.Drawing.Size(262, 30)
        Me.lblPasswortHinweis.TabIndex = 5
        Me.lblPasswortHinweis.Text = "Bei bestehenden Benutzern leer lassen, wenn das Passwort gleich bleibt."
        Me.lblPasswortHinweis.Name = "lblPasswortHinweis"
        '
        'lblRolle
        '
        Me.lblRolle.AutoSize = True
        Me.lblRolle.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRolle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblRolle.Location = New System.Drawing.Point(20, 194)
        Me.lblRolle.Size = New System.Drawing.Size(200, 13)
        Me.lblRolle.TabIndex = 6
        Me.lblRolle.Text = "ROLLE"
        Me.lblRolle.Name = "lblRolle"
        '
        'cmbRolle
        '
        Me.cmbRolle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbRolle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRolle.FormattingEnabled = True
        Me.cmbRolle.Items.AddRange(New Object() {"Admin", "User"})
        Me.cmbRolle.Location = New System.Drawing.Point(20, 212)
        Me.cmbRolle.Size = New System.Drawing.Size(260, 27)
        Me.cmbRolle.TabIndex = 7
        Me.cmbRolle.Name = "cmbRolle"
        '
        'chkAktiv
        '
        Me.chkAktiv.AutoSize = True
        Me.chkAktiv.Checked = True
        Me.chkAktiv.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAktiv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkAktiv.Location = New System.Drawing.Point(20, 252)
        Me.chkAktiv.Size = New System.Drawing.Size(190, 21)
        Me.chkAktiv.TabIndex = 8
        Me.chkAktiv.Text = "Aktiv (darf sich anmelden)"
        Me.chkAktiv.UseVisualStyleBackColor = True
        Me.chkAktiv.Name = "chkAktiv"
        '
        'lblInfo
        '
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblInfo.Location = New System.Drawing.Point(18, 286)
        Me.lblInfo.Size = New System.Drawing.Size(262, 40)
        Me.lblInfo.TabIndex = 9
        Me.lblInfo.Text = ""
        Me.lblInfo.Name = "lblInfo"
        '
        'pnlDetailFuss
        '
        Me.pnlDetailFuss.Controls.Add(Me.pnlDetailFussLinie)
        Me.pnlDetailFuss.Controls.Add(Me.btnNeu)
        Me.pnlDetailFuss.Controls.Add(Me.btnSpeichern)
        Me.pnlDetailFuss.BackColor = System.Drawing.Color.White
        Me.pnlDetailFuss.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDetailFuss.Location = New System.Drawing.Point(0, 392)
        Me.pnlDetailFuss.Size = New System.Drawing.Size(300, 64)
        Me.pnlDetailFuss.TabIndex = 10
        Me.pnlDetailFuss.Name = "pnlDetailFuss"
        '
        'pnlDetailFussLinie
        '
        Me.pnlDetailFussLinie.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlDetailFussLinie.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDetailFussLinie.Location = New System.Drawing.Point(0, 0)
        Me.pnlDetailFussLinie.Size = New System.Drawing.Size(800, 1)
        Me.pnlDetailFussLinie.TabIndex = 0
        Me.pnlDetailFussLinie.Name = "pnlDetailFussLinie"
        '
        'btnNeu
        '
        Me.btnNeu.BackColor = System.Drawing.Color.White
        Me.btnNeu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNeu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnNeu.FlatAppearance.BorderSize = 1
        Me.btnNeu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnNeu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNeu.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNeu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNeu.Location = New System.Drawing.Point(20, 13)
        Me.btnNeu.Size = New System.Drawing.Size(100, 38)
        Me.btnNeu.TabIndex = 0
        Me.btnNeu.Text = "＋  Neu"
        Me.btnNeu.UseVisualStyleBackColor = False
        Me.btnNeu.Name = "btnNeu"
        '
        'btnSpeichern
        '
        Me.btnSpeichern.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnSpeichern.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSpeichern.FlatAppearance.BorderSize = 0
        Me.btnSpeichern.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnSpeichern.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpeichern.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSpeichern.ForeColor = System.Drawing.Color.White
        Me.btnSpeichern.Location = New System.Drawing.Point(150, 13)
        Me.btnSpeichern.Size = New System.Drawing.Size(130, 38)
        Me.btnSpeichern.TabIndex = 1
        Me.btnSpeichern.Text = "Speichern"
        Me.btnSpeichern.UseVisualStyleBackColor = False
        Me.btnSpeichern.Name = "btnSpeichern"
        '
        'pnlKopf
        '
        Me.pnlKopf.Controls.Add(Me.lblTitel)
        Me.pnlKopf.Controls.Add(Me.lblUntertitel)
        Me.pnlKopf.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.pnlKopf.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlKopf.Location = New System.Drawing.Point(0, 0)
        Me.pnlKopf.Size = New System.Drawing.Size(800, 64)
        Me.pnlKopf.TabIndex = 0
        Me.pnlKopf.Name = "pnlKopf"
        '
        'lblTitel
        '
        Me.lblTitel.AutoSize = True
        Me.lblTitel.Font = New System.Drawing.Font("Segoe UI", 15!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitel.ForeColor = System.Drawing.Color.White
        Me.lblTitel.Location = New System.Drawing.Point(18, 9)
        Me.lblTitel.Size = New System.Drawing.Size(200, 28)
        Me.lblTitel.TabIndex = 0
        Me.lblTitel.Text = "Benutzer verwalten"
        Me.lblTitel.Name = "lblTitel"
        '
        'lblUntertitel
        '
        Me.lblUntertitel.AutoSize = True
        Me.lblUntertitel.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUntertitel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblUntertitel.Location = New System.Drawing.Point(20, 38)
        Me.lblUntertitel.Size = New System.Drawing.Size(300, 15)
        Me.lblUntertitel.TabIndex = 1
        Me.lblUntertitel.Text = "Anmeldungen, Rollen und Passwörter (nur für Administratoren)"
        Me.lblUntertitel.Name = "lblUntertitel"
        '
        'FrmBenutzer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(820, 520)
        Me.Controls.Add(Me.pnlListe)
        Me.Controls.Add(Me.pnlTrenner)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.pnlKopf)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.MinimumSize = New System.Drawing.Size(760, 560)
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Benutzer verwalten"
        Me.Name = "FrmBenutzer"
        Me.pnlListe.ResumeLayout(False)
        Me.pnlListe.PerformLayout()
        Me.pnlTrenner.ResumeLayout(False)
        Me.pnlTrenner.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        Me.pnlDetailFuss.ResumeLayout(False)
        Me.pnlDetailFuss.PerformLayout()
        Me.pnlDetailFussLinie.ResumeLayout(False)
        Me.pnlDetailFussLinie.PerformLayout()
        Me.pnlKopf.ResumeLayout(False)
        Me.pnlKopf.PerformLayout()
        CType(Me.dgvBenutzer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlListe As Panel
    Friend WithEvents dgvBenutzer As DataGridView
    Friend WithEvents pnlTrenner As Panel
    Friend WithEvents pnlDetail As Panel
    Friend WithEvents lblDetailTitel As Label
    Friend WithEvents lblBenutzername As Label
    Friend WithEvents txtBenutzer As TextBox
    Friend WithEvents lblPasswort As Label
    Friend WithEvents txtPasswort As TextBox
    Friend WithEvents lblPasswortHinweis As Label
    Friend WithEvents lblRolle As Label
    Friend WithEvents cmbRolle As ComboBox
    Friend WithEvents chkAktiv As CheckBox
    Friend WithEvents lblInfo As Label
    Friend WithEvents pnlDetailFuss As Panel
    Friend WithEvents pnlDetailFussLinie As Panel
    Friend WithEvents btnNeu As Button
    Friend WithEvents btnSpeichern As Button
    Friend WithEvents pnlKopf As Panel
    Friend WithEvents lblTitel As Label
    Friend WithEvents lblUntertitel As Label
End Class
