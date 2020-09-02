using System.Collections.Generic;
using DemoApp.Infra.Directories.Reading;
using DemoApp.Infra.Files.Reading;
using DemoApp.Infra.Files.Writing;
using DemoApp.Infra.Paths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace DemoApp.Tests
{
    [TestClass]
    public class FileParserTests
    {
        private FileParser _fileParser;
        private Mock<IFileReader> _fileReaderMock;
        private Mock<IFileWriter> _fileWriterMock;
        private Mock<IDirectoryReader> _directorReader;
        private Mock<IPathCombiner> _pathCombiner;

        [TestInitialize]
        public void Initialize()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _fileWriterMock = new Mock<IFileWriter>();
            _directorReader = new Mock<IDirectoryReader>();
            _pathCombiner = new Mock<IPathCombiner>();

            _fileParser = new FileParser(_fileReaderMock.Object, 
                                         _fileWriterMock.Object, 
                                         _directorReader.Object, 
                                         _pathCombiner.Object);
        }

        [TestMethod]
        public void ParseFiles_WhenCalledWithValidFile_ThenWritesFileToOutFolder()
        {
            var basePath = "basePath";
            var inFolder = "inFolder";
            var outFolder = "outFolder";
            var type = "03";
            var combinedInFolder = "basePath\\inFolder";
            var combinedOutFolder = "basePath\\outFolder";
            var filePath = "basePAth\\inFolder\\testfile.txt";
            var validFileContent = "03;123456;78;90;12;34";
            _pathCombiner.Setup(r => r.Combine(basePath, inFolder)).Returns(combinedInFolder);
            _pathCombiner.Setup(r => r.Combine(basePath, outFolder)).Returns(combinedOutFolder);
            _directorReader.Setup(r => r.GetFilePathsInFolder(combinedInFolder)).Returns(new List<string> {filePath});
            _fileReaderMock.Setup(r => r.ReadFile(filePath)).Returns(validFileContent);

            _fileParser.ParseFiles(basePath, inFolder, type, outFolder);

            _fileWriterMock.Verify(w => w.WriteFileToFolder("1.txt", validFileContent, combinedOutFolder), Times.Once);
            _fileParser.GetValidFiles().ShouldBe(1);
        }

        [TestMethod]
        public void ParseFiles_WhenCalledWithInvalidFile_ThenDoesNotWritesFileToOutFolder()
        {
            var basePath = "basePath";
            var inFolder = "inFolder";
            var outFolder = "outFolder";
            var type = "03";
            var combinedInFolder = "basePath\\inFolder";
            var combinedOutFolder = "basePath\\outFolder";
            var filePath = "basePAth\\inFolder\\testfile.txt";
            var invalidFileContent = "02;123456;78;90;12;34";
            _pathCombiner.Setup(r => r.Combine(basePath, inFolder)).Returns(combinedInFolder);
            _pathCombiner.Setup(r => r.Combine(basePath, outFolder)).Returns(combinedOutFolder);
            _directorReader.Setup(r => r.GetFilePathsInFolder(combinedInFolder)).Returns(new List<string> {filePath});
            _fileReaderMock.Setup(r => r.ReadFile(filePath)).Returns(invalidFileContent);

            _fileParser.ParseFiles(basePath, inFolder, type, outFolder);

            _fileWriterMock.Verify(w => w.WriteFileToFolder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _fileParser.GetInvalidFiles().ShouldBe(1);
        }
    }
}
