using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DemoApp.Infra.Tests.Assertions
{
    [TestClass]
    public class AssertTests
    {
        [TestMethod]
        public void IsGreaterThanOrEqualTo0_WhenCalledWith0Value_ThenDoesNotThrowException()
        {
            Should.NotThrow(() => Infra.Assertions.Assert.IsGreaterThanOrEqualTo0(0, "name"));
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo0_WhenCalledWithNegativeValue_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => Infra.Assertions.Assert.IsGreaterThanOrEqualTo0(-1, "name"));
        }

        [TestMethod]
        public void IsGreaterThan0_WhenCalledWith1Value_ThenDoesNotThrowException()
        {
            Should.NotThrow(() => Infra.Assertions.Assert.IsGreaterThan0(1, "name"));
        }

        [TestMethod]
        public void IsGreaterThan0_WhenCalledWith0Value_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => Infra.Assertions.Assert.IsGreaterThan0(0, "name"));
        }

        [TestMethod]
        public void IsNotNull_WhenCalledWithAnObject_ThenDoesNotThrowException()
        {
            Should.NotThrow(() => Infra.Assertions.Assert.IsNotNull(new {}, "name"));
        }

        [TestMethod]
        public void IsNotNull_WhenCalledWithNullValue_ThenThrowsArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => Infra.Assertions.Assert.IsNotNull(null, "name"));
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_WhenCalledWithANullValue_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => Infra.Assertions.Assert.IsNotNullOrWhitespace(null, "name"));
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_WhenCalledWithEmptyValue_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => Infra.Assertions.Assert.IsNotNullOrWhitespace(string.Empty, "name"));
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_WhenCalledWithAWhitespace_ThenThrowsArgumentException()
        {
            Should.Throw<ArgumentException>(() => Infra.Assertions.Assert.IsNotNullOrWhitespace("    ", "name"));
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_WhenCalledWithStringValue_ThenDoesNotThrowException()
        {
            Should.NotThrow(() => Infra.Assertions.Assert.IsNotNullOrWhitespace("value", "name"));
        }
    }
}
