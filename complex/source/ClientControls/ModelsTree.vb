Public Class ModelsTree
    Inherits System.Windows.Forms.TreeView

#Region "Const"

    Public Const MSG_BOX_TITLE As String = "Modelling tool"

#End Region

#Region "Enums"

    Public Enum TreeImages As Integer
        Folder = 0
        FolderOpened = 1
        No = 2
        Yes = 3
        GroupFolder = 4
        GroupFolderOpened = 5
        Root = 6
        History = 7
        Element = 8
        InputElement = 9
    End Enum

#End Region

#Region "Events"

    Public Event SelectedNodeChanged()

    Public Event ModelSelected(ByVal model As ModelBase.IModel)

    Public Event ValueSelected(ByVal value As ModelBase.Value)
    Public Event ValueClicked(ByVal value As ModelBase.Value)

    Public Event SavedConfigurationRemoved(ByVal savedConfiguration As ModelBase.SavedConfiguration)

    Public Event ChartRefreshNeeded()

#End Region

#Region "Declarations"

    Friend WithEvents TreeImageList As System.Windows.Forms.ImageList
    Friend WithEvents TreeContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SaveTreeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenTreeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditTreeMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveTreeMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend RunningProcess As New SharedControls.App.RunningProcess

#End Region

#Region "Properties"

    Private _IsSelectMode As Boolean = False
    ''' <summary>
    ''' If True tree is used in select mode (all values are shown with same icons, for input values special marks used)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsSelectMode() As Boolean
        Get
            Return _IsSelectMode
        End Get
        Set(ByVal value As Boolean)
            _IsSelectMode = value
        End Set
    End Property

    Private _ShowInputValues As Boolean = False
    ''' <summary>
    ''' If True values with type "Input" will be shown in tree
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowInputValues() As Boolean
        Get
            Return _ShowInputValues
        End Get
        Set(ByVal value As Boolean)
            _ShowInputValues = value
            FillTree()
        End Set
    End Property

    Private _ShowInternalValues As Boolean = False
    ''' <summary>
    ''' If True values with type "Internal" will be shown in tree
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowInternalValues() As Boolean
        Get
            Return _ShowInternalValues
        End Get
        Set(ByVal value As Boolean)
            _ShowInternalValues = value
            FillTree()
        End Set
    End Property

    Private _ContextMenuAllowed As Boolean = False
    ''' <summary>
    ''' If True on right mouse click native fot this tree context nemu will be shown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContextMenuAllowed() As Boolean
        Get
            Return _ContextMenuAllowed
        End Get
        Set(ByVal value As Boolean)
            _ContextMenuAllowed = value
        End Set
    End Property

    Private _NameEditingAllowed As Boolean = False
    ''' <summary>
    ''' If True on F2 object's name will be edited
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameEditingAllowed() As Boolean
        Get
            Return _NameEditingAllowed
        End Get
        Set(ByVal value As Boolean)
            _NameEditingAllowed = value
        End Set
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

    Private _HistoryConfigurations As List(Of ModelBase.SavedConfiguration)
    ''' <summary>
    ''' Currently opened saved configurations (to be shown in tree as history)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property HistoryConfigurations() As List(Of ModelBase.SavedConfiguration)
        Get
            If _HistoryConfigurations Is Nothing Then
                _HistoryConfigurations = New List(Of ModelBase.SavedConfiguration)
            End If
            Return _HistoryConfigurations
        End Get
        Set(ByVal value As List(Of ModelBase.SavedConfiguration))
            _HistoryConfigurations = value
        End Set
    End Property

    ''' <summary>
    ''' Ruterns selected in tree value if any
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedValue() As ModelBase.Value
        Get
            If SelectedNode Is Nothing OrElse _
               SelectedNode.Tag Is Nothing OrElse _
               Not TypeOf SelectedNode.Tag Is ModelBase.Value _
            Then
                Return Nothing
            End If

            Return DirectCast(SelectedNode.Tag, ModelBase.Value)
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        InitializeComponents()
    End Sub

#End Region


