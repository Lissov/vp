Public Interface IXmlObject

    Function ToXml(ByVal parentElement As System.Xml.XmlElement, ByVal name As String) As System.Xml.XmlElement
    Function ToXml(ByVal parentElement As System.Xml.XmlElement) As System.Xml.XmlElement
    Function FromXml(ByVal xmlElement As System.Xml.XmlElement, ByVal name As String) As Object
    Function FromXml(ByVal xmlElement As System.Xml.XmlElement) As Object

End Interface
