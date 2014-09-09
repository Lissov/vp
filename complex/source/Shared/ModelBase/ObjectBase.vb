Public Class ObjectBase
    Implements IXmlObject
    Implements ICloneable

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

    Public Const ROOT_NAME As String = "object"

    ''' <summary>
    ''' Returns name of the xml root for the class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable ReadOnly Property XmlName() As String
        Get
            Return Me.GetType().Name().Replace("`", "")
        End Get
    End Property

    Public Overridable Overloads Function ToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement Implements IXmlObject.ToXml
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)
        Me.ToXml(CurrentElement)
        Return CurrentElement
    End Function

    Public Overridable Overloads Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement Implements IXmlObject.ToXml
        Dim CurrentElement As System.Xml.XmlElement
        CurrentElement = parentElement.OwnerDocument.CreateElement(XmlName)

        parentElement.AppendChild(CurrentElement)

        Return CurrentElement
    End Function

    Public Function ToXmlDocument() As System.Xml.XmlDocument
        Dim XmlDocument As New System.Xml.XmlDocument
        Dim XmlElement As System.Xml.XmlElement = XmlDocument.CreateElement(ROOT_NAME)

        XmlDocument.AppendChild(XmlElement)

        Me.ToXml(XmlElement)

        Return XmlDocument
    End Function

    Public Function ToXmlString() As String
        Dim XmlDocument As System.Xml.XmlDocument = ToXmlDocument()

        Return XmlDocument.OuterXml()
    End Function

    Public Overridable Function FromXml(ByVal xmlElement As System.Xml.XmlElement, ByVal name As String) As Object Implements IXmlObject.FromXml
        If xmlElement Is Nothing OrElse xmlElement.Name <> name Then Return String.Empty

        Dim CurrentElement As System.Xml.XmlElement
        CurrentElement = xmlElement.Item(XmlName)
        Return FromXml(CurrentElement)
    End Function

    Public Overridable Function FromXml(ByVal xmlElement As System.Xml.XmlElement) As Object Implements IXmlObject.FromXml
        Return Nothing
    End Function

    Public Function FromXmlDocument(ByVal xmlDocument As System.Xml.XmlDocument) As Object
        If xmlDocument Is Nothing Then Return Nothing

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return Nothing

        Return FromXml(RootElement.Item(XmlName))
    End Function

#Region " XML "

#Region "Get"

    ''' <summary>
    ''' Gets value from attribute of node like <node attribute='value' />
    ''' </summary>
    ''' <param name="currentElement"></param>
    ''' <param name="attributeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetString(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As String
        Dim Attribute As System.Xml.XmlAttribute

        Attribute = TryCast(currentElement.Attributes.GetNamedItem(attributeName), System.Xml.XmlAttribute)
        If Not Attribute Is Nothing Then
            Return Attribute.Value
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetInteger(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As Integer
        Return Integer.Parse(GetString(currentElement, attributeName))
    End Function

    Public Shared Function GetBoolean(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As Boolean
        Return System.Xml.XmlConvert.ToBoolean(GetString(currentElement, attributeName))
    End Function

    Public Shared Function GetDouble(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As Double
        Dim StringValue As String = GetString(currentElement, attributeName)
        StringValue = StringValue.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        StringValue = StringValue.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)

        If StringValue = Double.MinValue.ToString Then
            Return Double.MinValue
        ElseIf StringValue = Double.MaxValue.ToString Then
            Return Double.MaxValue
        Else
            Return CType(StringValue, Double)
        End If

    End Function

    Public Shared Function GetDecimal(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As Decimal
        Dim StringValue As String = GetString(currentElement, attributeName)
        StringValue = StringValue.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        StringValue = StringValue.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)

        If StringValue = Decimal.MinValue.ToString Then
            Return Decimal.MinValue
        ElseIf StringValue = Decimal.MaxValue.ToString Then
            Return Decimal.MaxValue
        Else
            Return CType(StringValue, Decimal)
        End If

    End Function

    Public Shared Function GetSingle(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As Double
        Dim StringValue As String = GetString(currentElement, attributeName)
        StringValue = StringValue.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
        StringValue = StringValue.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)

        If StringValue = Single.MinValue.ToString Then
            Return Single.MinValue
        ElseIf StringValue = Single.MaxValue.ToString Then
            Return Single.MaxValue
        Else
            Return CType(StringValue, Single)
        End If

    End Function

    Public Shared Function GetDate(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String) As Date
        Dim Result As Date = Date.MinValue
        Try
            Dim ParseResult As Boolean = False
            ParseResult = Date.TryParse(GetString(currentElement, attributeName), Result)
            If ParseResult Then Return Result.ToUniversalTime

            Result = Date.FromOADate(GetDouble(currentElement, attributeName))
            Return Result.ToUniversalTime
        Catch ex As Exception
            Throw New InvalidCastException("Date was in incorrect format", ex)
        End Try

    End Function

#End Region

#Region "Set"

    ''' <summary>
    ''' Sets value to attribute of node
    ''' </summary>
    ''' <param name="currentElement"></param>
    ''' <param name="attributeName"></param>
    ''' <param name="attributeValue"></param>
    ''' <remarks></remarks>
    Protected Overloads Sub SetAttribute(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String, ByVal attributeValue As String)
        currentElement.Attributes.Append(currentElement.OwnerDocument.CreateAttribute(attributeName)).Value = attributeValue
    End Sub

    Protected Overloads Sub SetAttribute(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String, ByVal attributeValue As Integer)
        SetAttribute(currentElement, attributeName, attributeValue.ToString())
    End Sub

    Protected Overloads Sub SetAttribute(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String, ByVal attributeValue As Double)
        SetAttribute(currentElement, attributeName, attributeValue.ToString())
    End Sub

    Protected Overloads Sub SetAttribute(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String, ByVal attributeValue As Single)
        SetAttribute(currentElement, attributeName, attributeValue.ToString())
    End Sub

    Protected Overloads Sub SetAttribute(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String, ByVal attributeValue As Date)
        SetAttribute(currentElement, attributeName, attributeValue.ToString("u"))
    End Sub

    Protected Overloads Sub SetAttribute(ByVal currentElement As System.Xml.XmlElement, ByVal attributeName As String, ByVal attributeValue As Boolean)
        SetAttribute(currentElement, attributeName, Math.Abs(CType(attributeValue, Integer)).ToString())
    End Sub

#End Region

#End Region

    Public Overridable Function Clone() As Object Implements System.ICloneable.Clone
        Return Me.MemberwiseClone
    End Function

End Class
