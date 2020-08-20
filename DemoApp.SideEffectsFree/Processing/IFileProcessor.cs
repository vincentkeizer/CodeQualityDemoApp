namespace DemoApp.Processing
{
    public interface IFileProcessor
    {
        ProcessSummary ParseFiles(string basePath, string inFolder, string type, string outFolder);
    }
}
