Imports DevExpress.XtraEditors

Public Class ModelSelector
    Inherits DevExpress.XtraEditors.XtraForm

#Region "Properties"

    ''' <summary>
    ''' Ruterns selected in tree value if any
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedValue() As ModelBase.Value
        Get
            Return ModelsTreeView.SelectedValue
        End Get
    End Property

    Private _CurrentConfig As ModelBase.Configuration
    Private Property CurrentConfig() As ModelBase.Configuration
        Get
            Return _CurrentConfig
        End Get
        Set(ByVal value As ModelBase.Configuration)
            _CurrentConfig = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal configuration As ModelBase.Configuration)
        Me.New()

        Me.CurrentConfig = configuration
        Me.ModelsTreeView.SetConfiguration(Me.CurrentConfig)

    End Sub

#End Region

#Region "Private helping methods"

    Private Sub CheckButtons()
        ButtonOk.Enabled = ModelsTreeView.SelectedValue IsNot Nothing
    End Sub

#End Region


#Region "Events"

    Private Sub ModelsTreeView_ValueClicked(ByVal value As ModelBase.Value) Handles ModelsTreeView.ValueClicked
        ButtonOk_Click(ButtonOk, Nothing)
    End Sub

    Private Sub ModelsTreeView_SelectedNodeChanged() Handles ModelsTreeView.SelectedNodeChanged
        CheckButtons()
    End Sub

#End Region



#Region "Exit"

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region


End Class

