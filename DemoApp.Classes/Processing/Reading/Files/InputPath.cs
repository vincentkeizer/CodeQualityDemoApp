using System;
using System.Collections.Generic;
using System.Text;
using DemoApp.Infra.Assertions;

namespace DemoApp.Processing.Reading.Files
{
    public class InputPath
    {
        public InputPath(string basePath, string inFolder)
        {
            Assert.IsNotNullOrWhitespace(basePath, nameof(basePath));

            BasePath = basePath;
            InFolder = inFolder;
        }

        public string InFolder { get; }

        public string BasePath { get; }

        public bool HasInFolder() => !string.IsNullOrWhiteSpace(InFolder);
    }
}
