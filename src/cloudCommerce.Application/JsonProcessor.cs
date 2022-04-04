using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;
using Newtonsoft.Json.Linq;

namespace cloudCommerce.Application
{
    public class JsonProcessor : IJsonProcessor
    {
        private readonly IFileService _fileService;

        public JsonProcessor(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void ConvertAndSave(ConvertFile convertFile, string pathToSave)
        {
            var obj = new JObject();

            convertFile.Values.ForEach(values =>
            {
                for (int i = 0; i < values.Count; i++)
                {
                    var header = convertFile.Headers[i];
                    var value = values[i];

                    if (header.Contains('_'))
                        ProcessGroupHeader(header, value, obj);
                    else
                        obj.Add(new JProperty(header, value));
                }
            });

            _fileService.SaveJson(pathToSave, obj.ToString());
        }

        private static void ProcessGroupHeader(string groupHeader, string value, JObject root)
        {
            var splitedHeader = groupHeader.Split('_', 2);
            var header = splitedHeader.First();
            var subHeader = splitedHeader.Skip(1).First();

            if (root[header] == null)
            {
                root.Add(header, new JObject
                {
                    new JProperty(subHeader, value)
                });
            }
            else
            {
                var jsonObject = (root[header] as JObject)!;
                jsonObject.Add(new JProperty(subHeader, value));
            }
        }
    }
}
