using cloudCommerce.Application;
using cloudCommerce.Application.Interfaces;
using cloudCommerce.Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace cloudCommerce.Tests.cloudCommerce.Application
{
    internal class XmlProcessorTests
    {

        private Mock<IFileService> _fileService;
        private XmlProcessor _xmlProcessor;

        [SetUp]
        public void SetUp()
        {
            _fileService = new Mock<IFileService>();
            _xmlProcessor = new XmlProcessor(_fileService.Object);
        }

        [Test]
        public void ShouldConvertToXmlAndSave()
        {
            XmlDocument? xmlDocument = null;
            var expectedXml = File.ReadAllText(
                Path.Combine(TestContext.CurrentContext.TestDirectory, @"Samples\Xml\xmlExample.xml"));

            _fileService
                .Setup(f => f.SaveXml(It.IsAny<string>(), It.IsAny<XmlDocument>()))
                .Callback<string, XmlDocument>((p1, p2) => xmlDocument = p2);

            _xmlProcessor.ConvertAndSave(
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


            Assert.AreEqual(
               expectedXml.Replace("\r\n", "").Replace("\t", "").Replace(" ", "").Trim(),
               xmlDocument!.InnerXml.Replace("\r\n", "").Replace(" ", "").Trim());

            _fileService.Verify(f => f.SaveXml(It.IsAny<string>(), It.IsAny<XmlDocument>()), Times.Once());
        }
    }
}
