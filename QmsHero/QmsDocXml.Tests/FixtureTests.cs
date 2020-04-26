using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QmsDocXml.Tests
{
    [TestClass]
    public class FixtureTests
    {

        [TestMethod]
        public void ProcessingDirTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual(fixture.DefaultProcessingDirName, fixture.ProcessingDir.Name);
        }

        [TestMethod]
        public void ReferenceDirTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual(fixture.DefaultReferenceDirName, fixture.ReferenceDir.Name);
        }

        [TestMethod]
        public void ActiveQMSDocumentsTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual("Active QMS Documents", fixture.ActiveQMSDocuments.Name);
        }

        [TestMethod]
        public void Sop1DocumentsTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual("SOP-001 Quality Manual Documents", fixture.Sop1Documents.Name);
        }

        [TestMethod]
        public void WordSampleTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual(".docx", fixture.WordSampleCopy.Extension);
        }

        [TestMethod]
        public void ExcelSampleTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual(".xlsx", fixture.ExcelSampleCopy.Extension);
        
        }

        [TestMethod]
        public void LogoSampleJpgTest()
        {
            var fixture = new XmlFixture();
            Assert.AreEqual(".jpg", fixture.LogoSampleJpgCopy.Extension);
        }


    }
}
