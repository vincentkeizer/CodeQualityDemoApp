using System;
using System.Collections.Generic;
using DemoApp.Infra.Assertions;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Validating
{
    public class FilesWithValidFileContentsFilterer : IFilesWithValidFileContentsFilterer
    {
        private readonly IFileContentsValidator _fileContentsValidator;

        public FilesWithValidFileContentsFilterer(IFileContentsValidator fileContentsValidator)
        {
            _fileContentsValidator = fileContentsValidator;
        }

        public IEnumerable<ValidFileContents> GetValidFileContents(string validationType, IEnumerable<FileContents> fileContents)
        {
            Assert.IsNotNull(fileContents, nameof(fileContents));

            var validFiles = new List<ValidFileContents>();
            foreach (var fileContent in fileContents)
            {
                if (_fileContentsValidator.ValidateFile(validationType, fileContent))
                {
                    validFiles.Add(new ValidFileContents(fileContent));
                }
            }

            return validFiles;
        }
    }
}