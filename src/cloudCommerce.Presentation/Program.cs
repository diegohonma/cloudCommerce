using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain.Constants;
using cloudCommerce.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Sharprompt;


var formatFrom = Prompt.Select("Format from:", new[] { Constants.FORMAT_CSV });
var filePathFrom = PathFromFileFrom(formatFrom);

var formatTo = Prompt.Select("Format to:", new[] { Constants.FORMAT_XML, Constants.FORMAT_JSON });
var filePathTo = Path.Combine(
    new FileInfo(filePathFrom).Directory.FullName, $"{ Prompt.Input<string>($"Inform name from file {formatTo}") }.{formatTo}");

Console.WriteLine($"Converted file is going to be save in: {filePathTo}");

var serviceCollection = new ServiceCollection();
serviceCollection.RegisterDependencies();
var serviceProvider = serviceCollection.BuildServiceProvider();

var handler = serviceProvider.GetService<IHandlerParser>()!;
var isSuccess = handler.Parse(formatFrom, filePathFrom, formatTo, filePathTo);

Console.WriteLine(isSuccess
    ? $"File format {formatTo} generated with success"
    : $"Could not generated file format {formatTo}");

Console.WriteLine("Press any key to exit...");

Console.ReadKey();

static string PathFromFileFrom(string formatFrom)
{
    var path = Prompt.Input<string>($"Inform path from file {formatFrom}");
    var extension = $".{formatFrom}";

    while (!new FileInfo(path).Extension.Equals(extension, StringComparison.OrdinalIgnoreCase))
    {
        var directoryInfo = new DirectoryInfo(path);
        path = Path.Combine(path,
            Prompt.Select(
                "Select a file or directory:",
                directoryInfo.GetDirectories().Select(x => x.Name).ToArray().Concat(
                    directoryInfo.GetFiles().Where(x => x.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase)).Select(x => x.Name)))
            );
    }

    return path;
}