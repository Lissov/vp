Imports System.Threading
Imports ModelBase.Enums

Public Class SavedConfiguration
    Inherits ObjectBase

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

#End Region

#Region "Public methods"

    Public Function GetModelByName(ByVal modelName As String) As IModel
        Return GetModelByName(Models, modelName)
    End Function

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


#Region "Xml methods"

    Public Shared Function FromXml(ByVal xmlDocument As System.Xml.XmlDocument, ByVal fileName As String) As SavedConfiguration
        If xmlDocument Is Nothing Then Return Nothing

        Dim RootElement As System.Xml.XmlElement = xmlDocument.DocumentElement
        If RootElement Is Nothing OrElse RootElement.Name <> ROOT_NAME Then Return Nothing

        Dim ElementName As String
        ElementName = GetType(Configuration).Name().Replace("`", "")

        Return FromXml(RootElement.Item(ElementName), fileName)
    End Function

    Private Shared Function FromXml(ByVal currentElement As System.Xml.XmlElement, ByVal fileName As String) As SavedConfiguration
        If currentElement Is Nothing OrElse _
           currentElement.Name <> GetType(Configuration).Name().Replace("`", "") _
           Then
            Return Nothing
        End If

        Dim SavedConfiguration As New SavedConfiguration()
        SavedConfiguration.FileName = fileName
        SavedConfiguration.Name = IO.Path.GetFileNameWithoutExtension(fileName)

        'If currentElement.Attributes("Name") IsNot Nothing Then
        '    SavedConfiguration.Name = GetString(currentElement, "Name")
        'End If

        'load models
        Dim ModelsElement As System.Xml.XmlElement
        ModelsElement = currentElement.Item(Configuration.MODELS_XML_ELEMENT)
        If ModelsElement Is Nothing Then
            Throw New Exception("No models found")
        End If
        If ModelsElement.HasChildNodes Then
            For Each childElement As System.Xml.XmlElement In ModelsElement.ChildNodes
                Dim Model As IModel = New ModelBase
                CType(Model, ModelBase).Name = childElement.Name
                Model.ResultFromXml(childElement, Model.GetName)
                SavedConfiguration.Models.Add(Model)
            Next
        End If

        Return SavedConfiguration
    End Function

#End Region

End Class
