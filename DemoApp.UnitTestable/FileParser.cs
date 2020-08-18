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
        private readonly IDirectoryPathCombiner _directoryPathCombiner;
        private int _validFiles;
        private int _invalidFiles;

        public FileParser(IFileReader fileReader, 
                          IFileWriter fileWriter, 
                          IDirectoryReader directoryReader, 
                          IDirectoryPathCombiner directoryPathCombiner)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
            _directoryReader = directoryReader;
            _directoryPathCombiner = directoryPathCombiner;
        }

        public void ParseFiles(string basePath, string inFolder, string type, string outFolder)
        {
            if (!string.IsNullOrWhiteSpace(basePath)
            &&  !string.IsNullOrWhiteSpace(inFolder)
            &&  !string.IsNullOrWhiteSpace(type)
            &&  !string.IsNullOrWhiteSpace(outFolder)) {
                var fp = basePath;
                if (!string.IsNullOrWhiteSpace(inFolder))
                {
                    fp = _directoryPathCombiner.CombinePaths(basePath, inFolder);
                }
                var files = _directoryReader.GetFilePathsInFolder(fp);
                if (files.Count() != 0)
                {
                    var fileContents = new List<string>();
                    foreach (var file in files)
                    {
                        var content = _fileReader.ReadFile(file);
                        fileContents.Add(content);
                    }
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

                    for(var i = 0; i < validFiles.Count; i ++)
                    {
                        var fpout = basePath;
                        if (!string.IsNullOrWhiteSpace(outFolder))
                        {
                            fpout = _directoryPathCombiner.CombinePaths(basePath, outFolder);
                        }
                        else
                        {
                            fpout = _directoryPathCombiner.CombinePaths(basePath, $"{i + 1}.txt");
                        }
                        _fileWriter.WriteFileToFolder($"{i + 1}.txt", validFiles[i], fpout);
                    }
                }
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
