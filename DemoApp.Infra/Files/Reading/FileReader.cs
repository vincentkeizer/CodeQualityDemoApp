namespace DemoApp.Infra.Files.Reading
{
    public class FileReader : IFileReader
    {
        public string ReadFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
    }
}