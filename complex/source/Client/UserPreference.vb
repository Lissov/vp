Public Class UserPreference
    Inherits ModelBase.ObjectBase

#Region "Const"

    Public Const FILE_NAME As String = "default.prefs"

    Public Const PANELS_XML_ELEMENT As String = "Panels"
    Public Const FONT_XML_ELEMENT As String = "Font"
    Public Const BUTTONS_XML_ELEMENT As String = "Buttons"
    Public Const DOCKMANAGER_XML_ELEMENT As String = "DockManager"

#End Region

#Region "Base propeties"

    ''' <summary>
    ''' Returns name of the xml root for the class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides ReadOnly Property XmlName() As String
        Get
            Return "UserPreference"
        End Get
    End Property

#End Region

#Region "Properties"

    Private _ConfigurationFileName As String = String.Empty
    Public Property ConfigurationFileName() As String
        Get
            Return _ConfigurationFileName
        End Get
        Set(ByVal value As String)
            _ConfigurationFileName = value
        End Set
    End Property

    Private _ExperimentTime As Double = 10
    Public Property ExperimentTime() As Double
        Get
            Return _ExperimentTime
        End Get
        Set(ByVal value As Double)
            _ExperimentTime = value
        End Set
    End Property

    Private _HiddenTime As Double = 0
    Public Property HiddenTime() As Double
        Get
            Return _HiddenTime
        End Get
        Set(ByVal value As Double)
            _HiddenTime = value
        End Set
    End Property

    Private _ShownPoints As Double = 100
    ''' <summary>
    ''' Percentage of points which should be shown in chart
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShownPoints() As Double
        Get
            Return _ShownPoints
        End Get
        Set(ByVal value As Double)
            _ShownPoints = value
        End Set
    End Property

    Private _ShowResultInProgess As Boolean = False
    Public Property ShowResultInProgess() As Boolean
        Get
            Return _ShowResultInProgess
        End Get
        Set(ByVal value As Boolean)
            _ShowResultInProgess = value
        End Set
    End Property

    Private _ShowResultAfterCalculating As Boolean = True
    Public Property ShowResultAfterCalculating() As Boolean
        Get
            Return _ShowResultAfterCalculating
        End Get
        Set(ByVal value As Boolean)
            _ShowResultAfterCalculating = value
        End Set
    End Property

    Private _ShowInputValues As Boolean = False
    Public Property ShowInputValues() As Boolean
        Get
            Return _ShowInputValues
        End Get
        Set(ByVal value As Boolean)
            _ShowInputValues = value
        End Set
    End Property

    Private _ShowInternalValues As Boolean = False
    Public Property ShowInternalValues() As Boolean
        Get
            Return _ShowInternalValues
        End Get
        Set(ByVal value As Boolean)
            _ShowInternalValues = value
        End Set
    End Property

    Private _ShowHintOnSerieClick As Boolean = True
    Public Property ShowHintOnSerieClick() As Boolean
        Get
            Return _ShowHintOnSerieClick
        End Get
        Set(ByVal value As Boolean)
            _ShowHintOnSerieClick = value
        End Set
    End Property

    Private _ShowHintPercents As Boolean = False
    Public Property ShowHintPercents() As Boolean
        Get
            Return _ShowHintPercents
        End Get
        Set(ByVal value As Boolean)
            _ShowHintPercents = value
        End Set
    End Property

    Private _WindowState As FormWindowState = FormWindowState.Normal
    Public Property WindowState() As FormWindowState
        Get
            Return _WindowState
        End Get
        Set(ByVal value As FormWindowState)
            _WindowState = value
        End Set
    End Property

    Private _ClientPanels As List(Of Client.ClientPanel) = Nothing
    Public Property ClientPanels() As List(Of Client.ClientPanel)
        Get
            If _ClientPanels Is Nothing Then
                _ClientPanels = New List(Of Client.ClientPanel)
            End If
            Return _ClientPanels
        End Get
        Set(ByVal value As List(Of Client.ClientPanel))
            _ClientPanels = value
        End Set
    End Property

    Private _ClientButtons As List(Of DevExpress.XtraBars.BarButtonItem) = Nothing
    Public Property ClientButtons() As List(Of DevExpress.XtraBars.BarButtonItem)
        Get
            If _ClientButtons Is Nothing Then
                _ClientButtons = New List(Of DevExpress.XtraBars.BarButtonItem)
            End If
            Return _ClientButtons
        End Get
        Set(ByVal value As List(Of DevExpress.XtraBars.BarButtonItem))
            _ClientButtons = value
        End Set
    End Property

    Private _Font As Font = Nothing
    Public Property Font() As Font
        Get
            Return _Font
        End Get
        Set(ByVal value As Font)
            _Font = value
        End Set
    End Property

    Private _LeftScaleIncrement As Double
    ''' <summary>
    ''' Increment of the left chart scale
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LeftScaleIncrement() As Double
        Get
            Return _LeftScaleIncrement
        End Get
        Set(ByVal value As Double)
            _LeftScaleIncrement = value
        End Set
    End Property

    Private _BottomScaleIncrement As Double
    ''' <summary>
    ''' Increment of the bottom chart scale
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BottomScaleIncrement() As Double
        Get
            Return _BottomScaleIncrement
        End Get
        Set(ByVal value As Double)
            _BottomScaleIncrement = value
        End Set
    End Property

    Private _MaxPointsPerSecond As Integer = -1
    ''' <summary>
    ''' Maximum amount of shown points per 1 sec ( -1 - all points)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaxPointsPerSecond() As Integer
        Get
            Return _MaxPointsPerSecond
        End Get
        Set(ByVal value As Integer)
            _MaxPointsPerSecond = value
        End Set
    End Property

    Private _ShowHintBevel As Boolean = False
    ''' <summary>
    ''' Do we need to show bevel for hint
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowHintBevel() As Boolean
        Get
            Return _ShowHintBevel
        End Get
        Set(ByVal value As Boolean)
            _ShowHintBevel = value
        End Set
    End Property

    Private _HintColor As Color = Color.Transparent
    ''' <summary>
    ''' Color of the hint
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HintColor() As Color
        Get
            Return _HintColor
        End Get
        Set(ByVal value As Color)
            _HintColor = value
        End Set
    End Property

    Private _MaxMarksCount As Integer = 20
    ''' <summary>
    ''' Maximum amount of shown marks
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaxMarksCount() As Integer
        Get
            If _MaxMarksCount < 1 Then
                _MaxMarksCount = 1
            End If
            Return _MaxMarksCount
        End Get
        Set(ByVal value As Integer)
            _MaxMarksCount = value
        End Set
    End Property


