<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProductCard
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lblBeschreibung = New System.Windows.Forms.Label()
        Me.lblPreis = New System.Windows.Forms.Label()
        Me.btnEinfügen = New System.Windows.Forms.Button()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtPreisEdit = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnProduktSuchen = New System.Windows.Forms.Button()
        Me.btnAction = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pbBild = New System.Windows.Forms.PictureBox()
        Me.pnlBottom.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBild, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblBeschreibung
        '
        Me.lblBeschreibung.AutoSize = True
        Me.lblBeschreibung.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblBeschreibung.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeschreibung.ForeColor = System.Drawing.Color.Black
        Me.lblBeschreibung.Location = New System.Drawing.Point(5, 190)
        Me.lblBeschreibung.MaximumSize = New System.Drawing.Size(380, 0)
        Me.lblBeschreibung.Name = "lblBeschreibung"
        Me.lblBeschreibung.Size = New System.Drawing.Size(96, 15)
        Me.lblBeschreibung.TabIndex = 2
        Me.lblBeschreibung.Text = "lblBeschreibung"
        '
        'lblPreis
        '
        Me.lblPreis.AutoSize = True
        Me.lblPreis.Font = New System.Drawing.Font("Segoe UI Symbol", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblPreis.ForeColor = System.Drawing.Color.Black
        Me.lblPreis.Location = New System.Drawing.Point(3, 162)
        Me.lblPreis.MaximumSize = New System.Drawing.Size(380, 0)
        Me.lblPreis.Name = "lblPreis"
        Me.lblPreis.Size = New System.Drawing.Size(82, 25)
        Me.lblPreis.TabIndex = 3
        Me.lblPreis.Text = "lblPreis"
        '
        'btnEinfügen
        '
        Me.btnEinfügen.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnEinfügen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEinfügen.ForeColor = System.Drawing.Color.White
        Me.btnEinfügen.Location = New System.Drawing.Point(90, 141)
        Me.btnEinfügen.Name = "btnEinfügen"
        Me.btnEinfügen.Size = New System.Drawing.Size(108, 28)
        Me.btnEinfügen.TabIndex = 8
        Me.btnEinfügen.Text = "➕ Einfügen"
        Me.btnEinfügen.UseVisualStyleBackColor = False
        Me.btnEinfügen.Visible = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI Symbol", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(7, 141)
        Me.lblName.MaximumSize = New System.Drawing.Size(380, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(60, 21)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Name:"
        '
        'txtPreisEdit
        '
        Me.txtPreisEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreisEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPreisEdit.Location = New System.Drawing.Point(117, 166)
        Me.txtPreisEdit.Name = "txtPreisEdit"
        Me.txtPreisEdit.Size = New System.Drawing.Size(93, 20)
        Me.txtPreisEdit.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(201, 139)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(82, 25)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Update"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(260, 195)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(85, 23)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Löschen"
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'btnProduktSuchen
        '
        Me.btnProduktSuchen.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnProduktSuchen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProduktSuchen.ForeColor = System.Drawing.Color.White
        Me.btnProduktSuchen.Location = New System.Drawing.Point(260, 158)
        Me.btnProduktSuchen.Name = "btnProduktSuchen"
        Me.btnProduktSuchen.Size = New System.Drawing.Size(120, 25)
        Me.btnProduktSuchen.TabIndex = 14
        Me.btnProduktSuchen.Text = "Produkt wählen"
        Me.btnProduktSuchen.UseVisualStyleBackColor = False
        '
        'btnAction
        '
        Me.btnAction.BackColor = System.Drawing.Color.Green
        Me.btnAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAction.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAction.ForeColor = System.Drawing.Color.White
        Me.btnAction.Location = New System.Drawing.Point(1, 0)
        Me.btnAction.Name = "btnAction"
        Me.btnAction.Size = New System.Drawing.Size(373, 46)
        Me.btnAction.TabIndex = 0
        Me.btnAction.Text = "My Fresh Fruit Yeşil GmbH"
        Me.btnAction.UseVisualStyleBackColor = False
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.RoyalBlue
        Me.pnlBottom.Controls.Add(Me.btnAction)
        Me.pnlBottom.Location = New System.Drawing.Point(3, 208)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(373, 46)
        Me.pnlBottom.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Symbol", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(289, 141)
        Me.Label1.MaximumSize = New System.Drawing.Size(380, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 21)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Label1"
        Me.Label1.Visible = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox3.Image = Global.AngebotListe.My.Resources.Resources.icons8_fenster_schließen_23
        Me.PictureBox3.Location = New System.Drawing.Point(356, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox3.TabIndex = 17
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox2.Image = Global.AngebotListe.My.Resources.Resources.edit_19x19
        Me.PictureBox2.Location = New System.Drawing.Point(90, 166)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 10
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.AngebotListe.My.Resources.Resources.einfügen
        Me.PictureBox1.Location = New System.Drawing.Point(355, 26)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'pbBild
        '
        Me.pbBild.BackColor = System.Drawing.Color.White
        Me.pbBild.Dock = System.Windows.Forms.DockStyle.Top
        Me.pbBild.Location = New System.Drawing.Point(0, 0)
        Me.pbBild.Name = "pbBild"
        Me.pbBild.Size = New System.Drawing.Size(380, 140)
        Me.pbBild.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbBild.TabIndex = 0
        Me.pbBild.TabStop = False
        '
        'ProductCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.btnProduktSuchen)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtPreisEdit)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.pbBild)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.btnEinfügen)
        Me.Controls.Add(Me.lblPreis)
        Me.Controls.Add(Me.lblBeschreibung)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.Name = "ProductCard"
        Me.Size = New System.Drawing.Size(380, 268)
        Me.pnlBottom.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBild, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents pbBild As PictureBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents lblBeschreibung As Label
    Friend WithEvents lblPreis As Label
    Friend WithEvents btnEinfügen As Button
    Friend WithEvents lblName As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents txtPreisEdit As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnProduktSuchen As Button
    Friend WithEvents btnAction As Button
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox3 As PictureBox
End Class
