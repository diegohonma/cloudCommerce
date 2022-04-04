namespace cloudCommerce.Domain
{
    public class ConvertFile
    {
        public ConvertFile(List<string> headers, List<List<string>> values)
        {
            Headers = headers;
            Values = values;
        }

        public List<string> Headers { get; }

        public List<List<string>> Values { get; }
    }
}
