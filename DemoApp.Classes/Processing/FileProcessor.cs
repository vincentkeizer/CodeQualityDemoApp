using System.Collections.Generic;
using System.Linq;
using DemoApp.Infra.Directories.Reading;
using DemoApp.Infra.Files.Reading;
using DemoApp.Infra.Files.Writing;
using DemoApp.Infra.Paths;
using DemoApp.Processing.Reading;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;
using DemoApp.Processing.Validating;
using DemoApp.Processing.Writing;

namespace DemoApp.Processing
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IFilesToProcessReader _filesToProcessReader;
        private readonly IFileContentsReader _fileContentsReader;
        private readonly IFilesWithValidFileContentsFilterer _validContentFilesFilterer;
        private readonly IValidFileContentsWriter _validFileContentsWriter;

        public FileProcessor(IFilesToProcessReader filesToProcessReader,
                             IFileContentsReader fileContentsReader,
                             IFilesWithValidFileContentsFilterer validContentFilesFilterer,
                             IValidFileContentsWriter validFileContentsWriter)
        {
            _filesToProcessReader = filesToProcessReader;
            _fileContentsReader = fileContentsReader;
            _validContentFilesFilterer = validContentFilesFilterer;
            _validFileContentsWriter = validFileContentsWriter;
        }

        public ProcessSummary ProcessFiles(string basePath, string inFolder, string type, string outFolder)
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
                    var validFileContents = GetValidFileContents(type, fileContents);
                    numberOfValidFiles = GetNumberOfValidFiles(validFileContents);
                    CopyValidFilesToOutFolder(basePath, outFolder, validFileContents);
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

        private IEnumerable<FileToProcess> GetFilePathsFromInFolder(string basePath, string inFolder)
        {
            var inputPath = new InputPath(basePath, inFolder);
            return _filesToProcessReader.GetFilesToProcess(inputPath);
        }

        private bool HasFiles(IEnumerable<FileToProcess> filesToProcesses)
        {
            return filesToProcesses.Count() != 0;
        }

        private static int GetTotalNumberOfFiles(IEnumerable<FileToProcess> filesToProcess)
        {
            return filesToProcess.Count();
        }

        private IEnumerable<FileContents> GetFileContents(IEnumerable<FileToProcess> filesToProcess)
        {
            return _fileContentsReader.GetFileContents(filesToProcess);
        }

        private IEnumerable<ValidFileContents> GetValidFileContents(string type, IEnumerable<FileContents> fileContents)
        {
            return _validContentFilesFilterer.GetValidFileContents(type, fileContents);
        }

        private static int GetNumberOfValidFiles(IEnumerable<ValidFileContents> validFiles)
        {
            return validFiles.Count();
        }

        private void CopyValidFilesToOutFolder(string basePath, string outFolder, IEnumerable<ValidFileContents> validFileContents)
        {
            var outputPath = new OutputPath(basePath, outFolder);
            _validFileContentsWriter.WriteValidFileContents(outputPath, validFileContents);
        }
    }
}
