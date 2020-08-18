using DemoApp.Directories.Paths;

namespace DemoApp.Files.Writing
{
    public class FileWriter : IFileWriter
    {
        private readonly IDirectoryPathCombiner _directoryPathCombiner;

        public FileWriter(IDirectoryPathCombiner directoryPathCombiner)
        {
            _directoryPathCombiner = directoryPathCombiner;
        }

        public void WriteFileToFolder(string filename, string contents, string folder)
        {
            var fullFilePath = _directoryPathCombiner.CombinePaths(folder, filename);

            System.IO.File.WriteAllText(fullFilePath, contents);
        }
    }
}