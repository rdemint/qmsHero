
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml;

namespace QmsDocXml.Tests
{
    [TestClass]
    public class HeaderRevisionTests
    {
        [TestMethod]
        public void ReadTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var prop = new HeaderRevision();
            var result = doc.Inspect(prop);
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(fixture.WordSampleRevision, (string)result.Value.State);

            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = xl.Inspect(new HeaderRevision());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(fixture.ExcelSampleRevision, (string)result.Value.State);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new XmlFixture();
            
            //word
            var doc = new WordDoc(fixture.WordSampleCopy);

            string rev = "20";
            
            var prop = new HeaderRevision(rev);
            var result = doc.Process(prop);
            Assert.IsTrue(result.IsSuccess);

            result = doc.Inspect(new HeaderRevision());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(rev, (string)result.Value.State);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = xl.Inspect(new HeaderRevision());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(fixture.ExcelSampleRevision, (string)result.Value.State);

            rev = "12";
            xl.Process(new HeaderRevision(rev));
            result = xl.Inspect(new HeaderRevision());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(rev, (string)result.Value.State);

        }
    }
}
