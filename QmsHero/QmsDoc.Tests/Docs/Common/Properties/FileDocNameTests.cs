using QmsDoc.Docs.Common.Properties;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Word;
using QmsDoc.Docs.Excel;

namespace QmsDoc.Tests.Docs.Common.Properties
{
    [TestClass]
    public class FileDocNameTests
    {
        [TestMethod]
        public void ReadSopTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)doc.Inspect(new FileDocName()).State;
            Assert.AreEqual(fixture.WordSampleFileDocName, result);
        }

        [TestMethod]
        public void ReadFormTest()
        {
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new FileDocName()).State;
            Assert.AreEqual(fixture.ExcelSampleFileDocName, result);
        }

        public void WriteTest()
        {
            Assert.Fail();
        }
    }
}
