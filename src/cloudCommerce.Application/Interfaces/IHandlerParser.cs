namespace cloudCommerce.Application.Interfaces
{
    public interface IHandlerParser
    {
        bool Parse(string formatFrom, string filePathFrom, string formatTo, string filePathTo);
    }
}
