<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NeuesAngebotListeErstellen
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

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NeuesAngebotListeErstellen))
        Me.pnlSeite = New System.Windows.Forms.Panel()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.lblAppUnter = New System.Windows.Forms.Label()
        Me.navAngebote = New AngebotListe.NavKnopf()
        Me.navProdukte = New AngebotListe.NavKnopf()
        Me.navEinstellungen = New AngebotListe.NavKnopf()
        Me.navBenutzer = New AngebotListe.NavKnopf()
        Me.pnlBenutzerBox = New System.Windows.Forms.Panel()
        Me.lblInitialen = New System.Windows.Forms.Label()
        Me.lblBenutzerName = New System.Windows.Forms.Label()
        Me.lnkAbmelden = New System.Windows.Forms.LinkLabel()
        Me.pnlAngebot = New System.Windows.Forms.Panel()
        Me.pnlHost = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSpeicherStatus = New System.Windows.Forms.Label()
        Me.lnkPositionsliste = New System.Windows.Forms.LinkLabel()
        Me.lblTitel = New System.Windows.Forms.Label()
        Me.lblUntertitel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAngebotNr = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDatum = New System.Windows.Forms.DateTimePicker()
        Me.lblAngebotOeffnen = New System.Windows.Forms.Label()
        Me.cmbAngebotNr = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.pnlFooterLinie = New System.Windows.Forms.Panel()
        Me.flpLinks = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.btnProdukteHinzufuegen = New System.Windows.Forms.Button()
        Me.btnNeueProdukt = New System.Windows.Forms.Button()
        Me.flpRechts = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button18 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.btnSpeichern = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.pnlInhalt = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlAbstand = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnPDF = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlSeite.SuspendLayout()
        Me.pnlBenutzerBox.SuspendLayout()
        Me.pnlAngebot.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.flpLinks.SuspendLayout()
        Me.flpRechts.SuspendLayout()
        Me.pnlInhalt.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSeite
        '
        Me.pnlSeite.Controls.Add(Me.navBenutzer)
        Me.pnlSeite.Controls.Add(Me.navEinstellungen)
        Me.pnlSeite.Controls.Add(Me.navProdukte)
        Me.pnlSeite.Controls.Add(Me.navAngebote)
        Me.pnlSeite.Controls.Add(Me.lblAppUnter)
        Me.pnlSeite.Controls.Add(Me.lblAppName)
        Me.pnlSeite.Controls.Add(Me.pnlBenutzerBox)
        Me.pnlSeite.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.pnlSeite.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSeite.Location = New System.Drawing.Point(0, 0)
        Me.pnlSeite.Name = "pnlSeite"
        Me.pnlSeite.Size = New System.Drawing.Size(210, 800)
        Me.pnlSeite.TabIndex = 20
        '
        'lblAppName
        '
        Me.lblAppName.AutoSize = True
        Me.lblAppName.Font = New System.Drawing.Font("Segoe UI", 14!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppName.ForeColor = System.Drawing.Color.White
        Me.lblAppName.Location = New System.Drawing.Point(18, 22)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(130, 25)
        Me.lblAppName.TabIndex = 0
        Me.lblAppName.Text = "Angebotsliste"
        '
        'lblAppUnter
        '
        Me.lblAppUnter.AutoSize = True
        Me.lblAppUnter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppUnter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblAppUnter.Location = New System.Drawing.Point(20, 50)
        Me.lblAppUnter.Name = "lblAppUnter"
        Me.lblAppUnter.Size = New System.Drawing.Size(130, 13)
        Me.lblAppUnter.TabIndex = 1
        Me.lblAppUnter.Text = "AngebotsListenManager"
        '
        'navAngebote
        '
        Me.navAngebote.Aktiv = True
        Me.navAngebote.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        Me.navAngebote.Location = New System.Drawing.Point(12, 92)
        Me.navAngebote.Name = "navAngebote"
        Me.navAngebote.Size = New System.Drawing.Size(186, 40)
        Me.navAngebote.SymbolCode = "E8A5"
        Me.navAngebote.TabIndex = 2
        Me.navAngebote.Text = "Angebote"
        '
        'navProdukte
        '
        Me.navProdukte.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        Me.navProdukte.Location = New System.Drawing.Point(12, 138)
        Me.navProdukte.Name = "navProdukte"
        Me.navProdukte.Size = New System.Drawing.Size(186, 40)
        Me.navProdukte.SymbolCode = "E7B8"
        Me.navProdukte.TabIndex = 3
        Me.navProdukte.Text = "Produkte"
        '
        'navEinstellungen
        '
        Me.navEinstellungen.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        Me.navEinstellungen.Location = New System.Drawing.Point(12, 184)
        Me.navEinstellungen.Name = "navEinstellungen"
        Me.navEinstellungen.Size = New System.Drawing.Size(186, 40)
        Me.navEinstellungen.SymbolCode = "E713"
        Me.navEinstellungen.TabIndex = 4
        Me.navEinstellungen.Text = "Einstellungen"
        '
        'navBenutzer
        '
        Me.navBenutzer.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
        Me.navBenutzer.Location = New System.Drawing.Point(12, 230)
        Me.navBenutzer.Name = "navBenutzer"
        Me.navBenutzer.Size = New System.Drawing.Size(186, 40)
        Me.navBenutzer.SymbolCode = "E716"
        Me.navBenutzer.TabIndex = 5
        Me.navBenutzer.Text = "Benutzer"
        '
        'pnlBenutzerBox
        '
        Me.pnlBenutzerBox.Controls.Add(Me.lnkAbmelden)
        Me.pnlBenutzerBox.Controls.Add(Me.lblBenutzerName)
        Me.pnlBenutzerBox.Controls.Add(Me.lblInitialen)
        Me.pnlBenutzerBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.pnlBenutzerBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBenutzerBox.Location = New System.Drawing.Point(0, 732)
        Me.pnlBenutzerBox.Name = "pnlBenutzerBox"
        Me.pnlBenutzerBox.Size = New System.Drawing.Size(210, 68)
        Me.pnlBenutzerBox.TabIndex = 6
        '
        'lblInitialen
        '
        Me.lblInitialen.BackColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.lblInitialen.Font = New System.Drawing.Font("Segoe UI", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInitialen.ForeColor = System.Drawing.Color.White
        Me.lblInitialen.Location = New System.Drawing.Point(16, 16)
        Me.lblInitialen.Name = "lblInitialen"
        Me.lblInitialen.Size = New System.Drawing.Size(36, 36)
        Me.lblInitialen.TabIndex = 0
        Me.lblInitialen.Text = "AD"
        Me.lblInitialen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBenutzerName
        '
        Me.lblBenutzerName.AutoEllipsis = True
        Me.lblBenutzerName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBenutzerName.ForeColor = System.Drawing.Color.White
        Me.lblBenutzerName.Location = New System.Drawing.Point(62, 15)
        Me.lblBenutzerName.Name = "lblBenutzerName"
        Me.lblBenutzerName.Size = New System.Drawing.Size(140, 18)
        Me.lblBenutzerName.TabIndex = 1
        Me.lblBenutzerName.Text = "admin"
        '
        'lnkAbmelden
        '
        Me.lnkAbmelden.ActiveLinkColor = System.Drawing.Color.White
        Me.lnkAbmelden.AutoSize = True
        Me.lnkAbmelden.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkAbmelden.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkAbmelden.LinkColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lnkAbmelden.Location = New System.Drawing.Point(62, 35)
        Me.lnkAbmelden.Name = "lnkAbmelden"
        Me.lnkAbmelden.Size = New System.Drawing.Size(60, 13)
        Me.lnkAbmelden.TabIndex = 2
        Me.lnkAbmelden.TabStop = True
        Me.lnkAbmelden.Text = "Abmelden"
        '
        'pnlAngebot
        '
        Me.pnlAngebot.Controls.Add(Me.pnlInhalt)
        Me.pnlAngebot.Controls.Add(Me.pnlFooter)
        Me.pnlAngebot.Controls.Add(Me.pnlHeader)
        Me.pnlAngebot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAngebot.Location = New System.Drawing.Point(210, 0)
        Me.pnlAngebot.Name = "pnlAngebot"
        Me.pnlAngebot.Size = New System.Drawing.Size(1180, 800)
        Me.pnlAngebot.TabIndex = 0
        '
        'pnlHost
        '
        Me.pnlHost.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHost.Location = New System.Drawing.Point(210, 0)
        Me.pnlHost.Name = "pnlHost"
        Me.pnlHost.Size = New System.Drawing.Size(1180, 800)
        Me.pnlHost.TabIndex = 21
        Me.pnlHost.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lnkPositionsliste)
        Me.pnlHeader.Controls.Add(Me.lblSpeicherStatus)
        Me.pnlHeader.Controls.Add(Me.Label3)
        Me.pnlHeader.Controls.Add(Me.cmbAngebotNr)
        Me.pnlHeader.Controls.Add(Me.lblAngebotOeffnen)
        Me.pnlHeader.Controls.Add(Me.dtpDatum)
        Me.pnlHeader.Controls.Add(Me.Label2)
        Me.pnlHeader.Controls.Add(Me.txtAngebotNr)
        Me.pnlHeader.Controls.Add(Me.Label1)
        Me.pnlHeader.Controls.Add(Me.lblUntertitel)
        Me.pnlHeader.Controls.Add(Me.lblTitel)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1180, 84)
        Me.pnlHeader.TabIndex = 0
        '
        'lnkPositionsliste
        '
        Me.lnkPositionsliste.ActiveLinkColor = System.Drawing.Color.White
        Me.lnkPositionsliste.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.lnkPositionsliste.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkPositionsliste.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkPositionsliste.LinkColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lnkPositionsliste.Location = New System.Drawing.Point(1024, 63)
        Me.lnkPositionsliste.Name = "lnkPositionsliste"
        Me.lnkPositionsliste.Size = New System.Drawing.Size(138, 15)
        Me.lnkPositionsliste.TabIndex = 11
        Me.lnkPositionsliste.TabStop = True
        Me.lnkPositionsliste.Text = "Positionsliste anzeigen"
        Me.lnkPositionsliste.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpeicherStatus
        '
        Me.lblSpeicherStatus.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.lblSpeicherStatus.AutoSize = True
        Me.lblSpeicherStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpeicherStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblSpeicherStatus.Location = New System.Drawing.Point(484, 63)
        Me.lblSpeicherStatus.Name = "lblSpeicherStatus"
        Me.lblSpeicherStatus.Size = New System.Drawing.Size(160, 13)
        Me.lblSpeicherStatus.TabIndex = 10
        Me.lblSpeicherStatus.Text = "Neu – noch nicht gespeichert"
        '
        'lblTitel
        '
        Me.lblTitel.AutoSize = True
        Me.lblTitel.Font = New System.Drawing.Font("Segoe UI", 16!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitel.ForeColor = System.Drawing.Color.White
        Me.lblTitel.Location = New System.Drawing.Point(22, 14)
        Me.lblTitel.Name = "lblTitel"
        Me.lblTitel.Size = New System.Drawing.Size(160, 30)
        Me.lblTitel.TabIndex = 0
        Me.lblTitel.Text = "Angebotsliste"
        '
        'lblUntertitel
        '
        Me.lblUntertitel.AutoSize = True
        Me.lblUntertitel.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUntertitel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblUntertitel.Location = New System.Drawing.Point(25, 48)
        Me.lblUntertitel.Name = "lblUntertitel"
        Me.lblUntertitel.Size = New System.Drawing.Size(290, 15)
        Me.lblUntertitel.TabIndex = 1
        Me.lblUntertitel.Text = "Angebote erstellen, verwalten und per PDF oder WhatsApp versenden"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(486, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "ANGEBOT-NR."
        '
        'txtAngebotNr
        '
        Me.txtAngebotNr.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.txtAngebotNr.BackColor = System.Drawing.Color.White
        Me.txtAngebotNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAngebotNr.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAngebotNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.txtAngebotNr.Location = New System.Drawing.Point(486, 32)
        Me.txtAngebotNr.Name = "txtAngebotNr"
        Me.txtAngebotNr.Size = New System.Drawing.Size(184, 27)
        Me.txtAngebotNr.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(684, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "DATUM"
        '
        'dtpDatum
        '
        Me.dtpDatum.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.dtpDatum.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDatum.Location = New System.Drawing.Point(684, 32)
        Me.dtpDatum.Name = "dtpDatum"
        Me.dtpDatum.Size = New System.Drawing.Size(130, 27)
        Me.dtpDatum.TabIndex = 5
        '
        'lblAngebotOeffnen
        '
        Me.lblAngebotOeffnen.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.lblAngebotOeffnen.AutoSize = True
        Me.lblAngebotOeffnen.Font = New System.Drawing.Font("Segoe UI", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAngebotOeffnen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblAngebotOeffnen.Location = New System.Drawing.Point(828, 12)
        Me.lblAngebotOeffnen.Name = "lblAngebotOeffnen"
        Me.lblAngebotOeffnen.Size = New System.Drawing.Size(80, 13)
        Me.lblAngebotOeffnen.TabIndex = 6
        Me.lblAngebotOeffnen.Text = "ANGEBOT ÖFFNEN"
        '
        'cmbAngebotNr
        '
        Me.cmbAngebotNr.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.cmbAngebotNr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAngebotNr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAngebotNr.Font = New System.Drawing.Font("Segoe UI", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAngebotNr.FormattingEnabled = True
        Me.cmbAngebotNr.Location = New System.Drawing.Point(828, 32)
        Me.cmbAngebotNr.Name = "cmbAngebotNr"
        Me.cmbAngebotNr.Size = New System.Drawing.Size(180, 28)
        Me.cmbAngebotNr.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(1024, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 27)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Produkte: 0"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.White
        Me.pnlFooter.Controls.Add(Me.flpRechts)
        Me.pnlFooter.Controls.Add(Me.flpLinks)
        Me.pnlFooter.Controls.Add(Me.pnlFooterLinie)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 732)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.pnlFooter.Size = New System.Drawing.Size(1180, 68)
        Me.pnlFooter.TabIndex = 2
        '
        'pnlFooterLinie
        '
        Me.pnlFooterLinie.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.pnlFooterLinie.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFooterLinie.Location = New System.Drawing.Point(12, 0)
        Me.pnlFooterLinie.Name = "pnlFooterLinie"
        Me.pnlFooterLinie.Size = New System.Drawing.Size(1156, 1)
        Me.pnlFooterLinie.TabIndex = 0
        '
        'flpLinks
        '
        Me.flpLinks.AutoSize = True
        Me.flpLinks.Controls.Add(Me.Button16)
        Me.flpLinks.Controls.Add(Me.btnProdukteHinzufuegen)
        Me.flpLinks.Controls.Add(Me.btnNeueProdukt)
        Me.flpLinks.Dock = System.Windows.Forms.DockStyle.Left
        Me.flpLinks.Location = New System.Drawing.Point(12, 1)
        Me.flpLinks.Name = "flpLinks"
        Me.flpLinks.Padding = New System.Windows.Forms.Padding(0, 14, 0, 0)
        Me.flpLinks.Size = New System.Drawing.Size(300, 67)
        Me.flpLinks.TabIndex = 1
        Me.flpLinks.WrapContents = False
        '
        'Button16
        '
        Me.Button16.BackColor = System.Drawing.Color.White
        Me.Button16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button16.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Button16.FlatAppearance.BorderSize = 1
        Me.Button16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button16.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button16.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.Button16.Size = New System.Drawing.Size(150, 38)
        Me.Button16.TabIndex = 0
        Me.Button16.Text = "＋  Neues Angebot"
        Me.Button16.UseVisualStyleBackColor = False
        '
        'btnProdukteHinzufuegen
        '
        Me.btnProdukteHinzufuegen.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnProdukteHinzufuegen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnProdukteHinzufuegen.FlatAppearance.BorderSize = 0
        Me.btnProdukteHinzufuegen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.btnProdukteHinzufuegen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProdukteHinzufuegen.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProdukteHinzufuegen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnProdukteHinzufuegen.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnProdukteHinzufuegen.Name = "btnProdukteHinzufuegen"
        Me.btnProdukteHinzufuegen.Size = New System.Drawing.Size(196, 38)
        Me.btnProdukteHinzufuegen.TabIndex = 1
        Me.btnProdukteHinzufuegen.Text = "＋  Produkte hinzufügen"
        Me.btnProdukteHinzufuegen.UseVisualStyleBackColor = False
        '
        'btnNeueProdukt
        '
        Me.btnNeueProdukt.BackColor = System.Drawing.Color.White
        Me.btnNeueProdukt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNeueProdukt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnNeueProdukt.FlatAppearance.BorderSize = 1
        Me.btnNeueProdukt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.btnNeueProdukt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNeueProdukt.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNeueProdukt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNeueProdukt.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnNeueProdukt.Size = New System.Drawing.Size(130, 38)
        Me.btnNeueProdukt.TabIndex = 2
        Me.btnNeueProdukt.Text = "＋  Leere Karte"
        Me.btnNeueProdukt.UseVisualStyleBackColor = False
        '
        'flpRechts
        '
        Me.flpRechts.AutoSize = True
        Me.flpRechts.Controls.Add(Me.Button18)
        Me.flpRechts.Controls.Add(Me.Button14)
        Me.flpRechts.Controls.Add(Me.btnSpeichern)
        Me.flpRechts.Controls.Add(Me.Button15)
        Me.flpRechts.Controls.Add(Me.Button5)
        Me.flpRechts.Dock = System.Windows.Forms.DockStyle.Right
        Me.flpRechts.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flpRechts.Location = New System.Drawing.Point(520, 1)
        Me.flpRechts.Name = "flpRechts"
        Me.flpRechts.Padding = New System.Windows.Forms.Padding(0, 14, 0, 0)
        Me.flpRechts.Size = New System.Drawing.Size(648, 67)
        Me.flpRechts.TabIndex = 2
        Me.flpRechts.WrapContents = False
        '
        'Button18
        '
        Me.Button18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Button18.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button18.FlatAppearance.BorderSize = 0
        Me.Button18.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.Button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button18.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button18.ForeColor = System.Drawing.Color.White
        Me.Button18.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.Button18.Size = New System.Drawing.Size(150, 38)
        Me.Button18.TabIndex = 0
        Me.Button18.Text = "WhatsApp senden"
        Me.Button18.UseVisualStyleBackColor = False
        '
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.Color.White
        Me.Button14.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button14.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Button14.FlatAppearance.BorderSize = 1
        Me.Button14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button14.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button14.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.Button14.Size = New System.Drawing.Size(120, 38)
        Me.Button14.TabIndex = 1
        Me.Button14.Text = "PDF erstellen"
        Me.Button14.UseVisualStyleBackColor = False
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
        Me.btnSpeichern.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnSpeichern.Size = New System.Drawing.Size(120, 38)
        Me.btnSpeichern.TabIndex = 2
        Me.btnSpeichern.Text = "Speichern"
        Me.btnSpeichern.UseVisualStyleBackColor = False
        '
        'Button15
        '
        Me.Button15.BackColor = System.Drawing.Color.White
        Me.Button15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button15.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Button15.FlatAppearance.BorderSize = 1
        Me.Button15.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button15.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button15.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.Button15.Size = New System.Drawing.Size(100, 38)
        Me.Button15.TabIndex = 3
        Me.Button15.Text = "Ändern"
        Me.Button15.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(179, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.Button5.Size = New System.Drawing.Size(100, 38)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Löschen"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'pnlInhalt
        '
        Me.pnlInhalt.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlInhalt.Controls.Add(Me.FlowLayoutPanel1)
        Me.pnlInhalt.Controls.Add(Me.pnlAbstand)
        Me.pnlInhalt.Controls.Add(Me.DataGridView1)
        Me.pnlInhalt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInhalt.Location = New System.Drawing.Point(0, 84)
        Me.pnlInhalt.Name = "pnlInhalt"
        Me.pnlInhalt.Padding = New System.Windows.Forms.Padding(16, 16, 16, 12)
        Me.pnlInhalt.Size = New System.Drawing.Size(1180, 648)
        Me.pnlInhalt.TabIndex = 1
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(16, 16)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(8, 8, 8, 8)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1148, 396)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'pnlAbstand
        '
        Me.pnlAbstand.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlAbstand.Location = New System.Drawing.Point(16, 412)
        Me.pnlAbstand.Name = "pnlAbstand"
        Me.pnlAbstand.Size = New System.Drawing.Size(1148, 12)
        Me.pnlAbstand.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.ColumnHeadersHeight = 36
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.DataGridView1.Location = New System.Drawing.Point(16, 424)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(1148, 220)
        Me.DataGridView1.TabIndex = 2
        '
        'btnPDF
        '
        Me.btnPDF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPDF.Location = New System.Drawing.Point(681, 757)
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Size = New System.Drawing.Size(56, 35)
        Me.btnPDF.TabIndex = 9
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseVisualStyleBackColor = True
        Me.btnPDF.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(715, 769)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(36, 23)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(757, 764)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(41, 28)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(681, 765)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(44, 35)
        Me.Button3.TabIndex = 20
        Me.Button3.Text = "Whatsap senden"
        Me.Button3.UseVisualStyleBackColor = False
        Me.Button3.Visible = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(594, 734)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(98, 35)
        Me.Button4.TabIndex = 21
        Me.Button4.Text = "Ändern"
        Me.Button4.UseVisualStyleBackColor = False
        Me.Button4.Visible = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(673, 775)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(134, 35)
        Me.Button6.TabIndex = 23
        Me.Button6.Text = "Whatsap Senden"
        Me.Button6.UseVisualStyleBackColor = False
        Me.Button6.Visible = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Location = New System.Drawing.Point(8, 732)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(66, 35)
        Me.Button7.TabIndex = 24
        Me.Button7.Text = "Yeni"
        Me.Button7.UseVisualStyleBackColor = False
        Me.Button7.Visible = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.White
        Me.Button8.Location = New System.Drawing.Point(353, 744)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(66, 29)
        Me.Button8.TabIndex = 25
        Me.Button8.Text = "Yeni"
        Me.Button8.UseVisualStyleBackColor = False
        Me.Button8.Visible = False
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(446, 746)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(65, 21)
        Me.Button9.TabIndex = 26
        Me.Button9.Text = "Button9"
        Me.Button9.UseVisualStyleBackColor = True
        Me.Button9.Visible = False
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(699, 780)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(72, 27)
        Me.Button10.TabIndex = 27
        Me.Button10.Text = "Button10"
        Me.Button10.UseVisualStyleBackColor = True
        Me.Button10.Visible = False
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(673, 780)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(62, 32)
        Me.Button11.TabIndex = 28
        Me.Button11.Text = "Button11"
        Me.Button11.UseVisualStyleBackColor = True
        Me.Button11.Visible = False
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(308, 736)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(85, 34)
        Me.Button12.TabIndex = 29
        Me.Button12.Text = "Button12"
        Me.Button12.UseVisualStyleBackColor = True
        Me.Button12.Visible = False
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(741, 757)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(75, 28)
        Me.Button13.TabIndex = 30
        Me.Button13.Text = "Button13"
        Me.Button13.UseVisualStyleBackColor = True
        Me.Button13.Visible = False
        '
        'Button17
        '
        Me.Button17.Location = New System.Drawing.Point(521, 751)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(75, 24)
        Me.Button17.TabIndex = 35
        Me.Button17.Text = "Button17"
        Me.Button17.UseVisualStyleBackColor = True
        Me.Button17.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.AngebotListe.My.Resources.Resources.SalihS48x48
        Me.PictureBox1.Location = New System.Drawing.Point(750, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 30)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 36
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'NeuesAngebotListeErstellen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1390, 800)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button17)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnPDF)
        Me.Controls.Add(Me.pnlAngebot)
        Me.Controls.Add(Me.pnlHost)
        Me.Controls.Add(Me.pnlSeite)
        Me.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(1190, 660)
        Me.Name = "NeuesAngebotListeErstellen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AngebotsListenManager"
        Me.pnlSeite.ResumeLayout(False)
        Me.pnlSeite.PerformLayout()
        Me.pnlBenutzerBox.ResumeLayout(False)
        Me.pnlBenutzerBox.PerformLayout()
        Me.pnlAngebot.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        Me.flpLinks.ResumeLayout(False)
        Me.flpRechts.ResumeLayout(False)
        Me.pnlInhalt.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlSeite As Panel
    Friend WithEvents lblAppName As Label
    Friend WithEvents lblAppUnter As Label
    Friend WithEvents navAngebote As NavKnopf
    Friend WithEvents navProdukte As NavKnopf
    Friend WithEvents navEinstellungen As NavKnopf
    Friend WithEvents navBenutzer As NavKnopf
    Friend WithEvents pnlBenutzerBox As Panel
    Friend WithEvents lblInitialen As Label
    Friend WithEvents lblBenutzerName As Label
    Friend WithEvents lnkAbmelden As LinkLabel
    Friend WithEvents pnlAngebot As Panel
    Friend WithEvents pnlHost As Panel
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblSpeicherStatus As Label
    Friend WithEvents lnkPositionsliste As LinkLabel
    Friend WithEvents lblTitel As Label
    Friend WithEvents lblUntertitel As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAngebotNr As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpDatum As DateTimePicker
    Friend WithEvents lblAngebotOeffnen As Label
    Friend WithEvents cmbAngebotNr As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents pnlFooterLinie As Panel
    Friend WithEvents flpLinks As FlowLayoutPanel
    Friend WithEvents Button16 As Button
    Friend WithEvents btnProdukteHinzufuegen As Button
    Friend WithEvents btnNeueProdukt As Button
    Friend WithEvents flpRechts As FlowLayoutPanel
    Friend WithEvents Button18 As Button
    Friend WithEvents Button14 As Button
    Friend WithEvents btnSpeichern As Button
    Friend WithEvents Button15 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents pnlInhalt As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents pnlAbstand As Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnPDF As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button17 As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
