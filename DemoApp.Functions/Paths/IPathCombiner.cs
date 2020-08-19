namespace DemoApp.Directories.Paths
{
    public interface IPathCombiner
    {
        string Combine(string basePath, string relativePath);
    }
}
