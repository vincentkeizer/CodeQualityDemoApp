using DemoApp.Directories.Paths;

namespace DemoApp.Files.Writing
{
    public class FileWriter : IFileWriter
    {
        private readonly IPathCombiner _pathCombiner;

        public FileWriter(IPathCombiner pathCombiner)
        {
            _pathCombiner = pathCombiner;
        }

        public void WriteFileToFolder(string filename, string contents, string folder)
        {
            var fullFilePath = _pathCombiner.Combine(folder, filename);

            System.IO.File.WriteAllText(fullFilePath, contents);
        }
    }
}