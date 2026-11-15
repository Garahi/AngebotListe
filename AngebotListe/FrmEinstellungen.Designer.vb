<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEinstellungen
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
        Me.pnlInhalt = New System.Windows.Forms.Panel()
        Me.lblFirma = New System.Windows.Forms.Label()
        Me.txtFirma = New System.Windows.Forms.TextBox()
        Me.lblPdfOrdner = New System.Windows.Forms.Label()
        Me.txtPdfOrdner = New System.Windows.Forms.TextBox()
        Me.btnDurchsuchen = New System.Windows.Forms.Button()
        Me.lblKontakte = New System.Windows.Forms.Label()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTelefon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvKontakte = New System.Windows.Forms.DataGridView()
        Me.lblKontakteHinweis = New System.Windows.Forms.Label()
        Me.pnlFuss = New System.Windows.Forms.Panel()
        Me.pnlFussLinie = New System.Windows.Forms.Panel()
        Me.btnAbbrechen = New System.Windows.Forms.Button()
        Me.btnSpeichern = New System.Windows.Forms.Button()
        Me.pnlKopf = New System.Windows.Forms.Panel()
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblUntertitel = New System.Windows.Forms.Label()
        Me.dlgOrdner = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlInhalt.SuspendLayout()
        Me.pnlFuss.SuspendLayout()
        Me.pnlFussLinie.SuspendLayout()
        Me.pnlKopf.SuspendLayout()
        CType(Me.dgvKontakte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlInhalt
        '
        Me.pnlInhalt.Controls.Add(Me.lblFirma)
        Me.pnlInhalt.Controls.Add(Me.txtFirma)
        Me.pnlInhalt.Controls.Add(Me.lblPdfOrdner)
        Me.pnlInhalt.Controls.Add(Me.txtPdfOrdner)
        Me.pnlInhalt.Controls.Add(Me.btnDurchsuchen)
        Me.pnlInhalt.Controls.Add(Me.lblKontakte)
        Me.pnlInhalt.Controls.Add(Me.dgvKontakte)
        Me.pnlInhalt.Controls.Add(Me.lblKontakteHinweis)
        Me.pnlInhalt.BackColor = System.Drawing.Color.White
        Me.pnlInhalt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInhalt.Location = New System.Drawing.Point(0, 64)
        Me.pnlInhalt.Size = New System.Drawing.Size(560, 418)
        Me.pnlInhalt.TabIndex = 1
        Me.pnlInhalt.Name = "pnlInhalt"
        '
        'lblFirma
        '
        Me.lblFirma.AutoSize = True
        Me.lblFirma.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirma.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblFirma.Location = New System.Drawing.Point(24, 20)
        Me.lblFirma.Size = New System.Drawing.Size(200, 13)
        Me.lblFirma.TabIndex = 0
        Me.lblFirma.Text = "FIRMENNAME (AUF DEN PRODUKTKARTEN)"
        Me.lblFirma.Name = "lblFirma"
        '
        'txtFirma
        '
        Me.txtFirma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFirma.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirma.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtFirma.Location = New System.Drawing.Point(24, 38)
        Me.txtFirma.Size = New System.Drawing.Size(512, 25)
        Me.txtFirma.TabIndex = 1
        Me.txtFirma.Name = "txtFirma"
        '
        'lblPdfOrdner
        '
        Me.lblPdfOrdner.AutoSize = True
        Me.lblPdfOrdner.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPdfOrdner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblPdfOrdner.Location = New System.Drawing.Point(24, 80)
        Me.lblPdfOrdner.Size = New System.Drawing.Size(200, 13)
        Me.lblPdfOrdner.TabIndex = 2
        Me.lblPdfOrdner.Text = "ORDNER FÜR PDF-DATEIEN UND BILDER"
        Me.lblPdfOrdner.Name = "lblPdfOrdner"
        '
        'txtPdfOrdner
        '
        Me.txtPdfOrdner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPdfOrdner.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPdfOrdner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtPdfOrdner.Location = New System.Drawing.Point(24, 98)
        Me.txtPdfOrdner.Size = New System.Drawing.Size(396, 25)
        Me.txtPdfOrdner.TabIndex = 3
        Me.txtPdfOrdner.Name = "txtPdfOrdner"
        '
        'btnDurchsuchen
        '
        Me.btnDurchsuchen.BackColor = System.Drawing.Color.White
        Me.btnDurchsuchen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDurchsuchen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnDurchsuchen.FlatAppearance.BorderSize = 1
        Me.btnDurchsuchen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnDurchsuchen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDurchsuchen.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDurchsuchen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnDurchsuchen.Location = New System.Drawing.Point(428, 97)
        Me.btnDurchsuchen.Size = New System.Drawing.Size(108, 27)
        Me.btnDurchsuchen.TabIndex = 4
        Me.btnDurchsuchen.Text = "Durchsuchen…"
        Me.btnDurchsuchen.UseVisualStyleBackColor = False
        Me.btnDurchsuchen.Name = "btnDurchsuchen"
        '
        'lblKontakte
        '
        Me.lblKontakte.AutoSize = True
        Me.lblKontakte.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKontakte.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblKontakte.Location = New System.Drawing.Point(24, 142)
        Me.lblKontakte.Size = New System.Drawing.Size(200, 13)
        Me.lblKontakte.TabIndex = 5
        Me.lblKontakte.Text = "WHATSAPP-KONTAKTE"
        Me.lblKontakte.Name = "lblKontakte"
        '
        'colName
        '
        Me.colName.FillWeight = 55.0!
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        '
        'colTelefon
        '
        Me.colTelefon.FillWeight = 45.0!
        Me.colTelefon.HeaderText = "Telefon"
        Me.colTelefon.Name = "colTelefon"
        '
        'dgvKontakte
        '
        Me.dgvKontakte.AllowUserToAddRows = True
        Me.dgvKontakte.AllowUserToDeleteRows = True
        Me.dgvKontakte.AllowUserToResizeRows = False
        Me.dgvKontakte.BackgroundColor = System.Drawing.Color.White
        Me.dgvKontakte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgvKontakte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvKontakte.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvKontakte.ColumnHeadersHeight = 36
        Me.dgvKontakte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvKontakte.Dock = System.Windows.Forms.DockStyle.None
        Me.dgvKontakte.EnableHeadersVisualStyles = False
        Me.dgvKontakte.GridColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgvKontakte.Location = New System.Drawing.Point(24, 160)
        Me.dgvKontakte.MultiSelect = False
        Me.dgvKontakte.ReadOnly = False
        Me.dgvKontakte.RowHeadersVisible = False
        Me.dgvKontakte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvKontakte.Size = New System.Drawing.Size(512, 190)
        Me.dgvKontakte.TabIndex = 6
        Me.dgvKontakte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvKontakte.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colTelefon})
        Me.dgvKontakte.Name = "dgvKontakte"
        '
        'lblKontakteHinweis
        '
        Me.lblKontakteHinweis.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKontakteHinweis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblKontakteHinweis.Location = New System.Drawing.Point(22, 356)
        Me.lblKontakteHinweis.Size = New System.Drawing.Size(514, 34)
        Me.lblKontakteHinweis.TabIndex = 7
        Me.lblKontakteHinweis.Text = "Telefon mit Ländervorwahl, ohne + und Leerzeichen (z. B. 4917612345678). Neue Kontakte in der leeren Zeile unten eintragen, Entf löscht eine Zeile."
        Me.lblKontakteHinweis.Name = "lblKontakteHinweis"
        '
        'pnlFuss
        '
        Me.pnlFuss.Controls.Add(Me.pnlFussLinie)
        Me.pnlFuss.Controls.Add(Me.btnAbbrechen)
        Me.pnlFuss.Controls.Add(Me.btnSpeichern)
        Me.pnlFuss.BackColor = System.Drawing.Color.White
        Me.pnlFuss.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFuss.Location = New System.Drawing.Point(0, 482)
        Me.pnlFuss.Size = New System.Drawing.Size(560, 64)
        Me.pnlFuss.TabIndex = 2
        Me.pnlFuss.Name = "pnlFuss"
        '
        'pnlFussLinie
        '
        Me.pnlFussLinie.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlFussLinie.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFussLinie.Location = New System.Drawing.Point(0, 0)
        Me.pnlFussLinie.Size = New System.Drawing.Size(800, 1)
        Me.pnlFussLinie.TabIndex = 0
        Me.pnlFussLinie.Name = "pnlFussLinie"
        '
        'btnAbbrechen
        '
        Me.btnAbbrechen.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.btnAbbrechen.BackColor = System.Drawing.Color.White
        Me.btnAbbrechen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAbbrechen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnAbbrechen.FlatAppearance.BorderSize = 1
        Me.btnAbbrechen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnAbbrechen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbbrechen.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbbrechen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnAbbrechen.Location = New System.Drawing.Point(276, 13)
        Me.btnAbbrechen.Size = New System.Drawing.Size(120, 38)
        Me.btnAbbrechen.TabIndex = 0
        Me.btnAbbrechen.Text = "Abbrechen"
        Me.btnAbbrechen.UseVisualStyleBackColor = False
        Me.btnAbbrechen.Name = "btnAbbrechen"
        '
        'btnSpeichern
        '
        Me.btnSpeichern.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.btnSpeichern.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnSpeichern.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSpeichern.FlatAppearance.BorderSize = 0
        Me.btnSpeichern.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnSpeichern.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpeichern.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSpeichern.ForeColor = System.Drawing.Color.White
        Me.btnSpeichern.Location = New System.Drawing.Point(406, 13)
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
        Me.lblTitel.Text = "Einstellungen"
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
        Me.lblUntertitel.Text = "Firmenname, Speicherort und WhatsApp-Kontakte"
        Me.lblUntertitel.Name = "lblUntertitel"
        '
        'dlgOrdner
        '
        Me.dlgOrdner.Description = "Ordner für PDF-Dateien und Bilder wählen"
        '
        'FrmEinstellungen
        '
        Me.AcceptButton = Me.btnSpeichern
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.CancelButton = Me.btnAbbrechen
        Me.ClientSize = New System.Drawing.Size(560, 546)
        Me.Controls.Add(Me.pnlInhalt)
        Me.Controls.Add(Me.pnlFuss)
        Me.Controls.Add(Me.pnlKopf)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Einstellungen"
        Me.Name = "FrmEinstellungen"
        Me.pnlInhalt.ResumeLayout(False)
        Me.pnlInhalt.PerformLayout()
        Me.pnlFuss.ResumeLayout(False)
        Me.pnlFuss.PerformLayout()
        Me.pnlFussLinie.ResumeLayout(False)
        Me.pnlFussLinie.PerformLayout()
        Me.pnlKopf.ResumeLayout(False)
        Me.pnlKopf.PerformLayout()
        CType(Me.dgvKontakte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlInhalt As Panel
    Friend WithEvents lblFirma As Label
    Friend WithEvents txtFirma As TextBox
    Friend WithEvents lblPdfOrdner As Label
    Friend WithEvents txtPdfOrdner As TextBox
    Friend WithEvents btnDurchsuchen As Button
    Friend WithEvents lblKontakte As Label
    Friend WithEvents colName As DataGridViewTextBoxColumn
    Friend WithEvents colTelefon As DataGridViewTextBoxColumn
    Friend WithEvents dgvKontakte As DataGridView
    Friend WithEvents lblKontakteHinweis As Label
    Friend WithEvents pnlFuss As Panel
    Friend WithEvents pnlFussLinie As Panel
    Friend WithEvents btnAbbrechen As Button
    Friend WithEvents btnSpeichern As Button
    Friend WithEvents pnlKopf As Panel
    Friend WithEvents lblTitel As Label
    Friend WithEvents lblUntertitel As Label
    Friend WithEvents dlgOrdner As FolderBrowserDialog
End Class