#Region "Initialize"

    Private Sub InitializeComponents()
        '
        'TreeImageList
        '
        Me.TreeImageList = New System.Windows.Forms.ImageList
        Me.ImageList = Me.TreeImageList

        Me.TreeImageList.TransparentColor = System.Drawing.Color.Transparent

        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.folder_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.folder_open_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.delete_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.apply_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.blue_folder_downloads_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.blue_folder_open_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.StyleXP_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.history_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.element_16x16)
        Me.TreeImageList.Images.Add(Global.ClientControls.My.Resources.Resources.element_into_16x16)

        Me.TreeImageList.Images.SetKeyName(0, "folder_16x16.png")
        Me.TreeImageList.Images.SetKeyName(1, "folder_open_16x16.png")
        Me.TreeImageList.Images.SetKeyName(2, "delete_16x16.png")
        Me.TreeImageList.Images.SetKeyName(3, "apply_16x16.png")
        Me.TreeImageList.Images.SetKeyName(4, "blue_folder_downloads_16x16.png")
        Me.TreeImageList.Images.SetKeyName(5, "blue_folder_open_16x16.png")
        Me.TreeImageList.Images.SetKeyName(6, "StyleXP_16x16.jpg")
        Me.TreeImageList.Images.SetKeyName(7, "history_16x16.png")
        Me.TreeImageList.Images.SetKeyName(8, "element_16x16.jpg")
        Me.TreeImageList.Images.SetKeyName(9, "element_into_16x16.png")


        Me.TreeContextMenu = New System.Windows.Forms.ContextMenuStrip()
        Me.SaveTreeMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenTreeMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditTreeMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveTreeMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TreeContextMenu.SuspendLayout()
        '
        'TreeContextMenu
        '
        Me.TreeContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveTreeMenuItem, Me.OpenTreeMenuItem, Me.EditTreeMenuItem, Me.RemoveTreeMenuItem})
        Me.TreeContextMenu.Name = "TreeContextMenu"
        Me.TreeContextMenu.Size = New System.Drawing.Size(125, 92)
        '
        'SaveTreeMenuItem
        '
        Me.SaveTreeMenuItem.Image = Global.ClientControls.My.Resources.Resources.save_blue_16x16
        Me.SaveTreeMenuItem.Name = "SaveTreeMenuItem"
        Me.SaveTreeMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.SaveTreeMenuItem.Text = "Save"
        '
        'OpenTreeMenuItem
        '
        Me.OpenTreeMenuItem.Image = Global.ClientControls.My.Resources.Resources.open_16x16
        Me.OpenTreeMenuItem.Name = "OpenTreeMenuItem"
        Me.OpenTreeMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.OpenTreeMenuItem.Text = "Open"
        '
        'EditTreeMenuItem
        '
        Me.EditTreeMenuItem.Image = Global.ClientControls.My.Resources.Resources.edit_16x16
        Me.EditTreeMenuItem.Name = "EditTreeMenuItem"
        Me.EditTreeMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.EditTreeMenuItem.Text = "Edit"
        '
        'RemoveTreeMenuItem
        '
        Me.RemoveTreeMenuItem.Image = Global.ClientControls.My.Resources.Resources.delete_16x16
        Me.RemoveTreeMenuItem.Name = "RemoveTreeMenuItem"
        Me.RemoveTreeMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.RemoveTreeMenuItem.Text = "Remove"

        Me.TreeContextMenu.ResumeLayout(False)

    End Sub

#End Region

#Region "Public methods"

    Public Sub SetConfiguration(ByVal configuration As ModelBase.Configuration)
        CurrentConfig = configuration

        FillTree()
    End Sub

    Public Sub AddHistoryConfiguration(ByVal savedConfiguration As ModelBase.SavedConfiguration)
        If savedConfiguration Is Nothing Then Return

        If Not HistoryConfigurations.Contains(savedConfiguration) Then
            HistoryConfigurations.Add(savedConfiguration)
            FillTree()
        End If
    End Sub

    Public Sub RemoveHistoryConfiguration(ByVal savedConfiguration As ModelBase.SavedConfiguration)
        If savedConfiguration Is Nothing Then Return

        If HistoryConfigurations.Contains(savedConfiguration) Then
            HistoryConfigurations.Remove(savedConfiguration)
            FillTree()
        End If
    End Sub

