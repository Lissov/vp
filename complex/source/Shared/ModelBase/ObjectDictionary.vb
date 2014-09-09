Imports System.Collections.Generic

Public Class ObjectDictionary(Of TKey, TValue)
    Inherits System.Collections.Generic.Dictionary(Of TKey, TValue)

    Public Sub New()
        MyBase.new()
    End Sub

#Region " XML methods"

    Public Function ToXmlString() As String
        Dim XmlDocument As New System.Xml.XmlDocument
        Dim XmlElement As System.Xml.XmlElement = XmlDocument.CreateElement("dictionary")
        XmlDocument.AppendChild(XmlElement)
        Me.ToXml(XmlElement)
        Return XmlDocument.OuterXml
    End Function

    Private Function ToXml(ByVal parentElement As System.Xml.XmlElement) As Xml.XmlElement

        If GetType(ObjectBase).IsAssignableFrom(GetType(TKey)) AndAlso _
           GetType(TValue).IsValueType Then
            For Each keyValuePair As Generic.KeyValuePair(Of TKey, TValue) In Me
                Dim ObjectBase As ObjectBase = TryCast(keyValuePair.Key, ObjectBase)
                If ObjectBase IsNot Nothing Then
                    Dim KeyValuePairElement As Xml.XmlElement = ObjectBase.ToXml(parentElement)
                    Dim StringValue As String = ""
                    If TypeOf keyValuePair.Value Is Integer Then
                        StringValue = CStr(CInt(CObj(keyValuePair.Value)))
                    Else
                        StringValue = keyValuePair.Value.ToString
                    End If
                    KeyValuePairElement.Attributes.Append(KeyValuePairElement.OwnerDocument.CreateAttribute("TValue")).Value = StringValue
                End If
            Next
        ElseIf GetType(ObjectBase).IsAssignableFrom(GetType(TValue)) AndAlso _
              (GetType(TKey).IsValueType OrElse GetType(TKey).Equals(GetType(String))) _
              Then
            For Each keyValuePair As Generic.KeyValuePair(Of TKey, TValue) In Me
                Dim ObjectBase As ObjectBase = TryCast(keyValuePair.Value, ObjectBase)
                If ObjectBase IsNot Nothing Then
                    Dim KeyValuePairElement As Xml.XmlElement = ObjectBase.ToXml(parentElement)
                    Dim StringKey As String = ""
                    If TypeOf keyValuePair.Key Is Integer OrElse TypeOf keyValuePair.Key Is [Enum] Then
                        StringKey = CStr(CInt(CObj(keyValuePair.Key)))
                    Else
                        StringKey = keyValuePair.Key.ToString
                    End If
                    KeyValuePairElement.Attributes.Append(KeyValuePairElement.OwnerDocument.CreateAttribute("TKey")).Value = StringKey
                End If
            Next
        ElseIf GetType(ObjectBase).IsAssignableFrom(GetType(TKey)) AndAlso _
               GetType(ObjectBase).IsAssignableFrom(GetType(TValue)) _
               Then
            Throw New NotImplementedException("Case is not implemented.")

        ElseIf (GetType(TKey).IsValueType OrElse GetType(TKey).Equals(GetType(String))) AndAlso _
               (GetType(TValue).IsValueType OrElse GetType(TValue).Equals(GetType(String))) _
               Then
            For Each keyValuePair As Generic.KeyValuePair(Of TKey, TValue) In Me
                Dim KeyValuePairElement As Xml.XmlElement = parentElement.OwnerDocument.CreateElement(GetType(TKey).Name().Replace("`", ""))
                parentElement.AppendChild(KeyValuePairElement)
                Dim StringKey As String = ""
                If TypeOf keyValuePair.Key Is Integer OrElse TypeOf keyValuePair.Key Is [Enum] Then
                    StringKey = CStr(CInt(CObj(keyValuePair.Key)))
                Else
                    StringKey = keyValuePair.Key.ToString
                End If
                KeyValuePairElement.Attributes.Append(KeyValuePairElement.OwnerDocument.CreateAttribute("TKey")).Value = StringKey
                Dim StringValue As String = ""
                If TypeOf keyValuePair.Value Is Integer Then
                    StringValue = CStr(CInt(CObj(keyValuePair.Value)))
                Else
                    StringValue = keyValuePair.Value.ToString
                End If
                KeyValuePairElement.Attributes.Append(KeyValuePairElement.OwnerDocument.CreateAttribute("TValue")).Value = StringValue
            Next
        ElseIf (GetType(TKey).IsValueType OrElse GetType(TKey).Equals(GetType(String))) AndAlso _
               GetType(TValue).Equals(GetType(ObjectList(Of String))) _
               Then
            'key is string or simple value, value - list of strings
            For Each keyValuePair As Generic.KeyValuePair(Of TKey, TValue) In Me
                Dim KeyValuePairElement As Xml.XmlElement = parentElement.OwnerDocument.CreateElement(GetType(TKey).Name().Replace("`", ""))
                parentElement.AppendChild(KeyValuePairElement)
                Dim StringKey As String = ""
                If TypeOf keyValuePair.Key Is Integer OrElse TypeOf keyValuePair.Key Is [Enum] Then
                    StringKey = CStr(CInt(CObj(keyValuePair.Key)))
                Else
                    StringKey = keyValuePair.Key.ToString
                End If
                KeyValuePairElement.Attributes.Append(KeyValuePairElement.OwnerDocument.CreateAttribute("TKey")).Value = StringKey
                Dim StringValue As String
                StringValue = CType(CObj(keyValuePair.Value), ObjectList(Of String)).ToXmlString
                KeyValuePairElement.Attributes.Append(KeyValuePairElement.OwnerDocument.CreateAttribute("TValue")).Value = StringValue
            Next
        End If

        Return parentElement
    End Function

    Public Shared Function FromXmlString(ByVal xmlString As String) As ObjectDictionary(Of TKey, TValue)
        Dim XmlDocument As New Xml.XmlDocument
        XmlDocument.LoadXml(xmlString)
        Dim CurrentElement As Xml.XmlElement = XmlDocument.DocumentElement

        Dim Dictionary As New ObjectDictionary(Of TKey, TValue)

        If GetType(ObjectBase).IsAssignableFrom(GetType(TKey)) AndAlso _
           GetType(TValue).IsValueType _
           Then
            For Each Element As Xml.XmlElement In CurrentElement.ChildNodes
                Dim ObjectBase As ObjectBase
                ObjectBase = CType(Activator.CreateInstance(GetType(TKey)), ObjectBase)
                If ObjectBase IsNot Nothing Then
                    Dim Key As TKey = CType(ObjectBase.FromXml(Element), TKey)
                    Dictionary.Add(Key, CType(CObj(Element.Attributes("TValue").Value), TValue))
                End If
            Next

        ElseIf GetType(ObjectBase).IsAssignableFrom(GetType(TValue)) _
           AndAlso (GetType(TKey).IsValueType OrElse GetType(TKey).Equals(GetType(String))) _
           Then
            For Each Element As Xml.XmlElement In CurrentElement.ChildNodes
                If Not Element.HasAttribute("TKey") Then Continue For

                Dim ObjectBase As ObjectBase
                ObjectBase = CType(Activator.CreateInstance(GetType(TValue)), ObjectBase)
                If ObjectBase IsNot Nothing Then
                    Dim Value As TValue = CType(ObjectBase.FromXml(Element), TValue)
                    Dictionary.Add(CType(CObj(Element.Attributes("TKey").Value), TKey), Value)
                End If
            Next

        ElseIf (GetType(TKey).IsValueType OrElse GetType(TKey).Equals(GetType(String))) AndAlso _
           (GetType(TValue).IsValueType OrElse GetType(TValue).Equals(GetType(String))) _
           Then
            For Each Element As Xml.XmlElement In CurrentElement.ChildNodes
                If Not Element.HasAttribute("TKey") Then Continue For
                Dictionary.Add(CType(CObj(Element.Attributes("TKey").Value), TKey), CType(CObj(Element.Attributes("TValue").Value), TValue))
            Next
        ElseIf (GetType(TKey).IsValueType OrElse GetType(TKey).Equals(GetType(String))) AndAlso GetType(TValue).Equals(GetType(ObjectList(Of String))) Then
            'key is string or simple value, value - list of strings
            For Each Element As Xml.XmlElement In CurrentElement.ChildNodes
                If Not Element.HasAttribute("TKey") Then Continue For
                Dim Key As TKey = CType(CObj(Element.Attributes("TKey").Value), TKey)
                Dim ValueString As String = Element.Attributes("TValue").Value
                Dim ValueList As ObjectList(Of String)
                ValueList = ObjectList(Of String).FromXmlString(ValueString)
                Dictionary.Add(Key, CType(CObj(ValueList), TValue))
            Next
        End If

        Return Dictionary
    End Function

