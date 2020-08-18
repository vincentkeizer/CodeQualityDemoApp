using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp
{
    public interface IFileParser
    {
        void ParseFiles(string basePath, string inFolder, string type, string outFolder);

        int GetValidFiles();

        int GetInvalidFiles();
    }
}
