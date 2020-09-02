using System;
using System.Collections.Generic;
using System.Text;
using DemoApp.Infra.Assertions;

namespace DemoApp.Processing.Reading.Files
{
    public class FileToProcess
    {
        public FileToProcess(string fullPath)
        {
            Assert.IsNotNullOrWhitespace(fullPath, nameof(fullPath));

            FullPath = fullPath;
        }

        public string FullPath { get;  }
    }
}
