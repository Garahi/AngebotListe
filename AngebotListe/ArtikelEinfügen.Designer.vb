<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArtikelEinfügen
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnEinfügen = New System.Windows.Forms.Button()
        Me.btnAndern = New System.Windows.Forms.Button()
        Me.btnLöschen = New System.Windows.Forms.Button()
        Me.dgvProducts = New System.Windows.Forms.DataGridView()
        Me.btnNeue = New System.Windows.Forms.Button()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.cmbKategorie = New System.Windows.Forms.ComboBox()
        Me.txtPreis = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkAktiv = New System.Windows.Forms.CheckBox()
        Me.txtErstelltAm = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtBildPfad = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBeschreibung = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProduktId = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(32, 245)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = " "
        '
        'btnEinfügen
        '
        Me.btnEinfügen.AllowDrop = True
        Me.btnEinfügen.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnEinfügen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEinfügen.ForeColor = System.Drawing.Color.White
        Me.btnEinfügen.Location = New System.Drawing.Point(492, 420)
        Me.btnEinfügen.Name = "btnEinfügen"
        Me.btnEinfügen.Size = New System.Drawing.Size(75, 35)
        Me.btnEinfügen.TabIndex = 8
        Me.btnEinfügen.Text = "Einfügen"
        Me.btnEinfügen.UseVisualStyleBackColor = False
        '
        'btnAndern
        '
        Me.btnAndern.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnAndern.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAndern.ForeColor = System.Drawing.Color.White
        Me.btnAndern.Location = New System.Drawing.Point(569, 420)
        Me.btnAndern.Name = "btnAndern"
        Me.btnAndern.Size = New System.Drawing.Size(75, 35)
        Me.btnAndern.TabIndex = 9
        Me.btnAndern.Text = "Ändern"
        Me.btnAndern.UseVisualStyleBackColor = False
        '
        'btnLöschen
        '
        Me.btnLöschen.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnLöschen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLöschen.ForeColor = System.Drawing.Color.White
        Me.btnLöschen.Location = New System.Drawing.Point(646, 420)
        Me.btnLöschen.Name = "btnLöschen"
        Me.btnLöschen.Size = New System.Drawing.Size(75, 35)
        Me.btnLöschen.TabIndex = 10
        Me.btnLöschen.Text = "Löschen"
        Me.btnLöschen.UseVisualStyleBackColor = False
        '
        'dgvProducts
        '
        Me.dgvProducts.BackgroundColor = System.Drawing.Color.White
        Me.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProducts.Location = New System.Drawing.Point(16, 201)
        Me.dgvProducts.Name = "dgvProducts"
        Me.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProducts.Size = New System.Drawing.Size(705, 213)
        Me.dgvProducts.TabIndex = 15
        '
        'btnNeue
        '
        Me.btnNeue.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNeue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNeue.ForeColor = System.Drawing.Color.White
        Me.btnNeue.Location = New System.Drawing.Point(411, 420)
        Me.btnNeue.Name = "btnNeue"
        Me.btnNeue.Size = New System.Drawing.Size(75, 35)
        Me.btnNeue.TabIndex = 11
        Me.btnNeue.Text = "Neue"
        Me.btnNeue.UseVisualStyleBackColor = False
        '
        'txtId
        '
        Me.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtId.Location = New System.Drawing.Point(98, 435)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(63, 20)
        Me.txtId.TabIndex = 20
        Me.txtId.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(67, 435)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 15)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "ID:"
        Me.Label7.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.cmbKategorie)
        Me.Panel1.Controls.Add(Me.txtPreis)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.chkAktiv)
        Me.Panel1.Controls.Add(Me.txtErstelltAm)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.txtBildPfad)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtBeschreibung)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtProduktId)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(16, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(705, 187)
        Me.Panel1.TabIndex = 21
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox2.Image = Global.AngebotListe.My.Resources.Resources.einfügen
        Me.PictureBox2.Location = New System.Drawing.Point(672, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox2.TabIndex = 184
        Me.PictureBox2.TabStop = False
        '
        'cmbKategorie
        '
        Me.cmbKategorie.FormattingEnabled = True
        Me.cmbKategorie.Items.AddRange(New Object() {"Obst", "Gemüse", "Fleisch", "Milchprodukte", "Backwaren", "Getränke", "Sonstige"})
        Me.cmbKategorie.Location = New System.Drawing.Point(361, 121)
        Me.cmbKategorie.Name = "cmbKategorie"
        Me.cmbKategorie.Size = New System.Drawing.Size(124, 21)
        Me.cmbKategorie.TabIndex = 177
        '
        'txtPreis
        '
        Me.txtPreis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreis.Location = New System.Drawing.Point(144, 122)
        Me.txtPreis.Name = "txtPreis"
        Me.txtPreis.Size = New System.Drawing.Size(54, 20)
        Me.txtPreis.TabIndex = 174
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(5, 127)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 15)
        Me.Label9.TabIndex = 183
        Me.Label9.Text = "Preis:"
        '
        'chkAktiv
        '
        Me.chkAktiv.AutoSize = True
        Me.chkAktiv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAktiv.Location = New System.Drawing.Point(222, 123)
        Me.chkAktiv.Name = "chkAktiv"
        Me.chkAktiv.Size = New System.Drawing.Size(56, 20)
        Me.chkAktiv.TabIndex = 175
        Me.chkAktiv.Text = "Aktif"
        Me.chkAktiv.UseVisualStyleBackColor = True
        '
        'txtErstelltAm
        '
        Me.txtErstelltAm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtErstelltAm.Location = New System.Drawing.Point(144, 156)
        Me.txtErstelltAm.Name = "txtErstelltAm"
        Me.txtErstelltAm.Size = New System.Drawing.Size(91, 20)
        Me.txtErstelltAm.TabIndex = 185
        Me.txtErstelltAm.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 15)
        Me.Label8.TabIndex = 182
        Me.Label8.Text = "ErsteltDatum:"
        Me.Label8.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PictureBox1.Location = New System.Drawing.Point(509, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(191, 140)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 181
        Me.PictureBox1.TabStop = False
        '
        'txtBildPfad
        '
        Me.txtBildPfad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBildPfad.Location = New System.Drawing.Point(504, 161)
        Me.txtBildPfad.Name = "txtBildPfad"
        Me.txtBildPfad.Size = New System.Drawing.Size(198, 20)
        Me.txtBildPfad.TabIndex = 180
        Me.txtBildPfad.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(438, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 15)
        Me.Label5.TabIndex = 179
        Me.Label5.Text = "BildFad:"
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(287, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 15)
        Me.Label4.TabIndex = 178
        Me.Label4.Text = "Kategorie:"
        '
        'txtBeschreibung
        '
        Me.txtBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBeschreibung.Location = New System.Drawing.Point(144, 92)
        Me.txtBeschreibung.Name = "txtBeschreibung"
        Me.txtBeschreibung.Size = New System.Drawing.Size(341, 20)
        Me.txtBeschreibung.TabIndex = 173
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(139, 15)
        Me.Label3.TabIndex = 176
        Me.Label3.Text = "ArtikelBeschreibung:"
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(144, 55)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(341, 20)
        Me.txtName.TabIndex = 172
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 15)
        Me.Label2.TabIndex = 171
        Me.Label2.Text = "Artikelbezeichnung:"
        '
        'txtProduktId
        '
        Me.txtProduktId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProduktId.Location = New System.Drawing.Point(144, 19)
        Me.txtProduktId.Name = "txtProduktId"
        Me.txtProduktId.Size = New System.Drawing.Size(91, 20)
        Me.txtProduktId.TabIndex = 170
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 15)
        Me.Label1.TabIndex = 169
        Me.Label1.Text = "ArtikelNr:"
        '
        'ArtikelEinfügen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(744, 473)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnNeue)
        Me.Controls.Add(Me.dgvProducts)
        Me.Controls.Add(Me.btnLöschen)
        Me.Controls.Add(Me.btnAndern)
        Me.Controls.Add(Me.btnEinfügen)
        Me.Controls.Add(Me.Label6)
        Me.Name = "ArtikelEinfügen"
        Me.ShowIcon = False
        Me.Text = "ArtikelEinfügen"
        CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents btnEinfügen As Button
    Friend WithEvents btnAndern As Button
    Friend WithEvents btnLöschen As Button
    Friend WithEvents dgvProducts As DataGridView
    Friend WithEvents btnNeue As Button
    Friend WithEvents txtId As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents cmbKategorie As ComboBox
    Friend WithEvents txtPreis As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents chkAktiv As CheckBox
    Friend WithEvents txtErstelltAm As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtBildPfad As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtBeschreibung As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtProduktId As TextBox
    Friend WithEvents Label1 As Label
End Class
