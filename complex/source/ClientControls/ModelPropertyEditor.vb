Imports DevExpress.XtraEditors

Public Class ModelPropertyEditor
    Inherits DevExpress.XtraEditors.XtraForm

#Region "Properties"

    Private _ObjectChanged As Boolean = False
    ''' <summary>
    ''' If true object was edited by this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObjectChanged() As Boolean
        Get
            Return _ObjectChanged
        End Get
        Set(ByVal value As Boolean)
            _ObjectChanged = value
        End Set
    End Property

    Private _EnableEvents As Boolean = True
    Public Property EnableEvents() As Boolean
        Get
            Return _EnableEvents
        End Get
        Set(ByVal value As Boolean)
            _EnableEvents = value
        End Set
    End Property

    Private _ReadOnlyState As Boolean = False
    ''' <summary>
    ''' If true all edit actions will be forbidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReadOnlyState() As Boolean
        Get
            Return _ReadOnlyState
        End Get
        Set(ByVal value As Boolean)
            _ReadOnlyState = value
            SetControlsState(value)
        End Set
    End Property

    Private _Model As ModelBase.IModel
    ''' <summary>
    ''' Edited object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Model() As ModelBase.IModel
        Get
            Return _Model
        End Get
        Set(ByVal value As ModelBase.IModel)
            _Model = value
            FillControls()
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal model As ModelBase.IModel)
        Me.New()

        Me.Model = model
    End Sub

#End Region

#Region "Private helping methods"

    Private Sub SetControlsState(ByVal readOnlyState As Boolean)
        NameTextEdit.Properties.ReadOnly = readOnlyState
        DescriptionEdit.Properties.ReadOnly = readOnlyState
    End Sub

    Private Sub FillControls()
        EnableEvents = False

        Me.Text = String.Format("Edit properties of model '{0}'", Model.DisplayName)

        NameTextEdit.Text = Model.DisplayName
        DescriptionEdit.Text = Model.Description

        ObjectChanged = False

        EnableEvents = True

        CheckButtons()
    End Sub

    Private Sub FillValue()
        Model.DisplayName = NameTextEdit.Text
        Model.Description = DescriptionEdit.Text
    End Sub

    Private Sub CheckChanges()
        ObjectChanged = False

        ObjectChanged = ObjectChanged OrElse (NameTextEdit.Text <> Model.DisplayName)
        ObjectChanged = ObjectChanged OrElse (DescriptionEdit.Text <> Model.Description)
    End Sub

#End Region

#Region "Update menu & status"

    Private Sub CheckButtons()
        SaveButtonItem.Enabled = ObjectChanged
    End Sub

#End Region


#Region "Other events"

    Private Sub TextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameTextEdit.TextChanged, DescriptionEdit.TextChanged
        If Not EnableEvents Then Return

        CheckChanges()
        CheckButtons()

    End Sub

#End Region



#Region "Exit"

    Private Sub SaveButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveButtonItem.ItemClick
        FillValue()

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CloseButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles CloseButtonItem.ItemClick
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region



End Class