#Region "save result parameters"

    Private _Result_SaveOnlyVisiblePoints As Boolean = False
    Public Property Result_SaveOnlyVisiblePoints() As Boolean
        Get
            Return _Result_SaveOnlyVisiblePoints
        End Get
        Set(ByVal value As Boolean)
            _Result_SaveOnlyVisiblePoints = value
        End Set
    End Property

    Private _Report_SaveOnlyVisiblePoints As Boolean = False
    Public Property Report_SaveOnlyVisiblePoints() As Boolean
        Get
            Return _Report_SaveOnlyVisiblePoints
        End Get
        Set(ByVal value As Boolean)
            _Report_SaveOnlyVisiblePoints = value
        End Set
    End Property

    Private _ResultImage_SaveColoured As Boolean = True
    Public Property ResultImage_SaveColoured() As Boolean
        Get
            Return _ResultImage_SaveColoured
        End Get
        Set(ByVal value As Boolean)
            _ResultImage_SaveColoured = value
        End Set
    End Property

#End Region

#End Region


#Region "Constructors"

    Public Sub New(ByVal clientPanels As List(Of Client.ClientPanel), _
                   ByVal clientButtons As List(Of DevExpress.XtraBars.BarButtonItem))
        MyBase.New()

        Me.ClientPanels = clientPanels
        Me.ClientButtons = clientButtons
    End Sub

