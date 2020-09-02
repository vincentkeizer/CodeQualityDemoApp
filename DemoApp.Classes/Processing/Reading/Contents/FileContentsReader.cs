using System.Collections.Generic;
using DemoApp.Infra.Assertions;
using DemoApp.Infra.Files.Reading;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Reading.Contents
{
    public class FileContentsReader : IFileContentsReader
    {
        private readonly IFileReader _fileReader;

        public FileContentsReader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public IEnumerable<FileContents> GetFileContents(IEnumerable<FileToProcess> filesToProcess)
        {
            Assert.IsNotNull(filesToProcess, nameof(filesToProcess));

            var fileContents = new List<FileContents>();
            foreach (var file in filesToProcess)
            {
                var content = _fileReader.ReadFile(file.FullPath);
                fileContents.Add(new FileContents(content));
            }

            return fileContents;
        }
    }
}