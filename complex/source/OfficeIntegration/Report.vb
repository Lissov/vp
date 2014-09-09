Public Class Report

    Public Shared Sub GenerateReport(ByVal fileName As String, ByVal configuration As ModelBase.Configuration)
        If configuration Is Nothing Then Return

        'create empty xlsx document at fileName
        XlsxDocument.CreateDocument(fileName)

        'merge standard properties
        XlsxDocument.MergeStandartProperties(fileName, GetStandardProperties(configuration))

        'merge congiguration data
        XlsxDocument.CreateConfigurationSheet(fileName, configuration)

    End Sub

    Public Shared Sub SaveGridResult(ByVal fileName As String, ByVal configuration As ModelBase.Configuration, ByVal gridResult As String(,))
        If configuration Is Nothing Then Return

        'create empty xlsx document at fileName
        XlsxDocument.CreateDocument(fileName, XlsxDocument.RESULT_SHEET_NAME)

        'merge standard properties
        XlsxDocument.MergeStandartProperties(fileName, GetStandardProperties(configuration))

        'save data
        XlsxDocument.SaveGridResult(fileName, gridResult)

    End Sub


    ''' <summary>
    ''' Generates list of standart properties for report based on given configuration
    ''' </summary>
    ''' <param name="configuration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetStandardProperties(ByVal configuration As ModelBase.Configuration) As Dictionary(Of String, PropertyValue)
        Dim StandardProperties As New Dictionary(Of String, PropertyValue)

        StandardProperties.Add(OfficeDocument._Author, _
                               New PropertyValue(OfficeDocument._Author, "Modelling tool", OfficeDocument.ValueType.Text))
        StandardProperties.Add(OfficeDocument._Category, _
                       New PropertyValue(OfficeDocument._Category, "Report", OfficeDocument.ValueType.Text))
        StandardProperties.Add(OfficeDocument._Title, _
                       New PropertyValue(OfficeDocument._Title, configuration.Name, OfficeDocument.ValueType.Text))
        StandardProperties.Add(OfficeDocument._Company, _
                       New PropertyValue(OfficeDocument._Company, "Institute of Software Systems", OfficeDocument.ValueType.Text))
        StandardProperties.Add(OfficeDocument._Subject, _
               New PropertyValue(OfficeDocument._Subject, "Mathematical modelling", OfficeDocument.ValueType.Text))


        Return StandardProperties
    End Function

    ''' <summary>
    ''' Converts given text in rtf format to the simple text
    ''' </summary>
    ''' <param name="rtf"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertRtfToText(ByVal rtf As String) As String
        Dim Text As String
        Dim TempRTFBox As New System.Windows.Forms.RichTextBox
        Try
            TempRTFBox.Visible = False
            TempRTFBox.Rtf = rtf
            Text = TempRTFBox.Text
        Finally
            TempRTFBox.Dispose()
        End Try
        Return Text
    End Function


End Class
