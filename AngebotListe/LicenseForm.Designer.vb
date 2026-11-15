<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LicenseForm
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

        Me.pnlKopf = New System.Windows.Forms.Panel()
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblHinweis = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlKopf.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlKopf
        '
        Me.pnlKopf.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.pnlKopf.Controls.Add(Me.lblHinweis)
        Me.pnlKopf.Controls.Add(Me.lblTitel)
        Me.pnlKopf.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlKopf.Location = New System.Drawing.Point(0, 0)
        Me.pnlKopf.Name = "pnlKopf"
        Me.pnlKopf.Size = New System.Drawing.Size(440, 96)
        Me.pnlKopf.TabIndex = 0
        '
        'lblTitel
        '
        Me.lblTitel.AutoSize = True
        Me.lblTitel.Font = New System.Drawing.Font("Segoe UI", 16!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitel.ForeColor = System.Drawing.Color.White
        Me.lblTitel.Location = New System.Drawing.Point(30, 22)
        Me.lblTitel.Name = "lblTitel"
        Me.lblTitel.Size = New System.Drawing.Size(190, 30)
        Me.lblTitel.TabIndex = 0
        Me.lblTitel.Text = "Lizenz aktivieren"
        '
        'lblHinweis
        '
        Me.lblHinweis.AutoSize = True
        Me.lblHinweis.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHinweis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblHinweis.Location = New System.Drawing.Point(32, 58)
        Me.lblHinweis.Name = "lblHinweis"
        Me.lblHinweis.Size = New System.Drawing.Size(330, 17)
        Me.lblHinweis.TabIndex = 1
        Me.lblHinweis.Text = "Für diesen Computer ist noch keine gültige Lizenz hinterlegt."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(32, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Lizenzschlüssel"
        '
        'txtKey
        '
        Me.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKey.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtKey.Location = New System.Drawing.Point(32, 140)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(376, 29)
        Me.txtKey.TabIndex = 2
        '
        'btnValidate
        '
        Me.btnValidate.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnValidate.FlatAppearance.BorderSize = 0
        Me.btnValidate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnValidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnValidate.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.ForeColor = System.Drawing.Color.White
        Me.btnValidate.Location = New System.Drawing.Point(32, 186)
        Me.btnValidate.Size = New System.Drawing.Size(376, 42)
        Me.btnValidate.TabIndex = 3
        Me.btnValidate.Text = "Lizenz prüfen und starten"
        Me.btnValidate.UseVisualStyleBackColor = False
        '
        'lblInfo
        '
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblInfo.Location = New System.Drawing.Point(32, 240)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(376, 36)
        Me.lblInfo.TabIndex = 4
        Me.lblInfo.Text = "Die Lizenz wird beim ersten Start an diesen Computer gebunden. Fragen zur Lizenz: Tel. 0157-71562066"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LicenseForm
        '
        Me.AcceptButton = Me.btnValidate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(440, 290)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.btnValidate)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlKopf)
        Me.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenseForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lizenz – AngebotsListenManager"
        Me.pnlKopf.ResumeLayout(False)
        Me.pnlKopf.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlKopf As Panel
    Friend WithEvents lblTitel As Label
    Friend WithEvents lblHinweis As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtKey As TextBox
    Friend WithEvents btnValidate As Button
    Friend WithEvents lblInfo As Label
End Class
