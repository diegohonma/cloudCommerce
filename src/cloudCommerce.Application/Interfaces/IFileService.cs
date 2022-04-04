using System.Xml;

namespace cloudCommerce.Application.Interfaces
{
    public interface IFileService
    {
        void SaveJson(string jsonFilePath, string content);

        void SaveXml(string xmlFilePath, XmlDocument xmlDocument);
    }
}
