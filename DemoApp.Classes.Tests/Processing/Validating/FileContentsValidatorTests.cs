using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Validating;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DemoApp.Tests.Processing.Validating
{
    [TestClass]
    public class FileContentsValidatorTests
    {
        [TestMethod]
        public void ValidateFile_WhenTypeIsNull_ThenThrowsArgumentException()
        {
            var validator = new FileContentsValidator();

            Should.Throw<ArgumentException>(() => validator.ValidateFile(null, new FileContents("somecontents")));
        }

        [TestMethod]
        public void ValidateFile_WhenTypeIsEmpty_ThenThrowsArgumentException()
        {
            var validator = new FileContentsValidator();

            Should.Throw<ArgumentException>(() => validator.ValidateFile(string.Empty, new FileContents("somecontents")));
        }

        [TestMethod]
        public void ValidateFile_WhenContentStartsWithTypeAndHasSixSemicolonDelimiters_ThenContentIsValid()
        {
            var validator = new FileContentsValidator();
            var type = "03";
            var validContents = "03;123456;78;90;12;34";

            var isValid = validator.ValidateFile(type, new FileContents(validContents));

            isValid.ShouldBeTrue();
        }

        [TestMethod]
        public void ValidateFile_WhenContentDoesNotStartsWithTypeAndHasSixSemicolonDelimiters_ThenContentIsInvalid()
        {
            var validator = new FileContentsValidator();
            var type = "02";
            var validContents = "03;123456;78;90;12;34";

            var isValid = validator.ValidateFile(type, new FileContents(validContents));

            isValid.ShouldBeFalse();
        }

        [TestMethod]
        public void ValidateFile_WhenStartsWithTypeButHasLessThanSixSemicolonDelimiters_ThenContentIsInvalid()
        {
            var validator = new FileContentsValidator();
            var type = "02";
            var validContents = "03;123456;78;90;12";

            var isValid = validator.ValidateFile(type, new FileContents(validContents));

            isValid.ShouldBeFalse();
        }

        [TestMethod]
        public void ValidateFile_WhenStartsWithTypeButHasMoreThanSixSemicolonDelimiters_ThenContentIsInvalid()
        {
            var validator = new FileContentsValidator();
            var type = "02";
            var validContents = "03;123456;78;90;12;34;17";

            var isValid = validator.ValidateFile(type, new FileContents(validContents));

            isValid.ShouldBeFalse();
        }
    }
}
