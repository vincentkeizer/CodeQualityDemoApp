namespace DemoApp.Infra.Paths
{
    public interface IPathCombiner
    {
        string Combine(string basePath, string relativePath);
    }
}
