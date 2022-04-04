using cloudCommerce.Application;
using cloudCommerce.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cloudCommerce.Tests.cloudCommerce.Application
{
    internal class CsvProcessorTests
    {
        private CsvProcessor _csvProcessor;

        [SetUp]
        public void SetUp()
        {
            _csvProcessor = new CsvProcessor();
        }

        [Test]
        public void ShouldUnconvertCsv()
        {
            var expectedResponse = new ConvertFile(
                headers: new List<string>()
                {
                    "name", "address_line1", "address_line2"
                },
                values: new List<List<string>>
                {
                    new List<string> { "Dave", "Street", "Town" }
                });

            var convertFile = _csvProcessor.Unconvert(
                Path.Combine(TestContext.CurrentContext.TestDirectory, @"Samples\Csv\csvExample.csv"));

            Assert.IsNotNull(convertFile);
            Assert.AreEqual(expectedResponse.Headers[0], convertFile!.Headers[0]);
            Assert.AreEqual(expectedResponse.Headers[1], convertFile.Headers[1]);
            Assert.AreEqual(expectedResponse.Headers[2], convertFile.Headers[2]);
            Assert.AreEqual(expectedResponse.Values.First()[0], convertFile.Values.First()[0]);
            Assert.AreEqual(expectedResponse.Values.First()[1], convertFile.Values.First()[1]);
            Assert.AreEqual(expectedResponse.Values.First()[2], convertFile.Values.First()[2]);
        }
    }
}
