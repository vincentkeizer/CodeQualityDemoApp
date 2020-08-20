using System.Collections.Generic;

namespace DemoApp.Infra.Directories.Reading
{
    public interface IDirectoryReader
    {
        IEnumerable<string> GetFilePathsInFolder(string folderPath);
    }
}
