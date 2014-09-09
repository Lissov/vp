Imports System.Collections.Generic
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.IO.Packaging
Imports System.Xml
Imports System

Public Class XlsxDocument

#Region "Const"

    Public Const Max_Worksheet_Length As Integer = 10

    Public Const CONFIGURATION_SHEET_NAME As String = "Configuration"
    Public Const RESULT_SHEET_NAME As String = "Calculating result"
    Public Const CONFIGURATION_SHEET_ID As String = "1"

#End Region

#Region "Functions for properties"

    ''' <summary>
    ''' Fills all standart properties 
    ''' </summary>
    Public Shared Sub MergeStandartProperties(ByVal fileName As String, ByVal mergeValues As Dictionary(Of String, PropertyValue))
        Using SpreadSheet As SpreadsheetDocument = SpreadsheetDocument.Open(fileName, True)

            OfficeDocument.MergeStandartProperties(SpreadSheet, mergeValues)

            SpreadSheet.Close()
            SpreadSheet.Dispose()
        End Using

        GC.Collect()
    End Sub

    ''' <summary>
    ''' Fills all custom properties (and create if they doesn't exist)
    ''' </summary>
    Public Shared Sub MergeProperties(ByVal fileName As String, _
                                      ByVal mergeValues As Dictionary(Of String, PropertyValue))

        Using SpreadSheet As SpreadsheetDocument = SpreadsheetDocument.Open(fileName, True)

            OfficeDocument.MergeProperties(SpreadSheet, mergeValues)

            SpreadSheet.Close()
            SpreadSheet.Dispose()
        End Using

        GC.Collect()
    End Sub

#End Region

