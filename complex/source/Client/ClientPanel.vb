Public Class ClientPanel

#Region "Properties"

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Text As String
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value
        End Set
    End Property

    Private _Visible As Boolean
    Public Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
        End Set
    End Property

    Private _Tag As Object
    Public Property Tag() As Object
        Get
            Return _Tag
        End Get
        Set(ByVal value As Object)
            _Tag = value
        End Set
    End Property

#End Region

#Region "Public methods"

    ''' <summary>
    ''' Changes visibility of the panel.
    ''' </summary>
    ''' <param name="visible">If True panel will be shown otherwise - hidden</param>
    ''' <remarks></remarks>
    Public Sub ChangeVisibility(ByVal visible As Boolean)
        If Tag Is Nothing Then Return

        If TypeOf Tag Is DevExpress.XtraBars.Docking.DockPanel Then
            If visible Then
                DirectCast(Tag, DevExpress.XtraBars.Docking.DockPanel).Restore()
            Else
                DirectCast(Tag, DevExpress.XtraBars.Docking.DockPanel).HideImmediately()
                DirectCast(Tag, DevExpress.XtraBars.Docking.DockPanel).Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
            End If
        ElseIf TypeOf Tag Is DevExpress.XtraTab.XtraTabPage Then
            DirectCast(Tag, DevExpress.XtraTab.XtraTabPage).PageVisible = visible
        End If

    End Sub

#End Region

#Region "Convertors"

    Public Shared Function GetClientPanel(ByVal docPanel As DevExpress.XtraBars.Docking.DockPanel) As ClientPanel
        Dim ClientPanel As New ClientPanel

        ClientPanel.Name = docPanel.Name
        ClientPanel.Text = docPanel.Text
        ClientPanel.Visible = docPanel.Index >= 0

        ClientPanel.Tag = docPanel

        Return ClientPanel
    End Function

    Public Shared Function GetClientPanel(ByVal tabPanel As DevExpress.XtraTab.XtraTabPage) As ClientPanel
        Dim ClientPanel As New ClientPanel

        ClientPanel.Name = tabPanel.Name
        ClientPanel.Text = tabPanel.Text
        ClientPanel.Visible = tabPanel.PageVisible

        ClientPanel.Tag = tabPanel

        Return ClientPanel
    End Function


#End Region

End Class
