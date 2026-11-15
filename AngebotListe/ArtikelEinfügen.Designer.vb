<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ArtikelEinfügen
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
        Me.dgvProdukte = New System.Windows.Forms.DataGridView()
        Me.pnlSuche = New System.Windows.Forms.Panel()
        Me.txtSuche = New System.Windows.Forms.TextBox()
        Me.chkNurAktive = New System.Windows.Forms.CheckBox()
        Me.lblAnzahl = New System.Windows.Forms.Label()
        Me.pnlTrenner = New System.Windows.Forms.Panel()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.lblDetailTitel = New System.Windows.Forms.Label()
        Me.picBild = New System.Windows.Forms.PictureBox()
        Me.lblBildHinweis = New System.Windows.Forms.Label()
        Me.lnkBildEntfernen = New System.Windows.Forms.LinkLabel()
        Me.lblArtNr = New System.Windows.Forms.Label()
        Me.txtArtNr = New System.Windows.Forms.TextBox()
        Me.lblPreis = New System.Windows.Forms.Label()
        Me.txtPreis = New System.Windows.Forms.TextBox()
        Me.chkAktiv = New System.Windows.Forms.CheckBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblBeschreibung = New System.Windows.Forms.Label()
        Me.txtBeschreibung = New System.Windows.Forms.TextBox()
        Me.lblKategorie = New System.Windows.Forms.Label()
        Me.cmbKategorie = New System.Windows.Forms.ComboBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlDetailFuss = New System.Windows.Forms.Panel()
        Me.pnlDetailFussLinie = New System.Windows.Forms.Panel()
        Me.btnNeu = New System.Windows.Forms.Button()
        Me.btnLoeschen = New System.Windows.Forms.Button()
        Me.btnSpeichern = New System.Windows.Forms.Button()
        Me.pnlKopf = New System.Windows.Forms.Panel()
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblUntertitel = New System.Windows.Forms.Label()
        Me.dlgBild = New System.Windows.Forms.OpenFileDialog()
        Me.pnlListe.SuspendLayout()
        Me.pnlSuche.SuspendLayout()
        Me.pnlTrenner.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        Me.pnlDetailFuss.SuspendLayout()
        Me.pnlDetailFussLinie.SuspendLayout()
        Me.pnlKopf.SuspendLayout()
        CType(Me.dgvProdukte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBild, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlListe
        '
        Me.pnlListe.Controls.Add(Me.dgvProdukte)
        Me.pnlListe.Controls.Add(Me.pnlSuche)
        Me.pnlListe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlListe.Location = New System.Drawing.Point(0, 64)
        Me.pnlListe.Padding = New System.Windows.Forms.Padding(16, 12, 16, 16)
        Me.pnlListe.Size = New System.Drawing.Size(719, 616)
        Me.pnlListe.TabIndex = 1
        Me.pnlListe.Name = "pnlListe"
        '
        'dgvProdukte
        '
        Me.dgvProdukte.AllowUserToAddRows = False
        Me.dgvProdukte.AllowUserToDeleteRows = False
        Me.dgvProdukte.AllowUserToResizeRows = False
        Me.dgvProdukte.BackgroundColor = System.Drawing.Color.White
        Me.dgvProdukte.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvProdukte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvProdukte.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvProdukte.ColumnHeadersHeight = 36
        Me.dgvProdukte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvProdukte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProdukte.EnableHeadersVisualStyles = False
        Me.dgvProdukte.GridColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgvProdukte.Location = New System.Drawing.Point(16, 60)
        Me.dgvProdukte.MultiSelect = False
        Me.dgvProdukte.ReadOnly = True
        Me.dgvProdukte.RowHeadersVisible = False
        Me.dgvProdukte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProdukte.Size = New System.Drawing.Size(687, 540)
        Me.dgvProdukte.TabIndex = 1
        Me.dgvProdukte.Name = "dgvProdukte"
        '
        'pnlSuche
        '
        Me.pnlSuche.Controls.Add(Me.txtSuche)
        Me.pnlSuche.Controls.Add(Me.chkNurAktive)
        Me.pnlSuche.Controls.Add(Me.lblAnzahl)
        Me.pnlSuche.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSuche.Location = New System.Drawing.Point(16, 12)
        Me.pnlSuche.Size = New System.Drawing.Size(687, 48)
        Me.pnlSuche.TabIndex = 0
        Me.pnlSuche.Name = "pnlSuche"
        '
        'txtSuche
        '
        Me.txtSuche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuche.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuche.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtSuche.Location = New System.Drawing.Point(0, 6)
        Me.txtSuche.Size = New System.Drawing.Size(300, 27)
        Me.txtSuche.TabIndex = 0
        Me.txtSuche.Name = "txtSuche"
        '
        'chkNurAktive
        '
        Me.chkNurAktive.AutoSize = True
        Me.chkNurAktive.Checked = True
        Me.chkNurAktive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNurAktive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkNurAktive.Location = New System.Drawing.Point(316, 9)
        Me.chkNurAktive.Size = New System.Drawing.Size(96, 21)
        Me.chkNurAktive.TabIndex = 1
        Me.chkNurAktive.Text = "Nur aktive"
        Me.chkNurAktive.UseVisualStyleBackColor = True
        Me.chkNurAktive.Name = "chkNurAktive"
        '
        'lblAnzahl
        '
        Me.lblAnzahl.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.lblAnzahl.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnzahl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblAnzahl.Location = New System.Drawing.Point(487, 10)
        Me.lblAnzahl.Size = New System.Drawing.Size(200, 20)
        Me.lblAnzahl.TabIndex = 2
        Me.lblAnzahl.Text = "0 Artikel"
        Me.lblAnzahl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAnzahl.Name = "lblAnzahl"
        '
        'pnlTrenner
        '
        Me.pnlTrenner.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlTrenner.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlTrenner.Location = New System.Drawing.Point(719, 64)
        Me.pnlTrenner.Size = New System.Drawing.Size(1, 616)
        Me.pnlTrenner.TabIndex = 2
        Me.pnlTrenner.Name = "pnlTrenner"
        '
        'pnlDetail
        '
        Me.pnlDetail.Controls.Add(Me.lblDetailTitel)
        Me.pnlDetail.Controls.Add(Me.picBild)
        Me.pnlDetail.Controls.Add(Me.lblBildHinweis)
        Me.pnlDetail.Controls.Add(Me.lnkBildEntfernen)
        Me.pnlDetail.Controls.Add(Me.lblArtNr)
        Me.pnlDetail.Controls.Add(Me.txtArtNr)
        Me.pnlDetail.Controls.Add(Me.lblPreis)
        Me.pnlDetail.Controls.Add(Me.txtPreis)
        Me.pnlDetail.Controls.Add(Me.chkAktiv)
        Me.pnlDetail.Controls.Add(Me.lblName)
        Me.pnlDetail.Controls.Add(Me.txtName)
        Me.pnlDetail.Controls.Add(Me.lblBeschreibung)
        Me.pnlDetail.Controls.Add(Me.txtBeschreibung)
        Me.pnlDetail.Controls.Add(Me.lblKategorie)
        Me.pnlDetail.Controls.Add(Me.cmbKategorie)
        Me.pnlDetail.Controls.Add(Me.lblInfo)
        Me.pnlDetail.Controls.Add(Me.pnlDetailFuss)
        Me.pnlDetail.BackColor = System.Drawing.Color.White
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDetail.Location = New System.Drawing.Point(720, 64)
        Me.pnlDetail.Size = New System.Drawing.Size(380, 616)
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
        Me.lblDetailTitel.Text = "Neuer Artikel"
        Me.lblDetailTitel.Name = "lblDetailTitel"
        '
        'picBild
        '
        Me.picBild.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.picBild.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picBild.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picBild.Location = New System.Drawing.Point(20, 44)
        Me.picBild.Size = New System.Drawing.Size(340, 170)
        Me.picBild.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBild.TabIndex = 1
        Me.picBild.TabStop = False
        Me.picBild.Name = "picBild"
        '
        'lblBildHinweis
        '
        Me.lblBildHinweis.AutoSize = True
        Me.lblBildHinweis.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBildHinweis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblBildHinweis.Location = New System.Drawing.Point(18, 218)
        Me.lblBildHinweis.Size = New System.Drawing.Size(220, 13)
        Me.lblBildHinweis.TabIndex = 2
        Me.lblBildHinweis.Text = "Bild hierher ziehen oder anklicken"
        Me.lblBildHinweis.Name = "lblBildHinweis"
        '
        'lnkBildEntfernen
        '
        Me.lnkBildEntfernen.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.lnkBildEntfernen.AutoSize = True
        Me.lnkBildEntfernen.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkBildEntfernen.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkBildEntfernen.LinkColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.lnkBildEntfernen.Location = New System.Drawing.Point(304, 218)
        Me.lnkBildEntfernen.Size = New System.Drawing.Size(56, 13)
        Me.lnkBildEntfernen.TabIndex = 3
        Me.lnkBildEntfernen.TabStop = True
        Me.lnkBildEntfernen.Text = "Entfernen"
        Me.lnkBildEntfernen.Name = "lnkBildEntfernen"
        '
        'lblArtNr
        '
        Me.lblArtNr.AutoSize = True
        Me.lblArtNr.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArtNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblArtNr.Location = New System.Drawing.Point(20, 246)
        Me.lblArtNr.Size = New System.Drawing.Size(200, 13)
        Me.lblArtNr.TabIndex = 4
        Me.lblArtNr.Text = "ARTIKEL-NR."
        Me.lblArtNr.Name = "lblArtNr"
        '
        'txtArtNr
        '
        Me.txtArtNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtArtNr.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArtNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtArtNr.Location = New System.Drawing.Point(20, 264)
        Me.txtArtNr.Size = New System.Drawing.Size(120, 25)
        Me.txtArtNr.TabIndex = 5
        Me.txtArtNr.Name = "txtArtNr"
        '
        'lblPreis
        '
        Me.lblPreis.AutoSize = True
        Me.lblPreis.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblPreis.Location = New System.Drawing.Point(156, 246)
        Me.lblPreis.Size = New System.Drawing.Size(200, 13)
        Me.lblPreis.TabIndex = 6
        Me.lblPreis.Text = "PREIS (€)"
        Me.lblPreis.Name = "lblPreis"
        '
        'txtPreis
        '
        Me.txtPreis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreis.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtPreis.Location = New System.Drawing.Point(156, 264)
        Me.txtPreis.Size = New System.Drawing.Size(100, 25)
        Me.txtPreis.TabIndex = 7
        Me.txtPreis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPreis.Name = "txtPreis"
        '
        'chkAktiv
        '
        Me.chkAktiv.AutoSize = True
        Me.chkAktiv.Checked = True
        Me.chkAktiv.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAktiv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkAktiv.Location = New System.Drawing.Point(276, 266)
        Me.chkAktiv.Size = New System.Drawing.Size(56, 21)
        Me.chkAktiv.TabIndex = 8
        Me.chkAktiv.Text = "Aktiv"
        Me.chkAktiv.UseVisualStyleBackColor = True
        Me.chkAktiv.Name = "chkAktiv"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(20, 302)
        Me.lblName.Size = New System.Drawing.Size(200, 13)
        Me.lblName.TabIndex = 9
        Me.lblName.Text = "BEZEICHNUNG *"
        Me.lblName.Name = "lblName"
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(20, 320)
        Me.txtName.Size = New System.Drawing.Size(340, 25)
        Me.txtName.TabIndex = 10
        Me.txtName.Name = "txtName"
        '
        'lblBeschreibung
        '
        Me.lblBeschreibung.AutoSize = True
        Me.lblBeschreibung.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeschreibung.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblBeschreibung.Location = New System.Drawing.Point(20, 358)
        Me.lblBeschreibung.Size = New System.Drawing.Size(200, 13)
        Me.lblBeschreibung.TabIndex = 11
        Me.lblBeschreibung.Text = "BESCHREIBUNG"
        Me.lblBeschreibung.Name = "lblBeschreibung"
        '
        'txtBeschreibung
        '
        Me.txtBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBeschreibung.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBeschreibung.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtBeschreibung.Location = New System.Drawing.Point(20, 376)
        Me.txtBeschreibung.Size = New System.Drawing.Size(340, 64)
        Me.txtBeschreibung.TabIndex = 12
        Me.txtBeschreibung.Multiline = True
        Me.txtBeschreibung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBeschreibung.Name = "txtBeschreibung"
        '
        'lblKategorie
        '
        Me.lblKategorie.AutoSize = True
        Me.lblKategorie.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKategorie.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblKategorie.Location = New System.Drawing.Point(20, 452)
        Me.lblKategorie.Size = New System.Drawing.Size(200, 13)
        Me.lblKategorie.TabIndex = 13
        Me.lblKategorie.Text = "KATEGORIE"
        Me.lblKategorie.Name = "lblKategorie"
        '
        'cmbKategorie
        '
        Me.cmbKategorie.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbKategorie.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbKategorie.FormattingEnabled = True
        Me.cmbKategorie.Items.AddRange(New Object() {"Obst", "Gemüse", "Fleisch", "Milchprodukte", "Backwaren", "Getränke", "Sonstige"})
        Me.cmbKategorie.Location = New System.Drawing.Point(20, 470)
        Me.cmbKategorie.Size = New System.Drawing.Size(340, 27)
        Me.cmbKategorie.TabIndex = 14
        Me.cmbKategorie.Name = "cmbKategorie"
        '
        'lblInfo
        '
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblInfo.Location = New System.Drawing.Point(18, 508)
        Me.lblInfo.Size = New System.Drawing.Size(342, 20)
        Me.lblInfo.TabIndex = 15
        Me.lblInfo.Text = "Noch nicht gespeichert"
        Me.lblInfo.Name = "lblInfo"
        '
        'pnlDetailFuss
        '
        Me.pnlDetailFuss.Controls.Add(Me.pnlDetailFussLinie)
        Me.pnlDetailFuss.Controls.Add(Me.btnNeu)
        Me.pnlDetailFuss.Controls.Add(Me.btnLoeschen)
        Me.pnlDetailFuss.Controls.Add(Me.btnSpeichern)
        Me.pnlDetailFuss.BackColor = System.Drawing.Color.White
        Me.pnlDetailFuss.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDetailFuss.Location = New System.Drawing.Point(0, 552)
        Me.pnlDetailFuss.Size = New System.Drawing.Size(380, 64)
        Me.pnlDetailFuss.TabIndex = 16
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
        Me.btnNeu.Size = New System.Drawing.Size(90, 38)
        Me.btnNeu.TabIndex = 0
        Me.btnNeu.Text = "＋  Neu"
        Me.btnNeu.UseVisualStyleBackColor = False
        Me.btnNeu.Name = "btnNeu"
        '
        'btnLoeschen
        '
        Me.btnLoeschen.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnLoeschen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLoeschen.FlatAppearance.BorderSize = 0
        Me.btnLoeschen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(179, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnLoeschen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoeschen.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoeschen.ForeColor = System.Drawing.Color.White
        Me.btnLoeschen.Location = New System.Drawing.Point(120, 13)
        Me.btnLoeschen.Size = New System.Drawing.Size(100, 38)
        Me.btnLoeschen.TabIndex = 1
        Me.btnLoeschen.Text = "Löschen"
        Me.btnLoeschen.UseVisualStyleBackColor = False
        Me.btnLoeschen.Name = "btnLoeschen"
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
        Me.btnSpeichern.Location = New System.Drawing.Point(230, 13)
        Me.btnSpeichern.Size = New System.Drawing.Size(130, 38)
        Me.btnSpeichern.TabIndex = 2
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
        Me.lblTitel.Text = "Produkte verwalten"
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
        Me.lblUntertitel.Text = "Artikel anlegen, bearbeiten und deaktivieren"
        Me.lblUntertitel.Name = "lblUntertitel"
        '
        'dlgBild
        '
        Me.dlgBild.Filter = "Bilddateien (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
        Me.dlgBild.Title = "Bild auswählen"
        '
        'ArtikelEinfügen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1100, 680)
        Me.Controls.Add(Me.pnlListe)
        Me.Controls.Add(Me.pnlTrenner)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.pnlKopf)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.MinimumSize = New System.Drawing.Size(920, 720)
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Produkte verwalten"
        Me.KeyPreview = True
        Me.Name = "ArtikelEinfügen"
        Me.pnlListe.ResumeLayout(False)
        Me.pnlListe.PerformLayout()
        Me.pnlSuche.ResumeLayout(False)
        Me.pnlSuche.PerformLayout()
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
        CType(Me.dgvProdukte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBild, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlListe As Panel
    Friend WithEvents dgvProdukte As DataGridView
    Friend WithEvents pnlSuche As Panel
    Friend WithEvents txtSuche As TextBox
    Friend WithEvents chkNurAktive As CheckBox
    Friend WithEvents lblAnzahl As Label
    Friend WithEvents pnlTrenner As Panel
    Friend WithEvents pnlDetail As Panel
    Friend WithEvents lblDetailTitel As Label
    Friend WithEvents picBild As PictureBox
    Friend WithEvents lblBildHinweis As Label
    Friend WithEvents lnkBildEntfernen As LinkLabel
    Friend WithEvents lblArtNr As Label
    Friend WithEvents txtArtNr As TextBox
    Friend WithEvents lblPreis As Label
    Friend WithEvents txtPreis As TextBox
    Friend WithEvents chkAktiv As CheckBox
    Friend WithEvents lblName As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents lblBeschreibung As Label
    Friend WithEvents txtBeschreibung As TextBox
    Friend WithEvents lblKategorie As Label
    Friend WithEvents cmbKategorie As ComboBox
    Friend WithEvents lblInfo As Label
    Friend WithEvents pnlDetailFuss As Panel
    Friend WithEvents pnlDetailFussLinie As Panel
    Friend WithEvents btnNeu As Button
    Friend WithEvents btnLoeschen As Button
    Friend WithEvents btnSpeichern As Button
    Friend WithEvents pnlKopf As Panel
    Friend WithEvents lblTitel As Label
    Friend WithEvents lblUntertitel As Label
    Friend WithEvents dlgBild As OpenFileDialog
End Class
