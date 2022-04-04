using cloudCommerce.Application.Interfaces;
using System.Xml;

namespace cloudCommerce.Application
{
    public class FileService : IFileService
    {
        public void SaveJson(string jsonFilePath, string content)  => File.WriteAllText(jsonFilePath, content);

        public void SaveXml(string xmlFilePath, XmlDocument xmlDocument) => xmlDocument.Save(xmlFilePath);
    }
}
