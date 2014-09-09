Imports System.Collections.Generic
Imports System.Threading
Imports ModelBase.Enums

Public Interface IModel
    Inherits IXmlObject

    Function GetValues() As List(Of Value)
    Function GetParameters() As List(Of Parameter)
    Function GetSwitchParameters() As List(Of SwitchParameter)

    ''' <summary>
    ''' Returns value by its name
    ''' </summary>
    Function GetValue(ByVal valueName As String) As Value

    Function GetName() As String
    Property DisplayName() As String
    Property Description() As String

    Property [Step]() As Decimal

    ''' <summary>
    ''' Step whith which model's result should be shown
    ''' </summary>
    Property ShownStep() As Decimal


    ''' <summary>
    ''' Gets calculated value by its name and time; used in calculating for linking models
    ''' </summary>
    ''' <param name="valueName"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetValueByNameAndTime(ByVal valueName As String, ByVal time As Decimal) As Double
    ''' <summary>
    ''' Gets calculated value at given time; used while refreshing chart
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="time"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetValueByTime(ByVal value As Value, ByVal time As Decimal) As Double
    Function GetCurrentTime() As Decimal
    ''' <summary>
    ''' Gets max time which is surely calculated
    ''' </summary>
    Function GetLastCalculatedTime() As Decimal
    Function GetCurrentStep() As Integer
    ''' <summary>
    ''' Gets max step which is surely calculated
    ''' </summary>
    Function GetLastCalculatedStep() As Integer

    Property ExperimentTime() As Decimal

    Property CalculatingState() As CalculatingStates
    ''' <summary>
    ''' Used to ensure that all claculating will be correct after pause/restart
    ''' </summary>
    ''' <remarks></remarks>
    Sub UndoLastCalculatedStep()

    Sub BeforeCalculate()
    Sub Calculate()
    Event CycleStarted(ByVal model As IModel)
    Event CycleCalculated(ByVal model As IModel)
    Event ExceptionOccurred(ByVal model As IModel, ByVal ex As Exception)

    Function IsControlAlwaysShown() As Boolean
    Function GetControl() As System.Windows.Forms.UserControl
    Function GetMenuItems() As List(Of MenuItem)

    Property Thread() As Thread

    Function ResultToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement
    Sub ResultFromXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String)

    Function SelectionToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement
    Sub SelectionFromXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String)


    ''' <summary>
    ''' Currently used configuration for which this model belongs
    ''' </summary>
    Property Configuration() As Configuration

    ''' <summary>
    ''' Returns True if model has at least 1 value which should be shown
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property HasVisibleValues() As Boolean

    ''' <summary>
    ''' Use this property to lock extra actions for models which are created from .result file
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsOpenedFromResultFile() As Boolean

End Interface
