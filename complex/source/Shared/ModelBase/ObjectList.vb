Imports System.ComponentModel
Imports System.Collections.Generic


Public Class ObjectList(Of T)
    Inherits BindingList(Of T)

    Public Sub New()

    End Sub

#Region "To XML"

    Public Function ToXmlString() As String
        Dim xmlDocument As System.Xml.XmlDocument = ToXmlDocument()

        Return xmlDocument.OuterXml()
    End Function

    Public Overridable Overloads Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
        Dim currentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(GetType(T).Name() & "List")

        parentElement.AppendChild(currentElement)

        AddItemsToXmlElement(currentElement)

        Return currentElement
    End Function

    Public Overridable Overloads Sub ToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String)
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)

        AddItemsToXmlElement(CurrentElement)

    End Sub

    Public Overridable Function ToXmlElement(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As Xml.XmlElement
        Dim CurrentElement As System.Xml.XmlElement = parentElement.OwnerDocument.CreateElement(name)

        parentElement.AppendChild(CurrentElement)

        AddItemsToXmlElement(CurrentElement)

        Return CurrentElement
    End Function

    Private Sub AddItemsToXmlElement(ByRef xmlElement As System.Xml.XmlElement)

        If GetType(ObjectBase).IsAssignableFrom(GetType(T)) Then
            For Each InstanceOfT As T In Me
                Dim ObjectBase As ObjectBase = TryCast(InstanceOfT, ObjectBase)
                If Not ObjectBase Is Nothing Then
                    ObjectBase.ToXml(xmlElement)
                End If
            Next

        ElseIf GetType(T).Equals(GetType(String)) Then
            For Each InstanceOfT As T In Me
                Dim StringValue As String = TryCast(InstanceOfT, String)
                Dim StringElement As System.Xml.XmlElement = xmlElement.OwnerDocument.CreateElement("StringValue")
                StringElement.InnerText = StringValue
                xmlElement.AppendChild(StringElement)
            Next

        ElseIf GetType(T).IsValueType Then
            For Each InstanceOfT As T In Me
                Dim StringValue As String
                If TypeOf InstanceOfT Is Integer OrElse TypeOf InstanceOfT Is [Enum] Then
                    StringValue = CStr(CInt(CObj(InstanceOfT)))
                Else
                    StringValue = InstanceOfT.ToString
                End If
                Dim StringElement As System.Xml.XmlElement = xmlElement.OwnerDocument.CreateElement("ValueTypeValue")
                StringElement.InnerText = StringValue
                xmlElement.AppendChild(StringElement)
            Next

        End If
    End Sub

    Public Function ToXmlDocument() As System.Xml.XmlDocument
        Dim XmlDocument As New System.Xml.XmlDocument
        Dim XmlElement As System.Xml.XmlElement = XmlDocument.CreateElement("object")

        XmlDocument.AppendChild(XmlElement)

        Me.ToXml(XmlElement)

        Return XmlDocument
    End Function

#End Region

#Region "From XML "

    Public Shared Function FromXml(ByVal currentElement As System.Xml.XmlElement) As ObjectList(Of T)
        Dim List As New ObjectList(Of T)

        For Each childElement As System.Xml.XmlElement In currentElement.ChildNodes
            List.Add(GetValueFromXmlElement(childElement))
        Next

        Return List
    End Function

    Public Shared Function FromXml(ByVal currentElement As System.Xml.XmlElement, ByVal name As String) As ObjectList(Of T)
        Dim List As New ObjectList(Of T)

        For Each childElement As System.Xml.XmlElement In currentElement.ChildNodes
            If childElement.Name = name Then
                For Each childElement1 As System.Xml.XmlElement In childElement.ChildNodes
                    List.Add(GetValueFromXmlElement(childElement1))
                Next
            End If
        Next

        If List.Count = 0 Then
            Return Nothing
        End If

        Return List
    End Function

    Private Shared Function GetValueFromXmlElement(ByVal xmlElement As System.Xml.XmlElement) As T
        Dim Value As T

        If GetType(ObjectBase).IsAssignableFrom(GetType(T)) Then
            Dim ObjectBase As ObjectBase
            ObjectBase = CType(Activator.CreateInstance(GetType(T)), ObjectBase)
            If ObjectBase IsNot Nothing Then
                Value = CType(ObjectBase.FromXml(xmlElement), T)
            End If

        ElseIf GetType(T).Equals(GetType(String)) Then
            Value = CType(CObj(xmlElement.InnerText), T)

        ElseIf GetType(T).IsValueType Then
            Value = CType(CObj(xmlElement.InnerText), T)

        End If

        Return Value
    End Function

    Public Shared Function FromXmlDocument(ByVal xmlDocument As System.Xml.XmlDocument) As ObjectList(Of T)
        Dim XmlElement As System.Xml.XmlElement = TryCast(xmlDocument.SelectSingleNode("/object/*"), System.Xml.XmlElement)

        Return ObjectList(Of T).FromXml(XmlElement)
    End Function

    Public Shared Function FromXmlString(ByVal xmlString As String) As ObjectList(Of T)
        Dim xmlDocument As New System.Xml.XmlDocument
        xmlDocument.LoadXml(xmlString)
        Dim XmlElement As System.Xml.XmlElement = TryCast(xmlDocument.SelectSingleNode("/object/*"), System.Xml.XmlElement)

        Return ObjectList(Of T).FromXml(XmlElement)
    End Function

#End Region

#Region " SORTING  "

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Private Function GetPropertyValue(ByVal value As T, ByVal prop As String) As Object
        ' Get property
        Dim propertyInfo As System.Reflection.PropertyInfo = value.GetType().GetProperty(prop)

        ' Return value
        Return propertyInfo.GetValue(value, Nothing)
    End Function

    Private Function TryParseDoubleToSortHash(ByVal value As Object, ByRef hash As String) As Boolean
        Try
            Dim StringValue As String = CStr(value).ToLower
            Dim ParseResult As Boolean
            Dim DoubleValue As Double
            ParseResult = Double.TryParse(StringValue.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), DoubleValue)
            If ParseResult Then
                ' add normalize integers
                DoubleValue += 1000000
                Dim ValueLength As String
                If StringValue.StartsWith("-") Then
                    ValueLength = CStr(CStr(Math.Floor(DoubleValue)).Length - 1).PadLeft(3, "0"c)
                Else
                    ValueLength = CStr(CStr(Math.Floor(DoubleValue)).Length).PadLeft(3, "0"c)
                End If
                hash = (ValueLength + String.Format("{0:0.0000000000000000}", DoubleValue))
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function TryParseIntegerToSortHash(ByVal value As Object, ByRef hash As String) As Boolean
        Try
            Dim StringValue As String = CStr(value).ToLower
            Dim ParseResult As Boolean
            Dim IntegerValue As Integer
            ParseResult = Integer.TryParse(StringValue, IntegerValue)
            If ParseResult Then
                Dim ValueLength As String
                If StringValue.StartsWith("-") Then
                    ValueLength = CStr(StringValue.Length - 1).PadLeft(3, "0"c)
                Else
                    ValueLength = CStr(StringValue.Length).PadLeft(3, "0"c)
                End If
                hash = ValueLength + StringValue
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function TryParseDateToSortHash(ByVal value As Object, ByRef hash As String) As Boolean
        Try
            Dim ParseResult As Boolean
            Dim DateValue As Date
            ParseResult = Date.TryParse(CStr(value), DateValue)
            If ParseResult Then
                hash = String.Format("{0:yyyymmddhhmmss}", DateValue)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function TryParseStringToSortHash(ByVal value As Object, ByRef hash As String) As Boolean
        Try
            hash = CStr(value).ToLower
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub Sort(ByVal propertyName As String, ByVal direction As ListSortDirection)
        If Me.Items Is Nothing OrElse Me.Items.Count = 0 Then Exit Sub
        Dim TItem As T = Me.Item(0)
        Dim Properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(TItem)

        ' Sets an PropertyDescriptor to the specific property.
        Dim CurrentPropertyDescriptor As PropertyDescriptor = Properties.Find(propertyName, False)

        If CurrentPropertyDescriptor IsNot Nothing Then
            ApplySortCore(CurrentPropertyDescriptor, direction)
        End If

    End Sub

    ''' <summary>
    ''' Use it for simple types only!!!
    ''' </summary>
    ''' <param name="direction"></param>
    ''' <remarks></remarks>
    Public Sub Sort(ByVal direction As ListSortDirection)
        If Me.Items Is Nothing OrElse Me.Items.Count = 0 Then Exit Sub

        'ensure this is simple type
        If Not GetType(T).Equals(GetType(String)) AndAlso Not GetType(T).IsValueType Then Exit Sub

        ApplySimpleSortCore(direction)
    End Sub

#Region "Overrides"

    Protected Overrides Sub ApplySortCore(ByVal propertyDescriptor As PropertyDescriptor, _
                                          ByVal direction As ListSortDirection)
        _IsSortedCore = False
        _SortDirectionCore = direction
        _SortPropertyCore = propertyDescriptor

        ' Get list to sort
        Dim Items As List(Of T)
        Items = CType(Me.Items, List(Of T))

        ' Apply and set the sort, if items to sort
        If Not Items Is Nothing Then
            Dim Comparer As New PropertyComparer(Of T)(propertyDescriptor, direction)
            Items.Sort(Comparer)
            _IsSortedCore = True
        Else
            _IsSortedCore = False
        End If

        ' Let bound controls know they should refresh their views
        Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Protected Sub ApplySimpleSortCore(ByVal direction As ListSortDirection)
        _IsSortedCore = False
        _SortDirectionCore = direction

        ' Get list to sort
        Dim Items As List(Of T)
        Items = CType(Me.Items, List(Of T))

        ' Apply and set the sort, if items to sort
        If Not Items Is Nothing Then
            Dim Comparer As New SimplePropertyComparer(Of T)(direction)
            Items.Sort(Comparer)
            _IsSortedCore = True
        Else
            _IsSortedCore = False
        End If

        ' Let bound controls know they should refresh their views
        Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Private _IsSortedCore As Boolean
    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return _IsSortedCore
        End Get
    End Property

    Dim _SortDirectionCore As ListSortDirection
    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return _SortDirectionCore
        End Get
    End Property

    Dim _SortPropertyCore As PropertyDescriptor
    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
        Get
            Return _SortPropertyCore
        End Get
    End Property

    Protected Overrides Sub RemoveSortCore()
        _IsSortedCore = False
    End Sub

#End Region

    Public Class PropertyComparer(Of T)
        Implements IComparer(Of T)

        Private _Property As PropertyDescriptor
        Private _Direction As ListSortDirection

        Public Sub New(ByVal prop As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
            _property = prop
            _direction = direction
        End Sub

#Region "IComparer<T>"

        ''' <summary>
        ''' compare properties of objects
        ''' </summary>
        ''' <param name="xObject"></param>
        ''' <param name="yObject"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Compare(ByVal xObject As T, ByVal yObject As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
            ' Get property values
            Dim xValue As Object = GetPropertyValue(xObject, _property.Name)
            Dim yValue As Object = GetPropertyValue(yObject, _property.Name)

            ' Determine sort order
            If _Direction = System.ComponentModel.ListSortDirection.Ascending Then
                Return CompareAscending(xValue, yValue)
            Else
                Return CompareDescending(xValue, yValue)
            End If
        End Function

        Public Shadows Function Equals(ByVal xWord As T, ByVal yWord As T) As Boolean
            Return xWord.Equals(yWord)
        End Function

        Public Shadows Function GetHashCode(ByVal obj As T) As Integer
            Return obj.GetHashCode()
        End Function

#End Region

        ''' <summary>
        ''' Compare two property values of any type
        ''' </summary>
        ''' <param name="xValue"></param>
        ''' <param name="yValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CompareAscending(ByVal xValue As Object, ByVal yValue As Object) As Integer
            Dim result As Integer

            If xValue Is Nothing AndAlso yValue Is Nothing Then
                Return 0
            ElseIf xValue Is Nothing Then
                Return -1
            ElseIf yValue Is Nothing Then
                Return 1
            ElseIf TypeOf xValue Is String Then
                Return String.Compare(CStr(xValue), CStr(yValue), True)
                ' If values implement IComparer
            ElseIf TypeOf xValue Is IComparable Then
                result = (CType(xValue, IComparable)).CompareTo(yValue)
                ' If values don't implement IComparer but are equivalent
            ElseIf xValue.Equals(yValue) Then
                result = 0
                ' Values don't implement IComparer and are not equivalent, so compare as string values
            Else
                result = xValue.ToString().CompareTo(yValue.ToString())
            End If

            ' Return result
            Return result
        End Function

        Private Function CompareDescending(ByVal xValue As Object, ByVal yValue As Object) As Integer
            ' Return result adjusted for ascending or descending sort order ie
            ' multiplied by 1 for ascending or -1 for descending
            Return CompareAscending(xValue, yValue) * -1
        End Function

        Private Function GetPropertyValue(ByVal value As T, ByVal prop As String) As Object
            ' Get property
            Dim propertyInfo As System.Reflection.PropertyInfo = value.GetType().GetProperty(prop)

            ' Return value
            Return propertyInfo.GetValue(value, Nothing)
        End Function

    End Class

    Public Class SimplePropertyComparer(Of T)
        Implements IComparer(Of T)

        Private _Direction As ListSortDirection

        Public Sub New(ByVal direction As System.ComponentModel.ListSortDirection)
            _Direction = direction
        End Sub

#Region "IComparer<T>"

        ''' <summary>
        ''' compare properties of objects
        ''' </summary>
        ''' <param name="xObject"></param>
        ''' <param name="yObject"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Compare(ByVal xObject As T, ByVal yObject As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
            ' Determine sort order
            If _Direction = System.ComponentModel.ListSortDirection.Ascending Then
                Return CompareAscending(xObject, yObject)
            Else
                Return CompareDescending(xObject, yObject)
            End If
        End Function

        Public Shadows Function Equals(ByVal xWord As T, ByVal yWord As T) As Boolean
            Return xWord.Equals(yWord)
        End Function

        Public Shadows Function GetHashCode(ByVal obj As T) As Integer
            Return obj.GetHashCode()
        End Function

#End Region

        ''' <summary>
        ''' Compare two property values of any type
        ''' </summary>
        ''' <param name="xValue"></param>
        ''' <param name="yValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CompareAscending(ByVal xValue As Object, ByVal yValue As Object) As Integer
            Dim result As Integer

            If xValue Is Nothing AndAlso yValue Is Nothing Then
                Return 0
            ElseIf xValue Is Nothing Then
                Return -1
            ElseIf yValue Is Nothing Then
                Return 1
            ElseIf TypeOf xValue Is String Then
                Return String.Compare(CStr(xValue), CStr(yValue), True)
                ' If values implement IComparer
            ElseIf TypeOf xValue Is IComparable Then
                result = (CType(xValue, IComparable)).CompareTo(yValue)
                ' If values don't implement IComparer but are equivalent
            ElseIf xValue.Equals(yValue) Then
                result = 0
                ' Values don't implement IComparer and are not equivalent, so compare as string values
            Else
                result = xValue.ToString().CompareTo(yValue.ToString())
            End If

            ' Return result
            Return result
        End Function

        Private Function CompareDescending(ByVal xValue As Object, ByVal yValue As Object) As Integer
            ' Return result adjusted for ascending or descending sort order ie
            ' multiplied by 1 for ascending or -1 for descending
            Return CompareAscending(xValue, yValue) * -1
        End Function


    End Class

#End Region

#Region "Add range"

    Public Shared Sub AddRange(ByRef mainList As ObjectList(Of T), _
                               ByVal rangeList As ObjectList(Of T))

        If rangeList Is Nothing OrElse rangeList.Count = 0 Then Return
        If mainList Is Nothing Then
            mainList = New ObjectList(Of T)
        End If

        For Each element As T In rangeList
            mainList.Add(element)
        Next
    End Sub

    Public Sub AddRange(ByVal rangeList As Generic.IEnumerable(Of T))
        If rangeList Is Nothing Then Return

        For Each element As T In rangeList
            Me.Add(element)
        Next
    End Sub

#End Region

End Class

'''''''''''''''''''''''''''''''''''''''''''
'''''''  eaxmple of usage '''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''
'Dim l1 As New ModelBase.ObjectList(Of ModelBase.Parameter)
'        l1.Add(New ModelBase.Parameter("aa", "aa1"))
'        l1.Add(New ModelBase.Parameter("dd", "dd1"))
'        l1.Add(New ModelBase.Parameter("bb", "bb1"))
'Dim s As String = l1.ToXmlString
'Dim l2 As ModelBase.ObjectList(Of ModelBase.Parameter)
'        l2 = ModelBase.ObjectList(Of ModelBase.Parameter).FromXmlString(s)
'        l2.Sort("Name", ComponentModel.ListSortDirection.Descending)
'        l2.Sort("Name", ComponentModel.ListSortDirection.Ascending)


'Dim l3 As New ModelBase.ObjectList(Of Integer)
'        l3.Add(12)
'        l3.Add(48)
'        l3.Add(34)
'Dim s2 As String = l3.ToXmlString
'Dim l4 As ModelBase.ObjectList(Of Integer)
'        l4 = ModelBase.ObjectList(Of Integer).FromXmlString(s2)
'        l4.Sort(ComponentModel.ListSortDirection.Descending)
'        l4.Sort(ComponentModel.ListSortDirection.Ascending)
