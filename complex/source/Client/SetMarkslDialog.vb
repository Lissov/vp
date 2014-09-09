Public Class SetMarksDialog

#Region "Properties"

    Private _MarkItems As List(Of MarkItem)
    Public Property MarkItems() As List(Of MarkItem)
        Get
            If _MarkItems Is Nothing Then
                _MarkItems = New List(Of MarkItem)
            End If
            Return _MarkItems
        End Get
        Set(ByVal value As List(Of MarkItem))
            _MarkItems = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal markItems As List(Of MarkItem))
        Me.New()

        Me.MarkItems = Nothing
        For Each Item As MarkItem In markItems
            Me.MarkItems.Add(Item.Clone)
        Next

        RefreshGrid()
    End Sub

#End Region

#Region "Private methods"

    Private Sub RefreshGrid()
        ItemsGrid.Rows.Clear()

        Dim Row As DataGridViewRow
        Dim RowIndex As Integer
        For Each Item As MarkItem In MarkItems
            RowIndex = ItemsGrid.Rows.Add()
            Row = ItemsGrid.Rows(RowIndex)
            Row.Cells(NameColumn.Index).Value = Item.DisplayName
            Row.Cells(MarksStyleColumn.Index).Value = Item.MarkStyleDisplayName
            Row.Tag = Item
        Next

    End Sub

#End Region

#Region "Events handlers"


#End Region

#Region "Main buttons"

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        For Each Row As DataGridViewRow In ItemsGrid.Rows
            Dim Item As MarkItem = TryCast(Row.Tag, MarkItem)
            If Item IsNot Nothing Then
                Item.MarkStyleDisplayName = Row.Cells(MarksStyleColumn.Index).Value
            End If
        Next

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class