#End Region

#Region "Public methods"

    Public Function Save()
        Dim Result As Boolean = True

        Dim FileName As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, FILE_NAME)

        'delete file if exists
        If Functions.File.FileExists(FileName) Then
            Functions.File.DeleteFile(FileName, "")
        End If

        Try
            Dim XmlDocument As System.Xml.XmlDocument
            XmlDocument = Me.ToXmlDocument
            XmlDocument.Save(FileName)
        Catch ex As Exception
            Result = False
        End Try

        Return Result
    End Function

    Public Function Load() As UserPreference
        Dim Result As New UserPreference(Me.ClientPanels, Me.ClientButtons)

        Try
            Dim XmlDocument As New System.Xml.XmlDocument
            XmlDocument.Load(System.IO.Path.Combine(My.Application.Info.DirectoryPath, FILE_NAME))
            Result = FromXml(XmlDocument)
        Catch ex As Exception
        End Try

        Return Result
    End Function

#End Region

#Region "Private methods"

    Private Function GetPanelByName(ByVal name As String) As ClientPanel
        Dim Result As ClientPanel = Nothing

        For Each Panel As ClientPanel In ClientPanels
            If Panel.Name = name Then
                Result = Panel
                Exit For
            End If
        Next

        Return Result
    End Function

    Private Function GetButtonByName(ByVal name As String) As DevExpress.XtraBars.BarButtonItem
        Dim Result As DevExpress.XtraBars.BarButtonItem = Nothing

        For Each Button As DevExpress.XtraBars.BarButtonItem In ClientButtons
            If Button.Name = name Then
                Result = Button
                Exit For
            End If
        Next

        Return Result
    End Function

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save properties
        SetAttribute(CurrentElement, "ConfigurationFileName", ConfigurationFileName)
        SetAttribute(CurrentElement, "ExperimentTime", ExperimentTime)
        SetAttribute(CurrentElement, "HiddenTime", HiddenTime)
        SetAttribute(CurrentElement, "ShownPoints", ShownPoints)
        SetAttribute(CurrentElement, "ShowResultInProgess", ShowResultInProgess)
        SetAttribute(CurrentElement, "ShowResultAfterCalculating", ShowResultAfterCalculating)
        SetAttribute(CurrentElement, "ShowInputValues", ShowInputValues)
        SetAttribute(CurrentElement, "ShowInternalValues", ShowInternalValues)
        SetAttribute(CurrentElement, "ShowHintOnSerieClick", ShowHintOnSerieClick)
        SetAttribute(CurrentElement, "ShowHintPercents", ShowHintPercents)
        SetAttribute(CurrentElement, "WindowState", WindowState)
        SetAttribute(CurrentElement, "LeftScaleIncrement", LeftScaleIncrement)
        SetAttribute(CurrentElement, "BottomScaleIncrement", BottomScaleIncrement)
        SetAttribute(CurrentElement, "MaxPointsPerSecond", MaxPointsPerSecond)
        SetAttribute(CurrentElement, "ShowHintBevel", ShowHintBevel)
        SetAttribute(CurrentElement, "HintColor", HintColor.ToArgb)
        SetAttribute(CurrentElement, "MaxMarksCount", MaxMarksCount)
        'save result parameters
        SetAttribute(CurrentElement, "Result_SaveOnlyVisiblePoints", Result_SaveOnlyVisiblePoints)
        SetAttribute(CurrentElement, "Report_SaveOnlyVisiblePoints", Report_SaveOnlyVisiblePoints)
        SetAttribute(CurrentElement, "ResultImage_SaveColoured", ResultImage_SaveColoured)

        Dim SkinName As String
        SkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
        SetAttribute(CurrentElement, "SkinName", SkinName)

        'save panels
        Dim PanelsElement As System.Xml.XmlElement
        PanelsElement = CurrentElement.OwnerDocument.CreateElement(PANELS_XML_ELEMENT)
        CurrentElement.AppendChild(PanelsElement)
        For Each Panel As ClientPanel In ClientPanels
            PanelToXml(Panel, PanelsElement, Panel.Name)
        Next

        'save dock manager layout
        Dim DockManagerLayout As String = GetDockManagerLayout()
        If Not String.IsNullOrEmpty(DockManagerLayout) Then
            Dim DockManagerElement As System.Xml.XmlElement
            DockManagerElement = CurrentElement.OwnerDocument.CreateElement(DOCKMANAGER_XML_ELEMENT)
            CurrentElement.AppendChild(DockManagerElement)
            SetAttribute(DockManagerElement, "Layout", DockManagerLayout)
        End If

        'save font
        If Font IsNot Nothing Then
            Dim FontElement As System.Xml.XmlElement
            FontElement = CurrentElement.OwnerDocument.CreateElement(FONT_XML_ELEMENT)
            CurrentElement.AppendChild(FontElement)
            FontToXml(Font, FontElement)
        End If

        'save buttons
        Dim ButtonsElement As System.Xml.XmlElement
        ButtonsElement = CurrentElement.OwnerDocument.CreateElement(BUTTONS_XML_ELEMENT)
        CurrentElement.AppendChild(ButtonsElement)
        For Each Button As DevExpress.XtraBars.BarButtonItem In ClientButtons
            ButtonToXml(Button, ButtonsElement, Button.Name)
        Next

        Return CurrentElement
    End Function

    Public Function FromXml(ByVal xmlDocument As System.Xml.XmlDocument) As UserPreference
        If xmlDocument Is Nothing Then Return Nothing

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return Nothing

        Return FromXml(RootElement.Item(XmlName))
    End Function

    Public Function FromXml(ByVal currentElement As System.Xml.XmlElement) As UserPreference
        If currentElement Is Nothing OrElse _
           currentElement.Name <> XmlName _
           Then
            Return Nothing
        End If

        Dim UserPreference As New UserPreference(Me.ClientPanels, Me.ClientButtons)

        If currentElement.Attributes("ConfigurationFileName") IsNot Nothing Then
            UserPreference.ConfigurationFileName = GetString(currentElement, "ConfigurationFileName")
        End If
        If currentElement.Attributes("HiddenTime") IsNot Nothing Then
            UserPreference.HiddenTime = GetDouble(currentElement, "HiddenTime")
        End If
        If currentElement.Attributes("ShownPoints") IsNot Nothing Then
            UserPreference.ShownPoints = GetDouble(currentElement, "ShownPoints")
        End If
        If currentElement.Attributes("ExperimentTime") IsNot Nothing Then
            UserPreference.ExperimentTime = GetDouble(currentElement, "ExperimentTime")
        End If
        If currentElement.Attributes("ShowResultInProgess") IsNot Nothing Then
            UserPreference.ShowResultInProgess = GetBoolean(currentElement, "ShowResultInProgess")
        End If
        If currentElement.Attributes("ShowResultAfterCalculating") IsNot Nothing Then
            UserPreference.ShowResultAfterCalculating = GetBoolean(currentElement, "ShowResultAfterCalculating")
        End If
        If currentElement.Attributes("ShowInputValues") IsNot Nothing Then
            UserPreference.ShowInputValues = GetBoolean(currentElement, "ShowInputValues")
        End If
        If currentElement.Attributes("ShowInternalValues") IsNot Nothing Then
            UserPreference.ShowInternalValues = GetBoolean(currentElement, "ShowInternalValues")
        End If
        If currentElement.Attributes("ShowHintOnSerieClick") IsNot Nothing Then
            UserPreference.ShowHintOnSerieClick = GetBoolean(currentElement, "ShowHintOnSerieClick")
        End If
        If currentElement.Attributes("ShowHintPercents") IsNot Nothing Then
            UserPreference.ShowHintPercents = GetBoolean(currentElement, "ShowHintPercents")
        End If
        If currentElement.Attributes("WindowState") IsNot Nothing Then
            UserPreference.WindowState = CType(GetInteger(currentElement, "WindowState"), FormWindowState)
        End If
        If currentElement.Attributes("LeftScaleIncrement") IsNot Nothing Then
            UserPreference.LeftScaleIncrement = GetDouble(currentElement, "LeftScaleIncrement")
        End If
        If currentElement.Attributes("BottomScaleIncrement") IsNot Nothing Then
            UserPreference.BottomScaleIncrement = GetDouble(currentElement, "BottomScaleIncrement")
        End If

        If currentElement.Attributes("MaxPointsPerSecond") IsNot Nothing Then
            UserPreference.MaxPointsPerSecond = GetInteger(currentElement, "MaxPointsPerSecond")
        End If
        If currentElement.Attributes("ShowHintBevel") IsNot Nothing Then
            UserPreference.ShowHintBevel = GetBoolean(currentElement, "ShowHintBevel")
        End If
        If currentElement.Attributes("HintColor") IsNot Nothing Then
            UserPreference.HintColor = Color.FromArgb(GetInteger(currentElement, "HintColor"))
        End If
        If currentElement.Attributes("MaxMarksCount") IsNot Nothing Then
            UserPreference.MaxMarksCount = GetInteger(currentElement, "MaxMarksCount")
        End If

        'save result parameters
        If currentElement.Attributes("Result_SaveOnlyVisiblePoints") IsNot Nothing Then
            UserPreference.Result_SaveOnlyVisiblePoints = GetBoolean(currentElement, "Result_SaveOnlyVisiblePoints")
        End If
        If currentElement.Attributes("Report_SaveOnlyVisiblePoints") IsNot Nothing Then
            UserPreference.Report_SaveOnlyVisiblePoints = GetBoolean(currentElement, "Report_SaveOnlyVisiblePoints")
        End If
        If currentElement.Attributes("ResultImage_SaveColoured") IsNot Nothing Then
            UserPreference.ResultImage_SaveColoured = GetBoolean(currentElement, "ResultImage_SaveColoured")
        End If

        'load skin
        If currentElement.Attributes("SkinName") IsNot Nothing Then
            Dim SkinName As String
            SkinName = GetString(currentElement, "SkinName")
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(SkinName)
        End If

        'load panels
        Dim PanelsElement As System.Xml.XmlElement
        PanelsElement = currentElement.Item(PANELS_XML_ELEMENT)
        If PanelsElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In PanelsElement.ChildNodes
                Dim Panel As ClientPanel = GetPanelByName(childElement.Name)
                If Panel IsNot Nothing Then
                    Try
                        PanelFromXml(Panel, childElement)
                    Catch
                    End Try
                End If
            Next
        End If

        'load dock manager layout
        Dim DockManagerElement As System.Xml.XmlElement
        DockManagerElement = currentElement.Item(DOCKMANAGER_XML_ELEMENT)
        If DockManagerElement IsNot Nothing Then
            Dim DockManagerLayout As String
            DockManagerLayout = GetString(DockManagerElement, "Layout")
            Try
                SetDockManagerLayout(DockManagerLayout)
            Catch
                'do Nothing in this catch block - it's just in case of DevExpress failure
            End Try
        End If

        'load font
        Dim FontElement As System.Xml.XmlElement
        FontElement = currentElement.Item(FONT_XML_ELEMENT)
        If FontElement IsNot Nothing Then
            Try
                UserPreference.Font = FontFromXml(AppStaticClass.MainForm.Font, FontElement)
            Catch
            End Try
        End If

        'load buttons
        Dim ButtonsElement As System.Xml.XmlElement
        ButtonsElement = currentElement.Item(BUTTONS_XML_ELEMENT)
        If ButtonsElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In ButtonsElement.ChildNodes
                Dim Button As DevExpress.XtraBars.BarButtonItem = GetButtonByName(childElement.Name)
                If Button IsNot Nothing Then
                    Try
                        ButtonFromXml(Button, childElement)
                    Catch
                    End Try
                End If
            Next
        End If

        Return UserPreference
    End Function

