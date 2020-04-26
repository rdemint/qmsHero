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
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(fixture.WordSampleHeaderName, result);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(fixture.ExcelSampleHeaderName, result);
        }

        [TestMethod()]
        public void WriteWordTest()
        {
            var fixture = new XmlFixture();
            var docName = "New procedure (SOP-1)";
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(new HeaderName(docName));
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(docName, result);

        }

        [TestMethod()]
        public void WriteExcelTest()
        {
            string docName = "New Procedure (F-001B)";
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            doc.Process(new HeaderName(docName));
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(docName, result);
        }
    }
}