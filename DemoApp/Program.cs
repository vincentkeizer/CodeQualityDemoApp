using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

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
