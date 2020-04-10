using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QmsDoc.Test
{
    [TestClass]
    public class FixtureUtilTests
    {
        [TestMethod]
        public void GetSafeFilesTest()
        {
            var fixture = new FixtureUtil();
            Assert.AreEqual(fixture.SafeFiles.Count, 28);
        }

        [TestMethod]
        public void FixtureDirTest()
        {
            var fixture = new FixtureUtil();
            Assert.AreEqual(fixture.FixtureDir.Name, "Fixtures");
        }

        [TestMethod]
        public void ActiveQMSDocumentsTest()
        {
            var fixture = new FixtureUtil();
            var name = fixture.ActiveQMSDocuments.Name;
            Assert.AreEqual("Active QMS Documents", name);
        }

        [TestMethod]
        public void Sop1DocumentsTest()
        {
            var fixture = new FixtureUtil();
            var name = fixture.Sop1Documents.Name;
            Assert.AreEqual("SOP-001 Quality Manual Documents", name);
        }

        [TestMethod]
        public void WordSampleTest()
        {
            var fixture = new FixtureUtil();
            var ext = fixture.WordSample.Extension;
            Assert.AreEqual(".docx", ext);
        }

        [TestMethod]
        public void ExcelSampleTest()
        {
            var fixture = new FixtureUtil();
            var ext = fixture.ExcelSample.Extension;
            Assert.AreEqual(".xlsx", ext);
        }
    }
}
