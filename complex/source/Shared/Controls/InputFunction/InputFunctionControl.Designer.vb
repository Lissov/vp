<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InputFunctionControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InputFunctionControl))
        Me.LeftPanel = New System.Windows.Forms.Panel
        Me.FormulaLabel = New System.Windows.Forms.Label
        Me.FormulaTextBox = New System.Windows.Forms.TextBox
        Me.ParametersLabel = New System.Windows.Forms.Label
        Me.ParameterGrid = New System.Windows.Forms.DataGridView
        Me.ParameterNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ParameterValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FunctionPicture = New System.Windows.Forms.PictureBox
        Me.FunctionTypeLabel = New System.Windows.Forms.Label
        Me.FunctionTypeComboBox = New System.Windows.Forms.ComboBox
        Me.AllValuesLabel = New System.Windows.Forms.Label
        Me.AllValuesComboBox = New System.Windows.Forms.ComboBox
        Me.CentralPanel = New System.Windows.Forms.Panel
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.EndTimeTextBox = New System.Windows.Forms.TextBox
        Me.StartTimeTextBox = New System.Windows.Forms.TextBox
        Me.EndTimeLabel = New System.Windows.Forms.Label
        Me.StartTimeLabel = New System.Windows.Forms.Label
        Me.LoadButton = New System.Windows.Forms.Button
        Me.SaveButton = New System.Windows.Forms.Button
        Me.PreviewChart = New Steema.TeeChart.TChart
        Me.HelpButton = New System.Windows.Forms.Button
        Me.LeftPanel.SuspendLayout()
        CType(Me.ParameterGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FunctionPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CentralPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'LeftPanel
        '
        Me.LeftPanel.Controls.Add(Me.HelpButton)
        Me.LeftPanel.Controls.Add(Me.FormulaLabel)
        Me.LeftPanel.Controls.Add(Me.FormulaTextBox)
        Me.LeftPanel.Controls.Add(Me.ParametersLabel)
        Me.LeftPanel.Controls.Add(Me.ParameterGrid)
        Me.LeftPanel.Controls.Add(Me.FunctionPicture)
        Me.LeftPanel.Controls.Add(Me.FunctionTypeLabel)
        Me.LeftPanel.Controls.Add(Me.FunctionTypeComboBox)
        Me.LeftPanel.Controls.Add(Me.AllValuesLabel)
        Me.LeftPanel.Controls.Add(Me.AllValuesComboBox)
        Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.LeftPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftPanel.Name = "LeftPanel"
        Me.LeftPanel.Size = New System.Drawing.Size(200, 410)
        Me.LeftPanel.TabIndex = 0
        '
        'FormulaLabel
        '
        Me.FormulaLabel.AutoSize = True
        Me.FormulaLabel.Location = New System.Drawing.Point(10, 103)
        Me.FormulaLabel.Name = "FormulaLabel"
        Me.FormulaLabel.Size = New System.Drawing.Size(47, 13)
        Me.FormulaLabel.TabIndex = 8
        Me.FormulaLabel.Text = "Formula:"
        '
        'FormulaTextBox
        '
        Me.FormulaTextBox.Location = New System.Drawing.Point(13, 123)
        Me.FormulaTextBox.Name = "FormulaTextBox"
        Me.FormulaTextBox.Size = New System.Drawing.Size(181, 20)
        Me.FormulaTextBox.TabIndex = 7
        '
        'ParametersLabel
        '
        Me.ParametersLabel.AutoSize = True
        Me.ParametersLabel.Location = New System.Drawing.Point(13, 186)
        Me.ParametersLabel.Name = "ParametersLabel"
        Me.ParametersLabel.Size = New System.Drawing.Size(63, 13)
        Me.ParametersLabel.TabIndex = 6
        Me.ParametersLabel.Text = "Parameters:"
        '
        'ParameterGrid
        '
        Me.ParameterGrid.AllowUserToAddRows = False
        Me.ParameterGrid.AllowUserToDeleteRows = False
        Me.ParameterGrid.AllowUserToResizeRows = False
        Me.ParameterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ParameterGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ParameterNameColumn, Me.ParameterValueColumn})
        Me.ParameterGrid.Location = New System.Drawing.Point(13, 202)
        Me.ParameterGrid.Name = "ParameterGrid"
        Me.ParameterGrid.RowHeadersVisible = False
        Me.ParameterGrid.Size = New System.Drawing.Size(181, 191)
        Me.ParameterGrid.TabIndex = 5
        '
        'ParameterNameColumn
        '
        Me.ParameterNameColumn.HeaderText = "Name"
        Me.ParameterNameColumn.Name = "ParameterNameColumn"
        Me.ParameterNameColumn.ReadOnly = True
        Me.ParameterNameColumn.Width = 40
        '
        'ParameterValueColumn
        '
        Me.ParameterValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ParameterValueColumn.HeaderText = "Value"
        Me.ParameterValueColumn.Name = "ParameterValueColumn"
        '
        'FunctionPicture
        '
        Me.FunctionPicture.Image = Global.InputFunction.My.Resources.Resources.Impuls
        Me.FunctionPicture.InitialImage = Nothing
        Me.FunctionPicture.Location = New System.Drawing.Point(13, 103)
        Me.FunctionPicture.Name = "FunctionPicture"
        Me.FunctionPicture.Size = New System.Drawing.Size(181, 80)
        Me.FunctionPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.FunctionPicture.TabIndex = 4
        Me.FunctionPicture.TabStop = False
        '
        'FunctionTypeLabel
        '
        Me.FunctionTypeLabel.AutoSize = True
        Me.FunctionTypeLabel.Location = New System.Drawing.Point(10, 50)
        Me.FunctionTypeLabel.Name = "FunctionTypeLabel"
        Me.FunctionTypeLabel.Size = New System.Drawing.Size(74, 13)
        Me.FunctionTypeLabel.TabIndex = 3
        Me.FunctionTypeLabel.Text = "Function type:"
        '
        'FunctionTypeComboBox
        '
        Me.FunctionTypeComboBox.FormattingEnabled = True
        Me.FunctionTypeComboBox.Location = New System.Drawing.Point(13, 66)
        Me.FunctionTypeComboBox.Name = "FunctionTypeComboBox"
        Me.FunctionTypeComboBox.Size = New System.Drawing.Size(181, 21)
        Me.FunctionTypeComboBox.TabIndex = 2
        '
        'AllValuesLabel
        '
        Me.AllValuesLabel.AutoSize = True
        Me.AllValuesLabel.Location = New System.Drawing.Point(10, 10)
        Me.AllValuesLabel.Name = "AllValuesLabel"
        Me.AllValuesLabel.Size = New System.Drawing.Size(42, 13)
        Me.AllValuesLabel.TabIndex = 1
        Me.AllValuesLabel.Text = "Values:"
        '
        'AllValuesComboBox
        '
        Me.AllValuesComboBox.DisplayMember = "DisplayName"
        Me.AllValuesComboBox.FormattingEnabled = True
        Me.AllValuesComboBox.Location = New System.Drawing.Point(13, 26)
        Me.AllValuesComboBox.Name = "AllValuesComboBox"
        Me.AllValuesComboBox.Size = New System.Drawing.Size(181, 21)
        Me.AllValuesComboBox.TabIndex = 0
        '
        'CentralPanel
        '
        Me.CentralPanel.Controls.Add(Me.RefreshButton)
        Me.CentralPanel.Controls.Add(Me.EndTimeTextBox)
        Me.CentralPanel.Controls.Add(Me.StartTimeTextBox)
        Me.CentralPanel.Controls.Add(Me.EndTimeLabel)
        Me.CentralPanel.Controls.Add(Me.StartTimeLabel)
        Me.CentralPanel.Controls.Add(Me.LoadButton)
        Me.CentralPanel.Controls.Add(Me.SaveButton)
        Me.CentralPanel.Controls.Add(Me.PreviewChart)
        Me.CentralPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CentralPanel.Location = New System.Drawing.Point(200, 0)
        Me.CentralPanel.Name = "CentralPanel"
        Me.CentralPanel.Size = New System.Drawing.Size(300, 410)
        Me.CentralPanel.TabIndex = 1
        '
        'RefreshButton
        '
        Me.RefreshButton.Image = CType(resources.GetObject("RefreshButton.Image"), System.Drawing.Image)
        Me.RefreshButton.Location = New System.Drawing.Point(226, 250)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(57, 51)
        Me.RefreshButton.TabIndex = 7
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'EndTimeTextBox
        '
        Me.EndTimeTextBox.Location = New System.Drawing.Point(105, 281)
        Me.EndTimeTextBox.Name = "EndTimeTextBox"
        Me.EndTimeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.EndTimeTextBox.TabIndex = 6
        Me.EndTimeTextBox.Text = "0"
        '
        'StartTimeTextBox
        '
        Me.StartTimeTextBox.Location = New System.Drawing.Point(105, 250)
        Me.StartTimeTextBox.Name = "StartTimeTextBox"
        Me.StartTimeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.StartTimeTextBox.TabIndex = 5
        Me.StartTimeTextBox.Text = "0"
        '
        'EndTimeLabel
        '
        Me.EndTimeLabel.AutoSize = True
        Me.EndTimeLabel.Location = New System.Drawing.Point(6, 284)
        Me.EndTimeLabel.Name = "EndTimeLabel"
        Me.EndTimeLabel.Size = New System.Drawing.Size(91, 13)
        Me.EndTimeLabel.TabIndex = 4
        Me.EndTimeLabel.Text = "Preview end time:"
        '
        'StartTimeLabel
        '
        Me.StartTimeLabel.AutoSize = True
        Me.StartTimeLabel.Location = New System.Drawing.Point(6, 253)
        Me.StartTimeLabel.Name = "StartTimeLabel"
        Me.StartTimeLabel.Size = New System.Drawing.Size(93, 13)
        Me.StartTimeLabel.TabIndex = 3
        Me.StartTimeLabel.Text = "Preview start time:"
        '
        'LoadButton
        '
        Me.LoadButton.Image = CType(resources.GetObject("LoadButton.Image"), System.Drawing.Image)
        Me.LoadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LoadButton.Location = New System.Drawing.Point(6, 370)
        Me.LoadButton.Name = "LoadButton"
        Me.LoadButton.Size = New System.Drawing.Size(75, 23)
        Me.LoadButton.TabIndex = 2
        Me.LoadButton.Text = "Load"
        Me.LoadButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Image = CType(resources.GetObject("SaveButton.Image"), System.Drawing.Image)
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.Location = New System.Drawing.Point(92, 370)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 1
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
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
        Me.PreviewChart.Axes.Bottom.Grid.Color = System.Drawing.Color.Gray
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
        Me.PreviewChart.Axes.Bottom.Ticks.Length = 2
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
        Me.PreviewChart.Axes.Depth.Grid.Color = System.Drawing.Color.Gray
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
        Me.PreviewChart.Axes.Depth.Ticks.Length = 2
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
        Me.PreviewChart.Axes.DepthTop.Grid.Color = System.Drawing.Color.Gray
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
        Me.PreviewChart.Axes.DepthTop.Ticks.Length = 2
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
        Me.PreviewChart.Axes.Left.Grid.Color = System.Drawing.Color.Gray
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
        Me.PreviewChart.Axes.Left.Ticks.Length = 2
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
        Me.PreviewChart.Axes.Right.Grid.Color = System.Drawing.Color.Gray
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
        Me.PreviewChart.Axes.Right.Ticks.Length = 2
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
        Me.PreviewChart.Axes.Top.Grid.Color = System.Drawing.Color.Gray
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
        Me.PreviewChart.Axes.Top.Ticks.Length = 2
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
        Me.PreviewChart.Cursor = System.Windows.Forms.Cursors.Default
        Me.PreviewChart.Dock = System.Windows.Forms.DockStyle.Top
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
        Me.PreviewChart.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
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
        Me.PreviewChart.Legend.Visible = False
        Me.PreviewChart.Location = New System.Drawing.Point(0, 0)
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
        Me.PreviewChart.Panel.Gradient.EndColor = System.Drawing.Color.Yellow
        Me.PreviewChart.Panel.Gradient.MiddleColor = System.Drawing.Color.Empty
        Me.PreviewChart.Panel.Gradient.StartColor = System.Drawing.Color.White
        '
        '
        '
        Me.PreviewChart.Panel.Shadow.Height = 0
        Me.PreviewChart.Panel.Shadow.Visible = False
        Me.PreviewChart.Panel.Shadow.Width = 0
        Me.PreviewChart.Size = New System.Drawing.Size(300, 250)
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
        Me.PreviewChart.TabIndex = 0
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
        Me.PreviewChart.Walls.Right.Brush.Color = System.Drawing.Color.Silver
        '
        '
        '
        Me.PreviewChart.Walls.Right.Shadow.Visible = False
        '
        'HelpButton
        '
        Me.HelpButton.Image = CType(resources.GetObject("HelpButton.Image"), System.Drawing.Image)
        Me.HelpButton.Location = New System.Drawing.Point(174, 123)
        Me.HelpButton.Name = "HelpButton"
        Me.HelpButton.Size = New System.Drawing.Size(20, 20)
        Me.HelpButton.TabIndex = 8
        Me.HelpButton.UseVisualStyleBackColor = True
        '
        'InputFunctionControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CentralPanel)
        Me.Controls.Add(Me.LeftPanel)
        Me.Name = "InputFunctionControl"
        Me.Size = New System.Drawing.Size(500, 410)
        Me.LeftPanel.ResumeLayout(False)
        Me.LeftPanel.PerformLayout()
        CType(Me.ParameterGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FunctionPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CentralPanel.ResumeLayout(False)
        Me.CentralPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LeftPanel As System.Windows.Forms.Panel
    Friend WithEvents CentralPanel As System.Windows.Forms.Panel
    Friend WithEvents PreviewChart As Steema.TeeChart.TChart
    Friend WithEvents AllValuesLabel As System.Windows.Forms.Label
    Friend WithEvents AllValuesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents FunctionTypeLabel As System.Windows.Forms.Label
    Friend WithEvents FunctionTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ParameterGrid As System.Windows.Forms.DataGridView
    Friend WithEvents FunctionPicture As System.Windows.Forms.PictureBox
    Friend WithEvents FormulaLabel As System.Windows.Forms.Label
    Friend WithEvents FormulaTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ParametersLabel As System.Windows.Forms.Label
    Friend WithEvents LoadButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents ParameterNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParameterValueColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents StartTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EndTimeLabel As System.Windows.Forms.Label
    Friend WithEvents StartTimeLabel As System.Windows.Forms.Label
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents HelpButton As System.Windows.Forms.Button

End Class
