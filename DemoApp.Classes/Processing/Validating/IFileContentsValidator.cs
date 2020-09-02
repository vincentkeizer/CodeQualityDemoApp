using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Validating
{
    public interface IFileContentsValidator
    {
        bool ValidateFile(string validationType, FileContents fileContents);
    }
}
