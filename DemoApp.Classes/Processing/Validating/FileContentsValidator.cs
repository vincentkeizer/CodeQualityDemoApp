using System;
using System.Collections.Generic;
using DemoApp.Infra.Assertions;
using DemoApp.Processing.Reading;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;

namespace DemoApp.Processing.Validating
{
    public class FileContentsValidator : IFileContentsValidator
    {
        public bool ValidateFile(string validationType, FileContents fileContents)
        {
            Assert.IsNotNullOrWhitespace(validationType, nameof(validationType));
            Assert.IsNotNull(fileContents, nameof(fileContents));

            var parts = fileContents.Contents.Split(";");
            if (parts.Length == 6 && parts[0] == validationType)
            {
                return true;
            }

            return false;
        }
    }
}