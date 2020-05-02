using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml;
using QmsDocXml.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml
{
    [TestClass()]
    public class TextFlagTests
    {
        [TestMethod()]
        public void ReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(new TextFlag("collagen"));
            Assert.IsFalse(result.IsSuccess);
            //illustration text is in footnote. 
            result = doc.Inspect(new TextFlag("illustration"));
            Assert.IsFalse(result.IsSuccess);
            result = doc.Inspect(new TextFlag("Raines"));
            Assert.IsFalse(result.IsSuccess);
            result = doc.Inspect(new TextFlag("Maria"));
            Assert.IsTrue(result.IsSuccess);
            result = doc.Inspect(new TextFlag("SOP-002, Purchasing and Suppliers Controls"));
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(new TextFlag("collagen"));
            Assert.IsTrue(result.IsSuccess);
            //illustration text is in footnote. 
            result = doc.Inspect(new TextFlag("illustration"));
            Assert.IsTrue(result.IsSuccess);
            result = doc.Inspect(new TextFlag("Raines"));
            Assert.IsTrue(result.IsSuccess);
            //The following should only be in the company logo name, at least.
            result = doc.Inspect(new TextFlag("GT Medical"));
        }
    }
}