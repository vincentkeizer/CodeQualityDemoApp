namespace DemoApp.Processing
{
    public interface IFileProcessor
    {
        void ProcessFiles(string basePath, string inFolder, string type, string outFolder);

        int GetValidFiles();

        int GetInvalidFiles();
    }
}
