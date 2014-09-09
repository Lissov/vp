Imports DevExpress.XtraEditors


Public Class ParameterPropertyEditor
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

    Private _Parameter As ModelBase.Parameter
    ''' <summary>
    ''' Edited object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Parameter() As ModelBase.Parameter
        Get
            Return _Parameter
        End Get
        Set(ByVal value As ModelBase.Parameter)
            _Parameter = value
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

    Public Sub New(ByVal parameter As ModelBase.Parameter)
        Me.New()

        Me.Parameter = parameter
    End Sub

#End Region

#Region "Private helping methods"

    Private Sub SetControlsState(ByVal readOnlyState As Boolean)
        NameTextEdit.Properties.ReadOnly = readOnlyState
    End Sub

    Private Sub FillControls()
        EnableEvents = False

        If String.IsNullOrEmpty(Parameter.DisplayName) AndAlso String.IsNullOrEmpty(Parameter.Name) Then
            Me.Text = "Create parameter"
        Else
            Me.Text = String.Format("Edit properties of parameter '{0}'", Parameter.DisplayName)
        End If

        NameTextEdit.Text = Parameter.DisplayName
      
        ObjectChanged = False

        EnableEvents = True

        CheckButtons()
    End Sub

    Private Sub FillParameter()
        Parameter.DisplayName = NameTextEdit.Text
    End Sub

    Private Sub CheckChanges()
        ObjectChanged = False

        ObjectChanged = ObjectChanged OrElse (NameTextEdit.Text <> Parameter.DisplayName)
    End Sub

    Private Function ValidateParameter() As Boolean
        ErrorProvider.ClearErrors()

        If String.IsNullOrEmpty(NameTextEdit.Text) Then
            ErrorProvider.SetError(NameTextEdit, "Name must be filled in!")
            Return False
        End If
        If Not Functions.Text.IsValidVariableName(NameTextEdit.Text) Then
            ErrorProvider.SetError(NameTextEdit, "Name is not valid! The first character must be a letter A through Z (uppercase or lowercase letters may be used). Succeeding characters can be letters, digits, or the underscore (_) character (no spaces or other characters allowed).")
            Return False
        End If

        Return True
    End Function

#End Region

#Region "Update menu & status"

    Private Sub CheckButtons()
        SaveButtonItem.Enabled = ObjectChanged
    End Sub

#End Region


#Region "Other events"

    Private Sub TextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameTextEdit.TextChanged
        If Not EnableEvents Then Return

        CheckChanges()
        CheckButtons()

    End Sub

#End Region



#Region "Exit"

    Private Sub SaveButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveButtonItem.ItemClick
        If Not ValidateParameter() Then Return

        FillParameter()

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CloseButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles CloseButtonItem.ItemClick
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region



End Class

