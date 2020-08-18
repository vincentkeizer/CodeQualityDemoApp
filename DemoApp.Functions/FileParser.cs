using System.Collections.Generic;

namespace DemoApp
{
    public class FileParser : IFileParser
    {
        private int _validFiles;
        private int _invalidFiles;

        public void ParseFiles(string basePath, string inFolder, string type, string outFolder)
        {
            if (DoAllArgumentsContainText(basePath, inFolder, type, outFolder))
            {
                var filesPaths = GetFilePathsFromInFolder(basePath, inFolder);
                if (HasFiles(filesPaths))
                {
                    var fileContents = GetFileContents(filesPaths);
                    var validFiles = GetValidFileContents(type, fileContents);
                    MoveValidFilesToOutFolder(basePath, outFolder, validFiles);
                }
            }
        }

        private static bool DoAllArgumentsContainText(string basePath, string inFolder, string type, string outFolder)
        {
            return !string.IsNullOrWhiteSpace(basePath)
                   && !string.IsNullOrWhiteSpace(inFolder)
                   && !string.IsNullOrWhiteSpace(type)
                   && !string.IsNullOrWhiteSpace(outFolder);
        }

        private static string[] GetFilePathsFromInFolder(string basePath, string inFolder)
        {
            var fullPath = basePath;
            if (!string.IsNullOrWhiteSpace(inFolder))
            {
                fullPath = System.IO.Path.Combine(basePath, inFolder);
            }

            var filePaths = System.IO.Directory.GetFiles(fullPath);
            return filePaths;
        }

        private static bool HasFiles(string[] filePaths)
        {
            return filePaths.Length != 0;
        }

        private static List<string> GetFileContents(string[] files)
        {
            var fileContents = new List<string>();
            foreach (var file in files)
            {
                var content = System.IO.File.ReadAllText(file);
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

        private static void MoveValidFilesToOutFolder(string basePath, string outFolder, List<string> validFiles)
        {
            for (var i = 0; i < validFiles.Count; i++)
            {
                var fpout = basePath;
                if (!string.IsNullOrWhiteSpace(outFolder))
                {
                    fpout = System.IO.Path.Combine(basePath, outFolder, $"{i + 1}.txt");
                }
                else
                {
                    fpout = System.IO.Path.Combine(basePath, $"{i + 1}.txt");
                }

                System.IO.File.WriteAllText(fpout, validFiles[i]);
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
