using DemoApp.Infra.Assertions;

namespace DemoApp.Processing.Reading.Contents
{
    public class FileContents
    {
        public FileContents(string fileContents)
        {
            Assert.IsNotNullOrWhitespace(fileContents, nameof(fileContents));

            Contents = fileContents;
        }

        public string Contents { get; }
    }
}