using QmsDoc.Docs.Common.Properties;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Word;
using QmsDoc.Docs.Excel;

namespace QmsDoc.Tests.Docs.Common.Properties
{
    [TestClass()]
    public class FileDocNumberTests
    {
        [TestMethod()]
        public void WriteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadTest()
        {
            var fixture = new Fixture();
            var wdoc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)wdoc.Inspect(new FileDocNumber()).State;
            Assert.AreEqual(fixture.WordSampleDocNumber, result);

            var xldoc = new ExcelDoc(fixture.ExcelSampleCopy);
            var xlResult = xldoc.Inspect(new FileDocNumber()).State.ToString();
            Assert.AreEqual(fixture.ExcelSampleDocNumber, xlResult);

        }
    }
}
