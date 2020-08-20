using System;
using System.Collections.Generic;
using System.Text;
using DemoApp.Infra.Assertions;

namespace DemoApp.Processing
{
    public class ProcessSummary
    {
        public ProcessSummary(int totalNumberOfFiles, int numberOfValidFiles)
        {
            SetTotalNumberOfFiles(totalNumberOfFiles);
            SetNumberOfValidFiles(numberOfValidFiles);
        }

        public int TotalNumberOfFiles { get; private set; }
        public int NumberOfValidFiles { get; private set; }
        public int NumberOfInvalidFiles => TotalNumberOfFiles - NumberOfValidFiles;

        private void SetTotalNumberOfFiles(int totalNumberOfFiles)
        {
            Assert.IsGreaterThanOrEqualTo0(totalNumberOfFiles, nameof(totalNumberOfFiles));

            TotalNumberOfFiles = totalNumberOfFiles;
        }

        private void SetNumberOfValidFiles(int numberOfValidFiles)
        {
            Assert.IsGreaterThanOrEqualTo0(numberOfValidFiles, nameof(numberOfValidFiles));
            if (numberOfValidFiles > TotalNumberOfFiles)
            {
                throw new ArgumentException("valid files cannot be greater than total number of files");
            }

            NumberOfValidFiles = numberOfValidFiles;
        }
    }
}
