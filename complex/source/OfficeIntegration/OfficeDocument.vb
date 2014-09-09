Imports System.Collections.Generic
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.CustomProperties
Imports System.IO.Packaging


Public Class OfficeDocument

#Region "Enums"

    Public Enum ValueType
        NotDefined = 0
        Numeric = 1
        Text = 2
        [Date] = 3
    End Enum

#End Region

#Region "Standart properties names"

    Public Const _Author As String = "Author"
    Public Const _Category As String = "Category"
    Public Const _Comments As String = "Comments"
    Public Const _Company As String = "Company"
    Public Const _Keywords As String = "Keywords"
    Public Const _Manager As String = "Manager"
    Public Const _Subject As String = "Subject"
    Public Const _Title As String = "Title"
    Public Const _Status As String = "Status"

#End Region


#Region "Functions for properties"

    ''' <summary>
    ''' Get custom properties from document
    ''' </summary>
    ''' <param name="openXmlDocument">Document which is being merged</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetCustomProperties(ByVal openXmlDocument As OpenXmlPackage) As CustomFilePropertiesPart
        If TypeOf openXmlDocument Is SpreadsheetDocument Then
            Dim spreadSheet As SpreadsheetDocument = CType(openXmlDocument, SpreadsheetDocument)

            If spreadSheet.CustomFilePropertiesPart Is Nothing Then
                spreadSheet.AddCustomFilePropertiesPart()
            End If

            Return spreadSheet.CustomFilePropertiesPart
        ElseIf TypeOf openXmlDocument Is WordprocessingDocument Then
            Dim WordDoc As WordprocessingDocument = CType(openXmlDocument, WordprocessingDocument)

            If WordDoc.CustomFilePropertiesPart Is Nothing Then
                WordDoc.AddCustomFilePropertiesPart()
            End If

            Return WordDoc.CustomFilePropertiesPart
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Get package properties from document
    ''' </summary>
    ''' <param name="openXmlDocument">Document which is being merged</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetPackageProperties(ByVal openXmlDocument As OpenXmlPackage) As PackageProperties
        If TypeOf openXmlDocument Is SpreadsheetDocument Then
            Return CType(openXmlDocument, SpreadsheetDocument).PackageProperties
        ElseIf TypeOf openXmlDocument Is WordprocessingDocument Then
            Return CType(openXmlDocument, WordprocessingDocument).PackageProperties
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Get extended file properties from document
    ''' </summary>
    ''' <param name="openXmlDocument">Document which is being merged</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetExtendedFileProperties(ByVal openXmlDocument As OpenXmlPackage) As ExtendedFilePropertiesPart
        If TypeOf openXmlDocument Is SpreadsheetDocument Then
            Return CType(openXmlDocument, SpreadsheetDocument).ExtendedFilePropertiesPart
        ElseIf TypeOf openXmlDocument Is WordprocessingDocument Then
            Return CType(openXmlDocument, WordprocessingDocument).ExtendedFilePropertiesPart
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Used to get from given customProperties custom property with given name.
    ''' It it does not exists it will be created
    ''' </summary>
    ''' <param name="customProperties">customProperties of merged document</param>
    ''' <param name="propertyName">name of required property</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetCustomProperty(ByVal customProperties As CustomFilePropertiesPart, _
                                                ByVal propertyName As String) _
                                                    As [Property]
        Dim CustomProperty As [Property] = Nothing

        If customProperties.Properties IsNot Nothing Then
            For Each [Property] As [Property] In customProperties.Properties
                If [Property].Name.Value.ToLower = propertyName.ToLower Then
                    CustomProperty = [Property]
                    Exit For
                End If
            Next
        Else
            'customProperties.Properties = nothing -> they must be created
            customProperties.Properties = New Properties
        End If

        If CustomProperty Is Nothing Then
            CustomProperty = New [Property]
            CustomProperty.Name = propertyName
            CustomProperty.LinkTarget = Nothing
            CustomProperty.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"
            CustomProperty.PropertyId = customProperties.Properties.Count + 2

            customProperties.Properties.AppendChild(CustomProperty)
        End If

        Return CustomProperty
    End Function

    ''' <summary>
    ''' Used to incert new value into the given property
    ''' </summary>
    ''' <param name="customProperty">customProperty which must be updated</param>
    ''' <param name="newValue">New value which should be inserted</param>
    ''' <remarks></remarks>
    Protected Shared Sub InsertPropertyValue(ByVal customProperty As [Property], _
                                             ByVal newValue As PropertyValue)
        InsertPropertyValue(customProperty, newValue.Name, newValue.ValueType)
    End Sub

    ''' <summary>
    ''' Used to incert new value into the given property
    ''' </summary>
    ''' <param name="customProperty">customProperty which must be updated</param>
    ''' <param name="newValue">New value which should be inserted</param>
    ''' <param name="newValueType">Type of new value</param>
    ''' <remarks></remarks>
    Protected Shared Sub InsertPropertyValue(ByVal customProperty As [Property], _
                                             ByVal newValue As String, _
                                             ByVal newValueType As ValueType)

        'fix newValue before
        If newValue Is Nothing Then newValue = String.Empty

        ' Set the value of cell according to given type
        Select Case newValueType
            Case ValueType.Numeric
                If String.IsNullOrEmpty(newValue) Then
                    ' VTLPWSTR type should be used
                    customProperty.RemoveAllChildren()
                    customProperty.VTLPWSTR = New VariantTypes.VTLPWSTR(newValue)
                ElseIf Integer.TryParse(newValue, False) Then
                    'for int VTInt32 type should be used
                    customProperty.RemoveAllChildren()
                    customProperty.VTInt32 = New VariantTypes.VTInt32(newValue)
                Else
                    newValue = FixDouble(newValue)
                    'for double VTDouble type should be used
                    customProperty.RemoveAllChildren()
                    customProperty.VTDouble = New VariantTypes.VTDouble(newValue)
                End If
            Case ValueType.Text, ValueType.NotDefined, ValueType.Date
                ' VTLPWSTR type should be used
                customProperty.RemoveAllChildren()
                customProperty.VTLPWSTR = New VariantTypes.VTLPWSTR(newValue)
        End Select

    End Sub

#End Region

    ''' <summary>
    ''' Used to fill all standart properties 
    ''' </summary>
    ''' <param name="openXmlDocument">Document which is being merged</param>
    ''' <param name="mergeValues">New property values to be merged into given document</param>
    ''' <remarks></remarks>
    Public Shared Sub MergeStandartProperties(ByVal openXmlDocument As OpenXmlPackage, _
                                              ByVal mergeValues As Dictionary(Of String, PropertyValue))
        'check if there is something to merge
        If mergeValues Is Nothing OrElse mergeValues.Count = 0 Then Return

        Dim PackageProperties As PackageProperties = GetPackageProperties(openXmlDocument)
        If PackageProperties IsNot Nothing Then

            If mergeValues.ContainsKey(_Author) Then
                PackageProperties.Creator = mergeValues(_Author).Value
            End If
            If mergeValues.ContainsKey(_Category) Then
                PackageProperties.Category = mergeValues(_Category).Value
            End If
            If mergeValues.ContainsKey(_Comments) Then
                PackageProperties.Description = mergeValues(_Comments).Value
            End If
            If mergeValues.ContainsKey(_Subject) Then
                PackageProperties.Subject = mergeValues(_Subject).Value
            End If
            If mergeValues.ContainsKey(_Status) Then
                PackageProperties.ContentStatus = mergeValues(_Status).Value
            End If
            If mergeValues.ContainsKey(_Keywords) Then
                PackageProperties.Keywords = mergeValues(_Keywords).Value
            End If
            If mergeValues.ContainsKey(_Title) Then
                PackageProperties.Title = mergeValues(_Title).Value
            End If

        End If

        Dim ExtendedFileProperties As ExtendedFilePropertiesPart = GetExtendedFileProperties(openXmlDocument)
        If ExtendedFileProperties IsNot Nothing AndAlso ExtendedFileProperties.Properties IsNot Nothing Then

            If mergeValues.ContainsKey(_Company) Then
                If ExtendedFileProperties.Properties.Company Is Nothing Then
                    ExtendedFileProperties.Properties.Company = New ExtendedProperties.Company
                End If
                ExtendedFileProperties.Properties.Company.Text = mergeValues(_Company).Value
            End If
            If mergeValues.ContainsKey(_Manager) Then
                If ExtendedFileProperties.Properties.Manager Is Nothing Then
                    ExtendedFileProperties.Properties.Manager = New ExtendedProperties.Manager
                End If
                ExtendedFileProperties.Properties.Manager.Text = mergeValues(_Manager).Value
            End If

            ExtendedFileProperties.Properties.Save()

        End If

    End Sub

    ''' <summary>
    ''' Used to fill all custom properties (and create if they doesn't exist)
    ''' </summary>
    ''' <param name="openXmlDocument">Document which is being merged</param>
    ''' <param name="mergeValues">New property values to be merged into given document</param>
    ''' <remarks></remarks>
    Public Shared Sub MergeProperties(ByVal openXmlDocument As OpenXmlPackage, _
                                      ByVal mergeValues As Dictionary(Of String, PropertyValue))

        'check if there is something to merge
        If mergeValues Is Nothing OrElse mergeValues.Count = 0 Then Return

        'get/create custom propeties
        Dim CustomProperties As CustomFilePropertiesPart = GetCustomProperties(openXmlDocument)

        'perform merge
        For Each PropertyName As String In mergeValues.Keys
            InsertPropertyValue(GetCustomProperty(CustomProperties, PropertyName), mergeValues(PropertyName))
        Next

        'update document after merge
        If CustomProperties.Properties IsNot Nothing Then
            CustomProperties.Properties.Save()
        Else
            openXmlDocument.DeletePart(CustomProperties)
        End If
    End Sub

    ''' <summary>
    ''' Used to fix string representation of double value before inserting it into document's properties
    ''' </summary>
    ''' <param name="doubleValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FixDouble(ByVal doubleValue As String) As String
        If String.IsNullOrEmpty(doubleValue) Then Return String.Empty

        Dim Chars As Char() = doubleValue.ToCharArray
        For i As Integer = 0 To Chars.Length - 1
            If Not IsNumeric(Chars(i)) AndAlso Chars(i) <> "-"c Then Chars(i) = "."c
        Next

        Return New String(Chars)
    End Function

End Class
