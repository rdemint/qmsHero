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
    public class TextIsCleanTests
    {
        [TestMethod()]
        public void TextIsCleanReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(TextIsClean.Create("SOP-002"));
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void TextIsCleanWriteWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(TextIsClean.Create("SOP-002", "SOP-222"));
            Assert.IsTrue(result.IsSuccess);
            var casted = result.Value as TextIsClean;
            Assert.AreEqual(casted.Count, 8);
            var result2 = doc.Inspect(TextIsClean.Create("SOP-222"));
            var casted2 = result2.Value as TextIsClean;
            Assert.AreEqual(casted2.Count, 8);
            var result3 = doc.Inspect(TextIsClean.Create("SOP-002"));
            var casted3 = result3.Value as TextIsClean;
            Assert.AreEqual(casted3.Count, 0);


        }


    }
}