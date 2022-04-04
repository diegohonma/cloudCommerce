using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;
using cloudCommerce.Domain.Constants;
using CSVJsonParser.Application;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace cloudCommerce.Tests.cloudCommerce.Application
{
    internal class HandlerParserTests
    {
        private Mock<IJsonProcessor> _jsonProcessor;
        private Mock<IXmlProcessor> _xmlProcessor;
        private Mock<ICsvProcessor> _csvProcessor;

        private HandlerParser _handlerParser;

        [SetUp]
        public void SetUp()
        {
            _jsonProcessor = new Mock<IJsonProcessor>();
            _xmlProcessor = new Mock<IXmlProcessor>();
            _csvProcessor = new Mock<ICsvProcessor>();

            _csvProcessor
                .Setup(x => x.Unconvert(It.IsAny<string>()))
                .Returns(new ConvertFile(
                    headers: new List<string>()
                    {
                        "name", "address_line1", "address_line2"
                    },
                    values: new List<List<string>>
                    {
                        new List<string> { "Dave", "Street", "Town" }
                    }));

            _jsonProcessor
                .Setup(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()));

            _xmlProcessor
                .Setup(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()));

            _handlerParser = new HandlerParser(
                _jsonProcessor.Object, _xmlProcessor.Object, _csvProcessor.Object);
        }

        [Test]
        public void ShouldConvertFromCsvToJson()
        {
            var response = _handlerParser.Parse(Constants.FORMAT_CSV, "", Constants.FORMAT_JSON, "");

            Assert.IsTrue(response);

            _csvProcessor
                .Verify(x => x.Unconvert(It.IsAny<string>()), Times.Once);

            _jsonProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Once);

            _xmlProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ShouldConvertFromCsvToXml()
        {
            var response = _handlerParser.Parse(Constants.FORMAT_CSV, "", Constants.FORMAT_XML, "");

            Assert.IsTrue(response);

            _csvProcessor
                .Verify(x => x.Unconvert(It.IsAny<string>()), Times.Once);

            _jsonProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Never);
               
            _xmlProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ShouldReturnFalseWhenNoLines()
        {
            _csvProcessor
                .Setup(x => x.Unconvert(It.IsAny<string>()))
                .Returns(default(ConvertFile));

            var response = _handlerParser.Parse(Constants.FORMAT_CSV, "", Constants.FORMAT_JSON, "");

            Assert.IsFalse(response);

            _csvProcessor
                .Verify(x => x.Unconvert(It.IsAny<string>()), Times.Once);

            _xmlProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Never);

            _jsonProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ShouldReturnFalseWhenExceptionIsThrown()
        {
            _csvProcessor
                .Setup(x => x.Unconvert(It.IsAny<string>()))
                .Throws(new Exception());

            var response = _handlerParser.Parse(Constants.FORMAT_CSV, "", Constants.FORMAT_JSON, "");

            Assert.IsFalse(response);

            _csvProcessor
                .Verify(x => x.Unconvert(It.IsAny<string>()), Times.Once);

            _xmlProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Never);

            _jsonProcessor
                .Verify(x => x.ConvertAndSave(It.IsAny<ConvertFile>(), It.IsAny<string>()), Times.Never);
        }
    }
}