#Region "Panel to-from xml"

    Private Function PanelToXml(ByVal panel As ClientPanel, _
                                ByVal parentElement As System.Xml.XmlElement, _
                                ByVal name As String) _
                                    As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        PanelToXml(panel, CurrentElement)
        Return CurrentElement
    End Function

    Private Sub PanelToXml(ByVal panel As ClientPanel, _
                           ByVal currentElement As System.Xml.XmlElement)

        If panel.Tag Is Nothing Then Return

        If TypeOf panel.Tag Is DevExpress.XtraBars.Docking.DockPanel Then
            PanelToXml(DirectCast(panel.Tag, DevExpress.XtraBars.Docking.DockPanel), currentElement)
        ElseIf TypeOf panel.Tag Is DevExpress.XtraTab.XtraTabPage Then
            PanelToXml(DirectCast(panel.Tag, DevExpress.XtraTab.XtraTabPage), currentElement)
        End If
    End Sub

    Private Sub PanelFromXml(ByVal panel As ClientPanel, _
                             ByVal currentElement As System.Xml.XmlElement)
        If panel.Tag Is Nothing Then Return

        If TypeOf panel.Tag Is DevExpress.XtraBars.Docking.DockPanel Then
            PanelFromXml(DirectCast(panel.Tag, DevExpress.XtraBars.Docking.DockPanel), currentElement)
        ElseIf TypeOf panel.Tag Is DevExpress.XtraTab.XtraTabPage Then
            PanelFromXml(DirectCast(panel.Tag, DevExpress.XtraTab.XtraTabPage), currentElement)
        End If

    End Sub

    Private Sub PanelToXml(ByVal panel As DevExpress.XtraTab.XtraTabPage, _
                           ByVal currentElement As System.Xml.XmlElement)
        'save properties
        SetAttribute(currentElement, "PageVisible", panel.PageVisible)

    End Sub

    Private Sub PanelFromXml(ByVal panel As DevExpress.XtraTab.XtraTabPage, _
                             ByVal currentElement As System.Xml.XmlElement)

        If currentElement.Attributes("PageVisible") IsNot Nothing Then
            panel.PageVisible = GetBoolean(currentElement, "PageVisible")
        End If

    End Sub

    Private Sub PanelToXml(ByVal panel As DevExpress.XtraBars.Docking.DockPanel, _
                          ByVal currentElement As System.Xml.XmlElement)

        'save properties
        SetAttribute(currentElement, "Dock", panel.Dock)
        SetAttribute(currentElement, "FloatVertical", panel.FloatVertical)
        SetAttribute(currentElement, "Index", panel.Index)
        SetAttribute(currentElement, "Tabbed", panel.Tabbed)
        'If panel.Parent IsNot Nothing Then
        '    SetAttribute(currentElement, "ParentName", panel.Parent.Name)
        'End If

        SetAttribute(currentElement, "ParentName", AppStaticClass.MainForm.GetParentName(panel))

        SetAttribute(currentElement, "SavedDock", panel.SavedDock)
        SetAttribute(currentElement, "SavedIndex", panel.SavedIndex)
        SetAttribute(currentElement, "SavedTabbed", panel.SavedTabbed)
        If panel.SavedParent IsNot Nothing Then
            SetAttribute(currentElement, "SavedParentName", panel.SavedParent.Name)
        End If

        If panel.TopLevelControl IsNot Nothing Then
            SetAttribute(currentElement, "TopLevelControlName", panel.TopLevelControl.Name)
        End If

        SetAttribute(currentElement, "Height", panel.Height)
        SetAttribute(currentElement, "Width", panel.Width)
        SetAttribute(currentElement, "Left", panel.Left)
        SetAttribute(currentElement, "Top", panel.Top)

    End Sub

    Private Sub PanelFromXml(ByVal panel As DevExpress.XtraBars.Docking.DockPanel, _
                             ByVal currentElement As System.Xml.XmlElement)

        If currentElement.Attributes("Dock") IsNot Nothing Then
            panel.Dock = GetInteger(currentElement, "Dock")
        End If
        If currentElement.Attributes("Index") IsNot Nothing Then
            Dim Index As Integer = GetInteger(currentElement, "Index")
            If Index < 0 Then
                panel.HideImmediately()
            Else
                panel.Index = Index
            End If
        End If
        If currentElement.Attributes("FloatVertical") IsNot Nothing Then
            panel.FloatVertical = GetBoolean(currentElement, "FloatVertical")
        End If
        If currentElement.Attributes("Tabbed") IsNot Nothing Then
            panel.Tabbed = GetBoolean(currentElement, "Tabbed")
        End If
        'If currentElement.Attributes("ParentName") IsNot Nothing Then
        '    Dim ParentName As String
        '    ParentName = GetString(currentElement, "ParentName")
        '    Dim ParentControl As Control
        '    ParentControl = My.Application.Client.GetControlByName(ParentName)
        '    If ParentControl IsNot Nothing Then
        '        'panel.Parent = ParentControl
        '        ParentControl.Controls.Add(panel)
        '    End If
        'End If

        'If currentElement.Attributes("TopLevelControlName") IsNot Nothing Then
        '    Dim TopLevelControlName As String
        '    TopLevelControlName = GetString(currentElement, "TopLevelControlName")
        '    Dim TopLevelControl As Control
        '    TopLevelControl = My.Application.Client.GetControlByName(TopLevelControlName)
        '    If TopLevelControl IsNot Nothing Then
        '        TopLevelControl.Controls.Add(panel)
        '    End If
        'End If

        'If currentElement.Attributes("SavedDock") IsNot Nothing Then
        '    panel.SavedDock = GetInteger(currentElement, "SavedDock")
        'End If
        'If currentElement.Attributes("SavedIndex") IsNot Nothing Then
        '    panel.SavedIndex = GetInteger(currentElement, "SavedIndex")
        'End If
        'If currentElement.Attributes("SavedTabbed") IsNot Nothing Then
        '    panel.SavedTabbed = GetBoolean(currentElement, "SavedTabbed")
        'End If
        'If currentElement.Attributes("SavedParentName") IsNot Nothing Then
        '    Dim SavedParentName As String
        '    SavedParentName = GetString(currentElement, "SavedParentName")
        '    Dim SavedPanelControl As Control
        '    SavedPanelControl = My.Application.Client.GetControlByName(SavedParentName)
        '    If SavedPanelControl IsNot Nothing AndAlso TypeOf SavedPanelControl Is DevExpress.XtraBars.Docking.DockPanel Then
        '        panel.SavedParent = CType(SavedPanelControl, DevExpress.XtraBars.Docking.DockPanel)
        '    End If
        'End If

        If currentElement.Attributes("Height") IsNot Nothing Then
            panel.Height = GetInteger(currentElement, "Height")
        End If
        If currentElement.Attributes("Width") IsNot Nothing Then
            panel.Width = GetInteger(currentElement, "Width")
        End If
        If currentElement.Attributes("Left") IsNot Nothing Then
            panel.Left = GetInteger(currentElement, "Left")
        End If
        If currentElement.Attributes("Top") IsNot Nothing Then
            panel.Top = GetInteger(currentElement, "Top")
        End If

    End Sub

