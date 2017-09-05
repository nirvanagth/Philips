<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogTakeSample
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtSID = New System.Windows.Forms.TextBox()
        Me.lblSID = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.tmrOK = New System.Windows.Forms.Timer(Me.components)
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdNoDisk = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtSID
        '
        Me.txtSID.Location = New System.Drawing.Point(228, 201)
        Me.txtSID.Name = "txtSID"
        Me.txtSID.Size = New System.Drawing.Size(165, 20)
        Me.txtSID.TabIndex = 10
        '
        'lblSID
        '
        Me.lblSID.AutoSize = True
        Me.lblSID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSID.Location = New System.Drawing.Point(98, 201)
        Me.lblSID.Name = "lblSID"
        Me.lblSID.Size = New System.Drawing.Size(126, 20)
        Me.lblSID.TabIndex = 7
        Me.lblSID.Text = "Sample Set ID"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(135, 88)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(83, 25)
        Me.lblTitle.TabIndex = 6
        Me.lblTitle.Text = "Label1"
        '
        'tmrOK
        '
        '
        'cmdok
        '
        Me.cmdok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.Location = New System.Drawing.Point(481, 194)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(75, 30)
        Me.cmdok.TabIndex = 9
        Me.cmdok.Text = "OK"
        Me.cmdok.UseVisualStyleBackColor = True
        '
        'cmdNoDisk
        '
        Me.cmdNoDisk.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNoDisk.Location = New System.Drawing.Point(601, 194)
        Me.cmdNoDisk.Name = "cmdNoDisk"
        Me.cmdNoDisk.Size = New System.Drawing.Size(109, 30)
        Me.cmdNoDisk.TabIndex = 9
        Me.cmdNoDisk.Text = "No Disk"
        Me.cmdNoDisk.UseVisualStyleBackColor = True
        '
        'DialogTakeSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(811, 251)
        Me.Controls.Add(Me.txtSID)
        Me.Controls.Add(Me.lblSID)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.cmdNoDisk)
        Me.Controls.Add(Me.cmdok)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogTakeSample"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DialogTakeSample"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSID As System.Windows.Forms.TextBox
    Friend WithEvents lblSID As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents tmrOK As System.Windows.Forms.Timer
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents cmdNoDisk As System.Windows.Forms.Button

End Class
