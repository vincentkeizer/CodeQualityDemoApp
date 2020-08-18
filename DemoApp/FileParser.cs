using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp
{
    public class FileParser : IFileParser
    {
        private int _validFiles;
        private int _invalidFiles;

        public void ParseFiles(string basePath, string inFolder, string type, string outFolder)
        {
            if (!string.IsNullOrWhiteSpace(basePath)
            && !string.IsNullOrWhiteSpace(inFolder)
            && !string.IsNullOrWhiteSpace(type)
            && !string.IsNullOrWhiteSpace(outFolder))
            {
                var fp = basePath;
                if (!string.IsNullOrWhiteSpace(inFolder))
                {
                    fp = System.IO.Path.Combine(basePath, inFolder);
                }
                var files = System.IO.Directory.GetFiles(fp);
                if (files.Length != 0)
                {
                    var fileContents = new List<string>();
                    foreach (var file in files)
                    {
                        var content = System.IO.File.ReadAllText(file);
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
                            fpout = System.IO.Path.Combine(basePath, outFolder, $"{i + 1}.txt");
                        }
                        else
                        {
                            fpout = System.IO.Path.Combine(basePath, $"{i + 1}.txt");
                        }
                        System.IO.File.WriteAllText(fpout, validFiles[i]);
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
