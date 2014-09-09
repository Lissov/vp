Public Class ConfigurationGraphForm

#Region "Members"

    Private Graph As New Microsoft.Glee.Drawing.Graph("ConfigurationGraph")

#End Region

#Region "Properties"

    Private _Configuration As ModelBase.Configuration
    Private Property Configuration() As ModelBase.Configuration
        Get
            Return _Configuration
        End Get
        Set(ByVal value As ModelBase.Configuration)
            _Configuration = value
        End Set
    End Property

    Private _ShowAllEdges As Boolean = False
    ''' <summary>
    ''' If true edge will be generated for each link, otherwise - one edge between two models
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowAllEdges() As Boolean
        Get
            Return _ShowAllEdges
        End Get
        Set(ByVal value As Boolean)
            _ShowAllEdges = value
        End Set
    End Property

    Private _ShowLablesForEdges As Boolean = True
    ''' <summary>
    ''' If true label with link value name(s) will be added to each edge
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowLablesForEdges() As Boolean
        Get
            Return _ShowLablesForEdges
        End Get
        Set(ByVal value As Boolean)
            _ShowLablesForEdges = value
        End Set
    End Property

    Private _VertexColor As Microsoft.Glee.Drawing.Color = Microsoft.Glee.Drawing.Color.AliceBlue
    Public Property VertexColor() As Microsoft.Glee.Drawing.Color
        Get
            Return _VertexColor
        End Get
        Set(ByVal value As Microsoft.Glee.Drawing.Color)
            _VertexColor = value
        End Set
    End Property

    Private _VertexShape As Microsoft.Glee.Drawing.Shape = Microsoft.Glee.Drawing.Shape.Ellipse
    Public Property VertexShape() As Microsoft.Glee.Drawing.Shape
        Get
            Return _VertexShape
        End Get
        Set(ByVal value As Microsoft.Glee.Drawing.Shape)
            _VertexShape = value
        End Set
    End Property

    Private _EdgeColor As Microsoft.Glee.Drawing.Color = Microsoft.Glee.Drawing.Color.Black
    Public Property EdgeColor() As Microsoft.Glee.Drawing.Color
        Get
            Return _EdgeColor
        End Get
        Set(ByVal value As Microsoft.Glee.Drawing.Color)
            _EdgeColor = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal configuration As ModelBase.Configuration)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Graph.Directed = True

        Me.Configuration = configuration
    End Sub

#End Region

#Region "Build graph"

    Private Sub BuildGraph()
        If Configuration Is Nothing OrElse Configuration.Models Is Nothing OrElse Configuration.Models.Count = 0 Then Return

        'add vertexes
        For Each Model As ModelBase.IModel In Configuration.Models
            Dim ModelNode As Microsoft.Glee.Drawing.Node
            ModelNode = Graph.AddNode(Model.GetName)
            ModelNode.Attr.Fillcolor = VertexColor
            ModelNode.Attr.Shape = VertexShape
        Next

        'add edges
        If ShowAllEdges Then
            AddEdges_AllEdgesMode()
        Else
            AddEdges_UniqueEdgesMode()
        End If

        'create a viewer object 
        Dim Viewer As New Microsoft.Glee.GraphViewerGdi.GViewer()
        'bind the graph to the viewer 
        Viewer.Graph = Graph
        'associate the viewer with the form 
        MainPanel.SuspendLayout()
        Viewer.Dock = System.Windows.Forms.DockStyle.Fill
        MainPanel.Controls.Add(viewer)
        MainPanel.ResumeLayout()
    End Sub

    Private Sub AddEdges_AllEdgesMode()
        For Each Model As ModelBase.IModel In Configuration.Models
            If Model.GetValues Is Nothing Then Continue For

            For Each Value As ModelBase.Value In Model.GetValues
                If Value.Type = ModelBase.Value.ValueType.Input Then
                    If Value.LinkConst Is Nothing AndAlso Not String.IsNullOrEmpty(Value.LinkModelName) Then
                        Dim GraphEdge As Microsoft.Glee.Drawing.Edge
                        GraphEdge = Graph.AddEdge(Value.LinkModelName, Model.GetName)
                        GraphEdge.EdgeAttr.Color = EdgeColor
                        If ShowLablesForEdges Then GraphEdge.Attr.Label = Value.LinkValueName
                    End If
                End If
            Next

        Next
    End Sub

    Private Sub AddEdges_UniqueEdgesMode()
        Dim Edges As New Dictionary(Of String, Edge)

        For Each Model As ModelBase.IModel In Configuration.Models
            If Model.GetValues Is Nothing Then Continue For

            For Each Value As ModelBase.Value In Model.GetValues
                If Value.Type = ModelBase.Value.ValueType.Input Then
                    If Value.LinkConst Is Nothing AndAlso Not String.IsNullOrEmpty(Value.LinkModelName) Then
                        Dim Key As String = Edge.GetKey(Value.LinkModelName, Model.GetName)
                        If Not Edges.ContainsKey(Key) Then
                            Edges.Add(Key, New Edge(Value.LinkModelName, Model.GetName, Value.LinkValueName))
                        Else
                            Edges(Key).Name &= "; " & Value.LinkValueName
                        End If
                    End If
                End If
            Next

        Next

        For Each Edge As Edge In Edges.Values
            Dim GraphEdge As Microsoft.Glee.Drawing.Edge
            GraphEdge = Graph.AddEdge(Edge.Source, Edge.Target)
            GraphEdge.EdgeAttr.Color = EdgeColor
            If ShowLablesForEdges Then GraphEdge.Attr.Label = Edge.Name
        Next

    End Sub

#End Region

    Private Sub ConfigurationGraphForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BuildGraph()
    End Sub

#Region "Hot keys handlers"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Dim e As New KeyEventArgs(keyData)

        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

#End Region

End Class