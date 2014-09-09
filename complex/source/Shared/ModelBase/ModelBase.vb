Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading
Imports ModelBase.Enums
Imports System.Windows.Forms

Public Class ModelBase
    Inherits ObjectBase
    Implements IModel

#Region "Const"

    Public Const VALUES_XML_ELEMENT As String = "Values"
    Public Const PARAMETERS_XML_ELEMENT As String = "Parameters"
    Public Const SWITCH_PARAMETERS_XML_ELEMENT As String = "SwitchParameters"

    Public Const SETUP_EXTENSION As String = ".setup"

#End Region

#Region "Declarations"

    Public Event CycleCalculated(ByVal model As IModel) Implements IModel.CycleCalculated
    Public Event CycleStarted(ByVal model As IModel) Implements IModel.CycleStarted
    Public Event ExceptionOccurred(ByVal model As IModel, ByVal ex As Exception) Implements IModel.ExceptionOccurred

#End Region

#Region "Properties"

    Private _Values As List(Of Value)
    Public Property Values() As List(Of Value)
        Get
            If _Values Is Nothing Then
                _Values = New List(Of Value)
            End If
            Return _Values
        End Get
        Set(ByVal value As List(Of Value))
            _Values = value
        End Set
    End Property

    Private _Parameters As List(Of Parameter)
    Public Property Parameters() As List(Of Parameter)
        Get
            If _Parameters Is Nothing Then
                _Parameters = New List(Of Parameter)
            End If
            Return _Parameters
        End Get
        Set(ByVal value As List(Of Parameter))
            _Parameters = value
        End Set
    End Property

    Private _SwitchParameters As List(Of SwitchParameter)
    Public Property SwitchParameters() As List(Of SwitchParameter)
        Get
            If _SwitchParameters Is Nothing Then
                _SwitchParameters = New List(Of SwitchParameter)
            End If
            Return _SwitchParameters
        End Get
        Set(ByVal value As List(Of SwitchParameter))
            _SwitchParameters = value
        End Set
    End Property

    Private _DisplayName As String = String.Empty
    Public Property DisplayName() As String Implements IModel.DisplayName
        Get
            Return _DisplayName
        End Get
        Set(ByVal value As String)
            _DisplayName = value
        End Set
    End Property

    Private _Name As String = String.Empty
    Public Property Name() As String
        Get
            If String.IsNullOrEmpty(_Name) Then
                _Name = Me.GetType.FullName
            End If
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Description As String = String.Empty
    Public Property Description() As String Implements IModel.Description
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _IsLoaded As Boolean = False
    Public Property IsLoaded() As Boolean
        Get
            Return _IsLoaded
        End Get
        Set(ByVal value As Boolean)
            _IsLoaded = value
        End Set
    End Property

    Private _Step As Decimal = 0.1
    Public Property [Step]() As Decimal Implements IModel.Step
        Get
            Return _Step
        End Get
        Set(ByVal value As Decimal)
            If value <= 0 Then
                'todo
                value = 0.1
            End If
            _Step = value
        End Set
    End Property

    Private _ShownStep As Decimal
    ''' <summary>
    ''' Step whith which model's result should be shown
    ''' </summary>
    Public Property ShownStep() As Decimal Implements IModel.ShownStep
        Get
            Return _ShownStep
        End Get
        Set(ByVal value As Decimal)
            _ShownStep = value
        End Set
    End Property

    Private _CurrentStep As Integer = 0
    Public Property CurrentStep() As Integer
        Get
            Return _CurrentStep
        End Get
        Set(ByVal value As Integer)
            _CurrentStep = value
        End Set
    End Property

    Private _CurrentTime As Decimal = 0
    Public Property CurrentTime() As Decimal
        Get
            Return _CurrentTime
        End Get
        Set(ByVal value As Decimal)
            _CurrentTime = value
        End Set
    End Property

    Private _ExperimentTime As Double = 0
    Public Overridable Property ExperimentTime() As Decimal Implements IModel.ExperimentTime
        Get
            Return _ExperimentTime
        End Get
        Set(ByVal value As Decimal)
            _ExperimentTime = value
        End Set
    End Property

    Private _Thread As Thread
    Public Property Thread() As Thread Implements IModel.Thread
        Get
            Return _Thread
        End Get
        Set(ByVal value As Thread)
            _Thread = value
        End Set
    End Property

    Private _CalculatingState As CalculatingStates = CalculatingStates.NotStarted
    Public Property CalculatingState() As CalculatingStates Implements IModel.CalculatingState
        Get
            Return _CalculatingState
        End Get
        Set(ByVal value As CalculatingStates)
            _CalculatingState = value
        End Set
    End Property

    Private _Configuration As Configuration
    ''' <summary>
    ''' Currently used configuration for which this model belongs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Is not saved to xml</remarks>
    Public Property Configuration() As Configuration Implements IModel.Configuration
        Get
            Return _Configuration
        End Get
        Set(ByVal value As Configuration)
            _Configuration = value
        End Set
    End Property

    Private _ShowControlAlways As Boolean = False
    Protected Property ShowControlAlways() As Boolean
        Get
            Return _ShowControlAlways
        End Get
        Set(ByVal value As Boolean)
            _ShowControlAlways = value
        End Set
    End Property

    ''' <summary>
    ''' Returns name of the xml root for the class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides ReadOnly Property XmlName() As String
        Get
            Return "Model"
        End Get
    End Property

    ''' <summary>
    ''' Returns True if model has at least 1 value which should be shown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property HasVisibleValues() As Boolean Implements IModel.HasVisibleValues
        Get
            Dim _HasVisibleValues As Boolean = False

            If Me.GetValues IsNot Nothing AndAlso Me.GetValues.Count > 0 Then
                For Each Value As Value In Me.GetValues
                    If Value.Visible Then
                        _HasVisibleValues = True
                        Exit For
                    End If
                Next
            End If

            Return _HasVisibleValues
        End Get
    End Property

    Private _IsOpenedFromResultFile As Boolean = False
    ''' <summary>
    ''' Use this property to lock extra actions for models which are created from .result file
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsOpenedFromResultFile() As Boolean Implements IModel.IsOpenedFromResultFile
        Get
            Return _IsOpenedFromResultFile
        End Get
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        'CollectLists()
    End Sub

#End Region

    Public Overridable Sub CollectLists()

        Dim ParameterType As System.Type = GetType(Parameter)
        Dim ValueType As System.Type = GetType(Value)
        Dim SwitchParameterType As System.Type = GetType(SwitchParameter)

        Dim Parameter As Parameter
        Dim Value As Value
        Dim SwitchParameter As SwitchParameter

        For Each ModelField As FieldInfo In Me.GetType.GetFields
            If ModelField.FieldType.Equals(ParameterType) OrElse _
               ModelField.FieldType.IsSubclassOf(ParameterType) _
               Then
                Parameter = GetParameter(ModelField.Name)
                If Parameter IsNot Nothing Then
                    Parameters.Add(Parameter)
                End If
            ElseIf ModelField.FieldType.Equals(ValueType) OrElse _
               ModelField.FieldType.IsSubclassOf(ValueType) _
               Then
                Value = GetValue(ModelField.Name)
                If Value IsNot Nothing Then
                    Values.Add(Value)
                End If
            ElseIf ModelField.FieldType.Equals(SwitchParameterType) OrElse _
                   ModelField.FieldType.IsSubclassOf(SwitchParameterType) _
            Then
                SwitchParameter = GetSwitchParameter(ModelField.Name)
                If SwitchParameter IsNot Nothing Then
                    SwitchParameters.Add(SwitchParameter)
                End If
            End If
        Next

        IsLoaded = True
    End Sub

    Private Function GetParameter(ByVal parameterName As String) As Parameter
        Dim Parameter As Parameter = Nothing

        Dim ParameterObject As Object = Nothing
        Try
            ParameterObject = Me.GetType.InvokeMember(parameterName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If ParameterObject IsNot Nothing AndAlso TypeOf ParameterObject Is Parameter Then
            Parameter = DirectCast(ParameterObject, Parameter)
        End If

        Return Parameter
    End Function

    Private Function GetSwitchParameter(ByVal switchParameterName As String) As SwitchParameter
        Dim SwitchParameter As SwitchParameter = Nothing

        Dim SwitchParameterObject As Object = Nothing
        Try
            SwitchParameterObject = Me.GetType.InvokeMember(switchParameterName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If SwitchParameterObject IsNot Nothing AndAlso TypeOf SwitchParameterObject Is SwitchParameter Then
            SwitchParameter = DirectCast(SwitchParameterObject, SwitchParameter)
        End If

        Return SwitchParameter
    End Function

    Public Overridable Function GetValue(ByVal valueName As String) As Value Implements IModel.GetValue
        Dim Value As Value = Nothing

        Dim ValueObject As Object = Nothing
        Try
            ValueObject = Me.GetType.InvokeMember(valueName, BindingFlags.GetField, Nothing, Me, Nothing)
        Catch ex As Exception
        End Try

        If ValueObject IsNot Nothing AndAlso TypeOf ValueObject Is Value Then
            Value = DirectCast(ValueObject, Value)
        End If

        Return Value
    End Function

#Region "Methods to work with collections"

    Public Function GetValueByName(ByVal valueName As String) As Value
        Dim Result As Value = Nothing

        If _Values Is Nothing Then CollectLists()

        For Each Value As Value In Me.Values
            If Value.Name = valueName Then
                Result = Value
                Exit For
            End If
        Next

        Return Result
    End Function

    Public Function GetParameterByName(ByVal parameterName As String) As Parameter
        Dim Result As Parameter = Nothing

        If _Parameters Is Nothing Then CollectLists()

        Dim Parameters As List(Of Parameter) = GetParameters()
        If Parameters IsNot Nothing AndAlso Parameters.Count > 0 Then
            For Each Parameter As Parameter In Parameters
                If Parameter.Name = parameterName Then
                    Result = Parameter
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

    Public Function GetSwitchParameterByName(ByVal switchParameterName As String) As SwitchParameter
        Dim Result As SwitchParameter = Nothing

        If _SwitchParameters Is Nothing Then CollectLists()

        Dim SwitchParameters As List(Of SwitchParameter) = GetSwitchParameters()
        If SwitchParameters IsNot Nothing AndAlso SwitchParameters.Count > 0 Then
            For Each SwitchParameter As SwitchParameter In SwitchParameters
                If SwitchParameter.Name = switchParameterName Then
                    Result = SwitchParameter
                    Exit For
                End If
            Next
        End If

        Return Result
    End Function

#End Region

#Region "Methods for raising events"

    Protected Sub RaiseEvent_CycleCalculated(ByVal model As IModel)
        RaiseEvent CycleCalculated(model)
    End Sub

    Protected Sub RaiseEvent_CycleStarted(ByVal model As IModel)
        RaiseEvent CycleStarted(model)
    End Sub

    Protected Sub RaiseEvent_ExceptionOccurred(ByVal model As IModel, ByVal ex As Exception)
        RaiseEvent ExceptionOccurred(model, ex)
    End Sub

#End Region

#Region "Xml methods"

    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)
        SetAttribute(CurrentElement, "Description", Description)
        SetAttribute(CurrentElement, "Step", [Step])
        SetAttribute(CurrentElement, "CurrentStep", CurrentStep)
        SetAttribute(CurrentElement, "CurrentTime", CurrentTime)
        SetAttribute(CurrentElement, "ExperimentTime", ExperimentTime)
        SetAttribute(CurrentElement, "ShowControlAlways", ShowControlAlways)

        'save values
        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = CurrentElement.OwnerDocument.CreateElement(VALUES_XML_ELEMENT)
        CurrentElement.AppendChild(ValuesElement)
        For Each Value As Value In Values
            Value.ToXml(ValuesElement, Value.Name)
        Next

        'save parameters
        SaveParametersToXml(CurrentElement)

        'save switch parameters
        SaveSwitchParametersToXml(CurrentElement)

        Return CurrentElement
    End Function

    Public Shared Function FromXmlElement(ByVal currentElement As System.Xml.XmlElement) As ModelBase
        Dim ModelBase As New ModelBase
        Return ModelBase.FromXml(currentElement)
    End Function

    Public Overrides Function FromXml(ByVal currentElement As System.Xml.XmlElement) As Object
        Dim ModelBase As ModelBase = Me

        If currentElement.Attributes("Name") IsNot Nothing Then
            ModelBase.Name = GetString(currentElement, "Name")
        End If
        If currentElement.Attributes("DisplayName") IsNot Nothing Then
            ModelBase.DisplayName = GetString(currentElement, "DisplayName")
        End If
        If currentElement.Attributes("Description") IsNot Nothing Then
            ModelBase.Description = GetString(currentElement, "Description")
        End If
        If currentElement.Attributes("Step") IsNot Nothing Then
            ModelBase.Step = GetDouble(currentElement, "Step")
        End If
        If currentElement.Attributes("CurrentStep") IsNot Nothing Then
            ModelBase.CurrentStep = GetInteger(currentElement, "CurrentStep")
        End If
        If currentElement.Attributes("CurrentTime") IsNot Nothing Then
            ModelBase.CurrentTime = GetDouble(currentElement, "CurrentTime")
        End If
        If currentElement.Attributes("ExperimentTime") IsNot Nothing Then
            ModelBase.ExperimentTime = GetDouble(currentElement, "ExperimentTime")
        End If
        If currentElement.Attributes("ShowControlAlways") IsNot Nothing Then
            ModelBase.ShowControlAlways = GetBoolean(currentElement, "ShowControlAlways")
        End If

        'load values
        If Me.GetValues IsNot Nothing Then
            Dim ValuesElement As System.Xml.XmlElement
            ValuesElement = currentElement.Item(VALUES_XML_ELEMENT)
            If ValuesElement IsNot Nothing Then
                For Each childElement As System.Xml.XmlElement In ValuesElement.ChildNodes
                    Dim Value As Value = GetValueByName(childElement.Name)
                    If Value IsNot Nothing Then
                        Value.FromXml(childElement, Value.Name)
                    End If
                Next
            End If
        End If

        'load parameters
        LoadParametersFromXml(currentElement)

        'load switch parameters
        LoadSwitchParametersFromXml(currentElement)


        Return ModelBase
    End Function

#Region "Result"

    Public Function ResultToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement Implements IModel.ResultToXml
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        Me.ResultToXml(CurrentElement)
        Return CurrentElement
    End Function

    Public Overridable Function ResultToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        SetAttribute(CurrentElement, "Name", Name)
        SetAttribute(CurrentElement, "DisplayName", DisplayName)
        SetAttribute(CurrentElement, "Description", Description)
        SetAttribute(CurrentElement, "Step", [Step])
        SetAttribute(CurrentElement, "CurrentStep", CurrentStep)
        SetAttribute(CurrentElement, "CurrentTime", CurrentTime)
        SetAttribute(CurrentElement, "ExperimentTime", ExperimentTime)
        SetAttribute(CurrentElement, "ShownStep", ShownStep)

        'save parameters
        SaveParametersToXml(CurrentElement)

        'save switch parameters
        SaveSwitchParametersToXml(CurrentElement)

        'save result
        Dim SaveStepNumber As Integer 'Each saveStepNumber value should be saved
        SaveStepNumber = Math.Floor(ShownStep / [Step])
        If SaveStepNumber < 1 Then SaveStepNumber = 1
        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = CurrentElement.OwnerDocument.CreateElement(VALUES_XML_ELEMENT)
        CurrentElement.AppendChild(ValuesElement)
        For Each Value As Value In Values
            If Value.Visible Then
                'fix not calculated points
                If Value.Value.Length - 1 > GetLastCalculatedStep() Then
                    Dim LastCalculatedValue = Value.Value(GetLastCalculatedStep)
                    For i As Integer = GetLastCalculatedStep() + 1 To Value.Value.Length - 1
                        Value.Value(i) = LastCalculatedValue
                    Next
                End If
                Value.ResultToXml(ValuesElement, Value.Name, SaveStepNumber)
            End If
        Next

        Return CurrentElement
    End Function

    Public Sub ResultFromXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) Implements IModel.ResultFromXml
        If parentElement Is Nothing OrElse parentElement.Name <> name Then Return

        Dim CurrentElement As System.Xml.XmlElement
        CurrentElement = parentElement.Item(XmlName)

        If CurrentElement.Attributes("Name") IsNot Nothing Then
            name = GetString(CurrentElement, "Name")
        End If
        If CurrentElement.Attributes("DisplayName") IsNot Nothing Then
            DisplayName = GetString(CurrentElement, "DisplayName")
        End If
        If CurrentElement.Attributes("Description") IsNot Nothing Then
            Description = GetString(CurrentElement, "Description")
        End If
        If CurrentElement.Attributes("Step") IsNot Nothing Then
            Me.Step = GetDouble(CurrentElement, "Step")
        End If
        If CurrentElement.Attributes("CurrentStep") IsNot Nothing Then
            CurrentStep = GetInteger(CurrentElement, "CurrentStep")
        End If
        If CurrentElement.Attributes("CurrentTime") IsNot Nothing Then
            CurrentTime = GetDouble(CurrentElement, "CurrentTime")
        End If
        If CurrentElement.Attributes("ExperimentTime") IsNot Nothing Then
            ExperimentTime = GetDouble(CurrentElement, "ExperimentTime")
        End If
        If CurrentElement.Attributes("ShowControlAlways") IsNot Nothing Then
            ShowControlAlways = GetBoolean(CurrentElement, "ShowControlAlways")
        End If
        If CurrentElement.Attributes("ShownStep") IsNot Nothing Then
            Me.Step = GetDouble(CurrentElement, "ShownStep")
        End If
        '! do not forget to mark this model as creted from .result file
        _IsOpenedFromResultFile = True

        'load parameters
        Dim ParametersElement As System.Xml.XmlElement
        ParametersElement = CurrentElement.Item(PARAMETERS_XML_ELEMENT)
        If ParametersElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In ParametersElement.ChildNodes
                Dim Parameter As New Parameter(childElement.Name, "")
                Parameter = Parameter.FromXml(childElement, Parameter.Name)
                Me.Parameters.Add(Parameter)
            Next
        End If

        'load switch parameters
        Dim SwitchParametersElement As System.Xml.XmlElement
        SwitchParametersElement = CurrentElement.Item(SWITCH_PARAMETERS_XML_ELEMENT)
        If ParametersElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In SwitchParametersElement.ChildNodes
                Dim SwitchParameter As New SwitchParameter(childElement.Name, "")
                SwitchParameter = SwitchParameter.FromXml(childElement, SwitchParameter.Name)
                Me.SwitchParameters.Add(SwitchParameter)
            Next
        End If

        'load values
        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = CurrentElement.Item(VALUES_XML_ELEMENT)
        If ValuesElement IsNot Nothing Then
            For Each childElement As System.Xml.XmlElement In ValuesElement.ChildNodes
                Dim Value As New Value(childElement.Name, "", Global.ModelBase.Value.ValueType.Output)
                Value.ResultFromXml(childElement, Value.Name)
                Me.Values.Add(Value)
            Next
        End If

    End Sub

#End Region

#Region "Selection"

    Public Function SelectionToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement Implements IModel.SelectionToXml
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        Me.SelectionToXml(CurrentElement)
        Return CurrentElement
    End Function

    Public Function SelectionToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save values
        Dim ValuesElement As System.Xml.XmlElement
        ValuesElement = CurrentElement.OwnerDocument.CreateElement(VALUES_XML_ELEMENT)
        CurrentElement.AppendChild(ValuesElement)
        For Each Value As Value In Values
            Value.SelectionToXml(ValuesElement, Value.Name)
        Next

        Return CurrentElement
    End Function


    Public Sub SelectionFromXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) Implements IModel.SelectionFromXml
        If parentElement Is Nothing OrElse parentElement.Name <> name Then Return

        Dim CurrentElement As System.Xml.XmlElement
        CurrentElement = parentElement.Item(XmlName)

        'load values
        If Me.GetValues IsNot Nothing Then
            Dim ValuesElement As System.Xml.XmlElement
            ValuesElement = CurrentElement.Item(VALUES_XML_ELEMENT)
            If ValuesElement IsNot Nothing Then
                For Each childElement As System.Xml.XmlElement In ValuesElement.ChildNodes
                    Dim Value As Value = GetValueByName(childElement.Name)
                    If Value IsNot Nothing Then
                        Value.SelectionFromXml(childElement, Value.Name)
                    End If
                Next
            End If
        End If

    End Sub

#End Region

#Region "Get Name"

    ''' <summary>
    ''' Extracts model's name from given xml
    ''' </summary>
    ''' <param name="xmlDocument"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function GetNameFromXml(ByVal xmlDocument As System.Xml.XmlDocument) As String
        If xmlDocument Is Nothing Then Return String.Empty

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return String.Empty

        Return GetNameFromXml(RootElement.Item(XmlName))
    End Function

    ''' <summary>
    ''' Extracts model's name from given xml
    ''' </summary>
    ''' <param name="currentElement"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function GetNameFromXml(ByVal currentElement As System.Xml.XmlElement) As String
        Dim ModelName As String = String.Empty

        If currentElement.Attributes("Name") IsNot Nothing Then
            ModelName = GetString(currentElement, "Name")
        End If

        Return ModelName
    End Function

#End Region

#Region "Parameters"

    Private Sub SaveParametersToXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        'save parameters
        Dim ParametersToSave As List(Of Parameter) = GetParameters()
        If ParametersToSave IsNot Nothing AndAlso ParametersToSave.Count > 0 Then
            Dim ParametersElement As System.Xml.XmlElement
            ParametersElement = currentElement.OwnerDocument.CreateElement(PARAMETERS_XML_ELEMENT)
            currentElement.AppendChild(ParametersElement)
            For Each Parameter As Parameter In ParametersToSave
                Parameter.ToXml(ParametersElement, Parameter.Name)
            Next
        End If
    End Sub

    Private Sub LoadParametersFromXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        If Me.GetParameters IsNot Nothing Then
            Dim ParametersElement As System.Xml.XmlElement
            ParametersElement = currentElement.Item(PARAMETERS_XML_ELEMENT)
            If ParametersElement IsNot Nothing Then
                For Each childElement As System.Xml.XmlElement In ParametersElement.ChildNodes
                    Dim Parameter As Parameter = GetParameterByName(childElement.Name)
                    If Parameter IsNot Nothing Then
                        Parameter = Parameter.FromXml(childElement, Parameter.Name)
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region "Switch Parameters"

    Private Sub SaveSwitchParametersToXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        'save switch parameters
        Dim SwitchParametersToSave As List(Of SwitchParameter) = GetSwitchParameters()
        If SwitchParametersToSave IsNot Nothing AndAlso SwitchParametersToSave.Count > 0 Then
            Dim SwitchParametersElement As System.Xml.XmlElement
            SwitchParametersElement = currentElement.OwnerDocument.CreateElement(SWITCH_PARAMETERS_XML_ELEMENT)
            currentElement.AppendChild(SwitchParametersElement)
            For Each SwitchParameter As SwitchParameter In SwitchParametersToSave
                SwitchParameter.ToXml(SwitchParametersElement, SwitchParameter.Name)
            Next
        End If
    End Sub

    Private Sub LoadSwitchParametersFromXml(ByRef currentElement As System.Xml.XmlElement)
        If currentElement Is Nothing Then Return

        If Me.GetSwitchParameters IsNot Nothing Then
            Dim SwitchParametersElement As System.Xml.XmlElement
            SwitchParametersElement = currentElement.Item(SWITCH_PARAMETERS_XML_ELEMENT)
            If SwitchParametersElement IsNot Nothing Then
                For Each childElement As System.Xml.XmlElement In SwitchParametersElement.ChildNodes
                    Dim SwitchParameter As SwitchParameter = GetSwitchParameterByName(childElement.Name)
                    If SwitchParameter IsNot Nothing Then
                        SwitchParameter = SwitchParameter.FromXml(childElement, SwitchParameter.Name)
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Calculate"

    ''' <summary>
    ''' Sets all parameters and resizes arrays based on ExperimentTime which must be already set
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub BeforeCalculate() Implements IModel.BeforeCalculate

        Dim MaxStep As Integer
        MaxStep = Math.Ceiling(ExperimentTime / [Step])
        'resize arrays
        For Each Value As Value In Values
            ReDim Value.Value(MaxStep)
            Value.Value(0) = Value.InitValue
        Next

        'clear values
        CurrentTime = [Step]
        CurrentStep = 1
        CalculatingState = CalculatingStates.InProcess
    End Sub

    Public Overridable Sub Calculate() Implements IModel.Calculate
        CalculatingState = CalculatingStates.InProcess

        While CurrentTime < ExperimentTime
            If CalculatingState <> CalculatingStates.InProcess Then Continue While

            'ask for input values
            RaiseEvent_CycleStarted(Me)

            'calculate current step
            Try
                Cycle()
            Catch ex As Exception
                RaiseEvent_ExceptionOccurred(Me, ex)
                If Me.Thread IsNot Nothing Then
                    Me.Thread.Abort()
                End If
            End Try

            'inform others that step is calculated
            RaiseEvent_CycleCalculated(Me)

            'update values
            CurrentTime += [Step]
            CurrentStep += 1

        End While

        CurrentStep -= 1

        CalculatingState = CalculatingStates.Finished
    End Sub

    ''' <summary>
    ''' Calculates current step
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Cycle()
        'must be override in child classes
    End Sub

    ''' <summary>
    ''' Used to ensure that all claculating will be correct after pause/restart
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UndoLastCalculatedStep() Implements IModel.UndoLastCalculatedStep
        CurrentTime -= [Step]
        CurrentStep -= 1
    End Sub

#End Region

#Region "Public methods"

    ''' <summary>
    ''' Gets calculated value by its name and time; used in calculating for linking models
    ''' </summary>
    ''' <param name="valueName"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function GetValueByNameAndTime(ByVal valueName As String, ByVal time As Decimal) As Double Implements IModel.GetValueByNameAndTime

        If time > CurrentTime Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} can not be get as current time is {3}", _
                                    New String() {Me.Name, valueName, time.ToString, CurrentTime.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        Dim Value As Value = GetValueByName(valueName)
        If Value Is Nothing Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} is nothing. Current time is {3}", _
                                    New String() {Me.Name, valueName, time.ToString, CurrentTime.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        Dim TimeStep As Integer
        TimeStep = Math.Floor(time / [Step])
        If TimeStep > CurrentStep Then
            TimeStep = CurrentStep
        End If

        If Value.Value Is Nothing Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} can not be get as current value array has not been initialized yet", _
                                    New String() {Me.Name, valueName, time.ToString, TimeStep.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        If Value.Value.Length <= TimeStep Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} can not be get as length of current value array is {3} whereas needed step is {4}", _
                                    New String() {Me.Name, valueName, time.ToString, Value.Value.Length.ToString, TimeStep.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        Return Value.Value(TimeStep)
    End Function

    ''' <summary>
    ''' Gets calculated value at given time; used while refreshing chart
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function GetValueByTime(ByVal value As Value, ByVal time As Decimal) As Double Implements IModel.GetValueByTime
        If time > CurrentTime Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} can not be get as current time is {3}", _
                                    New String() {Me.Name, value.Name, time.ToString, CurrentTime.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        Dim TimeStep As Integer
        TimeStep = Math.Floor(time / [Step])
        If TimeStep > CurrentStep Then
            TimeStep = CurrentStep
        End If

        If value.Value Is Nothing Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} can not be get as current value array has not been initialized yet", _
                                    New String() {Me.Name, value.Name, time.ToString, TimeStep.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        If value.Value.Length <= TimeStep Then
            Dim Message As String
            Message = String.Format("Model {0}: value {1} at time {2} can not be get as length of current value array is {3} whereas needed step is {4}", _
                                    New String() {Me.Name, value.Name, time.ToString, value.Value.Length.ToString, TimeStep.ToString})
            Message &= vbCrLf
            Throw New Exception(Message)
        End If

        Return value.Value(TimeStep)
    End Function

    Public Overridable Function GetCurrentTime() As Decimal Implements IModel.GetCurrentTime
        Return CurrentTime
    End Function

    ''' <summary>
    ''' Gets max time which is surely calculated
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function GetLastCalculatedTime() As Decimal Implements IModel.GetLastCalculatedTime
        If CalculatingState = CalculatingStates.Finished Then
            Return CurrentTime
        ElseIf CurrentTime > [Step] Then
            Return CurrentTime - [Step]
        End If

        'time 0 has init value so it is always calculated
        Return 0
    End Function

    ''' <summary>
    ''' Gets max step which is surely calculated
    ''' </summary>
    Public Overridable Function GetLastCalculatedStep() As Integer Implements IModel.GetLastCalculatedStep
        If CalculatingState = CalculatingStates.Finished Then
            Return CurrentStep
        ElseIf CurrentStep > [Step] Then
            Return CurrentStep - 1
        End If

        'time 0 has init value so it is always calculated
        Return 0
    End Function

    Public Overridable Function GetInputValues() As List(Of Value)
        If Not IsLoaded Then CollectLists()

        Dim InputValues As New List(Of Value)

        If Values IsNot Nothing AndAlso Values.Count > 0 Then
            For Each Value As Value In Values
                If Value.Type = Global.ModelBase.Value.ValueType.Input Then
                    InputValues.Add(Value)
                End If
            Next
        End If

        Return InputValues
    End Function

