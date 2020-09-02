using DemoApp.Infra.Assertions;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Validating
{
    public class ValidFileContents
    {
        public ValidFileContents(FileContents fileContents)
        {
            Assert.IsNotNull(fileContents, nameof(fileContents));

            FileContents = fileContents;
        }

        public FileContents FileContents { get;  }
    }
}