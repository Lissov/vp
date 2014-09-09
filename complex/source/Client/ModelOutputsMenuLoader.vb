Imports System.Collections.Generic

''' <summary>
''' All classes that want use MenuLoader must implement this interface
''' </summary>
''' <remarks></remarks>
Public Interface IObjectWithMenu

    ''' <summary>
    ''' This sub used to handle  event ItemClick
    ''' </summary>
    ''' <param name="item">selected item</param>
    ''' <remarks></remarks>
    Sub MenuItemSelected(ByVal item As MenuItem)

End Interface

Public Class MenuItem

#Region "Constructors"

    Public Sub New()

    End Sub

    Public Sub New(ByVal modelName As String, ByVal valueName As String)
        Me.ModelName = modelName
        Me.ValueName = valueName
    End Sub

    Public Sub New(ByVal modelName As String, ByVal valueName As String, ByVal groupName As String)
        Me.new(modelName, valueName)

        Me.GroupName = groupName
    End Sub

#End Region

#Region "Properties"

    Private _ModelName As String
    Public Property ModelName() As String
        Get
            Return _ModelName
        End Get
        Set(ByVal value As String)
            _ModelName = value
        End Set
    End Property

    Private _ValueName As String
    Public Property ValueName() As String
        Get
            Return _ValueName
        End Get
        Set(ByVal value As String)
            _ValueName = value
        End Set
    End Property

    Private _GroupName As String
    Public Property GroupName() As String
        Get
            Return _GroupName
        End Get
        Set(ByVal value As String)
            _GroupName = value
        End Set
    End Property

    Private _LinkConst As Double?
    Public Property LinkConst() As Double?
        Get
            Return _LinkConst
        End Get
        Set(ByVal value As Double?)
            _LinkConst = value
        End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
        Return _ValueName
    End Function

End Class

Public Class ModelOutputsMenuLoader

#Region "Properties"

    Private _objectWithMenu As IObjectWithMenu
    ''' <summary>
    ''' This propery used to stroreparent object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ObjectWithMenu() As IObjectWithMenu
        Get
            Return _objectWithMenu
        End Get
        Set(ByVal value As IObjectWithMenu)
            _objectWithMenu = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal objectWithMenu As IObjectWithMenu)
        Me.ObjectWithMenu = objectWithMenu
    End Sub

#End Region

#Region "Build menu"

    ''' <summary>
    ''' Used to build menu with all model's output values
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BuildMenu(ByVal models As List(Of ModelBase.IModel)) As System.Windows.Forms.ContextMenuStrip
        Dim ContextMenuStrip As New ContextMenuStrip

        ' clear garbage from ToolStrip
        ContextMenuStrip.Items.Clear()

        'add fields
        BuildList(ContextMenuStrip, models)

        'add const
        AddConstItem(ContextMenuStrip)

        Return ContextMenuStrip
    End Function

#End Region

#Region "Private methods fro building menu"

    ''' <summary>
    ''' This sub used to add items to context menu list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BuildList(ByRef contextMenuStrip As ContextMenuStrip, _
                         ByVal models As List(Of ModelBase.IModel))

        If models Is Nothing Then Return

        For Each model As ModelBase.IModel In Models
            BuildModelsList(contextMenuStrip, model)
        Next

    End Sub

    ''' <summary>
    ''' This sub used to rebuild models list 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BuildModelsList(ByRef contextMenuStrip As ContextMenuStrip, _
                               ByVal model As ModelBase.IModel)

        Dim ItemName As String = model.GetName
        'remove previous version if exist
        If contextMenuStrip.Items.ContainsKey(ItemName) Then
            contextMenuStrip.Items.RemoveByKey(ItemName)
        End If

        ' add group item for model
        Dim ModelMenuItem As ToolStripMenuItem
        ModelMenuItem = AddGroup(ItemName, contextMenuStrip)

        FillValues(ModelMenuItem, model)

        If ModelMenuItem.DropDownItems.Count = 0 Then
            ModelMenuItem.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Adds menu items for model's output values
    ''' </summary>
    ''' <param name="systemMenuItem"></param>
    ''' <param name="model"></param>
    ''' <remarks></remarks>
    Private Sub FillValues(ByVal systemMenuItem As ToolStripMenuItem, _
                           ByVal model As ModelBase.IModel)

        systemMenuItem.DropDownItems.Clear()

        If model Is Nothing OrElse model.GetValues.Count = 0 Then Return

        Dim ItemList As New List(Of MenuItem)
        For Each Value As ModelBase.Value In model.GetValues
            If Value.Type <> ModelBase.Value.ValueType.Output Then Continue For

            Dim MenuItem As New MenuItem(model.GetName, Value.Name, Value.GroupName)
            ItemList.Add(MenuItem)
        Next

        For Each Item As MenuItem In ItemList
            Dim ClickableToolStripItem As New ToolStripMenuItem(Item.ValueName)
            ClickableToolStripItem.Tag = Item
            If String.IsNullOrEmpty(Item.GroupName) Then
                systemMenuItem.DropDownItems.Add(ClickableToolStripItem)
            Else
                Dim GroupItem As ToolStripMenuItem = GetGroupByName(systemMenuItem, Item.GroupName)
                GroupItem.DropDownItems.Add(ClickableToolStripItem)
            End If
            AddHandler ClickableToolStripItem.Click, AddressOf ClickableToolStripItem_Click
        Next

    End Sub

    ''' <summary>
    ''' This sub used to add item with 'const' to context menu list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddConstItem(ByRef contextMenuStrip As ContextMenuStrip)
        'add separator
        contextMenuStrip.Items.Add("-")

        'add menu item
        Dim ClickableToolStripItem As New ToolStripMenuItem("Constant")
        AddHandler ClickableToolStripItem.Click, AddressOf ConstToolStripItem_Click
        contextMenuStrip.Items.Add(ClickableToolStripItem)
    End Sub

#End Region

#Region "Functions for groups"

    ''' <summary>
    ''' This sub adds ToolStrip item to the given item
    ''' </summary>
    ''' <param name="label">Text of the item</param>
    ''' <param name="fieldNameContextMenuStrip">Toolstrip to which items should be added.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddGroup(ByVal label As String, _
                              ByRef fieldNameContextMenuStrip As ContextMenuStrip, _
                              Optional ByVal tag As String = "") _
                                     As ToolStripMenuItem

        Dim GroupMenuItem As New ToolStripMenuItem(label)
        GroupMenuItem.Name = label

        If Not String.IsNullOrEmpty(tag) Then
            GroupMenuItem.Tag = tag
            AddHandler GroupMenuItem.Click, AddressOf ClickableToolStripItem_Click
        End If

        fieldNameContextMenuStrip.Items.Add(GroupMenuItem)

        Return GroupMenuItem
    End Function

    ''' <summary>
    ''' Gets group from parent item by its name; if group does not exists it will be created
    ''' </summary>
    ''' <param name="parentItem"></param>
    ''' <param name="groupName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGroupByName(ByVal parentItem As ToolStripMenuItem, ByVal groupName As String) As ToolStripMenuItem
        Dim GroupItem As ToolStripMenuItem

        For Each Item As ToolStripMenuItem In parentItem.DropDownItems
            If Item.Text = groupName AndAlso Item.Tag Is Nothing Then
                GroupItem = Item
                Exit For
            End If
        Next

        If GroupItem Is Nothing Then
            GroupItem = New ToolStripMenuItem(groupName)
            GroupItem.Tag = Nothing
            parentItem.DropDownItems.Add(GroupItem)
        End If

        Return GroupItem
    End Function

#End Region

#Region "Event handlers"

    ''' <summary>
    ''' This sub handles clicks on menu items
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ClickableToolStripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If sender Is Nothing OrElse Not TypeOf sender Is ToolStripMenuItem Then
            Throw New Exception("Toolstrip was wrongly designed. Incorrect object in sender")
        End If

        Dim ToolStripMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        Dim MenuItem As MenuItem = TryCast(ToolStripMenuItem.Tag, MenuItem)

        If ObjectWithMenu IsNot Nothing Then
            ObjectWithMenu.MenuItemSelected(MenuItem)
        End If

    End Sub

    ''' <summary>
    ''' This sub handles clicks on menu item 'const'
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ConstToolStripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If sender Is Nothing OrElse Not TypeOf sender Is ToolStripMenuItem Then
            Throw New Exception("Toolstrip was wrongly designed. Incorrect object in sender")
        End If

        Dim ConstString As String
        ConstString = InputBox("Please, determine the constant:")
        If Not String.IsNullOrEmpty(ConstString) Then
            Dim ConstValue As Double
            If Double.TryParse(ConstString, ConstValue) Then
                If ObjectWithMenu IsNot Nothing Then
                    Dim MenuItem As New MenuItem()
                    MenuItem.LinkConst = ConstValue
                    ObjectWithMenu.MenuItemSelected(MenuItem)
                End If
            Else
                MsgBox("Unable to parse double value", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            End If
        End If
    End Sub

#End Region

End Class
