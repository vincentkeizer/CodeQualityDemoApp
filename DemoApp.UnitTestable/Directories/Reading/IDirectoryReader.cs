using System.Collections.Generic;

namespace DemoApp.Directories.Reading
{
    public interface IDirectoryReader
    {
        IEnumerable<string> GetFilePathsInFolder(string folderPath);
    }
}
