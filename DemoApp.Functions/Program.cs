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
            //setup our DI
            IServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();

            var logDirectory = config.GetValue<string>("logPath");
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logDirectory)
                .CreateLogger();

            services.AddLogging(configure => configure.AddSerilog());
            services.AddSingleton(config);
            services.AddTransient<IFileParser, FileParser>();

            var serviceProvider = services.BuildServiceProvider();

            var fileParser = serviceProvider.GetService<IFileParser>();
            fileParser.ParseFiles(config.GetValue<string>("inFolder"), 
                                  config.GetValue<string>("basePath"), 
                                  config.GetValue<string>("type"),
                                  config.GetValue<string>("outFolder"));
        }
    }
}
