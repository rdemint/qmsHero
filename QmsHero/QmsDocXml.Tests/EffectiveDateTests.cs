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
    public class EffectiveDateTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            
            var fixture = new Fixture();
            var prop = new EffectiveDate();
            
            //word
            var doc = new WordDoc(fixture.WordSampleCopy);
            string result = (string)doc.Inspect(prop).State;
            Assert.AreEqual(result, fixture.WordSampleEffectiveDate);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = (string)xl.Inspect(new EffectiveDate()).State;
            Assert.AreEqual(result, fixture.ExcelSampleEffectiveDate);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new Fixture();
            string effDate = "2020-20-20";
            var prop = new EffectiveDate(effDate);
            string result = null;

            //word
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(prop);
            result = (string)doc.Inspect(new EffectiveDate()).State; 
            Assert.AreEqual(result, effDate);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            xl.Process(prop);
            result = (string)xl.Inspect(new EffectiveDate()).State;
            Assert.AreEqual(effDate, result);
        }

    }
}