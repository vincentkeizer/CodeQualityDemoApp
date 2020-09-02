using System;
using System.Collections.Generic;
using System.Linq;
using DemoApp.Infra.Assertions;
using DemoApp.Infra.Directories.Reading;
using DemoApp.Infra.Paths;
using DemoApp.Processing.Writing;

namespace DemoApp.Processing.Reading.Files
{
    public class FilesToProcessReader : IFilesToProcessReader
    {
        private readonly IPathCombiner _pathCombiner;
        private readonly IDirectoryReader _directoryReader;

        public FilesToProcessReader(IPathCombiner pathCombiner, 
                                    IDirectoryReader directoryReader)
        {
            _pathCombiner = pathCombiner;
            _directoryReader = directoryReader;
        }

        public IEnumerable<FileToProcess> GetFilesToProcess(InputPath inputPath)
        {
            Assert.IsNotNull(inputPath, nameof(inputPath));

            var fullPath = inputPath.BasePath;
            if (inputPath.HasInFolder())
            {
                fullPath = _pathCombiner.Combine(inputPath.BasePath, inputPath.InFolder);
            }

            var filePaths = _directoryReader.GetFilePathsInFolder(fullPath);
            return filePaths.Select(filePath => new FileToProcess(filePath));
        }
    }
}