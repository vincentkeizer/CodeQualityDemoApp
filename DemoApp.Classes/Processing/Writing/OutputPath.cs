using System;
using System.Collections.Generic;
using System.Text;
using DemoApp.Infra.Assertions;

namespace DemoApp.Processing.Writing
{
    public class OutputPath
    {
        public OutputPath(string basePath, string outFolder)
        {
            Assert.IsNotNullOrWhitespace(basePath, nameof(basePath));

            BasePath = basePath;
            OutFolder = outFolder;
        }

        public string OutFolder { get; }

        public string BasePath { get; }

        public bool HasOutFolder() => !string.IsNullOrWhiteSpace(OutFolder);
    }
}
