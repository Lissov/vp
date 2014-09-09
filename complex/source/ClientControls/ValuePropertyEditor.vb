Imports DevExpress.XtraEditors


Public Class ValuePropertyEditor
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

    Private _Value As ModelBase.Value
    ''' <summary>
    ''' Edited object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value() As ModelBase.Value
        Get
            Return _Value
        End Get
        Set(ByVal value As ModelBase.Value)
            _Value = value
            FillControls()
        End Set
    End Property

    Private _FullEditState As Boolean = False
    ''' <summary>
    ''' If true all edit controls will be shown; validation will be performed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FullEditState() As Boolean
        Get
            Return _FullEditState
        End Get
        Set(ByVal value As Boolean)
            _FullEditState = value

            If _FullEditState Then
                TypeLabel.Visible = True
                TypeComboBox.Visible = True
            End If
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FillTypeComboBox()
    End Sub

    Public Sub New(ByVal value As ModelBase.Value)
        Me.New()

        Me.Value = value
    End Sub

#End Region

#Region "Private helping methods"

    Private Sub SetControlsState(ByVal readOnlyState As Boolean)
        NameTextEdit.Properties.ReadOnly = readOnlyState
        GroupNameTextEdit.Properties.ReadOnly = readOnlyState
        MeasureTextEdit.Properties.ReadOnly = readOnlyState
    End Sub

    Private Sub FillControls()
        EnableEvents = False

        If String.IsNullOrEmpty(Value.DisplayName) AndAlso String.IsNullOrEmpty(Value.Name) Then
            Me.Text = "Create value"
        Else
            Me.Text = String.Format("Edit properties of value '{0}'", Value.DisplayName)
        End If

        NameTextEdit.Text = Value.DisplayName
        GroupNameTextEdit.Text = Value.GroupName
        MeasureTextEdit.Text = Value.Measure

        TypeComboBox.SelectedIndex = Value.Type

        ObjectChanged = False

        EnableEvents = True

        CheckButtons()
    End Sub

    Private Sub FillValue()
        Value.DisplayName = NameTextEdit.Text
        Value.GroupName = GroupNameTextEdit.Text
        Value.Measure = MeasureTextEdit.Text

        If FullEditState Then
            Value.Type = CType(TypeComboBox.SelectedIndex, ModelBase.Value.ValueType)
        End If
    End Sub

    Private Sub CheckChanges()
        ObjectChanged = False

        ObjectChanged = ObjectChanged OrElse (NameTextEdit.Text <> Value.DisplayName)
        ObjectChanged = ObjectChanged OrElse (GroupNameTextEdit.Text <> Value.GroupName)
        ObjectChanged = ObjectChanged OrElse (MeasureTextEdit.Text <> Value.Measure)

        If FullEditState Then
            ObjectChanged = ObjectChanged OrElse (Value.Type <> CType(TypeComboBox.SelectedIndex, ModelBase.Value.ValueType))
        End If
    End Sub

    Private Function ValidateValue() As Boolean
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

    Private Sub FillTypeComboBox()
        TypeComboBox.Properties.Items.Clear()

        Dim Type As ModelBase.Value.ValueType

        For Type = ModelBase.Value.ValueType.Input To ModelBase.Value.ValueType.Output Step 1
            Dim StringValue As String
            Select Case Type
                Case ModelBase.Value.ValueType.Input
                    StringValue = "input"
                Case ModelBase.Value.ValueType.Internal
                    StringValue = "internal"
                Case ModelBase.Value.ValueType.Output
                    StringValue = "output"
            End Select
            If Not String.IsNullOrEmpty(StringValue) Then
                TypeComboBox.Properties.Items.Add(StringValue)
            End If
        Next

        TypeComboBox.SelectedIndex = 0
    End Sub

#End Region

#Region "Update menu & status"

    Private Sub CheckButtons()
        SaveButtonItem.Enabled = ObjectChanged
    End Sub

#End Region


#Region "Other events"

    Private Sub TextEdit_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameTextEdit.TextChanged, MeasureTextEdit.TextChanged, GroupNameTextEdit.TextChanged
        If Not EnableEvents Then Return

        CheckChanges()
        CheckButtons()

    End Sub

    Private Sub TypeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeComboBox.SelectedIndexChanged
        If Not FullEditState Then Return
        CheckChanges()
    End Sub

#End Region



#Region "Exit"

    Private Sub SaveButtonItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles SaveButtonItem.ItemClick
        If FullEditState AndAlso Not ValidateValue() Then Return

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

