using DemoApp.Directories.Paths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DemoApp.UnitTestable.Tests.Paths
{
    [TestClass]
    public class PathCombinerTests
    {
        [TestMethod]
        public void Combine_WhenCalledWithTwoPaths_ThenCombinesPaths()
        {
            var pathCombiner = new PathCombiner();

            var combinedPath = pathCombiner.Combine("c:\\test", "subfolder");

            combinedPath.ShouldBe("c:\\test\\subfolder");
        }

        [TestMethod]
        public void Combine_WhenCalledWithTwoPathAndFile_ThenCombinesParts()
        {
            var pathCombiner = new PathCombiner();

            var combinedPath = pathCombiner.Combine("c:\\test", "log.txt");

            combinedPath.ShouldBe("c:\\test\\log.txt");
        }
    }
}
