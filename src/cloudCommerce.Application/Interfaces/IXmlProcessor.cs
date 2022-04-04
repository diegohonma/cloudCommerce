using cloudCommerce.Domain;

namespace cloudCommerce.Application.Interfaces
{
    public interface IXmlProcessor
    {
        void ConvertAndSave(ConvertFile convertFile, string pathToSave);
    }
}