#End Region

    ''' <summary>
    ''' Inserts an element into the dictionary at the specified index.
    ''' </summary>
    ''' <param name="index">The zero-based index at which item should be inserted</param>
    ''' <param name="key">The key of the element to add</param>
    ''' <param name="value">The value of the element to add. The value can be null for reference types</param>
    ''' <remarks></remarks>
    Public Sub Insert(ByVal index As Integer, ByVal key As TKey, ByVal value As TValue)
        If Me.ContainsKey(key) Then Return

        Dim List As New List(Of KeyValuePair(Of TKey, TValue))
        For Each Pair As KeyValuePair(Of TKey, TValue) In Me
            List.Add(Pair)
        Next
        List.Insert(index, New KeyValuePair(Of TKey, TValue)(key, value))

        Me.Clear()
        For Each Pair As KeyValuePair(Of TKey, TValue) In List
            Me.Add(Pair.Key, Pair.Value)
        Next

    End Sub

    ''' <summary>
    ''' Add records to current dictionary from other dictionary.
    ''' </summary>
    ''' <param name="Destination">Dictionary to merge</param>
    ''' <param name="overwriteIfConflicts">Overwrite records if there exist records with same keys</param>
    ''' <remarks></remarks>
    Public Shared Sub MergeDictionary(ByVal Source As Generic.Dictionary(Of TKey, TValue), ByVal Destination As Generic.Dictionary(Of TKey, TValue), ByVal overwriteIfConflicts As Boolean)
        If Source Is Nothing OrElse Destination Is Nothing Then Return
        For Each D As Generic.KeyValuePair(Of TKey, TValue) In Source
            If Destination.ContainsKey(D.Key) AndAlso overwriteIfConflicts Then
                Destination.Remove(D.Key)
                Destination.Add(D.Key, D.Value)
            Else
                Destination.Add(D.Key, D.Value)
            End If
        Next
    End Sub

End Class


'''''''''''''''''''''''''''''''''''''''''''
'''''''  eaxmple of usage '''''''''''''''''
'''''''''''''''''''''''''''''''''''''''''''
'Dim d1 As New ModelBase.ObjectDictionary(Of Integer, ModelBase.Parameter)
'        d1.Add(1, New ModelBase.Parameter("aa", "aa1"))
'        d1.Add(2, New ModelBase.Parameter("bb", "bb1"))
'        d1.Add(3, New ModelBase.Parameter("cc", "cc1"))
'Dim s1 As String = d1.ToXmlString
'Dim d2 As ModelBase.ObjectDictionary(Of Integer, ModelBase.Parameter)
'        d2 = ModelBase.ObjectDictionary(Of Integer, ModelBase.Parameter).FromXmlString(s1)

'Dim d3 As New ModelBase.ObjectDictionary(Of ModelBase.Parameter, Integer)
'        d3.Add(New ModelBase.Parameter("aa", "aa1"), 1)
'        d3.Add(New ModelBase.Parameter("bb", "bb1"), 2)
'        d3.Add(New ModelBase.Parameter("cc", "cc1"), 3)
'Dim s2 As String = d3.ToXmlString
'Dim d4 As ModelBase.ObjectDictionary(Of ModelBase.Parameter, Integer)
'        d4 = ModelBase.ObjectDictionary(Of ModelBase.Parameter, Integer).FromXmlString(s2)

'Dim d5 As New ModelBase.ObjectDictionary(Of Integer, String)
'        d5.Add(1, "aa")
'        d5.Add(2, "bb")
'        d5.Add(3, "cc")
'Dim s3 As String = d5.ToXmlString
'Dim d6 As ModelBase.ObjectDictionary(Of Integer, String)
'        d6 = ModelBase.ObjectDictionary(Of Integer, String).FromXmlString(s3)