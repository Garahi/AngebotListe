<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProduktSuche
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
        Me.dgvErgebnisse = New System.Windows.Forms.DataGridView()
        Me.txtSuche = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.dgvErgebnisse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvErgebnisse
        '
        Me.dgvErgebnisse.BackgroundColor = System.Drawing.Color.White
        Me.dgvErgebnisse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvErgebnisse.Location = New System.Drawing.Point(12, 41)
        Me.dgvErgebnisse.Name = "dgvErgebnisse"
        Me.dgvErgebnisse.Size = New System.Drawing.Size(739, 270)
        Me.dgvErgebnisse.TabIndex = 0
        '
        'txtSuche
        '
        Me.txtSuche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuche.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSuche.Location = New System.Drawing.Point(195, 12)
        Me.txtSuche.Name = "txtSuche"
        Me.txtSuche.Size = New System.Drawing.Size(247, 20)
        Me.txtSuche.TabIndex = 12
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(448, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 25)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Suchen"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(12, 317)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(117, 29)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "Neue Artikel Einfügen"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'FrmProduktSuche
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(767, 358)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtSuche)
        Me.Controls.Add(Me.dgvErgebnisse)
        Me.Name = "FrmProduktSuche"
        Me.Text = "FrmProduktSuche"
        CType(Me.dgvErgebnisse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvErgebnisse As DataGridView
    Friend WithEvents txtSuche As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
