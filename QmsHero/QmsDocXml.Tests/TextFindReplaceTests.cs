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
    public class TextFindReplaceTests
    {
        [TestMethod()]
        public void TextIsCleanReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(TextFindReplace.Create("SOP-002"));
            Assert.IsTrue(result.IsSuccess);
            var casted = result.Value as TextFindReplace;
            Assert.AreEqual(casted.Count, 8);
        }

        [TestMethod]
        public void TextFindExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(TextFindReplace.Create(fixture.ExcelSampleDocNumber));
            Assert.IsTrue(result.IsSuccess);
            var casted = result.Value as TextFindReplace;
            Assert.AreEqual(casted.Count, 1);

        }

        [TestMethod]
        public void TextIsCleanWriteWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(TextFindReplace.Create("SOP-002", "SOP-222"));
            Assert.IsTrue(result.IsSuccess);
            var casted = result.Value as TextFindReplace;
            Assert.AreEqual(casted.Count, 8);
            var result2 = doc.Inspect(TextFindReplace.Create("SOP-222"));
            var casted2 = result2.Value as TextFindReplace;
            Assert.AreEqual(casted2.Count, 8);
            var result3 = doc.Inspect(TextFindReplace.Create("SOP-002"));
            var casted3 = result3.Value as TextFindReplace;
            Assert.AreEqual(casted3.Count, 0);


        }


    }
}