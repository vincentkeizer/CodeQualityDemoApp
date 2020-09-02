namespace DemoApp.Processing
{
    public interface IFileProcessor
    {
        ProcessSummary ProcessFiles(string basePath, string inFolder, string type, string outFolder);
    }
}
