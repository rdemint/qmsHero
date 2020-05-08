using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
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
    public class HeaderEffectiveDateTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            
            var fixture = new XmlFixture();
            var prop = new HeaderEffectiveDate();
            
            //word
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(prop);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(fixture.WordSampleEffectiveDate, (string)result.Value);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = xl.Inspect(new HeaderEffectiveDate());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(fixture.ExcelSampleEffectiveDate, (string)result.Value);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new XmlFixture();
            string effDate = "2020-20-20";
            var prop = new HeaderEffectiveDate(effDate);

            //word
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(prop);
            Assert.IsTrue(result.IsSuccess);

            result = doc.Inspect(new HeaderEffectiveDate());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(effDate, (string)result.Value);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = xl.Process(prop);
            Assert.IsTrue(result.IsSuccess);

            result = xl.Inspect(new HeaderEffectiveDate());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(effDate, (string)result.Value);
        }

    }
}