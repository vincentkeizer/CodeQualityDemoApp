﻿using System.Collections.Generic;

namespace DemoApp.Infra.Directories.Reading
{
    public class DirectoryReader : IDirectoryReader
    {
        public IEnumerable<string> GetFilePathsInFolder(string folderPath)
        {
            return System.IO.Directory.GetFiles(folderPath);
        }
    }
}