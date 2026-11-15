<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProduktSuche
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
        Me.dgvErgebnisse = New System.Windows.Forms.DataGridView()
        Me.pnlSuche = New System.Windows.Forms.Panel()
        Me.txtSuche = New System.Windows.Forms.TextBox()
        Me.lblAnzahl = New System.Windows.Forms.Label()
        Me.pnlFuss = New System.Windows.Forms.Panel()
        Me.pnlFussLinie = New System.Windows.Forms.Panel()
        Me.btnVerwalten = New System.Windows.Forms.Button()
        Me.lblAuswahl = New System.Windows.Forms.Label()
        Me.btnAbbrechen = New System.Windows.Forms.Button()
        Me.btnUebernehmen = New System.Windows.Forms.Button()
        Me.pnlKopf = New System.Windows.Forms.Panel()
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblUntertitel = New System.Windows.Forms.Label()
        Me.pnlInhalt.SuspendLayout()
        Me.pnlSuche.SuspendLayout()
        Me.pnlFuss.SuspendLayout()
        Me.pnlFussLinie.SuspendLayout()
        Me.pnlKopf.SuspendLayout()
        CType(Me.dgvErgebnisse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlInhalt
        '
        Me.pnlInhalt.Controls.Add(Me.dgvErgebnisse)
        Me.pnlInhalt.Controls.Add(Me.pnlSuche)
        Me.pnlInhalt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInhalt.Location = New System.Drawing.Point(0, 64)
        Me.pnlInhalt.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
        Me.pnlInhalt.Size = New System.Drawing.Size(860, 432)
        Me.pnlInhalt.TabIndex = 1
        Me.pnlInhalt.Name = "pnlInhalt"
        '
        'dgvErgebnisse
        '
        Me.dgvErgebnisse.AllowUserToAddRows = False
        Me.dgvErgebnisse.AllowUserToDeleteRows = False
        Me.dgvErgebnisse.AllowUserToResizeRows = False
        Me.dgvErgebnisse.BackgroundColor = System.Drawing.Color.White
        Me.dgvErgebnisse.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvErgebnisse.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvErgebnisse.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvErgebnisse.ColumnHeadersHeight = 36
        Me.dgvErgebnisse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvErgebnisse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvErgebnisse.EnableHeadersVisualStyles = False
        Me.dgvErgebnisse.GridColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgvErgebnisse.Location = New System.Drawing.Point(16, 60)
        Me.dgvErgebnisse.MultiSelect = False
        Me.dgvErgebnisse.ReadOnly = True
        Me.dgvErgebnisse.RowHeadersVisible = False
        Me.dgvErgebnisse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvErgebnisse.Size = New System.Drawing.Size(828, 360)
        Me.dgvErgebnisse.TabIndex = 1
        Me.dgvErgebnisse.Name = "dgvErgebnisse"
        '
        'pnlSuche
        '
        Me.pnlSuche.Controls.Add(Me.txtSuche)
        Me.pnlSuche.Controls.Add(Me.lblAnzahl)
        Me.pnlSuche.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSuche.Location = New System.Drawing.Point(16, 12)
        Me.pnlSuche.Size = New System.Drawing.Size(828, 48)
        Me.pnlSuche.TabIndex = 0
        Me.pnlSuche.Name = "pnlSuche"
        '
        'txtSuche
        '
        Me.txtSuche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuche.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuche.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtSuche.Location = New System.Drawing.Point(0, 6)
        Me.txtSuche.Size = New System.Drawing.Size(360, 27)
        Me.txtSuche.TabIndex = 0
        Me.txtSuche.Name = "txtSuche"
        '
        'lblAnzahl
        '
        Me.lblAnzahl.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.lblAnzahl.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnzahl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblAnzahl.Location = New System.Drawing.Point(628, 10)
        Me.lblAnzahl.Size = New System.Drawing.Size(200, 20)
        Me.lblAnzahl.TabIndex = 1
        Me.lblAnzahl.Text = "0 Produkte"
        Me.lblAnzahl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAnzahl.Name = "lblAnzahl"
        '
        'pnlFuss
        '
        Me.pnlFuss.Controls.Add(Me.pnlFussLinie)
        Me.pnlFuss.Controls.Add(Me.btnVerwalten)
        Me.pnlFuss.Controls.Add(Me.lblAuswahl)
        Me.pnlFuss.Controls.Add(Me.btnAbbrechen)
        Me.pnlFuss.Controls.Add(Me.btnUebernehmen)
        Me.pnlFuss.BackColor = System.Drawing.Color.White
        Me.pnlFuss.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFuss.Location = New System.Drawing.Point(0, 496)
        Me.pnlFuss.Size = New System.Drawing.Size(860, 64)
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
        'btnVerwalten
        '
        Me.btnVerwalten.BackColor = System.Drawing.Color.White
        Me.btnVerwalten.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVerwalten.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnVerwalten.FlatAppearance.BorderSize = 1
        Me.btnVerwalten.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnVerwalten.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVerwalten.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerwalten.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnVerwalten.Location = New System.Drawing.Point(16, 13)
        Me.btnVerwalten.Size = New System.Drawing.Size(180, 38)
        Me.btnVerwalten.TabIndex = 0
        Me.btnVerwalten.Text = "Produkte verwalten…"
        Me.btnVerwalten.UseVisualStyleBackColor = False
        Me.btnVerwalten.Name = "btnVerwalten"
        '
        'lblAuswahl
        '
        Me.lblAuswahl.AutoSize = True
        Me.lblAuswahl.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuswahl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblAuswahl.Location = New System.Drawing.Point(212, 24)
        Me.lblAuswahl.Size = New System.Drawing.Size(0, 17)
        Me.lblAuswahl.TabIndex = 3
        Me.lblAuswahl.Text = ""
        Me.lblAuswahl.Name = "lblAuswahl"
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
        Me.btnAbbrechen.Location = New System.Drawing.Point(574, 13)
        Me.btnAbbrechen.Size = New System.Drawing.Size(120, 38)
        Me.btnAbbrechen.TabIndex = 1
        Me.btnAbbrechen.Text = "Abbrechen"
        Me.btnAbbrechen.UseVisualStyleBackColor = False
        Me.btnAbbrechen.Name = "btnAbbrechen"
        '
        'btnUebernehmen
        '
        Me.btnUebernehmen.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.btnUebernehmen.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnUebernehmen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUebernehmen.FlatAppearance.BorderSize = 0
        Me.btnUebernehmen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnUebernehmen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUebernehmen.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUebernehmen.ForeColor = System.Drawing.Color.White
        Me.btnUebernehmen.Location = New System.Drawing.Point(704, 13)
        Me.btnUebernehmen.Size = New System.Drawing.Size(140, 38)
        Me.btnUebernehmen.TabIndex = 2
        Me.btnUebernehmen.Text = "Übernehmen"
        Me.btnUebernehmen.UseVisualStyleBackColor = False
        Me.btnUebernehmen.Name = "btnUebernehmen"
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
        Me.lblTitel.Text = "Produkt auswählen"
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
        Me.lblUntertitel.Text = "Doppelklick oder Enter übernimmt das Produkt in die Karte"
        Me.lblUntertitel.Name = "lblUntertitel"
        '
        'FrmProduktSuche
        '
        Me.AcceptButton = Me.btnUebernehmen
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.CancelButton = Me.btnAbbrechen
        Me.ClientSize = New System.Drawing.Size(860, 560)
        Me.Controls.Add(Me.pnlInhalt)
        Me.Controls.Add(Me.pnlFuss)
        Me.Controls.Add(Me.pnlKopf)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.MinimumSize = New System.Drawing.Size(700, 480)
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Produkt auswählen"
        Me.KeyPreview = True
        Me.Name = "FrmProduktSuche"
        Me.pnlInhalt.ResumeLayout(False)
        Me.pnlInhalt.PerformLayout()
        Me.pnlSuche.ResumeLayout(False)
        Me.pnlSuche.PerformLayout()
        Me.pnlFuss.ResumeLayout(False)
        Me.pnlFuss.PerformLayout()
        Me.pnlFussLinie.ResumeLayout(False)
        Me.pnlFussLinie.PerformLayout()
        Me.pnlKopf.ResumeLayout(False)
        Me.pnlKopf.PerformLayout()
        CType(Me.dgvErgebnisse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlInhalt As Panel
    Friend WithEvents dgvErgebnisse As DataGridView
    Friend WithEvents pnlSuche As Panel
    Friend WithEvents txtSuche As TextBox
    Friend WithEvents lblAnzahl As Label
    Friend WithEvents pnlFuss As Panel
    Friend WithEvents pnlFussLinie As Panel
    Friend WithEvents btnVerwalten As Button
    Friend WithEvents lblAuswahl As Label
    Friend WithEvents btnAbbrechen As Button
    Friend WithEvents btnUebernehmen As Button
    Friend WithEvents pnlKopf As Panel
    Friend WithEvents lblTitel As Label
    Friend WithEvents lblUntertitel As Label
End Class
