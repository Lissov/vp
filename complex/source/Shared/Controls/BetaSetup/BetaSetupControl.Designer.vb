<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BetaSetupControl
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Me.Label_Alpha = New System.Windows.Forms.Label
        Me.Label_Beta = New System.Windows.Forms.Label
        Me.Label_Th = New System.Windows.Forms.Label
        Me.TextBox_Alpha = New System.Windows.Forms.TextBox
        Me.TextBox_Beta = New System.Windows.Forms.TextBox
        Me.TextBox_Th = New System.Windows.Forms.TextBox
        Me.TrackBar_Alpha = New System.Windows.Forms.TrackBar
        Me.TrackBar_Beta = New System.Windows.Forms.TrackBar
        Me.TrackBar_Th = New System.Windows.Forms.TrackBar
        Me.PreviewChart = New Steema.TeeChart.TChart
        CType(Me.TrackBar_Alpha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_Beta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_Th, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_Alpha
        '
        Me.Label_Alpha.AutoSize = True
        Me.Label_Alpha.Location = New System.Drawing.Point(22, 26)
        Me.Label_Alpha.Name = "Label_Alpha"
        Me.Label_Alpha.Size = New System.Drawing.Size(37, 13)
        Me.Label_Alpha.TabIndex = 0
        Me.Label_Alpha.Text = "Alpha:"
        '
        'Label_Beta
        '
        Me.Label_Beta.AutoSize = True
        Me.Label_Beta.Location = New System.Drawing.Point(22, 63)
        Me.Label_Beta.Name = "Label_Beta"
        Me.Label_Beta.Size = New System.Drawing.Size(32, 13)
        Me.Label_Beta.TabIndex = 1
        Me.Label_Beta.Text = "Beta:"
        '
        'Label_Th
        '
        Me.Label_Th.AutoSize = True
        Me.Label_Th.Location = New System.Drawing.Point(22, 103)
        Me.Label_Th.Name = "Label_Th"
        Me.Label_Th.Size = New System.Drawing.Size(23, 13)
        Me.Label_Th.TabIndex = 2
        Me.Label_Th.Text = "Th:"
        '
        'TextBox_Alpha
        '
        Me.TextBox_Alpha.BackColor = System.Drawing.Color.White
        Me.TextBox_Alpha.Location = New System.Drawing.Point(65, 23)
        Me.TextBox_Alpha.Name = "TextBox_Alpha"
        Me.TextBox_Alpha.ReadOnly = True
        Me.TextBox_Alpha.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_Alpha.TabIndex = 3
        Me.TextBox_Alpha.Text = "0"
        '
        'TextBox_Beta
        '
        Me.TextBox_Beta.BackColor = System.Drawing.Color.White
        Me.TextBox_Beta.Location = New System.Drawing.Point(65, 60)
        Me.TextBox_Beta.Name = "TextBox_Beta"
        Me.TextBox_Beta.ReadOnly = True
        Me.TextBox_Beta.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_Beta.TabIndex = 4
        Me.TextBox_Beta.Text = "0"
        '
        'TextBox_Th
        '
        Me.TextBox_Th.BackColor = System.Drawing.Color.White
        Me.TextBox_Th.Location = New System.Drawing.Point(65, 100)
        Me.TextBox_Th.Name = "TextBox_Th"
        Me.TextBox_Th.ReadOnly = True
        Me.TextBox_Th.Size = New System.Drawing.Size(59, 20)
        Me.TextBox_Th.TabIndex = 5
        Me.TextBox_Th.Text = "0"
        '
        'TrackBar_Alpha
        '
        Me.TrackBar_Alpha.BackColor = System.Drawing.Color.White
        Me.TrackBar_Alpha.Location = New System.Drawing.Point(140, 16)
        Me.TrackBar_Alpha.Maximum = 50
        Me.TrackBar_Alpha.Name = "TrackBar_Alpha"
        Me.TrackBar_Alpha.Size = New System.Drawing.Size(285, 45)
        Me.TrackBar_Alpha.TabIndex = 6
        Me.TrackBar_Alpha.Value = 10
        '
        'TrackBar_Beta
        '
        Me.TrackBar_Beta.BackColor = System.Drawing.Color.White
        Me.TrackBar_Beta.Location = New System.Drawing.Point(140, 57)
        Me.TrackBar_Beta.Maximum = 50
        Me.TrackBar_Beta.Name = "TrackBar_Beta"
        Me.TrackBar_Beta.Size = New System.Drawing.Size(285, 45)
        Me.TrackBar_Beta.TabIndex = 7
        Me.TrackBar_Beta.Value = 10
        '
        'TrackBar_Th
        '
        Me.TrackBar_Th.BackColor = System.Drawing.Color.White
        Me.TrackBar_Th.Location = New System.Drawing.Point(140, 97)
        Me.TrackBar_Th.Maximum = 50
        Me.TrackBar_Th.Name = "TrackBar_Th"
        Me.TrackBar_Th.Size = New System.Drawing.Size(285, 45)
        Me.TrackBar_Th.TabIndex = 8
        Me.TrackBar_Th.Value = 10
        '
        'PreviewChart
        '
        '
        '
        '
        Me.PreviewChart.Aspect.ElevationFloat = 345
        Me.PreviewChart.Aspect.RotationFloat = 345
        Me.PreviewChart.Aspect.View3D = False
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Bottom.Automatic = True
        '
        '
        '
        Me.PreviewChart.Axes.Bottom.Grid.ZPosition = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Bottom.Labels.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Bottom.Labels.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Bottom.Labels.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Bottom.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Bottom.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Bottom.Title.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Axes.Depth.Automatic = True
        '
        '
        '
        Me.PreviewChart.Axes.Depth.Grid.ZPosition = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Depth.Labels.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Depth.Labels.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Depth.Labels.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Depth.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Depth.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Depth.Title.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Axes.DepthTop.Automatic = True
        '
        '
        '
        Me.PreviewChart.Axes.DepthTop.Grid.ZPosition = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.DepthTop.Labels.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.DepthTop.Labels.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.DepthTop.Labels.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.DepthTop.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.DepthTop.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.DepthTop.Title.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Axes.Left.Automatic = True
        '
        '
        '
        Me.PreviewChart.Axes.Left.Grid.ZPosition = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Left.Labels.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Left.Labels.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Left.Labels.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Left.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Left.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Left.Title.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Axes.Right.Automatic = True
        '
        '
        '
        Me.PreviewChart.Axes.Right.Grid.ZPosition = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Right.Labels.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Right.Labels.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Right.Labels.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Right.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Right.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Right.Title.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Axes.Top.Automatic = True
        '
        '
        '
        Me.PreviewChart.Axes.Top.Grid.ZPosition = 0
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Top.Labels.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Top.Labels.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Top.Labels.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Axes.Top.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Axes.Top.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Axes.Top.Title.Shadow.Visible = False
        Me.PreviewChart.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Footer.Font.Shadow.Visible = False
        Me.PreviewChart.Footer.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Footer.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Header.Font.Shadow.Visible = False
        Me.PreviewChart.Header.Font.Unit = System.Drawing.GraphicsUnit.World
        Me.PreviewChart.Header.Lines = New String() {"Preview"}
        '
        '
        '
        Me.PreviewChart.Header.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Legend.Font.Shadow.Visible = False
        Me.PreviewChart.Legend.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Legend.Title.Font.Bold = True
        '
        '
        '
        Me.PreviewChart.Legend.Title.Font.Shadow.Visible = False
        Me.PreviewChart.Legend.Title.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.Legend.Title.Pen.Visible = False
        '
        '
        '
        Me.PreviewChart.Legend.Title.Shadow.Visible = False
        Me.PreviewChart.Location = New System.Drawing.Point(25, 148)
        Me.PreviewChart.Name = "PreviewChart"
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Panel.Bevel.Width = 0
        '
        '
        '
        Me.PreviewChart.Panel.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.PreviewChart.Panel.Shadow.Visible = False
        Me.PreviewChart.Size = New System.Drawing.Size(400, 250)
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.SubFooter.Font.Shadow.Visible = False
        Me.PreviewChart.SubFooter.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.SubFooter.Shadow.Visible = False
        '
        '
        '
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.SubHeader.Font.Shadow.Visible = False
        Me.PreviewChart.SubHeader.Font.Unit = System.Drawing.GraphicsUnit.World
        '
        '
        '
        Me.PreviewChart.SubHeader.Shadow.Visible = False
        Me.PreviewChart.TabIndex = 9
        '
        '
        '
        '
        '
        '
        Me.PreviewChart.Walls.Back.AutoHide = False
        '
        '
        '
        Me.PreviewChart.Walls.Back.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Walls.Bottom.AutoHide = False
        '
        '
        '
        Me.PreviewChart.Walls.Bottom.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Walls.Left.AutoHide = False
        '
        '
        '
        Me.PreviewChart.Walls.Left.Shadow.Visible = False
        '
        '
        '
        Me.PreviewChart.Walls.Right.AutoHide = False
        '
        '
        '
        Me.PreviewChart.Walls.Right.Shadow.Visible = False
        '
        'BetaSetupControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PreviewChart)
        Me.Controls.Add(Me.TrackBar_Th)
        Me.Controls.Add(Me.TrackBar_Beta)
        Me.Controls.Add(Me.TrackBar_Alpha)
        Me.Controls.Add(Me.TextBox_Th)
        Me.Controls.Add(Me.TextBox_Beta)
        Me.Controls.Add(Me.TextBox_Alpha)
        Me.Controls.Add(Me.Label_Th)
        Me.Controls.Add(Me.Label_Beta)
        Me.Controls.Add(Me.Label_Alpha)
        Me.Name = "BetaSetupControl"
        Me.Size = New System.Drawing.Size(500, 410)
        CType(Me.TrackBar_Alpha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_Beta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_Th, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label_Alpha As System.Windows.Forms.Label
    Friend WithEvents Label_Beta As System.Windows.Forms.Label
    Friend WithEvents Label_Th As System.Windows.Forms.Label
    Friend WithEvents TextBox_Alpha As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Beta As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Th As System.Windows.Forms.TextBox
    Friend WithEvents TrackBar_Alpha As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar_Beta As System.Windows.Forms.TrackBar
    Friend WithEvents TrackBar_Th As System.Windows.Forms.TrackBar
    Friend WithEvents PreviewChart As Steema.TeeChart.TChart

End Class
