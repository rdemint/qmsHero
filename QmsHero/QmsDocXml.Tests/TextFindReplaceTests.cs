using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TextFindReplaceTests
    {
        [TestMethod()]
        public void TextFindReplaceReadTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            Regex rx = new Regex("SOP-002*");
            var result = doc.Inspect(new TextFindReplace(), rx);
            Assert.IsTrue(result.IsSuccess);

        }

        [TestMethod()]
        public void TextFindReplaceTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteTest()
        {
            Assert.Fail();
        }
    }
}