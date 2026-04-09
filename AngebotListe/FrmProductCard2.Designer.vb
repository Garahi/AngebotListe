<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProductCard2
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnNeueProdukt = New System.Windows.Forms.Button()
        Me.btnWhatsApp = New System.Windows.Forms.Button()
        Me.NeuesAngebotListeErstellen = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(813, 165)
        Me.DataGridView1.TabIndex = 0
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(12, 183)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(813, 404)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'btnNeueProdukt
        '
        Me.btnNeueProdukt.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNeueProdukt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNeueProdukt.ForeColor = System.Drawing.Color.White
        Me.btnNeueProdukt.Location = New System.Drawing.Point(12, 593)
        Me.btnNeueProdukt.Name = "btnNeueProdukt"
        Me.btnNeueProdukt.Size = New System.Drawing.Size(136, 35)
        Me.btnNeueProdukt.TabIndex = 15
        Me.btnNeueProdukt.Text = "btnNeuesProdukt  "
        Me.btnNeueProdukt.UseVisualStyleBackColor = False
        '
        'btnWhatsApp
        '
        Me.btnWhatsApp.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnWhatsApp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWhatsApp.ForeColor = System.Drawing.Color.White
        Me.btnWhatsApp.Location = New System.Drawing.Point(182, 591)
        Me.btnWhatsApp.Name = "btnWhatsApp"
        Me.btnWhatsApp.Size = New System.Drawing.Size(136, 35)
        Me.btnWhatsApp.TabIndex = 16
        Me.btnWhatsApp.Text = "Whatsap a Yolla"
        Me.btnWhatsApp.UseVisualStyleBackColor = False
        '
        'NeuesAngebotListeErstellen
        '
        Me.NeuesAngebotListeErstellen.BackColor = System.Drawing.Color.Green
        Me.NeuesAngebotListeErstellen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NeuesAngebotListeErstellen.ForeColor = System.Drawing.Color.White
        Me.NeuesAngebotListeErstellen.Location = New System.Drawing.Point(335, 593)
        Me.NeuesAngebotListeErstellen.Name = "NeuesAngebotListeErstellen"
        Me.NeuesAngebotListeErstellen.Size = New System.Drawing.Size(136, 35)
        Me.NeuesAngebotListeErstellen.TabIndex = 17
        Me.NeuesAngebotListeErstellen.Text = "NeuesAngebotListeErstellen"
        Me.NeuesAngebotListeErstellen.UseVisualStyleBackColor = False
        '
        'FrmProductCard2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 628)
        Me.Controls.Add(Me.NeuesAngebotListeErstellen)
        Me.Controls.Add(Me.btnWhatsApp)
        Me.Controls.Add(Me.btnNeueProdukt)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "FrmProductCard2"
        Me.Text = "FrmProductCard2"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnNeueProdukt As Button
    Friend WithEvents btnWhatsApp As Button
    Friend WithEvents NeuesAngebotListeErstellen As Button
End Class
