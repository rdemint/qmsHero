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
            //var myHeader = "DOCUMENT NAME: Quality Manual (SOP-001)";
            //var config = new ExcelDocConfig();
            //Match match = config.HeaderNameRegex.Match(myHeader);
            //Assert.AreEqual(match.Success, true);

        }

        [TestMethod()]
        public void WriteWordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteExcelTest()
        {
            Assert.Fail();
        }
    }
}