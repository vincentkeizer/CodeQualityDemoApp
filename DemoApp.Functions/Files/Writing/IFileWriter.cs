﻿namespace DemoApp.Files.Writing
{
    public interface IFileWriter
    {
        void WriteFileToFolder(string filename, string contents, string folder);
    }
}
