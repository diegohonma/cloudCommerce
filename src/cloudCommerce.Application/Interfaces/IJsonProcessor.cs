using cloudCommerce.Domain;

namespace cloudCommerce.Application.Interfaces
{
    public interface IJsonProcessor
    {
        void ConvertAndSave(ConvertFile convertFile, string pathToSave);
    }
}
