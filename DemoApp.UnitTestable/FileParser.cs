using System.Collections.Generic;
using System.Linq;
using DemoApp.Infra.Directories.Reading;
using DemoApp.Infra.Files.Reading;
using DemoApp.Infra.Files.Writing;
using DemoApp.Infra.Paths;

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
            if (!string.IsNullOrWhiteSpace(basePath)
             && !string.IsNullOrWhiteSpace(type))
            {
                var fp = basePath;
                if (!string.IsNullOrWhiteSpace(inFolder))
                {
                    fp = _pathCombiner.Combine(basePath, inFolder);
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
