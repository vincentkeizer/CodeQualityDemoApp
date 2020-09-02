using System.Collections.Generic;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Reading.Contents
{
    public interface IFileContentsReader
    {
        IEnumerable<FileContents> GetFileContents(IEnumerable<FileToProcess> filesToProcess);
    }
}