#End Region

#Region "Fill tree"

    Private Sub FillTree()
        Nodes.Clear()

        Tree_ShowCurrentConfig()

        Tree_ShowHistory()

    End Sub

    Private Sub Tree_ShowCurrentConfig()
        If CurrentConfig Is Nothing Then Return

        'add root node
        Dim ConfigNode As System.Windows.Forms.TreeNode
        ConfigNode = New System.Windows.Forms.TreeNode(CurrentConfig.Name)
        ConfigNode.Tag = CurrentConfig
        ConfigNode.ImageIndex = TreeImages.Root
        ConfigNode.SelectedImageIndex = TreeImages.Root
        Nodes.Add(ConfigNode)

        'add nodes for models
        If CurrentConfig.Models IsNot Nothing AndAlso CurrentConfig.Models.Count > 0 Then

            Dim ModelNode As System.Windows.Forms.TreeNode
            Dim ValueNode As System.Windows.Forms.TreeNode

            For Each Model As ModelBase.IModel In CurrentConfig.Models
                ModelNode = New System.Windows.Forms.TreeNode(Model.DisplayName)
                ModelNode.ToolTipText = Model.Description
                ModelNode.Tag = Model
                ModelNode.ImageIndex = TreeImages.Folder
                ModelNode.SelectedImageIndex = TreeImages.Folder
                ConfigNode.Nodes.Add(ModelNode)

                For Each Value As ModelBase.Value In Model.GetValues
                    If Not ShowInputValues AndAlso Value.Type = ModelBase.Value.ValueType.Input Then Continue For
                    If Not ShowInternalValues AndAlso Value.Type = ModelBase.Value.ValueType.Internal Then Continue For

                    ValueNode = New System.Windows.Forms.TreeNode(Value.DisplayName)
                    ValueNode.Tag = Value
                    UpdateImageForValueNode(ValueNode, Value)

                    If String.IsNullOrEmpty(Value.GroupName) Then
                        'no grouping for this value -> just add node under model's node
                        ModelNode.Nodes.Add(ValueNode)
                    Else
                        'get (create) group node and add vaule node under this node
                        GetGroupNode(ModelNode, Value.GroupName).Nodes.Add(ValueNode)
                    End If
                Next
            Next

        End If

        'select first node
        Focus()
        ConfigNode.Expand()
        SelectedNode = ConfigNode
    End Sub

    Private Sub Tree_ShowHistory()
        If HistoryConfigurations.Count = 0 Then Return

        For Each SavedConfiguration As ModelBase.SavedConfiguration In HistoryConfigurations
            Tree_ShowSavedConfiguration(SavedConfiguration)
        Next
    End Sub

    Private Sub Tree_ShowSavedConfiguration(ByVal savedConfiguration As ModelBase.SavedConfiguration)
        'add root node
        Dim ConfigNode As System.Windows.Forms.TreeNode
        ConfigNode = New System.Windows.Forms.TreeNode(savedConfiguration.Name)
        ConfigNode.Tag = savedConfiguration
        ConfigNode.ImageIndex = TreeImages.History
        ConfigNode.SelectedImageIndex = TreeImages.History
        Nodes.Add(ConfigNode)

        'add nodes for models
        If savedConfiguration.Models IsNot Nothing AndAlso savedConfiguration.Models.Count > 0 Then

            Dim ModelNode As System.Windows.Forms.TreeNode
            Dim ValueNode As System.Windows.Forms.TreeNode

            For Each Model As ModelBase.IModel In savedConfiguration.Models
                ModelNode = New System.Windows.Forms.TreeNode(Model.DisplayName)
                ModelNode.ToolTipText = Model.Description
                ModelNode.Tag = Model
                ModelNode.ImageIndex = TreeImages.Folder
                ModelNode.SelectedImageIndex = TreeImages.Folder
                ConfigNode.Nodes.Add(ModelNode)

                For Each Value As ModelBase.Value In Model.GetValues
                    If Not ShowInputValues AndAlso Value.Type = ModelBase.Value.ValueType.Input Then Continue For
                    If Not ShowInternalValues AndAlso Value.Type = ModelBase.Value.ValueType.Internal Then Continue For

                    ValueNode = New System.Windows.Forms.TreeNode(Value.DisplayName)
                    ValueNode.Tag = Value
                    UpdateImageForValueNode(ValueNode, Value)

                    If String.IsNullOrEmpty(Value.GroupName) Then
                        'no grouping for this value -> just add node under model's node
                        ModelNode.Nodes.Add(ValueNode)
                    Else
                        'get (create) group node and add vaule node under this node
                        GetGroupNode(ModelNode, Value.GroupName).Nodes.Add(ValueNode)
                    End If
                Next
            Next

        End If
    End Sub

