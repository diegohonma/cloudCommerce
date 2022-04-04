using cloudCommerce.Application;
using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace cloudCommerce.Tests.cloudCommerce.Application
{
    internal class JsonProcessorTests
    {
        private Mock<IFileService> _fileService;
        private JsonProcessor _jsonProcessor;

        [SetUp]
        public void SetUp()
        {
            _fileService = new Mock<IFileService>();
            _jsonProcessor = new JsonProcessor(_fileService.Object);
        }

        [Test]
        public void ShouldConvertToJsonAndSave()
        {
            var jsonContent = string.Empty;
            var expectedJson = File.ReadAllText(
                Path.Combine(TestContext.CurrentContext.TestDirectory, @"Samples\Json\jsonExample.json"));

            _fileService
                .Setup(f => f.SaveJson(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((p1, p2) => jsonContent = p2);

            var root = new JObject();

            _jsonProcessor.ConvertAndSave(
                new ConvertFile(
                    headers: new List<string>()
                    {
                        "name", "address_line1", "address_line2"
                    },
                    values: new List<List<string>>
                    {
                        new List<string> { "Dave", "Street", "Town" }
                    }),
                pathToSave: "");


            Assert.AreEqual(expectedJson,jsonContent);
            _fileService.Verify(f => f.SaveJson(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