#End Region

#Region "Font to-from xml"

    Private Function FontToXml(ByVal font As Font, _
                               ByVal parentElement As System.Xml.XmlElement, _
                               ByVal name As String) _
                                    As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        FontToXml(font, CurrentElement)
        Return CurrentElement
    End Function

    Private Sub FontToXml(ByVal font As Font, _
                          ByVal currentElement As System.Xml.XmlElement)

        'save properties
        SetAttribute(currentElement, "FamilyName", font.Name)
        SetAttribute(currentElement, "EmSize", font.SizeInPoints)

    End Sub

    Private Function FontFromXml(ByVal font As Font, _
                                 ByVal currentElement As System.Xml.XmlElement) _
                                    As Font

        Dim FamilyName As String = font.Name
        Dim EmSize As Single = font.SizeInPoints
        Dim Style As System.Drawing.FontStyle = font.Style

        If currentElement.Attributes("FamilyName") IsNot Nothing Then
            FamilyName = GetString(currentElement, "FamilyName")
        End If
        If currentElement.Attributes("EmSize") IsNot Nothing Then
            EmSize = GetSingle(currentElement, "EmSize")
        End If

        Return New Font(FamilyName, EmSize, Style)
    End Function

#End Region

#Region "Button to-from xml"

    Private Function ButtonToXml(ByVal button As DevExpress.XtraBars.BarButtonItem, _
                                 ByVal parentElement As System.Xml.XmlElement, _
                                 ByVal name As String) _
                                    As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        ButtonToXml(button, CurrentElement)
        Return CurrentElement
    End Function

    Private Sub ButtonToXml(ByVal button As DevExpress.XtraBars.BarButtonItem, _
                            ByVal currentElement As System.Xml.XmlElement)

        'save properties
        'SetAttribute(currentElement, "Enabled", button.Enabled)
        SetAttribute(currentElement, "Visibility", CInt(button.Visibility))

    End Sub

    Private Sub ButtonFromXml(ByVal button As DevExpress.XtraBars.BarButtonItem, _
                              ByVal currentElement As System.Xml.XmlElement)

        'If currentElement.Attributes("Enabled") IsNot Nothing Then
        '    button.Enabled = GetInteger(currentElement, "Enabled")
        'End If
        If currentElement.Attributes("Visibility") IsNot Nothing Then
            button.Visibility = GetInteger(currentElement, "Visibility")
        End If

    End Sub

