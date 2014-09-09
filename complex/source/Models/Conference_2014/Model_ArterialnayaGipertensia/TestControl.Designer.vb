<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.RadioGroup1 = New DevExpress.XtraEditors.RadioGroup()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.RadioGroup1)
        Me.GroupControl1.Location = New System.Drawing.Point(22, 18)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(362, 298)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Гипотезы:"
        '
        'RadioGroup1
        '
        Me.RadioGroup1.Location = New System.Drawing.Point(5, 23)
        Me.RadioGroup1.Name = "RadioGroup1"
        Me.RadioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RadioGroup1.Properties.Appearance.Options.UseBackColor = True
        Me.RadioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RadioGroup1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Почечная"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Рост ОПС"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Рост МОК"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Нейрогенная"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Энергетическая")})
        Me.RadioGroup1.Size = New System.Drawing.Size(137, 275)
        Me.RadioGroup1.TabIndex = 0
        '
        'TestControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "TestControl"
        Me.Size = New System.Drawing.Size(503, 347)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.RadioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RadioGroup1 As DevExpress.XtraEditors.RadioGroup

End Class
