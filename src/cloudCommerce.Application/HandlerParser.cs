using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;
using cloudCommerce.Domain.Constants;

namespace CSVJsonParser.Application
{
    public class HandlerParser : IHandlerParser
    {
        private readonly IJsonProcessor _jsonProcessor;
        private readonly IXmlProcessor _xmlProcessor;
        private readonly ICsvProcessor _csvProcessor;

        public HandlerParser(IJsonProcessor jsonProcessor, IXmlProcessor xmlProcessor, ICsvProcessor csvProcessor)
        {
            _jsonProcessor = jsonProcessor;
            _xmlProcessor = xmlProcessor;
            _csvProcessor = csvProcessor;
        }

        public bool Parse(string formatFrom, string filePathFrom, string formatTo, string filePathTo)
        {
            try
            {
                var convertFile = default(ConvertFile);

                switch (formatFrom)
                {
                    case Constants.FORMAT_CSV:
                        convertFile = _csvProcessor.Unconvert(filePathFrom);
                        break;
                }

                if (convertFile == null)
                    return false;


                switch (formatTo)
                {
                    case Constants.FORMAT_JSON:
                        _jsonProcessor.ConvertAndSave(convertFile, filePathTo);
                        break;

                    case Constants.FORMAT_XML:
                        _xmlProcessor.ConvertAndSave(convertFile, filePathTo);
                        break;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