#End Region

#Region "DockManager layout to-from xml"

    Private Function GetDockManagerLayout() As String
        Dim Result As String = String.Empty

        If AppStaticClass.MainForm IsNot Nothing AndAlso AppStaticClass.MainForm.DockManager1 IsNot Nothing Then
            Dim Stream As System.IO.MemoryStream
            Stream = New System.IO.MemoryStream()
            AppStaticClass.MainForm.DockManager1.SaveLayoutToStream(Stream)

            Stream.Position = 0

            Dim Bytes(Stream.Length) As Byte
            Bytes = Stream.ToArray()

            For i As Integer = 0 To Bytes.Length - 1
                If i <> Bytes.Length - 1 Then
                    Result &= Bytes(i).ToString() & ";"
                Else
                    Result &= Bytes(i).ToString()
                End If
            Next
        End If

        Return Result
    End Function

    Private Sub SetDockManagerLayout(ByVal dockManagerLayout As String)
        If String.IsNullOrEmpty(dockManagerLayout) Then Return
        Dim Lines As String() = dockManagerLayout.Split(";")
        If Lines Is Nothing OrElse Lines.Count = 0 Then Return

        Dim Bytes(Lines.Length) As Byte

        For i As Integer = 0 To Lines.Count - 1
            If Not Byte.TryParse(Lines(i), Bytes(i)) Then
                'data is corrupted - do not load !!!
                Exit Sub
            End If
        Next


        If AppStaticClass.MainForm IsNot Nothing AndAlso AppStaticClass.MainForm.DockManager1 IsNot Nothing Then
            Dim Stream As System.IO.MemoryStream
            Stream = New System.IO.MemoryStream()

            Stream.Position = 0

            For i As Integer = 0 To Bytes.Count - 1
                Stream.WriteByte(Bytes(i))
            Next

            ' Set the stream pointer to the beginning.
            Stream.Seek(0, System.IO.SeekOrigin.Begin)
            AppStaticClass.MainForm.DockManager1.RestoreLayoutFromStream(Stream)
        End If

    End Sub

#End Region

#End Region

End Class
