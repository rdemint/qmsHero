using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml.Tests
{
    [TestClass()]
    public class HeaderNameTests
    {
        [TestMethod()]
        public void ReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(new HeaderName());
            Assert.IsTrue(result.IsSuccess);
            //Assert.AreEqual(fixture.WordSampleHeaderName, (string)result.Value.State);
            Assert.AreEqual(fixture.WordSampleFileDocName, (string)result.Value.State);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(new HeaderName());
            Assert.IsTrue(result.IsSuccess);
            //Assert.AreEqual(fixture.ExcelSampleHeaderName, (string)result.Value.State);
            Assert.AreEqual(fixture.ExcelSampleFileDocName, (string)result.Value.State);
        }

        [TestMethod()]
        public void WriteWordTest()
        {
            var fixture = new XmlFixture();
            var docName = "New procedure";
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(new HeaderName(docName));
            Assert.IsTrue(result.IsSuccess);

            result = doc.Inspect(new HeaderName());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(docName, (string)result.Value.State);

        }

        [TestMethod()]
        public void WriteExcelTest()
        {
            string docName = "New Procedure";
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Process(new HeaderName(docName));
            Assert.IsTrue(result.IsSuccess);

            result = doc.Inspect(new HeaderName());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(docName, (string)result.Value.State);
        }
    }
}