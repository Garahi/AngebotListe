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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtAngebotNr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDatum = New System.Windows.Forms.DateTimePicker()
        Me.btnSpeichern = New System.Windows.Forms.Button()
        Me.btnPDF = New System.Windows.Forms.Button()
        Me.btnNeueProdukt = New System.Windows.Forms.Button()
        Me.cmbAngebotNr = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.Button16 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button18 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.ForeColor = System.Drawing.Color.White
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(8, 35)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(813, 538)
        Me.FlowLayoutPanel1.TabIndex = 3
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(8, 578)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(813, 173)
        Me.DataGridView1.TabIndex = 2
        '
        'txtAngebotNr
        '
        Me.txtAngebotNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAngebotNr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAngebotNr.Location = New System.Drawing.Point(109, 9)
        Me.txtAngebotNr.Name = "txtAngebotNr"
        Me.txtAngebotNr.Size = New System.Drawing.Size(129, 20)
        Me.txtAngebotNr.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "AngebotNr:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(244, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 15)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "AngebotDatum:"
        '
        'dtpDatum
        '
        Me.dtpDatum.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDatum.Location = New System.Drawing.Point(362, 9)
        Me.dtpDatum.Name = "dtpDatum"
        Me.dtpDatum.Size = New System.Drawing.Size(98, 20)
        Me.dtpDatum.TabIndex = 7
        '
        'btnSpeichern
        '
        Me.btnSpeichern.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnSpeichern.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSpeichern.ForeColor = System.Drawing.Color.White
        Me.btnSpeichern.Location = New System.Drawing.Point(235, 772)
        Me.btnSpeichern.Name = "btnSpeichern"
        Me.btnSpeichern.Size = New System.Drawing.Size(107, 35)
        Me.btnSpeichern.TabIndex = 8
        Me.btnSpeichern.Text = "Einfügen"
        Me.btnSpeichern.UseVisualStyleBackColor = False
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
        'btnNeueProdukt
        '
        Me.btnNeueProdukt.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNeueProdukt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNeueProdukt.ForeColor = System.Drawing.Color.White
        Me.btnNeueProdukt.Location = New System.Drawing.Point(137, 772)
        Me.btnNeueProdukt.Name = "btnNeueProdukt"
        Me.btnNeueProdukt.Size = New System.Drawing.Size(100, 35)
        Me.btnNeueProdukt.TabIndex = 16
        Me.btnNeueProdukt.Text = "NeueCard"
        Me.btnNeueProdukt.UseVisualStyleBackColor = False
        '
        'cmbAngebotNr
        '
        Me.cmbAngebotNr.FormattingEnabled = True
        Me.cmbAngebotNr.Location = New System.Drawing.Point(498, 8)
        Me.cmbAngebotNr.Name = "cmbAngebotNr"
        Me.cmbAngebotNr.Size = New System.Drawing.Size(143, 21)
        Me.cmbAngebotNr.TabIndex = 17
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
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Location = New System.Drawing.Point(425, 772)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(98, 35)
        Me.Button5.TabIndex = 22
        Me.Button5.Text = "Löschen"
        Me.Button5.UseVisualStyleBackColor = False
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
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button14.ForeColor = System.Drawing.Color.White
        Me.Button14.Location = New System.Drawing.Point(521, 772)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(99, 35)
        Me.Button14.TabIndex = 31
        Me.Button14.Text = "PDF"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'Button15
        '
        Me.Button15.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button15.ForeColor = System.Drawing.Color.White
        Me.Button15.Location = New System.Drawing.Point(340, 772)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(86, 35)
        Me.Button15.TabIndex = 32
        Me.Button15.Text = "Ändern"
        Me.Button15.UseVisualStyleBackColor = False
        '
        'Button16
        '
        Me.Button16.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button16.ForeColor = System.Drawing.Color.White
        Me.Button16.Location = New System.Drawing.Point(33, 772)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(108, 35)
        Me.Button16.TabIndex = 33
        Me.Button16.Text = "Neue Angebot"
        Me.Button16.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(666, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 15)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Produkte:"
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
        'Button18
        '
        Me.Button18.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button18.ForeColor = System.Drawing.Color.White
        Me.Button18.Location = New System.Drawing.Point(626, 772)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(125, 35)
        Me.Button18.TabIndex = 37
        Me.Button18.Text = "WhatsapSenden"
        Me.Button18.UseVisualStyleBackColor = False
        '
        'NeuesAngebotListeErstellen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(828, 811)
        Me.Controls.Add(Me.Button18)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button17)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbAngebotNr)
        Me.Controls.Add(Me.btnNeueProdukt)
        Me.Controls.Add(Me.btnPDF)
        Me.Controls.Add(Me.btnSpeichern)
        Me.Controls.Add(Me.dtpDatum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAngebotNr)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "NeuesAngebotListeErstellen"
        Me.Text = "NeuesAngebotListeErstellen"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtAngebotNr As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpDatum As DateTimePicker
    Friend WithEvents btnSpeichern As Button
    Friend WithEvents btnPDF As Button
    Friend WithEvents btnNeueProdukt As Button
    Friend WithEvents cmbAngebotNr As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button14 As Button
    Friend WithEvents Button15 As Button
    Friend WithEvents Button16 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Button17 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button18 As Button
End Class
