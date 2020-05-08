using QmsDoc.Docs.Common.Properties;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Word;
using QmsDoc.Docs.Excel;
using QmsDoc.Exceptions;

namespace QmsDoc.Tests.Docs.Common.Properties
{
    [TestClass()]
    public class FileDocNumberTests
    {
        [TestMethod()]
        public void WriteSopTest()
        {
            var newNum = "SOP-100";
            var fixture = new Fixture();
            var wdoc = new WordDoc(fixture.WordSampleCopy);
            wdoc.Process(new FileDocNumber(newNum));
            var result = wdoc.Inspect(new FileDocNumber());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod()]
        public void IsValidSopTest()
        {
            var newNum = "SOP-10B";
            var fixture = new Fixture();
            var wdoc = new WordDoc(fixture.WordSampleCopy);
            var result = wdoc.Process(new FileDocNumber(newNum));
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod()]
        public void IsValidFormTest()
        {
            var newNum = "F-01B";
            var fixture = new Fixture();
            var wdoc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = wdoc.Process(new FileDocNumber(newNum));
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod()]
        public void WriteFormTest()
        {
            var newNum = "F-100Z";
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            doc.Process(new FileDocNumber(newNum));
            var result = doc.Inspect(new FileDocNumber());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod()]
        public void ReadSopTest()
        {
            var fixture = new Fixture();
            var wdoc = new WordDoc(fixture.WordSampleCopy);

            var result = wdoc.Inspect(new FileDocNumber());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(fixture.WordSampleDocNumber, (string)result.Value);
        }

        [TestMethod()]
        public void ReadFormTest()
        {
            var fixture = new Fixture();

            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(new FileDocNumber());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(fixture.ExcelSampleDocNumber, (string)result.Value);

        }
    }
}