#End Region


#Region "Events"

    Private Sub ModelsTree_AfterExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterExpand
        If e.Node.Tag Is Nothing Then
            'group node
            e.Node.SelectedImageIndex = TreeImages.GroupFolderOpened
            e.Node.ImageIndex = TreeImages.GroupFolderOpened
        ElseIf TypeOf e.Node.Tag Is ModelBase.IModel Then
            'model node
            e.Node.SelectedImageIndex = TreeImages.FolderOpened
            e.Node.ImageIndex = TreeImages.FolderOpened
        End If
    End Sub

    Private Sub ModelsTree_AfterCollapse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterCollapse
        If e.Node.Tag Is Nothing Then
            'group node
            e.Node.SelectedImageIndex = TreeImages.GroupFolder
            e.Node.ImageIndex = TreeImages.GroupFolder
        ElseIf TypeOf e.Node.Tag Is ModelBase.IModel Then
            'model node
            e.Node.SelectedImageIndex = TreeImages.Folder
            e.Node.ImageIndex = TreeImages.Folder
        End If
    End Sub

    Private Sub ModelsTree_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        If e.Node Is Nothing Then Return

        'if right mouse click on root - show context menu
        If e.Button = Windows.Forms.MouseButtons.Right AndAlso _
           TreeNode_HasContextMenu(e.Node) _
           Then
            SelectedNode = e.Node
            TreeContextMenu_CheckButtons(e.Node.Tag)
            TreeContextMenu.Show(Me, e.Location)
        End If

        If e.Button <> Windows.Forms.MouseButtons.Left Then Return

        If TypeOf e.Node.Tag Is ModelBase.Value Then
            Dim Value As ModelBase.Value = DirectCast(e.Node.Tag, ModelBase.Value)
            If Not IsSelectMode Then
                Value.Visible = Not Value.Visible
                UpdateImageForValueNode(e.Node, Value)
            End If
            RaiseEvent ValueSelected(Value)
        End If

        Dim SelectedItem As Object = e.Node.Tag
        If TypeOf SelectedItem Is ModelBase.IModel Then
            RaiseEvent ModelSelected(DirectCast(SelectedItem, ModelBase.IModel))
        End If

        SelectedNode = e.Node
        RaiseEvent SelectedNodeChanged()

    End Sub

    Private Sub ModelsTree_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles Me.NodeMouseDoubleClick
        If e.Node Is Nothing Then Return

        If e.Button <> Windows.Forms.MouseButtons.Left Then Return

        SelectedNode = e.Node

        If TypeOf e.Node.Tag Is ModelBase.Value Then
            RaiseEvent ValueClicked(DirectCast(e.Node.Tag, ModelBase.Value))
        End If

    End Sub

    Private Sub ModelsTree_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso _
           Equals(sender) AndAlso _
           SelectedNode IsNot Nothing AndAlso _
           CanEditNodeName(SelectedNode) _
           Then
            LabelEdit = True
            SelectedNode.BeginEdit()
            e.Handled = True
        End If
    End Sub

    Private Sub ModelsTree_AfterLabelEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles Me.AfterLabelEdit
        If e Is Nothing OrElse e.Node Is Nothing OrElse e.Node.Tag Is Nothing Then
            e.CancelEdit = True
            Return
        End If

        If String.IsNullOrEmpty(e.Label) Then
            e.CancelEdit = True
            Return
        End If

        If TypeOf e.Node.Tag Is ModelBase.IModel Then
            CType(e.Node.Tag, ModelBase.IModel).DisplayName = e.Label
        ElseIf TypeOf e.Node.Tag Is ModelBase.Value Then
            CType(e.Node.Tag, ModelBase.Value).DisplayName = e.Label
        End If

        LabelEdit = False
    End Sub

