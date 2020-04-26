using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties.Tests
{
    [TestClass()]
    public class FileRevisionTests
    {
        [TestMethod()]
        public void ReadWordTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)doc.Inspect(new FileRevision()).State;
            Assert.AreEqual(fixture.WordSampleRevision, result);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new FileRevision()).State;
            Assert.AreEqual(fixture.ExcelSampleRevision, result);
        }

        [TestMethod()]
        public void WriteWordTest()
        {
            string newRev = "12";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(new FileRevision(newRev));
            var result = (string)doc.Inspect(new FileRevision()).State;
            Assert.AreEqual(doc.FileInfo.Name, result);
        }

        [TestMethod()]
        public void WriteExcelTest()
        {
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new FileRevision()).State;
            Assert.AreEqual(fixture.ExcelSampleRevision, result);
        }


    }
}