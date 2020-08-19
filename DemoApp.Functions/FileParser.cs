using System.Collections.Generic;
using System.Linq;
using DemoApp.Directories.Paths;
using DemoApp.Directories.Reading;
using DemoApp.Files.Reading;
using DemoApp.Files.Writing;

namespace DemoApp
{
    public class FileParser : IFileParser
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;
        private readonly IDirectoryReader _directoryReader;
        private readonly IPathCombiner _pathCombiner;
        private int _validFiles;
        private int _invalidFiles;

        public FileParser(IFileReader fileReader,
            IFileWriter fileWriter,
            IDirectoryReader directoryReader,
            IPathCombiner pathCombiner)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
            _directoryReader = directoryReader;
            _pathCombiner = pathCombiner;
        }

        public void ParseFiles(string basePath, string inFolder, string type, string outFolder)
        {
            if (ValidateRequiredArguments(basePath, type))
            {
                var filesPaths = GetFilePathsFromInFolder(basePath, inFolder);
                if (HasFiles(filesPaths))
                {
                    var fileContents = GetFileContents(filesPaths);
                    var validFiles = GetValidFileContents(type, fileContents);
                    CopyValidFilesToOutFolder(basePath, outFolder, validFiles);
                }
            }
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
                    _validFiles++;
                }
                else
                {
                    _invalidFiles++;
                }
            }

            return validFiles;
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

        public int GetInvalidFiles()
        {
            return _invalidFiles;
        }

        public int GetValidFiles()
        {
            return _validFiles;
        }
    }
}