#Region "Public functions"

    Public Shared Sub SaveGridResult(ByVal fileName As String, ByVal gridResult As String(,))
        If gridResult Is Nothing Then Return

        Using SpreadSheet As SpreadsheetDocument = SpreadsheetDocument.Open(fileName, True)
            For i As Integer = 0 To gridResult.GetLength(0) - 1
                For j As Integer = 0 To gridResult.GetLength(1) - 1
                    Dim ColName As String = GetColumnName(j + 1)
                    Dim RowName As Integer = i + 1

                    Dim NameVal As DefinedNameVal = GetDefinedNameVal(RESULT_SHEET_NAME, ColName, RowName)
                    InsertCellValue(SpreadSheet, NameVal, gridResult(i, j), OfficeDocument.ValueType.Text)
                Next
            Next

            SpreadSheet.Close()
            SpreadSheet.Dispose()
        End Using

        GC.Collect()
    End Sub

    Public Shared Sub CreateConfigurationSheet(ByVal fileName As String, ByVal configuration As ModelBase.Configuration)

        Using SpreadSheet As SpreadsheetDocument = SpreadsheetDocument.Open(fileName, True)

            Dim ColName As String = "B"
            Dim RowName As Integer = 2

            Dim ConfigNameTitle As DefinedNameVal = GetDefinedNameVal(CONFIGURATION_SHEET_NAME, ColName, RowName)
            InsertCellValue(SpreadSheet, ConfigNameTitle, "Configuration: ", OfficeDocument.ValueType.Text)
            RowName += 1
            Dim ConfigNameValue As DefinedNameVal = GetDefinedNameVal(CONFIGURATION_SHEET_NAME, ColName, RowName)
            InsertCellValue(SpreadSheet, ConfigNameValue, configuration.Name, OfficeDocument.ValueType.Text)
            RowName += 2

            Dim ConfigFileNameTitle As DefinedNameVal = GetDefinedNameVal(CONFIGURATION_SHEET_NAME, ColName, RowName)
            InsertCellValue(SpreadSheet, ConfigFileNameTitle, "File name: ", OfficeDocument.ValueType.Text)
            RowName += 1
            Dim ConfigFileNameValue As DefinedNameVal = GetDefinedNameVal(CONFIGURATION_SHEET_NAME, ColName, RowName)
            InsertCellValue(SpreadSheet, ConfigFileNameValue, configuration.FileName, OfficeDocument.ValueType.Text)
            RowName += 2

            If configuration.Models IsNot Nothing AndAlso configuration.Models.Count > 0 Then
                Dim ModelsTitle As DefinedNameVal = GetDefinedNameVal(CONFIGURATION_SHEET_NAME, ColName, RowName)
                InsertCellValue(SpreadSheet, ModelsTitle, "Models: ", OfficeDocument.ValueType.Text)
                RowName += 1
                For Each Model As ModelBase.IModel In configuration.Models
                    Dim ModelsVal As DefinedNameVal = GetDefinedNameVal(CONFIGURATION_SHEET_NAME, ColName, RowName)
                    InsertCellValue(SpreadSheet, ModelsVal, Model.DisplayName, OfficeDocument.ValueType.Text)
                    MergeModel(SpreadSheet, Model)
                    RowName += 1
                Next
            End If

            'Dim Description As String = Report.ConvertRtfToText(configuration.Description)
            'Dim ConfigDescriptionTitle As New DefinedNameVal("", CONFIGURATION_SHEET_NAME, "B", "6", "B", "6")
            'InsertCellValue(SpreadSheet, ConfigDescriptionTitle, "Description: ", OfficeDocument.ValueType.Text)
            'Dim ConfigDescriptionValue As New DefinedNameVal("", CONFIGURATION_SHEET_NAME, "B", "7", "B", "7")
            'InsertCellValue(SpreadSheet, ConfigDescriptionValue, Description, OfficeDocument.ValueType.Text)


            'UpdateConfigurationRowsAndColumns(SpreadSheet)

            SpreadSheet.Close()
            SpreadSheet.Dispose()
        End Using

        GC.Collect()
    End Sub

    Private Shared Function GetDefinedNameVal(ByVal sheetName As String, ByVal colName As String, ByVal rowName As Integer) As DefinedNameVal
        Return New DefinedNameVal("", sheetName, colName, CStr(rowName))
    End Function

    Private Shared Sub MergeModel(ByVal spreadSheet As SpreadsheetDocument, ByVal model As ModelBase.IModel)

        InsertWorksheet(spreadSheet, model.GetName)

        If model.GetValues.Count = 0 Then Return

        Dim Times As Double() = GetTimes(model)
        If Times Is Nothing Then Return


        Dim ColName1 As String = "B"
        Dim ColName2 As Integer = 3 '"C"
        Dim RowName As Integer = 2

        Dim ValuesGeneralTitle As DefinedNameVal = GetDefinedNameVal(model.GetName, ColName1, RowName)
        InsertCellValue(spreadSheet, ValuesGeneralTitle, "Values: ", OfficeDocument.ValueType.Text)
        RowName += 1

        Dim ValuesTimeTitle As DefinedNameVal = GetDefinedNameVal(model.GetName, ColName1, RowName)
        InsertCellValue(spreadSheet, ValuesTimeTitle, "Time ", OfficeDocument.ValueType.Text)
        RowName += 1
        Dim TimeValue As DefinedNameVal
        For Each Time As Double In Times
            TimeValue = GetDefinedNameVal(model.GetName, ColName1, RowName)
            InsertCellValue(spreadSheet, TimeValue, CStr(Math.Round(Time, 5)), OfficeDocument.ValueType.Numeric)
            RowName += 1
        Next

        Dim ValuesValueTitle As DefinedNameVal
        For Each Value As ModelBase.Value In model.GetValues
            RowName = 3
            Dim ColName3 As String = GetColumnName(ColName2)
            ValuesValueTitle = GetDefinedNameVal(model.GetName, ColName3, RowName)
            InsertCellValue(spreadSheet, ValuesValueTitle, Value.Name, OfficeDocument.ValueType.Text)
            RowName += 1
            Dim CalcValue As Double
            Dim ValueDefName As DefinedNameVal
            For i As Integer = 0 To Times.Length - 1
                CalcValue = Value.Value(i)
                If Double.IsNaN(CalcValue) OrElse Double.IsInfinity(CalcValue) Then
                    CalcValue = Double.MaxValue
                End If
                ValueDefName = GetDefinedNameVal(model.GetName, ColName3, RowName)
                InsertCellValue(spreadSheet, ValueDefName, CStr(Math.Round(CalcValue, 5)), OfficeDocument.ValueType.Numeric)
                RowName += 1
            Next
            ColName2 += 1
        Next
        RowName += 1



    End Sub

    Private Shared Function GetTimes(ByVal model As ModelBase.IModel) As Double()
        Dim Times As Double()

        Dim CurrentTime As Double = model.GetLastCalculatedTime
        'total amount of calculated steps
        Dim MaxStep As Integer = Math.Floor(CurrentTime / model.ShownStep)
        'total amount of steps to be shown
        Dim ShownSteps As Integer = Math.Floor((MaxStep * 100) / 100)
        'check whether there is something to show
        If Not ShownSteps > 0 Then Return Times
        ReDim Times(ShownSteps - 1)
        'last added step
        Dim LastStep As Integer = 0

        'each ShownNumber point should be displayed
        Dim ShownNumber As Integer = Math.Floor(MaxStep / ShownSteps)

        Dim Time As Double = 0
        Dim CurrentStep As Integer = 0
        LastStep = 0
        While Time <= CurrentTime AndAlso CurrentStep < MaxStep AndAlso LastStep < ShownSteps
            If CurrentStep Mod ShownNumber = 0 Then
                Times(LastStep) = Time
                LastStep += 1
            End If
            'update values
            Time += model.Step
            CurrentStep += 1
        End While

        Return Times
    End Function

    ''' <summary>
    ''' Inserts new worksheen with given name
    ''' </summary>
    ''' <param name="spreadSheet"></param>
    ''' <param name="sheetName"></param>
    ''' <remarks></remarks>
    Private Shared Sub InsertWorksheet(ByVal spreadSheet As SpreadsheetDocument, ByVal sheetName As String)
        ' Add a blank WorksheetPart.
        Dim newWorksheetPart As WorksheetPart = spreadSheet.WorkbookPart.AddNewPart(Of WorksheetPart)()
        newWorksheetPart.Worksheet = New Worksheet(New SheetData())
        newWorksheetPart.Worksheet.Save()

        Dim sheets As Sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild(Of Sheets)()
        Dim relationshipId As String = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart)

        ' Get a unique ID for the new worksheet.
        Dim sheetId As UInteger = 1
        If (sheets.Elements(Of Sheet).Count > 0) Then
            sheetId = sheets.Elements(Of Sheet).Select(Function(s) s.SheetId.Value).Max + 1
        End If

        ' Append the new worksheet and associate it with the workbook.
        Dim sheet As Sheet = New Sheet
        sheet.Id = relationshipId
        sheet.SheetId = sheetId
        sheet.Name = sheetName
        sheets.Append(sheet)
        spreadSheet.WorkbookPart.Workbook.Save()
    End Sub


    Private Shared Sub UpdateConfigurationRowsAndColumns(ByVal spreadSheet As SpreadsheetDocument)
        'get WorksheetPart
        Dim WorksheetPart As WorksheetPart = GetWorksheetPart(spreadSheet, CONFIGURATION_SHEET_NAME)
        If WorksheetPart Is Nothing Then Return

        'Worsheet - contains sheet, columns and some other properties
        Dim Worksheet As Worksheet = WorksheetPart.Worksheet

        'Sheet data - contains collection of rows, each row contains its cells
        Dim SheetData As SheetData = Worksheet.GetFirstChild(Of SheetData)()

        'update description row
        Dim row As Row
        If (SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value = 7).Count() <> 0) Then
            row = SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value = 7).First()
            row.CustomHeight = True
            row.Height = 150
        End If

        'update columns
        Dim Columns As Columns = Nothing
        If (Worksheet.Elements(Of Columns).Count() <> 0) Then
            Columns = Worksheet.Elements(Of Columns).First()
        End If
        'If Columns Is Nothing Then
        '    Columns = New Columns
        '    Worksheet.AppendChild(Columns)
        'End If

        Dim ColumnNumber As Integer
        Dim Column As Column = Nothing
        'update title column
        ColumnNumber = GetColumnNumber("B")
        If (Worksheet.Elements(Of Column).Where(Function(c) c.LocalName = "B").Count() <> 0) Then
            Column = Worksheet.Elements(Of Column).Where(Function(c) c.LocalName = "B").First()
            Column.CustomWidth = True
            Column.Width = 100
        Else
            'Column = New Column
            'Column.Min = New UInt32Value
            'Column.Min.Value = ColumnNumber
            'Column.CustomWidth = True
            'Column.Width = 100
            'Columns.AppendChild(Column)
        End If

        ' Save the changes
        WorksheetPart.Worksheet.Save()

    End Sub


    ''' <summary>
    ''' Used to merge all data to the document (properties and values)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PerformMerge(ByVal fileName As String, ByVal configuration As ModelBase.Configuration)

        'Using SpreadSheet As SpreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook)

        '    'MergeProperties(spreadSheet)
        '    'MergeStandartProperties(spreadSheet)

        '    'Dim WorkbookPart As WorkbookPart
        '    SpreadSheet.AddWorkbookPart()

        '    ' Add a blank WorksheetPart.
        '    Dim newWorksheetPart As WorksheetPart = SpreadSheet.WorkbookPart.AddNewPart(Of WorksheetPart)()
        '    newWorksheetPart.Worksheet = New Worksheet(New SheetData())
        '    newWorksheetPart.Worksheet.Save()

        '    Dim sheets As Sheets = SpreadSheet.WorkbookPart.Workbook.GetFirstChild(Of Sheets)()
        '    Dim relationshipId As String = SpreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart)

        '    ' Get a unique ID for the new worksheet.
        '    Dim sheetId As UInteger = 1
        '    If (sheets.Elements(Of Sheet).Count > 0) Then
        '        sheetId = sheets.Elements(Of Sheet).Select(Function(s) s.SheetId.Value).Max + 1
        '    End If

        '    ' Give the new worksheet a name.
        '    Dim sheetName As String = ("Sheet" + sheetId.ToString())

        '    ' Append the new worksheet and associate it with the workbook.
        '    Dim sheet As Sheet = New Sheet
        '    sheet.Id = relationshipId
        '    sheet.SheetId = sheetId
        '    sheet.Name = sheetName
        '    sheets.Append(sheet)
        '    SpreadSheet.WorkbookPart.Workbook.Save()


        '    SpreadSheet.Close()
        '    SpreadSheet.Dispose()
        'End Using

        GC.Collect()

    End Sub


    Public Shared Sub CreateDocument(ByVal fileName As String, Optional ByVal sheetName As String = CONFIGURATION_SHEET_NAME)

        Using SpreadSheet As SpreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook)

            'add a workbook to the package
            Dim WorkbookPart As WorkbookPart = SpreadSheet.AddWorkbookPart()

            'add a worksheet to the workbook
            Dim WorksheetPart As WorksheetPart = SpreadSheet.WorkbookPart.AddNewPart(Of WorksheetPart)()

            'generate the id for the worksheet
            Dim RelId As String = WorkbookPart.GetIdOfPart(WorksheetPart)

            'This should handle the relationship thing of the package with one workbook and one worksheet only.
            'We now need to create the real .xml document for them.
            Dim NameTable As NameTable = New NameTable()
            Dim NamespaceManager As XmlNamespaceManager = New XmlNamespaceManager(NameTable)
            Dim XmlWorkbook As XmlDocument = New XmlDocument(NameTable)

            'create a xml document for workbook.xml
            'I believe this is what every .xml must have
            XmlWorkbook.AppendChild(XmlWorkbook.CreateXmlDeclaration("1.0", "UTF-8", "yes"))

            'add the root element, 
            'I Believe there are better ways to handle namespace
            'if you know, please tell me
            Dim ElementWorkbook As XmlElement = XmlWorkbook.CreateElement("workbook")
            ElementWorkbook.SetAttribute("xmlns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")
            ElementWorkbook.SetAttribute("xmlns:r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships")
            XmlWorkbook.AppendChild(ElementWorkbook)
            ElementWorkbook.AppendChild(XmlWorkbook.CreateElement("sheets"))
            Dim ElementSheet As XmlElement = XmlWorkbook.CreateElement("sheet")
            ElementSheet.SetAttribute("name", sheetName)
            ElementSheet.SetAttribute("sheetId", CONFIGURATION_SHEET_ID)
            ElementSheet.SetAttribute("id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships", RelId)
            ElementWorkbook.FirstChild.AppendChild(ElementSheet)

            'write this .xml to workbookpart
            Dim WorkbookStream As Stream = WorkbookPart.GetStream()
            XmlWorkbook.Save(WorkbookStream)
            'do the same for worksheet .xml
            Dim XmlWorksheet As XmlDocument = New XmlDocument()
            XmlWorksheet.AppendChild(XmlWorksheet.CreateXmlDeclaration("1.0", "UTF-8", "yes"))
            Dim ElementWorksheet As XmlElement = XmlWorksheet.CreateElement("worksheet")
            ElementWorksheet.SetAttribute("xmlns", "http://schemas.openxmlformats.org/spreadsheetml/2006/main")
            ElementWorksheet.SetAttribute("xmlns:r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships")
            XmlWorksheet.AppendChild(ElementWorksheet)
            Dim ElementSheetData As XmlElement = XmlWorksheet.CreateElement("sheetData")
            ElementWorksheet.AppendChild(ElementSheetData)
            Dim WorksheetStream As Stream = WorksheetPart.GetStream()
            XmlWorksheet.Save(WorksheetStream)
            'close it
            SpreadSheet.Close()
            SpreadSheet.Dispose()
        End Using

        GC.Collect()
    End Sub

