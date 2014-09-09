Imports System.Threading
Imports ModelBase.Enums

Public Class Configuration
    Inherits ObjectBase

#Region "Const"

    Public Const CONFIG_EXTENSION As String = ".config"
    Public Const SELECTION_EXTENSION As String = ".sel"
    Public Const RESULT_EXTENSION As String = ".result"
    Public Const REPORT_EXTENSION As String = ".xlsx"

    Public Const MODELS_XML_ELEMENT As String = "Models"

#End Region

#Region "Events"

    Public Event CalculationStopped()

#End Region

#Region "Properties"

    Private _Name As String = String.Empty
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Description As String = String.Empty
    ''' <summary>
    ''' Configuration's description in RTF format
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _FileName As String = String.Empty
    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Private _Models As List(Of IModel)
    Public Property Models() As List(Of IModel)
        Get
            If _Models Is Nothing Then
                _Models = New List(Of IModel)
            End If
            Return _Models
        End Get
        Set(ByVal value As List(Of IModel))
            _Models = value
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

    Private _ExperimentTime As Decimal = -1
    Public Property ExperimentTime() As Decimal
        Get
            Return _ExperimentTime
        End Get
        Set(ByVal value As Decimal)
            _ExperimentTime = value
        End Set
    End Property

    Private _ResultGridViewData As String = String.Empty
    ''' <summary>
    ''' Values/times to be shown in grid result table
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResultGridViewData() As String
        Get
            Return _ResultGridViewData
        End Get
        Set(ByVal value As String)
            _ResultGridViewData = value
        End Set
    End Property

#End Region


#Region "Constructors"

    Public Sub New(ByVal name As String, ByVal models As List(Of IModel))
        MyBase.New()

        Me.Name = name
        Me.Models = models
    End Sub

#End Region

#Region "Public methods"

#Region "Save"

    ''' <summary>
    ''' Saves configuration to its file name
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Exeption-safe</remarks>
    Public Function Save() As Boolean
        Dim Result As Boolean = True

        Try
            Dim XmlDocument As System.Xml.XmlDocument
            XmlDocument = Me.ToXmlDocument
            XmlDocument.Save(Me.FileName)
        Catch ex As Exception
            Result = False
        End Try

        Return Result
    End Function

    ''' <summary>
    ''' Saves result to file with given name
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveResult(ByVal fileName As String) As Boolean
        Dim Result As Boolean = True

        Dim XmlDocument As System.Xml.XmlDocument
        XmlDocument = Me.ResultToXmlDocument
        XmlDocument.Save(fileName)

        Return Result
    End Function

    ''' <summary>
    ''' Saves selection to file with given name
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSelection(ByVal fileName As String) As Boolean
        Dim Result As Boolean = True

        Dim XmlDocument As System.Xml.XmlDocument
        XmlDocument = Me.SelectionToXmlDocument
        XmlDocument.Save(fileName)

        Return Result
    End Function

#End Region

    Public Sub AddModel(ByVal model As IModel)
        Models.Add(model)
    End Sub

    Public Sub RemoveModel(ByVal model As IModel)
        If Models.Contains(model) Then
            Models.Remove(model)
        End If
    End Sub

    Public Function GetModelByName(ByVal modelName As String) As IModel
        Return GetModelByName(Models, modelName)
    End Function

    Public Function GetModelByValue(ByVal value As Value) As IModel
        Dim Result As IModel = Nothing

        For Each Model As IModel In Models
            If Model.GetValues.Contains(value) Then
                Result = Model
                Exit For
            End If
        Next

        Return Result
    End Function

    Public Overrides Function Clone() As Object
        Dim CloneModels As New List(Of IModel)
        If Models IsNot Nothing AndAlso Models.Count > 0 Then
            For Each Model As Object In Models
                CloneModels.Add(Model)
            Next
        End If

        Dim CloneConfiguration As New Configuration(Name, CloneModels)

        CloneConfiguration.Description = Description
        CloneConfiguration.FileName = FileName
        CloneConfiguration.ExperimentTime = ExperimentTime
        CloneConfiguration.ResultGridViewData = ResultGridViewData

        Return CloneConfiguration
    End Function

#End Region