#End Region

#Region "Helping methods"

    ''' <summary>
    ''' Sets convinient image in given tree node
    ''' </summary>
    ''' <param name="valueNode"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Private Sub UpdateImageForValueNode(ByVal valueNode As TreeNode, ByVal value As ModelBase.Value)
        If IsSelectMode Then
            If value.Type = ModelBase.Value.ValueType.Input Then
                valueNode.ImageIndex = TreeImages.InputElement
                valueNode.SelectedImageIndex = TreeImages.InputElement
            Else
                valueNode.ImageIndex = TreeImages.Element
                valueNode.SelectedImageIndex = TreeImages.Element
            End If
        Else
            If value.Visible Then
                valueNode.ImageIndex = TreeImages.Yes
                valueNode.SelectedImageIndex = TreeImages.Yes
            Else
                valueNode.ImageIndex = TreeImages.No
                valueNode.SelectedImageIndex = TreeImages.No
            End If
        End If
    End Sub

    ''' <summary>
    ''' Gets group node from given model node or creates if it does not exist
    ''' </summary>
    ''' <param name="modelNode">Model node (parent for group node)</param>
    ''' <param name="groupName">Name of the group</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGroupNode(ByVal modelNode As TreeNode, ByVal groupName As String) As TreeNode
        Dim GroupNode As TreeNode

        If modelNode IsNot Nothing AndAlso modelNode.Nodes IsNot Nothing Then
            For Each Node As TreeNode In modelNode.Nodes
                If Node.Text = groupName AndAlso Node.Tag Is Nothing Then
                    GroupNode = Node
                    Exit For
                End If
            Next
        End If

        If GroupNode Is Nothing Then
            GroupNode = New TreeNode(groupName)
            GroupNode.Tag = Nothing
            GroupNode.ImageIndex = TreeImages.GroupFolder
            GroupNode.SelectedImageIndex = TreeImages.GroupFolder
            modelNode.Nodes.Add(GroupNode)
        End If

        Return GroupNode
    End Function

    ''' <summary>
    ''' If true name of given node can be edited using F2
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanEditNodeName(ByVal node As TreeNode) As Boolean
        If Not NameEditingAllowed Then Return False

        If node.Tag Is Nothing Then Return False

        Dim result As Boolean = False

        If TypeOf node.Tag Is ModelBase.IModel Then
            result = True
        ElseIf TypeOf node.Tag Is ModelBase.Value Then
            result = True
        End If

        Return result
    End Function

#End Region

#Region "Context menu"

    ''' <summary>
    ''' Returns true if for this node context menu is available
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TreeNode_HasContextMenu(ByVal node As TreeNode) As Boolean
        If Not ContextMenuAllowed Then Return False

        If node.Tag Is Nothing Then Return False

        If TypeOf node.Tag Is ModelBase.Configuration OrElse _
           TypeOf node.Tag Is ModelBase.SavedConfiguration OrElse _
           TypeOf node.Tag Is ModelBase.Value OrElse _
           TypeOf node.Tag Is ModelBase.IModel _
        Then
            Return True
        End If

        Return False
    End Function

    Private Sub TreeContextMenu_CheckButtons(ByVal selectedObject As Object)
        If TypeOf selectedObject Is ModelBase.Configuration Then
            SaveTreeMenuItem.Visible = True
            OpenTreeMenuItem.Visible = True
            EditTreeMenuItem.Visible = False
            RemoveTreeMenuItem.Visible = False
        ElseIf TypeOf selectedObject Is ModelBase.Value Then
            SaveTreeMenuItem.Visible = False
            OpenTreeMenuItem.Visible = False
            EditTreeMenuItem.Visible = True
            RemoveTreeMenuItem.Visible = False
        ElseIf TypeOf selectedObject Is ModelBase.IModel Then
            SaveTreeMenuItem.Visible = False
            OpenTreeMenuItem.Visible = False
            EditTreeMenuItem.Visible = True
            RemoveTreeMenuItem.Visible = False
        ElseIf TypeOf selectedObject Is ModelBase.SavedConfiguration Then
            SaveTreeMenuItem.Visible = False
            OpenTreeMenuItem.Visible = False
            EditTreeMenuItem.Visible = False
            RemoveTreeMenuItem.Visible = True
        End If
    End Sub

    Private Sub SaveTreeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTreeMenuItem.Click
        If CurrentConfig Is Nothing Then Return

        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = ModelBase.Configuration.RESULT_EXTENSION
        SaveFileDialog.InitialDirectory = MyApplication.DataFolder
        SaveFileDialog.Filter = String.Format("tree selection (*{0})|*{0}", ModelBase.Configuration.SELECTION_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim FileName As String = SaveFileDialog.FileName

            SaveFileDialog.Dispose()
            GC.Collect()
            Me.Refresh()

            ShowProgress("Saving tree selection...")
            Try
                CurrentConfig.SaveSelection(FileName)
                ShowInformationMessage("Selection was successfully saved")
            Catch ex As Exception
                ShowExeptionMessage(ex, "Saving selection failed")
            Finally
                HideProgress()
            End Try

        End If
    End Sub

    Private Sub OpenTreeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenTreeMenuItem.Click
        If CurrentConfig Is Nothing Then Return

        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = ModelBase.Configuration.CONFIG_EXTENSION
        OpenFileDialog.InitialDirectory = MyApplication.DataFolder
        OpenFileDialog.Filter = String.Format("tree selection (*{0})|*{0}", ModelBase.Configuration.SELECTION_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim XmlDocument As New System.Xml.XmlDocument
                XmlDocument.Load(OpenFileDialog.FileName)

                CurrentConfig.SelectionFromXml(XmlDocument)

                'refresh tree
                RefreshTree()

                'refresh chart
                RaiseEvent ChartRefreshNeeded()

            Catch ex As Exception
                ShowExeptionMessage(ex, String.Format("Unable to load selection from file '{0}'", OpenFileDialog.FileName))
                Return
            End Try
        End If
    End Sub

    Private Sub EditTreeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditTreeMenuItem.Click
        If SelectedNode Is Nothing OrElse SelectedNode.Tag Is Nothing Then Return

        If TypeOf SelectedNode.Tag Is ModelBase.Value Then
            EditValue(CType(SelectedNode.Tag, ModelBase.Value))
        ElseIf TypeOf SelectedNode.Tag Is ModelBase.IModel Then
            EditModel(CType(SelectedNode.Tag, ModelBase.IModel))
        End If

    End Sub

    Private Sub RemoveTreeMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveTreeMenuItem.Click
        If SelectedNode Is Nothing OrElse SelectedNode.Tag Is Nothing Then Return

        Dim SavedConfiguration As ModelBase.SavedConfiguration
        SavedConfiguration = TryCast(SelectedNode.Tag, ModelBase.SavedConfiguration)
        If SavedConfiguration IsNot Nothing Then
            HistoryConfigurations.Remove(SavedConfiguration)
            SelectedNode.Remove()

            RaiseEvent SavedConfigurationRemoved(SavedConfiguration)

            'refresh chart
            RaiseEvent ChartRefreshNeeded()

        End If
    End Sub

    Private Sub EditValue(ByVal value As ModelBase.Value)
        Dim ValueNode As TreeNode = SelectedNode

        Dim OldGroupName As String = value.GroupName

        Dim ValueEditor As New ValuePropertyEditor(value)
        If ValueEditor.ShowDialog = Windows.Forms.DialogResult.OK Then
            ValueNode.Text = value.DisplayName
            If value.GroupName <> OldGroupName Then
                Dim ModelNode As TreeNode = GetModelNodeForValueNode(ValueNode)
                If ModelNode IsNot Nothing Then
                    ValueNode.Remove()
                    If String.IsNullOrEmpty(value.GroupName) Then
                        Dim GroupNode As TreeNode = GetGroupNode(ModelNode, OldGroupName)
                        If GroupNode IsNot Nothing AndAlso GroupNode.Nodes.Count = 0 Then
                            GroupNode.Remove()
                        End If
                        ModelNode.Nodes.Add(ValueNode)
                    Else
                        GetGroupNode(ModelNode, value.GroupName).Nodes.Add(ValueNode)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub EditModel(ByVal model As ModelBase.IModel)
        Dim ModelNode As TreeNode = SelectedNode
        Dim ModelEditor As New ModelPropertyEditor(model)
        If ModelEditor.ShowDialog = Windows.Forms.DialogResult.OK Then
            ModelNode.Text = model.DisplayName
            ModelNode.ToolTipText = model.Description
        End If
    End Sub

    Private Function GetModelNodeForValueNode(ByVal valueNode As TreeNode) As TreeNode
        If valueNode Is Nothing Then Return Nothing
        Dim ParentNode As TreeNode = valueNode.Parent
        Dim Model As ModelBase.IModel

        While Model Is Nothing AndAlso ParentNode IsNot Nothing
            If ParentNode.Tag IsNot Nothing AndAlso TypeOf ParentNode.Tag Is ModelBase.IModel Then
                Model = CType(ParentNode.Tag, ModelBase.IModel)
            End If
            If Model Is Nothing Then
                ParentNode = ParentNode.Parent
            End If
        End While

        If Model IsNot Nothing Then
            Return ParentNode
        End If

        Return Nothing
    End Function


    Private Sub RefreshTree()
        If Nodes Is Nothing OrElse Nodes.Count = 0 Then Return

        For Each Node As TreeNode In Nodes
            If Node.Tag IsNot Nothing AndAlso TypeOf Node.Tag Is ModelBase.Value Then
                Dim Value As ModelBase.Value = DirectCast(Node.Tag, ModelBase.Value)
                UpdateImageForValueNode(Node, Value)
            End If
            RefreshTreeRecurcive(Node)
        Next
    End Sub

    Private Sub RefreshTreeRecurcive(ByVal parentNode As TreeNode)
        If parentNode Is Nothing OrElse parentNode.Nodes Is Nothing OrElse parentNode.Nodes.Count = 0 Then Return

        For Each Node As TreeNode In parentNode.Nodes
            If Node.Tag IsNot Nothing AndAlso TypeOf Node.Tag Is ModelBase.Value Then
                Dim Value As ModelBase.Value = DirectCast(Node.Tag, ModelBase.Value)
                UpdateImageForValueNode(Node, Value)
            End If
            RefreshTreeRecurcive(Node)
        Next

    End Sub

#End Region

#Region "Progress-messages"

    Private Sub ShowProgress(ByVal message As String)
        If Not RunningProcess.IsProgressRunning Then
            RunningProcess = New SharedControls.App.RunningProcess(message)
            RunningProcess.StartProgress()
        Else
            RunningProcess.UpdateProgress(message)
        End If
    End Sub

    Private Sub HideProgress()
        If RunningProcess.IsProgressRunning Then
            RunningProcess.EndProgress()
        End If
    End Sub

    Private Sub ShowInformationMessage(ByVal prompt As String)
        HideProgress()
        MsgBox(prompt, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
    End Sub

    Private Sub ShowErrorMessage(ByVal prompt As String)
        HideProgress()
        MsgBox(prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, MSG_BOX_TITLE)
    End Sub

    Private Sub ShowExeptionMessage(ByVal ex As Exception, ByVal description As String)
        HideProgress()

        Dim ExceptionMessage As New SharedControls.ExceptionMessage(description, ex)
        ExceptionMessage.ShowDialog()

    End Sub

#End Region



End Class