#End Region

#Region "Private functions"

    ''' <summary>
    ''' Merge all values in cells
    ''' </summary>
    ''' <param name="spreadSheet">Document which is being merged</param>
    ''' <remarks>returns True if some values were changed</remarks>
    Private Shared Function MergeValues(ByVal spreadSheet As SpreadsheetDocument) As Boolean

        Dim Result As Boolean = False

        'Dim DefinedNamesList As List(Of DefinedNameVal)
        'DefinedNamesList = BuildDefinedNamesTable(spreadSheet.WorkbookPart.Workbook)

        'For Each DefinedName As DefinedNameVal In DefinedNamesList

        '    'Merge MergeFields if needed
        '    If OpenXmlPropertySet.MergeNames IsNot Nothing AndAlso OpenXmlPropertySet.MergeNames.Contains(DefinedName.Key.ToLower) Then
        '        Dim Index As Integer = System.Array.IndexOf(OpenXmlPropertySet.MergeNames, DefinedName.Key.ToLower)
        '        InsertCellValue(spreadSheet, DefinedName, OpenXmlPropertySet.MergeValues(Index), OpenXmlPropertySet.MergeValueTypes(Index))
        '        Result = True
        '    End If

        '    'Merge CustomFields if needed
        '    If OpenXmlPropertySet.CustomPropertyNames IsNot Nothing Then
        '        ' MsOfficeProfile allows user to define names with spaces
        '        ' But user defined names in Exel can not contain spaces
        '        ' So UDN in exel is equal to MsOffice profile name without spaces
        '        Dim Index As Integer = -1
        '        For i As Integer = 0 To OpenXmlPropertySet.CustomPropertyNames.Length - 1
        '            If Not String.IsNullOrEmpty(OpenXmlPropertySet.CustomPropertyNames(i)) AndAlso OpenXmlPropertySet.CustomPropertyNames(i).Replace(" ", "").ToLower = DefinedName.Key.ToLower Then
        '                Index = i
        '                Exit For
        '            End If
        '        Next
        '        If Index > -1 Then
        '            InsertCellValue(spreadSheet, DefinedName, OpenXmlPropertySet.CustomPropertyValues(Index), OpenXmlPropertySet.CustomPropertyValueTypes(Index))
        '            Result = True
        '        End If
        '    End If

        '    'Merge StandartFields if needed
        '    If OpenXmlPropertySet.StandardPropertyNames IsNot Nothing AndAlso OpenXmlPropertySet.StandardPropertyNames.Contains(DefinedName.Key) Then
        '        Dim Index As Integer = System.Array.IndexOf(OpenXmlPropertySet.StandardPropertyNames, DefinedName.Key)
        '        InsertCellValue(spreadSheet, DefinedName, OpenXmlPropertySet.StandardPropertyValues(Index), OpenXmlPropertySet.StandardPropertyValueTypes(Index))
        '        Result = True
        '    End If

        'Next

        Return Result
    End Function

    ''' <summary>
    ''' Force recalculating for all formulas
    ''' </summary>
    ''' <param name="spreadSheet"></param>
    ''' <remarks></remarks>
    Private Shared Sub FixFormulas(ByVal spreadSheet As SpreadsheetDocument)
        If spreadSheet.WorkbookPart Is Nothing Then Return

        Dim WorksheetParts As IEnumerable(Of WorksheetPart)
        Dim WorksheetPart As WorksheetPart

        Try
            WorksheetParts = spreadSheet.WorkbookPart.WorksheetParts
            If WorksheetParts Is Nothing OrElse WorksheetParts.Count = 0 Then Return

            For Each WorksheetPart In WorksheetParts
                Dim ReadStream As System.IO.Stream
                Dim WriteStream As System.IO.Stream
                Dim Document As String = String.Empty

                Try
                    ReadStream = Nothing
                    ReadStream = WorksheetPart.GetStream(IO.FileMode.Open, IO.FileAccess.Read)

                    'check file size before reading (in bytes)
                    If Max_Worksheet_Length > 0 AndAlso _
                       ReadStream.Length > Max_Worksheet_Length * 1024 * 1024 _
                       Then
                        Continue For
                    End If

                    Using StreamReader As StreamReader = New StreamReader(ReadStream)
                        Document = StreamReader.ReadToEnd
                        StreamReader.Close()
                    End Using
                    RemoveFormulasCache(Document)
                    If ReadStream IsNot Nothing Then
                        ReadStream.Close()
                        ReadStream.Dispose()
                    End If

                    WriteStream = Nothing
                    WriteStream = WorksheetPart.GetStream(IO.FileMode.Open, IO.FileAccess.Write)
                    WriteStream.SetLength(Document.Length)
                    Using sw As StreamWriter = New StreamWriter(WriteStream)
                        sw.AutoFlush = False
                        sw.WriteLine(Document)
                        sw.Close()
                    End Using
                    If WriteStream IsNot Nothing Then
                        WriteStream.Close()
                        WriteStream.Dispose()
                    End If


                Catch
                Finally
                    Document = String.Empty
                    WorksheetPart = Nothing
                    GC.Collect()
                End Try

            Next

        Catch
        Finally
            WorksheetParts = Nothing
            GC.Collect()
        End Try


    End Sub

    ''' <summary>
    ''' Remove cashed values from cells with formulas
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Private Shared Sub RemoveFormulasCache(ByRef text As String)
        Dim Regex As Regex
        Dim RegExFormula As String
        Dim Index As Integer

        'in text for key could be:
        '</f> <v>55</v> where 55 - some value which could be different
        ' or </x:f><x:v>3</x:v> where x - name of the namespace
        'old value (in example 55) must be removed
        Dim FormulaKey As String = "f>"
        Dim ValueOpenKey As String = "<((\w)*:)?v>"
        Dim ValueCloseKey As String = "</((\w)*:)?v>"

        RegExFormula = FormulaKey & ValueOpenKey & "[^<>]*" & ValueCloseKey
        Try
            Regex = New Regex(RegExFormula, RegexOptions.IgnoreCase)

            'use match collection in case there are more than one merge field
            Dim MatchCollection As MatchCollection = Regex.Matches(text)
            If MatchCollection Is Nothing OrElse MatchCollection.Count = 0 Then Return
            For Each Match As Match In MatchCollection
                If Match.Length > 0 Then
                    'remove value but </f> should stay in the document
                    Index = text.IndexOf(Match.Value) + FormulaKey.Length
                    text = text.Substring(0, Index) & text.Substring(Index + Match.Value.Length - FormulaKey.Length)
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub

