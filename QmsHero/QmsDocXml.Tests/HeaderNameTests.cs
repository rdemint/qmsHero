using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Word;
using QmsDocXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Assert.AreEqual("Quality Manual (SOP-001)", result);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            Assert.Fail();
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