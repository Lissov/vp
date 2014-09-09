<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BetaSetupForm
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
        Me.SetupControl = New BetaSetup.BetaSetupControl
        Me.SuspendLayout()
        '
        'SetupControl
        '
        Me.SetupControl.Alpha = Nothing
        Me.SetupControl.Beta = Nothing
        Me.SetupControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SetupControl.EndTime = 100
        Me.SetupControl.EventsAllowed = True
        Me.SetupControl.Location = New System.Drawing.Point(0, 0)
        Me.SetupControl.Name = "SetupControl"
        Me.SetupControl.Size = New System.Drawing.Size(449, 401)
        Me.SetupControl.StartTime = 0
        Me.SetupControl.StepsCount = 100
        Me.SetupControl.TabIndex = 0
        Me.SetupControl.Th = Nothing
        '
        'BetaSetupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 401)
        Me.Controls.Add(Me.SetupControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BetaSetupForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Beta setup"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SetupControl As BetaSetup.BetaSetupControl
End Class
