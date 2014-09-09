<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
        Partial Public Class ExceptionMessage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExceptionMessage))
        Me.OkButton = New System.Windows.Forms.Button
        Me.AdvancedButton = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.NiceMessageLabel = New System.Windows.Forms.Label
        Me.ErrorImagePictureBox = New System.Windows.Forms.PictureBox
        Me.VersionInfoGroupBox = New System.Windows.Forms.GroupBox
        Me.VersionInfoValue = New System.Windows.Forms.Label
        Me.VersionInfoLabel = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.ErrorImagePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VersionInfoGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'OkButton
        '
        Me.OkButton.AutoSize = True
        Me.OkButton.Location = New System.Drawing.Point(349, 89)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(75, 23)
        Me.OkButton.TabIndex = 0
        Me.OkButton.Text = "Close"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'AdvancedButton
        '
        Me.AdvancedButton.AutoSize = True
        Me.AdvancedButton.Location = New System.Drawing.Point(430, 89)
        Me.AdvancedButton.Name = "AdvancedButton"
        Me.AdvancedButton.Size = New System.Drawing.Size(75, 23)
        Me.AdvancedButton.TabIndex = 1
        Me.AdvancedButton.Text = "Details >>"
        Me.AdvancedButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 200)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(498, 313)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Exception message"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 18)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(486, 287)
        Me.TextBox1.TabIndex = 2
        '
        'NiceMessageLabel
        '
        Me.NiceMessageLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NiceMessageLabel.Location = New System.Drawing.Point(82, 9)
        Me.NiceMessageLabel.Name = "NiceMessageLabel"
        Me.NiceMessageLabel.Size = New System.Drawing.Size(428, 64)
        Me.NiceMessageLabel.TabIndex = 4
        '
        'ErrorImagePictureBox
        '
        Me.ErrorImagePictureBox.Image = CType(resources.GetObject("ErrorImagePictureBox.Image"), System.Drawing.Image)
        Me.ErrorImagePictureBox.Location = New System.Drawing.Point(12, 9)
        Me.ErrorImagePictureBox.Name = "ErrorImagePictureBox"
        Me.ErrorImagePictureBox.Size = New System.Drawing.Size(64, 64)
        Me.ErrorImagePictureBox.TabIndex = 5
        Me.ErrorImagePictureBox.TabStop = False
        '
        'VersionInfoGroupBox
        '
        Me.VersionInfoGroupBox.AutoSize = True
        Me.VersionInfoGroupBox.Controls.Add(Me.VersionInfoValue)
        Me.VersionInfoGroupBox.Controls.Add(Me.VersionInfoLabel)
        Me.VersionInfoGroupBox.Location = New System.Drawing.Point(12, 118)
        Me.VersionInfoGroupBox.Name = "VersionInfoGroupBox"
        Me.VersionInfoGroupBox.Size = New System.Drawing.Size(498, 76)
        Me.VersionInfoGroupBox.TabIndex = 6
        Me.VersionInfoGroupBox.TabStop = False
        Me.VersionInfoGroupBox.Text = "Version"
        '
        'VersionInfoValue
        '
        Me.VersionInfoValue.Location = New System.Drawing.Point(167, 16)
        Me.VersionInfoValue.Name = "VersionInfoValue"
        Me.VersionInfoValue.Size = New System.Drawing.Size(197, 44)
        Me.VersionInfoValue.TabIndex = 1
        '
        'VersionInfoLabel
        '
        Me.VersionInfoLabel.Location = New System.Drawing.Point(8, 16)
        Me.VersionInfoLabel.Name = "VersionInfoLabel"
        Me.VersionInfoLabel.Size = New System.Drawing.Size(165, 44)
        Me.VersionInfoLabel.TabIndex = 0
        '
        'ExceptionMessage
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 521)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.AdvancedButton)
        Me.Controls.Add(Me.VersionInfoGroupBox)
        Me.Controls.Add(Me.ErrorImagePictureBox)
        Me.Controls.Add(Me.NiceMessageLabel)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExceptionMessage"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Smo exception"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ErrorImagePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VersionInfoGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents AdvancedButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents NiceMessageLabel As System.Windows.Forms.Label
    Friend WithEvents ErrorImagePictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents VersionInfoGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents VersionInfoLabel As System.Windows.Forms.Label
    Friend WithEvents VersionInfoValue As System.Windows.Forms.Label
End Class
