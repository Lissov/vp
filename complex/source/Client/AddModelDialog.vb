Public Class AddModelDialog

#Region "Properties"

    Private _Models As List(Of ModelBase.IModel)
    Public Property Models() As List(Of ModelBase.IModel)
        Get
            If _Models Is Nothing Then
                _Models = New List(Of ModelBase.IModel)
            End If
            Return _Models
        End Get
        Set(ByVal value As List(Of ModelBase.IModel))
            _Models = value
        End Set
    End Property

    Private _SelectedModels As List(Of ModelBase.IModel)
    Public ReadOnly Property SelectedModels() As List(Of ModelBase.IModel)
        Get
            _SelectedModels = New List(Of ModelBase.IModel)

            For Each Row As DataGridViewRow In ModelsGrid.SelectedRows
                Dim Model As ModelBase.IModel
                Model = TryCast(Row.Tag, ModelBase.IModel)
                If Model IsNot Nothing Then _SelectedModels.Add(Model)
            Next

            Return _SelectedModels
        End Get
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal models As List(Of ModelBase.IModel))
        Me.New()

        Me.Models = models

        RefreshModelsGrid()
        CheckButtons()
    End Sub

#End Region

#Region "Private methods"

    Private Sub CheckButtons()
        OkButton.Enabled = ModelsGrid.SelectedRows.Count > 0
    End Sub

    Private Sub RefreshModelsGrid()
        ModelsGrid.Rows.Clear()

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Model As ModelBase.IModel In Models
            RowIndex = ModelsGrid.Rows.Add()
            Row = ModelsGrid.Rows(RowIndex)
            Row.Cells(NameColumn.Index).Value = Model.DisplayName
            Row.Tag = Model
        Next

    End Sub

#End Region

#Region "Events handlers"

    Private Sub ModelsGrid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModelsGrid.SelectionChanged
        CheckButtons()
    End Sub

    Private Sub ModelsGrid_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ModelsGrid.CellDoubleClick
        OkButton_Click(Nothing, Nothing)
    End Sub

#End Region

#Region "Main buttons"

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class