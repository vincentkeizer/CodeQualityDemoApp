using System;
using System.Collections.Generic;
using System.Linq;
using DemoApp.Infra.Assertions;
using DemoApp.Infra.Files.Writing;
using DemoApp.Infra.Paths;
using DemoApp.Processing.Validating;

namespace DemoApp.Processing.Writing
{
    public class ValidFileContentsWriter : IValidFileContentsWriter
    {
        private readonly IFileWriter _fileWriter;
        private readonly IPathCombiner _pathCombiner;

        public ValidFileContentsWriter(IFileWriter fileWriter, 
                                IPathCombiner pathCombiner)
        {
            _fileWriter = fileWriter;
            _pathCombiner = pathCombiner;
        }

        public void WriteValidFileContents(OutputPath outputPath, IEnumerable<ValidFileContents> validFiles)
        {
            Assert.IsNotNull(outputPath, nameof(outputPath));
            Assert.IsNotNull(validFiles, nameof(validFiles));

            var validFilesList = validFiles.ToList();
            for (var i = 0; i < validFilesList.Count; i++)
            {
                var absoluteOutPath = outputPath.BasePath;
                if (outputPath.HasOutFolder())
                {
                    absoluteOutPath = _pathCombiner.Combine(outputPath.BasePath, outputPath.OutFolder);
                }

                _fileWriter.WriteFileToFolder($"{i + 1}.txt", validFilesList[i].FileContents.Contents, absoluteOutPath);
            }
        }
    }
}