#End Region

#Region "Functions for Spreadsheet"

    ''' <summary>
    ''' Gets list of user defined names from given workbook
    ''' </summary>
    ''' <param name="workbook"></param>
    ''' <returns></returns>
    ''' <remarks>Never returns Nothing</remarks>
    Public Shared Function BuildDefinedNamesTable(ByVal workbook As Workbook) _
                                                        As List(Of DefinedNameVal)
        If workbook Is Nothing OrElse workbook.ChildElements Is Nothing OrElse workbook.ChildElements.Count = 0 Then Return New List(Of DefinedNameVal)
        Dim DefinedNames As DefinedNames = workbook.GetFirstChild(Of DefinedNames)()
        If DefinedNames Is Nothing Then Return New List(Of DefinedNameVal)

        Dim ResultList As New List(Of DefinedNameVal)

        For Each DefinedName As DefinedName In DefinedNames
            'Parse defined name string 
            'inner text like Sheet1!$A$1 in simple case or with more rows like 'Sales Order'!$A$22:$G$36 
            Dim Key As String = DefinedName.Name
            Dim Reference As String = DefinedName.InnerText
            Dim SheetName As String = Reference.Split("!")(0).Trim("'")
            Dim Range As String = Reference.Split("!")(1)
            Dim RangeArray() As String = Range.Split("$")
            If RangeArray.Length < 3 Then Continue For
            Dim StartColumn As String = RangeArray(1)
            Dim StartRow As String = RangeArray(2).TrimEnd(":")
            Dim EndColumn As String = Nothing
            Dim EndRow As String = Nothing
            If RangeArray.Length > 3 Then
                EndColumn = RangeArray(3)
                EndRow = RangeArray(4)
            End If
            ResultList.Add(New DefinedNameVal(Key, SheetName, StartColumn, StartRow, EndColumn, EndRow))
        Next

        Return ResultList
    End Function

    ''' <summary>
    ''' Gets cell from given worksheet part by its name.
    ''' </summary>
    ''' <param name="worksheetPart">WorksheetPart in which cell will be serched</param>
    ''' <param name="cellName">Full name of the cell</param>
    ''' <param name="createCellIfNotExist">If true and cell does not exist it should be created</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCell(ByVal worksheetPart As WorksheetPart, _
                                   ByVal cellName As DefinedNameVal, _
                                   ByVal createCellIfNotExist As Boolean) _
                                        As Cell
        If cellName Is Nothing Then Return Nothing

        Dim CellReference As String = cellName.StartColumn & cellName.StartRow
        'get exact cell based on reference 
        Dim Cell As Cell

        Try
            Cell = worksheetPart.Worksheet.Descendants(Of Cell).Where(Function(c, b) CellReference.Equals(c.CellReference)).First
        Catch
            Cell = Nothing
        End Try

        If Cell Is Nothing AndAlso createCellIfNotExist Then
            'create new cell and then add it to the worksheet
            Cell = InsertCellInWorksheet(cellName.StartColumn, CInt(cellName.StartRow), worksheetPart)
        End If

        Return Cell
    End Function

    ''' <summary>
    ''' Inserts a cell into the worksheet. 
    ''' </summary>
    ''' <param name="columnName">Name of the column into which cell must be inserted</param>
    ''' <param name="rowIndex">Index of the row into which cell must be inserted</param>
    ''' <param name="worksheetPart">WorksheetPart into which cell must be inserted</param>
    ''' <returns></returns>
    ''' <remarks>If row doesn't exist it will be created</remarks>
    Private Shared Function InsertCellInWorksheet(ByVal columnName As String, _
                                                  ByVal rowIndex As UInteger, _
                                                  ByVal worksheetPart As WorksheetPart) _
                                                    As Cell

        'Worsheet - contains sheet, columns and some other properties
        Dim Worksheet As Worksheet = worksheetPart.Worksheet

        'Sheet data - contains collection of rows, each row contains its cells
        Dim SheetData As SheetData = Worksheet.GetFirstChild(Of SheetData)()

        'Cellreference - used to adress cell in the row
        Dim CellReference As String = (columnName + rowIndex.ToString())

        ' If the worksheet does not contain a row with the specified row index, insert one.
        Dim row As Row
        If (SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value = rowIndex).Count() <> 0) Then
            row = SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value = rowIndex).First()
        Else
            row = New Row()
            row.RowIndex = rowIndex
            ' 1 - try to find first row with bigger index and insert row before it
            ' 2 - try to find last row with smaller index and insert row after it
            ' 3 - it is the only row in this Sheet so just insert it
            If (SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value > rowIndex).Count() <> 0) Then
                SheetData.InsertBefore(row, SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value > rowIndex).First())
            ElseIf (SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value < rowIndex).Count() <> 0) Then
                SheetData.InsertAfter(row, SheetData.Elements(Of Row).Where(Function(r) r.RowIndex.Value < rowIndex).Last)
            Else
                SheetData.Append(row)
            End If
        End If

        Dim newCell As Cell = New Cell
        newCell.CellReference = CellReference

        'set cell's style
        'if row has style cell MUST have this style
        'if row doesn't have style but column has cell MUST have column's style
        If row.StyleIndex IsNot Nothing Then
            newCell.StyleIndex = row.StyleIndex
        Else
            Dim Columns As Columns = Nothing
            If (Worksheet.Elements(Of Columns).Count() <> 0) Then
                Columns = Worksheet.Elements(Of Columns).First()
            End If
            If Columns IsNot Nothing Then
                'get column from collection by its number
                'in collection: column.min = column.max = columnNumber
                Dim ColumnNumber As Integer = GetColumnNumber(columnName)
                Dim Column As Column = Nothing
                For Each WorksheetColumn As Column In Columns
                    If WorksheetColumn.Min.Value = ColumnNumber Then
                        Column = WorksheetColumn
                        Exit For
                    End If
                Next
                If Column IsNot Nothing Then
                    newCell.StyleIndex = Column.Style
                End If
            End If
        End If

        'Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
        'Columns are numbered like ...Y,Z, AA...
        'and AA < Z so  DO NOT USE
        'String.Compare(cell.CellReference.Value, cellReference, True) > 0)
        'to compare cell's references
        '1 - compare only columns
        '2 - column name which is longer is always bigger
        Dim refCell As Cell = Nothing
        For Each cell As Cell In row.Elements(Of Cell)()
            Dim CellColumn As String = cell.CellReference.Value.Trim(rowIndex.ToString.ToCharArray)
            If CellColumn.Length >= columnName.Length AndAlso CellColumn > columnName Then
                refCell = cell
                Exit For
            End If
        Next
        row.InsertBefore(newCell, refCell)

        Return newCell

    End Function

    ''' <summary>
    ''' Gets column number by its name
    ''' </summary>
    ''' <param name="columnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnNumber(ByVal columnName As String) As Integer
        Dim Result As Integer

        'column numbers in exel:
        ' A - 1, B - 2, ..., AA - 27, ...
        Dim CharArray As Char() = columnName.ToLower.ToCharArray
        For Each NamePart As Char In CharArray
            Result += Asc(NamePart) - Asc("a"c) + 1
        Next

        Return Result
    End Function

    ''' <summary>
    ''' Gets column name by its number
    ''' </summary>
    ''' <param name="columnNumber">First is 1!</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetColumnName(ByVal columnNumber As Integer) As String
        Dim Result As String = String.Empty

        Dim MaxNumber As Integer = 26
        Dim ACode As Integer = Asc("a"c)
        If columnNumber <= MaxNumber Then
            Result = Chr(columnNumber - 1 + ACode)
        Else
            Dim Total As Integer = columnNumber \ MaxNumber
            Dim Rest As Integer = columnNumber - Total * MaxNumber
            If Total > MaxNumber Then
                Dim Code As Integer = Total Mod MaxNumber
                Total = Total \ MaxNumber
                Result = Chr(Code - 1 + ACode) & Result
            End If
            Result = Chr(Total - 1 + ACode) & Result
            Result &= Chr(Rest - 1 + ACode)
        End If

        Return Result
    End Function

    ''' <summary>
    ''' Gets worksheet part which contains cell with given name
    ''' </summary>
    ''' <param name="spreadSheet">Document which is being merged</param>
    ''' <param name="worksheetName">Name of the Sheet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetWorksheetPart(ByVal spreadSheet As SpreadsheetDocument, _
                                     ByVal worksheetName As String) _
                                            As WorksheetPart
        If spreadSheet.WorkbookPart Is Nothing OrElse _
           spreadSheet.WorkbookPart.WorksheetParts Is Nothing OrElse _
           spreadSheet.WorkbookPart.WorksheetParts.Count = 0 _
           Then Return Nothing

        'get worksheet based on defined name 
        Dim WorksheetPart As WorksheetPart = Nothing
        Dim WorksheetPartId As String
        Try
            WorksheetPartId = spreadSheet.WorkbookPart.Workbook.Descendants(Of Sheet)().Where(Function(s, b) worksheetName.Equals(s.Name)).First.Id
            WorksheetPart = spreadSheet.WorkbookPart.GetPartById(WorksheetPartId)

            'check file size
            Dim ReadStream As System.IO.Stream
            ReadStream = WorksheetPart.GetStream(IO.FileMode.Open, IO.FileAccess.Read)
            If Max_Worksheet_Length > 0 AndAlso _
               ReadStream.Length > Max_Worksheet_Length * 1024 * 1024 _
               Then
                Return Nothing
            End If

        Catch
        End Try

        Return WorksheetPart
    End Function

    ''' <summary>
    ''' Inserts new value into the cell with given name
    ''' </summary>
    ''' <param name="spreadSheet">Document which is being merged</param>
    ''' <param name="cellName">Name of the cell which must be merged</param>
    ''' <param name="newValue">New value which should be inserted</param>
    ''' <param name="newValueType">Type of new value</param>
    ''' <remarks></remarks>
    Public Shared Sub InsertCellValue(ByVal spreadSheet As SpreadsheetDocument, _
                                      ByVal cellName As DefinedNameVal, _
                                      ByVal newValue As String, _
                                      ByVal newValueType As OfficeDocument.ValueType)

        'get WorksheetPart
        Dim WorksheetPart As WorksheetPart = GetWorksheetPart(spreadSheet, cellName.SheetName)
        If WorksheetPart Is Nothing Then Return
        ' Get cell or create it if doesn't exist
        'BUT empty cell should not be created
        Dim Cell As Cell = GetCell(WorksheetPart, cellName, Not String.IsNullOrEmpty(newValue))

        If Cell IsNot Nothing Then

            If String.IsNullOrEmpty(newValue) Then
                'value from empty cell should be completely removed!!!
                Cell.CellValue = Nothing
                Cell.DataType = Nothing
            Else
                ' Set the value of cell according to given type
                Select Case newValueType
                    Case OfficeDocument.ValueType.Numeric
                        If Integer.TryParse(newValue, False) Then
                            'for int numeric style should be used
                            Cell.CellValue = New CellValue(newValue)
                            Cell.DataType = Nothing
                        Else
                            'for double no style should be set
                            newValue = OfficeDocument.FixDouble(newValue)
                            Cell.CellValue = New CellValue(newValue)
                            Cell.DataType = Nothing
                        End If
                    Case OfficeDocument.ValueType.Text, OfficeDocument.ValueType.Date, OfficeDocument.ValueType.NotDefined
                        ' Insert the text into the SharedStringTablePart.
                        Dim index As Integer = InsertSharedStringItem(newValue, spreadSheet)
                        ' Set the value of cell 
                        Cell.CellValue = New CellValue(index.ToString)
                        Cell.DataType = New EnumValue(Of CellValues)(CellValues.SharedString)
                End Select
            End If

        End If

        ' Save the changes
        WorksheetPart.Worksheet.Save()

    End Sub

    ''' <summary>
    ''' Creates a SharedStringItem with the specified text and inserts it into the SharedStringTablePart. 
    ''' If the item already exists, returns its index.
    ''' </summary>
    ''' <param name="text">Text to insert into  shared strings</param>
    ''' <param name="spreadSheet">Document which is being merged</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertSharedStringItem(ByVal text As String, ByVal spreadSheet As SpreadsheetDocument) As Integer

        ' Get the SharedStringTablePart. If it does not exist, create a new one.
        Dim sharedStringPart As SharedStringTablePart
        If (spreadSheet.WorkbookPart.GetPartsOfType(Of SharedStringTablePart).Count() > 0) Then
            sharedStringPart = spreadSheet.WorkbookPart.GetPartsOfType(Of SharedStringTablePart).First()
        Else
            sharedStringPart = spreadSheet.WorkbookPart.AddNewPart(Of SharedStringTablePart)()
        End If

        ' If the part does not contain a SharedStringTable, create one.
        If (sharedStringPart.SharedStringTable Is Nothing) Then
            sharedStringPart.SharedStringTable = New SharedStringTable
        End If

        Dim InsertedStringIndex As Integer = 0

        ' Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
        For Each item As SharedStringItem In sharedStringPart.SharedStringTable.Elements(Of SharedStringItem)()
            If (item.InnerText = text) Then
                Return InsertedStringIndex
            End If
            InsertedStringIndex += 1
        Next

        ' The text does not exist in the part. Create the SharedStringItem and return its index.
        sharedStringPart.SharedStringTable.AppendChild(New SharedStringItem(New DocumentFormat.OpenXml.Spreadsheet.Text(text)))
        sharedStringPart.SharedStringTable.Save()

        Return InsertedStringIndex
    End Function

#End Region

End Class
