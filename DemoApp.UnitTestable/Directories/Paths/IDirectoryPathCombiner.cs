namespace DemoApp.Directories.Paths
{
    public interface IDirectoryPathCombiner
    {
        string CombinePaths(string basePath, string relativePath);
    }
}
