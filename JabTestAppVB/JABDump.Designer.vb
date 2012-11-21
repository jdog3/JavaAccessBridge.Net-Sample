<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JABDump
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtTreeOutput = New System.Windows.Forms.TextBox()
        Me.lblWindowHandle = New System.Windows.Forms.Label()
        Me.txtWindowHandle = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(317, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Dump Control Hierarchy"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtTreeOutput
        '
        Me.txtTreeOutput.Location = New System.Drawing.Point(22, 37)
        Me.txtTreeOutput.Multiline = True
        Me.txtTreeOutput.Name = "txtTreeOutput"
        Me.txtTreeOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTreeOutput.Size = New System.Drawing.Size(979, 585)
        Me.txtTreeOutput.TabIndex = 1
        '
        'lblWindowHandle
        '
        Me.lblWindowHandle.AutoSize = True
        Me.lblWindowHandle.Location = New System.Drawing.Point(19, 15)
        Me.lblWindowHandle.Name = "lblWindowHandle"
        Me.lblWindowHandle.Size = New System.Drawing.Size(114, 13)
        Me.lblWindowHandle.TabIndex = 2
        Me.lblWindowHandle.Text = "Window Handle (HEX)"
        '
        'txtWindowHandle
        '
        Me.txtWindowHandle.Location = New System.Drawing.Point(150, 12)
        Me.txtWindowHandle.Name = "txtWindowHandle"
        Me.txtWindowHandle.Size = New System.Drawing.Size(143, 20)
        Me.txtWindowHandle.TabIndex = 3
        '
        'JABDump
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1013, 634)
        Me.Controls.Add(Me.txtWindowHandle)
        Me.Controls.Add(Me.lblWindowHandle)
        Me.Controls.Add(Me.txtTreeOutput)
        Me.Controls.Add(Me.Button1)
        Me.Name = "JABDump"
        Me.Text = "JAB Dump"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtTreeOutput As System.Windows.Forms.TextBox
    Friend WithEvents lblWindowHandle As System.Windows.Forms.Label
    Friend WithEvents txtWindowHandle As System.Windows.Forms.TextBox

End Class
