namespace DemoApp.Directories.Paths
{
    public class DirectoryPathCombiner : IDirectoryPathCombiner
    {
        public string CombinePaths(string basePath, string relativePath)
        {
            return System.IO.Path.Combine(basePath, relativePath);
        }
    }
}