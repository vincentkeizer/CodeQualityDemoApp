namespace DemoApp.Directories.Paths
{
    public class PathCombiner : IPathCombiner
    {
        public string Combine(string basePath, string relativePath)
        {
            return System.IO.Path.Combine(basePath, relativePath);
        }
    }
}