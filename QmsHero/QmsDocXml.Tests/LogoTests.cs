using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Word;

namespace QmsDocXml.Tests
{
    [TestClass]
    public class LogoTests
    {
        [TestMethod]
        public void ReadTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.CopyToProcessingDir(fixture.WordSample));
            var result = (string)doc.Inspect(new Logo()).State;
            Assert.AreEqual("GT Medical Logo II.jpg", result);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.CopyToProcessingDir(fixture.WordSample));
            var initial = (string)doc.Inspect(new Logo()).State;
            Assert.AreEqual("GT Medical Logo II.jpg", initial);

            var logo = new Logo(fixture.LogoSampleJpg.FullName);
            doc.Process(logo);
            var result = (string)doc.Inspect(new Logo()).State;
            Assert.AreEqual(fixture.LogoSampleJpg.Name, result);
        }
    }
}
