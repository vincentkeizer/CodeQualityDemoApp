using System.Collections.Generic;
using DemoApp.Infra.Directories.Reading;
using DemoApp.Infra.Files.Reading;
using DemoApp.Infra.Files.Writing;
using DemoApp.Infra.Paths;
using DemoApp.Processing;
using DemoApp.Processing.Reading.Contents;
using DemoApp.Processing.Reading.Files;
using DemoApp.Processing.Validating;
using DemoApp.Processing.Writing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace DemoApp.Tests.Processing
{
    [TestClass]
    public class FileProcessorTests
    {
        private FileProcessor _fileProcessor;
        private Mock<IFilesToProcessReader> _filesToProcessReaderMock;
        private Mock<IFileContentsReader> _fileContentsReader;
        private Mock<IFilesWithValidFileContentsFilterer> _filesWithValidFileContentsFilterer;
        private Mock<IValidFileContentsWriter> _validFileContentsWriter;

        [TestInitialize]
        public void Initialize()
        {
            _filesToProcessReaderMock = new Mock<IFilesToProcessReader>();
            _fileContentsReader = new Mock<IFileContentsReader>();
            _filesWithValidFileContentsFilterer = new Mock<IFilesWithValidFileContentsFilterer>();
            _validFileContentsWriter= new Mock<IValidFileContentsWriter>();

            _fileProcessor = new FileProcessor(_filesToProcessReaderMock.Object, 
                                               _fileContentsReader.Object, 
                                               _filesWithValidFileContentsFilterer.Object, 
                                               _validFileContentsWriter.Object);
        }

        [TestMethod]
        public void ProcessFiles_WhenCalledWithValidFile_ThenWritesFileToOutFolder()
        {
            var basePath = "basePath";
            var inFolder = "inFolder";
            var outFolder = "outFolder";
            var type = "03";
            var filePath = "basePAth\\inFolder\\testfile.txt";
            var validFileContent = "03;123456;78;90;12;34";

            var fileToProcess = new List<FileToProcess> { new FileToProcess(filePath) };
            var fileContents = new List<FileContents> { new FileContents(validFileContent) };
            var validFiles = new List<ValidFileContents> {new ValidFileContents(new FileContents(validFileContent))};
            _filesToProcessReaderMock.Setup(r => r.GetFilesToProcess(It.Is<InputPath>(inputPath => inputPath.BasePath == basePath && inputPath.InFolder == inFolder))).Returns(fileToProcess);
            _fileContentsReader.Setup(r => r.GetFileContents(fileToProcess)).Returns(fileContents);
            _filesWithValidFileContentsFilterer.Setup(f => f.GetValidFileContents(type, fileContents)).Returns(validFiles);

            var processSummary =  _fileProcessor.ProcessFiles(basePath, inFolder, type, outFolder);

            _validFileContentsWriter.Verify(
                w => w.WriteValidFileContents(It.Is<OutputPath>(outputPath => outputPath.BasePath == basePath 
                                                                                                                      && outputPath.OutFolder == outFolder), 
                                                                            validFiles), Times.Once);
            processSummary.NumberOfValidFiles.ShouldBe(1);
        }
    }
}
