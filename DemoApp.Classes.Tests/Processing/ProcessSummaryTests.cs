using System;
using DemoApp.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DemoApp.Tests.Processing
{
    [TestClass]
    public class ProcessSummaryTests
    {
        [TestMethod]
        public void Ctor_WhenCalledWithCorrectNumbers_ThenSummarizesNumbers()
        {
            var summary = new ProcessSummary(10, 6);

            summary.TotalNumberOfFiles.ShouldBe(10);
            summary.NumberOfValidFiles.ShouldBe(6);
            summary.NumberOfInvalidFiles.ShouldBe(4);
        }

        [TestMethod]
        public void Ctor_WhenCalledWithNegativeTotalNumberOfFiles_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => new ProcessSummary(-1, 6));
        }

        [TestMethod]
        public void Ctor_WhenCalledWithNegativeNumberOfValidFiles_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => new ProcessSummary(10, -5));
        }

        [TestMethod]
        public void Ctor_WhenCalledWithGreaterNumberOfValidFilesThanTotalNumberOfFiles_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => new ProcessSummary(5, 10));
        }
    }
}