#End Region

#Region "UI Methods"

    Public Overridable Function IsControlAlwaysShown() As Boolean Implements IModel.IsControlAlwaysShown
        Return ShowControlAlways
    End Function

    Public Overridable Function GetControl() As System.Windows.Forms.UserControl Implements IModel.GetControl
        Return Nothing
    End Function

    Public Overridable Function GetMenuItems() As List(Of MenuItem) Implements IModel.GetMenuItems
        'add File panel by default
        Dim MenuItems As New List(Of MenuItem)

        Dim Open As New MenuItem("File", "Open", My.Resources.open_32x32)
        Open.Enabled = Not IsOpenedFromResultFile
        AddHandler Open.ItemClicked, AddressOf OpenItemClicked
        MenuItems.Add(Open)

        Dim Save As New MenuItem("File", "Save", My.Resources.save_as2_blue_32x32)
        Save.Enabled = Not IsOpenedFromResultFile
        AddHandler Save.ItemClicked, AddressOf SaveItemClicked
        MenuItems.Add(Save)

        Return MenuItems
    End Function

    Private Sub OpenItemClicked(ByVal sender As MenuItem)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.DefaultExt = SETUP_EXTENSION
        OpenFileDialog.Filter = String.Format("setup (*{0})|*{0}", SETUP_EXTENSION)
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim XmlDocument As New System.Xml.XmlDocument
                XmlDocument.Load(OpenFileDialog.FileName)

                'check names
                Dim SavedModelName As String = GetNameFromXml(XmlDocument)
                If SavedModelName = Me.GetName Then
                    'this is data saved from this model
                    FromXmlDocument(XmlDocument)
                    'show info message
                    Dim Prompt As String
                    Prompt = String.Format("Setup file {0} was successfully opened.", New Object() {OpenFileDialog.FileName})
                    Dim Title As String = "Modelling tool"
                    MsgBox(Prompt, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, Title)
                Else
                    'this is data from some other model
                    Dim Prompt As String
                    Prompt = String.Format("You are not allowed to open setup file of model '{0}' while editing model '{1}'!", New Object() {SavedModelName, Me.GetName})
                    Dim Title As String = "Modelling tool"
                    MsgBox(Prompt, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, Title)
                End If
            Catch ex As Exception
                Dim ExceptionMessage As New SharedControls.ExceptionMessage(String.Format("Unable to load setup from file '{0}'", OpenFileDialog.FileName), ex)
                ExceptionMessage.ShowDialog()
                Return
            End Try
        End If
    End Sub

    Private Sub SaveItemClicked(ByVal sender As MenuItem)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.DefaultExt = SETUP_EXTENSION
        SaveFileDialog.Filter = String.Format("setup (*{0})|*{0}", SETUP_EXTENSION)
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim XmlDocument As System.Xml.XmlDocument
                XmlDocument = Me.ToXmlDocument
                XmlDocument.Save(SaveFileDialog.FileName)
                MsgBox("Setup was successfully saved", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Catch ex As Exception
                Dim ExceptionMessage As New SharedControls.ExceptionMessage(String.Format("Unable to save setup to the file '{0}'", SaveFileDialog.FileName), ex)
                ExceptionMessage.ShowDialog()
            End Try
        End If

    End Sub

#End Region

#Region "IModel"

    Public Overridable Function GetValues() As List(Of Value) Implements IModel.GetValues
        If Not IsLoaded Then CollectLists()

        Return Values
    End Function

    Public Overridable Function GetParameters() As List(Of Parameter) Implements IModel.GetParameters
        If Not IsLoaded Then CollectLists()

        Return Parameters
    End Function

    Public Overridable Function GetSwitchParameters() As List(Of SwitchParameter) Implements IModel.GetSwitchParameters
        If Not IsLoaded Then CollectLists()

        Return SwitchParameters
    End Function

    Function GetName() As String Implements IModel.GetName
        Return Name
    End Function

    Function GetCurrentStep() As Integer Implements IModel.GetCurrentStep
        Return Me.CurrentStep
    End Function

#End Region

End Class
