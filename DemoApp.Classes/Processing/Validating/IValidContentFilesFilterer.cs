using System.Collections.Generic;
using System.Text;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Validating
{
    public interface IValidContentFilesFilterer
    {
        IEnumerable<ValidFileContents> GetValidFileContents(string validationType, IEnumerable<FileContents> fileContents);
    }
}