#Region "Calculate"

    Public Sub StartCalculating(ByVal experimentTime As Double)
        Me.ExperimentTime = experimentTime

        '1 - prepare calculating
        For Each Model As IModel In Models
            If Model.CalculatingState <> CalculatingStates.Paused Then
                Model.ExperimentTime = experimentTime
                Try
                    Model.BeforeCalculate()
                Catch ex1 As System.OutOfMemoryException
                    StopCalculating()
                    RaiseEvent CalculationStopped()

                    Dim Message As String
                    Message = "There is not enough memory to perform such experiment. Please, reduce experiment time and try again."
                    MsgBox(Message, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Modelling Tool")
                    Return
                Catch ex2 As System.OverflowException
                    StopCalculating()
                    RaiseEvent CalculationStopped()

                    Dim Message As String
                    Message = "There is not enough memory to perform such experiment. Please, reduce experiment time and try again."
                    MsgBox(Message, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Modelling Tool")
                    Return
                Catch ex As Exception
                    StopCalculating()
                    RaiseEvent CalculationStopped()

                    Dim Description As String
                    Description = "Configuration: error while starting calculating"
                    Dim ExceptionMessage As New SharedControls.ExceptionMessage(Description, ex)
                    ExceptionMessage.ShowDialog()
                    Return
                End Try
            End If
        Next

        '2 - start calculating
        For Each Model As IModel In Models
            AddHandler Model.CycleStarted, AddressOf Model_CycleStarted
            AddHandler Model.ExceptionOccurred, AddressOf Model_ExceptionOccurred
            Dim Thread As New Thread(AddressOf Model.Calculate)
            Thread.IsBackground = True
            Model.Thread = Thread
            Thread.Start()
        Next
    End Sub

    Public Sub StopCalculating()
        'disable all events while calculation is being stoppped
        EnableEvents = False

        'stop calculatuing
        For Each Model As IModel In Models
            Try
                Model.CalculatingState = CalculatingStates.Finished
                Model.Thread.Abort()
            Catch
            End Try
        Next

        'allow events
        EnableEvents = True
    End Sub

    Public Sub PauseCalculating()

        'pause calculatuing
        For Each Model As IModel In Models
            Try
                Model.CalculatingState = CalculatingStates.Paused
                Model.UndoLastCalculatedStep()
                Model.Thread.Abort()
            Catch
            End Try
        Next

    End Sub

    Public Function GetCurrentTime() As Decimal
        Dim CurrentTime As Decimal = Decimal.MaxValue

        For Each Model As IModel In Models
            If Model.GetCurrentTime < CurrentTime Then
                CurrentTime = Model.GetCurrentTime
            End If
        Next

        Return CurrentTime
    End Function

    Public Function GetCalculatingState() As CalculatingStates
        Dim CalculatingState As CalculatingStates = -1

        For Each Model As IModel In Models
            Select Case Model.CalculatingState
                Case CalculatingStates.NotStarted
                    CalculatingState = CalculatingStates.NotStarted
                Case CalculatingStates.InProcess
                    CalculatingState = CalculatingStates.InProcess
                Case CalculatingStates.Finished
                    If CalculatingState = -1 OrElse CalculatingState = CalculatingStates.Finished Then
                        CalculatingState = CalculatingStates.Finished
                    Else
                        CalculatingState = CalculatingStates.InProcess
                    End If
                Case CalculatingStates.Paused
                    If CalculatingState = -1 OrElse CalculatingState = CalculatingStates.Paused Then
                        CalculatingState = CalculatingStates.Paused
                    Else
                        CalculatingState = CalculatingStates.InProcess
                    End If
            End Select
        Next

        Return CalculatingState
    End Function

    Private Sub Model_CycleStarted(ByVal model As IModel)
        If Not EnableEvents Then Return

        If model.CalculatingState = CalculatingStates.Finished OrElse model.GetCurrentStep = 0 Then
            'model was already stopped
            Return
        End If

        For Each Value As Value In model.GetValues
            If Value.Type = Value.ValueType.Input Then
                If Value.LinkConst IsNot Nothing Then
                    'link value with const
                    Try
                        Value.Value(model.GetCurrentStep - 1) = Value.LinkConst.Value
                    Catch ex As Exception
                        If Not EnableEvents Then Return

                        StopCalculating()
                        RaiseEvent CalculationStopped()

                        Dim Description As String
                        Description = "Configuration: error while setting model links"
                        Dim ExceptionMessage As New SharedControls.ExceptionMessage(Description, ex)
                        ExceptionMessage.ShowDialog()
                    End Try
                Else
                    'link value with const with value from other model
                    Dim LinkModel As IModel = GetModelByName(Value.LinkModelName)
                    If LinkModel Is Nothing Then
                        StopCalculating()
                        RaiseEvent CalculationStopped()

                        Dim Description As String
                        Description = "Configuration: linked model " & Value.LinkModelName & " does not exist"
                        Dim ExceptionMessage As New SharedControls.ExceptionMessage(Description, "")
                        ExceptionMessage.ShowDialog()
                    End If

                    Dim LinkTime As Double = model.GetLastCalculatedTime
                    While LinkModel.GetLastCalculatedTime < LinkTime
                        model.Thread.Sleep(0)
                    End While

                    Try
                        Value.Value(model.GetCurrentStep - 1) = LinkModel.GetValueByNameAndTime(Value.LinkValueName, LinkTime)
                    Catch ex As Exception
                        If Not EnableEvents Then Return

                        StopCalculating()
                        RaiseEvent CalculationStopped()

                        Dim Description As String
                        Description = "Configuration: error while setting model links"
                        Dim ExceptionMessage As New SharedControls.ExceptionMessage(Description, ex)
                        ExceptionMessage.ShowDialog()
                    End Try
                End If
            End If
        Next
    End Sub

    Private Sub Model_ExceptionOccurred(ByVal model As IModel, ByVal ex As Exception)
        If Not EnableEvents Then Return

        If model.CalculatingState = CalculatingStates.Finished Then
            'model was already stopped
            Return
        End If

        StopCalculating()
        RaiseEvent CalculationStopped()

        Dim Description As String
        Description = String.Format("Configuration: error while calculating model {0} at time {1}", New String() {model.DisplayName, model.GetCurrentTime})
        Description &= vbCrLf
        Dim ExceptionMessage As New SharedControls.ExceptionMessage(Description, ex)
        ExceptionMessage.ShowDialog()

    End Sub

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save properties
        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "Description", Description)
        SetAttribute(CurrentElement, "ExperimentTime", ExperimentTime)
        SetAttribute(CurrentElement, "ResultGridViewData", ResultGridViewData)

        'save models
        Dim ModelsElement As System.Xml.XmlElement
        ModelsElement = CurrentElement.OwnerDocument.CreateElement(MODELS_XML_ELEMENT)
        CurrentElement.AppendChild(ModelsElement)
        For Each Model As IModel In Models
            Model.ToXml(ModelsElement, Model.GetName)
        Next

        Return CurrentElement
    End Function

    Public Shared Function FromXml(ByVal xmlDocument As System.Xml.XmlDocument, ByVal allModels As List(Of IModel), ByVal fileName As String) As Configuration
        If xmlDocument Is Nothing Then Return Nothing

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return Nothing

        Dim ElementName As String
        ElementName = GetType(Configuration).Name().Replace("`", "")

        Return FromXml(RootElement.Item(ElementName), allModels, fileName)
    End Function

    Private Shared Function FromXml(ByVal currentElement As System.Xml.XmlElement, ByVal allModels As List(Of IModel), ByVal fileName As String) As Configuration
        If currentElement Is Nothing OrElse _
           currentElement.Name <> GetType(Configuration).Name().Replace("`", "") _
           Then
            Return Nothing
        End If

        Dim Configuration As New Configuration("", Nothing)
        Configuration.FileName = fileName

        If currentElement.Attributes("Name") IsNot Nothing Then
            Configuration.Name = GetString(currentElement, "Name")
        End If
        If currentElement.Attributes("Description") IsNot Nothing Then
            Configuration.Description = GetString(currentElement, "Description")
        End If
        If currentElement.Attributes("ExperimentTime") IsNot Nothing Then
            Configuration.ExperimentTime = GetDecimal(currentElement, "ExperimentTime")
        End If
        If currentElement.Attributes("ResultGridViewData") IsNot Nothing Then
            Configuration.ResultGridViewData = GetString(currentElement, "ResultGridViewData")
        End If

        'load models
        Dim ModelsElement As System.Xml.XmlElement
        ModelsElement = currentElement.Item(MODELS_XML_ELEMENT)
        If ModelsElement Is Nothing Then
            Throw New Exception("No models found")
        End If
        For Each childElement As System.Xml.XmlElement In ModelsElement.ChildNodes
            Dim Model As IModel = GetModelByName(allModels, childElement.Name)
            If Model Is Nothing Then
                Throw New Exception(String.Format("Model '{0}' no longer exists", childElement.Name))
            End If
            If Configuration.Models.Contains(Model) Then
                Throw New Exception(String.Format("There is more then one instance of model '{0}'", childElement.Name))
            End If
            Model.Configuration = Configuration
            Model = CType(Model.FromXml(childElement, Model.GetName), IModel)
            Configuration.Models.Add(Model)
        Next

        Return Configuration
    End Function

#Region "Result"

    Private Function ResultToXmlDocument() As System.Xml.XmlDocument
        Dim XmlDocument As New System.Xml.XmlDocument
        Dim XmlElement As System.Xml.XmlElement = XmlDocument.CreateElement(ROOT_NAME)

        XmlDocument.AppendChild(XmlElement)

        Me.ResultToXml(XmlElement)

        Return XmlDocument
    End Function

    Private Function ResultToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save properties
        SetAttribute(CurrentElement, "Name", Name)

        'save results in models 
        Dim ModelsElement As System.Xml.XmlElement
        ModelsElement = CurrentElement.OwnerDocument.CreateElement(MODELS_XML_ELEMENT)
        CurrentElement.AppendChild(ModelsElement)
        For Each Model As IModel In Models
            Model.ResultToXml(ModelsElement, Model.GetName)
        Next

        Return CurrentElement
    End Function

#End Region

#Region "Selection"

    Public Function SelectionToXmlDocument() As System.Xml.XmlDocument
        Dim XmlDocument As New System.Xml.XmlDocument
        Dim XmlElement As System.Xml.XmlElement = XmlDocument.CreateElement(ROOT_NAME)

        XmlDocument.AppendChild(XmlElement)

        Me.SelectionToXml(XmlElement)

        Return XmlDocument
    End Function

    Private Function SelectionToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save selection in models 
        Dim ModelsElement As System.Xml.XmlElement
        ModelsElement = CurrentElement.OwnerDocument.CreateElement(MODELS_XML_ELEMENT)
        CurrentElement.AppendChild(ModelsElement)
        For Each Model As IModel In Models
            Model.SelectionToXml(ModelsElement, Model.GetName)
        Next

        Return CurrentElement
    End Function

    Public Sub SelectionFromXml(ByVal xmlDocument As System.Xml.XmlDocument)
        If xmlDocument Is Nothing Then Return

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return

        Dim ElementName As String
        ElementName = GetType(Configuration).Name().Replace("`", "")

        SelectionFromXml(RootElement.Item(ElementName))
    End Sub

    Public Sub SelectionFromXml(ByVal currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing OrElse _
           currentElement.Name <> GetType(Configuration).Name().Replace("`", "") _
           Then
            Return
        End If

        'load selection from models
        Dim ModelsElement As System.Xml.XmlElement
        ModelsElement = currentElement.Item(MODELS_XML_ELEMENT)
        If ModelsElement Is Nothing Then
            Throw New Exception("No models found")
        End If
        For Each childElement As System.Xml.XmlElement In ModelsElement.ChildNodes
            Dim Model As IModel = GetModelByName(Models, childElement.Name)
            If Model IsNot Nothing Then
                Model.SelectionFromXml(childElement, Model.GetName)
            End If
        Next

    End Sub

#End Region

#End Region

#Region "Private methods"

    Private Shared Function GetModelByName(ByVal models As List(Of IModel), ByVal modelName As String) As IModel
        Dim Result As IModel = Nothing

        For Each Model As IModel In models
            If Model.GetName = modelName Then
                Result = Model
                Exit For
            End If
        Next

        Return Result
    End Function

#End Region

End Class
