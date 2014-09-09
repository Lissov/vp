Imports Functions

Public Class InputFunction
    Inherits ModelBase.ObjectBase

#Region "Const"

    Public Const FUNCTIONS_XML_ELEMENT As String = "Functions"

#End Region

#Region "Enums"

    Public Enum FunctionTypes
        First = 0
        Formula = 1
        Constant = 2
        Linear = 3
        Sin = 4
        Impuls = 5
        [Step] = 6
        Beta = 7
        Last = 999
    End Enum

    Public Shared Function EnumValueToString(ByVal functionType As FunctionTypes) As String
        Select Case functionType
            Case FunctionTypes.Formula
                Return "Formula"
            Case FunctionTypes.Constant
                Return "Constant"
            Case FunctionTypes.Linear
                Return "Linear"
            Case FunctionTypes.Impuls
                Return "Impuls"
            Case FunctionTypes.Step
                Return "Step"
            Case FunctionTypes.Sin
                Return "Sin"
            Case FunctionTypes.Beta
                Return "Beta"
        End Select

        Return String.Empty
    End Function

#End Region

#Region "Properties"

    Private _Type As FunctionTypes = FunctionTypes.Formula
    Public Property Type() As FunctionTypes
        Get
            Return _Type
        End Get
        Set(ByVal value As FunctionTypes)
            _Type = value
        End Set
    End Property

    Private _Formula As String = MathExpression.TIME
    Public Property Formula() As String
        Get
            Return _Formula
        End Get
        Set(ByVal value As String)
            _Formula = value
        End Set
    End Property

    Private _Functions As List(Of FunctionBase)
    Public Property Functions() As List(Of FunctionBase)
        Get
            If _Functions Is Nothing Then
                _Functions = New List(Of FunctionBase)
            End If
            Return _Functions
        End Get
        Set(ByVal value As List(Of FunctionBase))
            _Functions = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        'add functions
        Functions.Add(New ConstantFunction)
        Functions.Add(New LinearFunction)
        Functions.Add(New SinFunction)
        Functions.Add(New ImpulsFunction)
        Functions.Add(New StepFunction)
        Functions.Add(New BetaFunction)
    End Sub

    Public Sub New(ByVal type As FunctionTypes)
        Me.New()

        Me.Type = type
    End Sub

#End Region

#Region "XML"


    Public Overrides Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = MyBase.ToXml(parentElement)

        'save properties
        SetAttribute(CurrentElement, "Type", Type)
        SetAttribute(CurrentElement, "Formula", Formula)

        'save functions
        Dim FunctionsElement As System.Xml.XmlElement
        FunctionsElement = CurrentElement.OwnerDocument.CreateElement(FUNCTIONS_XML_ELEMENT)
        CurrentElement.AppendChild(FunctionsElement)
        For Each [Function] As FunctionBase In Functions
            [Function].ToXml(FunctionsElement, [Function].Name)
        Next

        Return CurrentElement
    End Function

    Public Shared Function FromXml(ByVal currentElement As System.Xml.XmlElement) As InputFunction
        If currentElement Is Nothing OrElse _
           currentElement.Name <> GetType(InputFunction).Name().Replace("`", "") _
           Then
            Return Nothing
        End If

        Dim InputFunction As New InputFunction()

        If currentElement.Attributes("Type") IsNot Nothing Then
            InputFunction.Type = CType(GetInteger(currentElement, "Type"), FunctionTypes)
        End If
        If currentElement.Attributes("Formula") IsNot Nothing Then
            InputFunction.Formula = GetString(currentElement, "Formula")
        End If

        'load(Functions)
        Dim FunctionsElement As System.Xml.XmlElement
        FunctionsElement = currentElement.Item(FUNCTIONS_XML_ELEMENT)
        If FunctionsElement Is Nothing Then
            Throw New Exception("No functions found")
        End If
        For Each childElement As System.Xml.XmlElement In FunctionsElement.ChildNodes
            Dim [Function] As FunctionBase = InputFunction.GetFunctionByName(childElement.Name)
            If [Function] IsNot Nothing Then
                [Function] = CType([Function].FromXml(childElement, [Function].Name), FunctionBase)
            End If
        Next

        Return InputFunction
    End Function


#End Region

#Region "Private methods"

    Private Function GetFunctionByName(ByVal name As String) As FunctionBase
        If Functions Is Nothing OrElse Functions.Count = 0 Then Return Nothing

        Dim Result As FunctionBase = Nothing

        For Each [Function] As FunctionBase In Functions
            If [Function].Name = name Then
                Result = [Function]
                Exit For
            End If
        Next

        Return Result
    End Function

#End Region

#Region "Public methods"

    Public Function GetFunctionByType(ByVal type As FunctionTypes) As FunctionBase
        If Functions Is Nothing OrElse Functions.Count = 0 Then Return Nothing

        Dim Result As FunctionBase = Nothing

        For Each [Function] As FunctionBase In Functions
            If [Function].Type = type Then
                Result = [Function]
                Exit For
            End If
        Next

        Return Result
    End Function

    Public Function GetCalculatedValue(ByVal time As Double) As Double
        Dim Result As Double

        If Type = FunctionTypes.Formula Then
            Result = MathExpression.EvaluateTimeExpression(Formula, time)
        Else
            Result = GetFunctionByType(Type).GetCalculatedValue(time)
        End If

        Return Result
    End Function

#End Region

End Class
