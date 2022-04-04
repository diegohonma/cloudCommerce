using cloudCommerce.Domain;

namespace cloudCommerce.Application.Interfaces
{
    public interface ICsvProcessor
    {
        ConvertFile? Unconvert(string filePath);
    }
}
