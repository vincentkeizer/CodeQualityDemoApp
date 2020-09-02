using System.Collections.Generic;
using System.Text;
using DemoApp.Processing.Validating;

namespace DemoApp.Processing.Writing
{
    public interface IValidFileContentsWriter
    {
        void WriteValidFile(OutputPath outputPath, IEnumerable<ValidFileContents> validFiles);
    }
}
