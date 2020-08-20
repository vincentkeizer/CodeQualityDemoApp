using System.Collections.Generic;
using System.Linq;
using DemoApp.Infra.Directories.Reading;
using DemoApp.Infra.Files.Reading;
using DemoApp.Infra.Files.Writing;
using DemoApp.Infra.Paths;

namespace DemoApp.Processing
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;
        private readonly IDirectoryReader _directoryReader;
        private readonly IPathCombiner _pathCombiner;


        public FileProcessor(IFileReader fileReader,
            IFileWriter fileWriter,
            IDirectoryReader directoryReader,
            IPathCombiner pathCombiner)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
            _directoryReader = directoryReader;
            _pathCombiner = pathCombiner;
        }

        public ProcessSummary ParseFiles(string basePath, string inFolder, string type, string outFolder)
        {
            var totalNumberOfFiles = 0;
            var numberOfValidFiles = 0;
            if (ValidateRequiredArguments(basePath, type))
            {
                var filesPaths = GetFilePathsFromInFolder(basePath, inFolder);
                if (HasFiles(filesPaths))
                {
                    totalNumberOfFiles = GetTotalNumberOfFiles(filesPaths);
                    var fileContents = GetFileContents(filesPaths);
                    var validFiles = GetValidFileContents(type, fileContents);
                    numberOfValidFiles = GetNumberOfValidFiles(validFiles);
                    CopyValidFilesToOutFolder(basePath, outFolder, validFiles);
                }
            }

            var summary = new ProcessSummary(totalNumberOfFiles, numberOfValidFiles);
            return summary;
        }

        private bool ValidateRequiredArguments(string basePath, string type)
        {
            return !string.IsNullOrWhiteSpace(basePath)
                   && !string.IsNullOrWhiteSpace(type);
        }

        private IEnumerable<string> GetFilePathsFromInFolder(string basePath, string inFolder)
        {
            var fullPath = basePath;
            if (!string.IsNullOrWhiteSpace(inFolder))
            {
                fullPath = _pathCombiner.Combine(basePath, inFolder);
            }

            var filePaths = _directoryReader.GetFilePathsInFolder(fullPath);
            return filePaths;
        }

        private bool HasFiles(IEnumerable<string> filePaths)
        {
            return filePaths.Count() != 0;
        }

        private static int GetTotalNumberOfFiles(IEnumerable<string> filesPaths)
        {
            return filesPaths.Count();
        }

        private List<string> GetFileContents(IEnumerable<string> files)
        {
            var fileContents = new List<string>();
            foreach (var file in files)
            {
                var content = _fileReader.ReadFile(file);
                fileContents.Add(content);
            }

            return fileContents;
        }

        private List<string> GetValidFileContents(string type, List<string> fileContents)
        {
            var validFiles = new List<string>();
            foreach (var content in fileContents)
            {
                var parts = content.Split(";");
                if (parts.Length == 6 && parts[0] == type)
                {
                    validFiles.Add(content);
                }
            }

            return validFiles;
        }

        private static int GetNumberOfValidFiles(List<string> validFiles)
        {
            return validFiles.Count();
        }

        private void CopyValidFilesToOutFolder(string basePath, string outFolder, List<string> validFiles)
        {
            for (var i = 0; i < validFiles.Count; i++)
            {
                var fpout = basePath;
                if (!string.IsNullOrWhiteSpace(outFolder))
                {
                    fpout = _pathCombiner.Combine(basePath, outFolder);
                }

                _fileWriter.WriteFileToFolder($"{i + 1}.txt", validFiles[i], fpout);
            }
        }
    }
}
