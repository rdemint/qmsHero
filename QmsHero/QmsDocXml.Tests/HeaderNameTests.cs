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
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(fixture.WordSampleHeaderName, result);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(fixture.ExcelSampleHeaderName, result);
        }

        [TestMethod()]
        public void WriteWordTest()
        {
            var fixture = new Fixture();
            var myName = "New procedure (SOP-1)";
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(new HeaderName(myName));
            var result = (string)doc.Inspect(new HeaderName()).State;
            Assert.AreEqual(myName, result);

        }

        [TestMethod()]
        public void WriteExcelTest()
        {
            Assert.Fail();
        }
    }
}