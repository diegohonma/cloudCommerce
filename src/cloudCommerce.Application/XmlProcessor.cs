using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;
using System.Xml;

namespace cloudCommerce.Application
{
    public class XmlProcessor : IXmlProcessor
    {
        private readonly IFileService _fileService;

        public XmlProcessor(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void ConvertAndSave(ConvertFile convertFile, string pathToSave)
        {
            var xmlDocument = new XmlDocument();
            var root = xmlDocument.CreateElement("root");

            convertFile.Values.ForEach(values =>
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var header = convertFile.Headers[i];
                    var value = values[i];

                    if (header.Contains('_'))
                    {
                        ProcessGroupHeader(header, value, xmlDocument, root);
                    }
                    else
                    {
                        var newXmlElement = xmlDocument.CreateElement(header);
                        newXmlElement.InnerText = value;
                        root.AppendChild(newXmlElement);
                    }
                }
            });

            xmlDocument.AppendChild(root);
            _fileService.SaveXml(pathToSave, xmlDocument);
        }

        private static void ProcessGroupHeader(string groupHeader, string value, XmlDocument xmlDocument, XmlElement root)
        {
            var splitedHeader = groupHeader.Split('_', 2);
            var header = splitedHeader.First();
            var subHeader = splitedHeader.Skip(1).First();
                        
            if (root[header] == null)
            {
                var newXmlElement = xmlDocument.CreateElement(header);
                var xmlNode = xmlDocument.CreateElement(subHeader);
                xmlNode.InnerText = value;
                newXmlElement.AppendChild(xmlNode);
                root.AppendChild(newXmlElement);
            }
            else
            {
                var xmlNode = xmlDocument.CreateElement(subHeader);
                xmlNode.InnerText = value;
                root[header]!.AppendChild(xmlNode);
            }
        }
    }
}
