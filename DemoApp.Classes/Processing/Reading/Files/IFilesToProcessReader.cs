using System.Collections.Generic;
using System.Text;
using DemoApp.Processing.Writing;

namespace DemoApp.Processing.Reading.Files
{
    public interface IFilesToProcessReader
    {
        IEnumerable<FileToProcess> GetFilesToProcess(InputPath inputPath);
    }
}
