using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using DemoApp.Directories.Paths;
using DemoApp.Directories.Reading;
using DemoApp.Files.Reading;
using DemoApp.Files.Writing;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = CreateServiceCollection();
            var config = ConfigureConfig();
            ConfigureLogging(config);

            services.AddSingleton(config);
            services.AddLogging(configure => configure.AddSerilog());
            services.AddTransient<IDirectoryPathCombiner, DirectoryPathCombiner>();
            services.AddTransient<IDirectoryReader, DirectoryReader>();
            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<IFileWriter, FileWriter>();
            services.AddTransient<IFileParser, FileParser>();

            var serviceProvider = services.BuildServiceProvider();

            var fileParser = serviceProvider.GetService<IFileParser>();
            fileParser.ParseFiles(config.GetValue<string>("inFolder"), 
                                  config.GetValue<string>("basePath"), 
                                  config.GetValue<string>("type"),
                                  config.GetValue<string>("outFolder"));
        }

        private static IConfigurationRoot ConfigureConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            return config;
        }

        private static void ConfigureLogging(IConfigurationRoot config)
        {
            var logDirectory = config.GetValue<string>("logPath");
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logDirectory)
                .CreateLogger();
        }

        private static IServiceCollection CreateServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();
            return services;
        }
    }
}
