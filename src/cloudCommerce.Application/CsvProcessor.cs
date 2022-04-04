using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;

namespace cloudCommerce.Application
{
    public class CsvProcessor: ICsvProcessor
    {
        public ConvertFile? Unconvert(string filePath)
        {
            var lines = ReadLines(new FileInfo(filePath));

            if (!lines.Any())
                return null;

            var headers = lines[0].Split(",").ToList();
            var values = lines.Skip(1).Select(line => line.Split(",").ToList()).ToList();

            return new ConvertFile(headers, values);
        }

        private static string[] ReadLines(FileInfo csvFileInfo) => csvFileInfo.Exists && csvFileInfo.Extension.Equals(".csv")
            ? File.ReadAllLines(csvFileInfo.FullName)
            : Array.Empty<string>();

    }
}
