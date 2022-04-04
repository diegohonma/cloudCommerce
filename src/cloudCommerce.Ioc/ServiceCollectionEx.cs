using cloudCommerce.Application;
using cloudCommerce.Application.Interfaces;
using CSVJsonParser.Application;
using Microsoft.Extensions.DependencyInjection;

namespace cloudCommerce.Ioc
{
    public static class ServiceCollectionEx
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterHandlers(services);
            RegisterProcessors(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            services.AddScoped<IHandlerParser, HandlerParser>();
        }

        private static void RegisterProcessors(IServiceCollection services)
        {
            services.AddScoped<ICsvProcessor, CsvProcessor>();
            services.AddScoped<IJsonProcessor, JsonProcessor>();
            services.AddScoped<IXmlProcessor, XmlProcessor>();
        }
    }
}
