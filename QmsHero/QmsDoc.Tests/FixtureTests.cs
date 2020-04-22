using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QmsDoc.Tests
{
    [TestClass]
    public class FixtureTests
    {

        [TestMethod]
        public void ProcessingDirTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(fixture.DefaultProcessingDirName, fixture.ProcessingDir.Name);
        }

        [TestMethod]
        public void ReferenceDirTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(fixture.DefaultReferenceDirName, fixture.ReferenceDir.Name);
        }

        [TestMethod]
        public void ActiveQMSDocumentsTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual("Active QMS Documents", fixture.ActiveQMSDocuments.Name);
        }

        [TestMethod]
        public void Sop1DocumentsTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual("SOP-001 Quality Manual Documents", fixture.Sop1Documents.Name);
        }

        [TestMethod]
        public void WordSampleTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(".docx", fixture.WordSample.Extension);
        }

        [TestMethod]
        public void ExcelSampleTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(".xlsx", fixture.ExcelSample.Extension);
        
        }

        [TestMethod]
        public void LogoSampleJpgTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(".jpg", fixture.LogoSampleJpg.Extension);
        }


    }
}
