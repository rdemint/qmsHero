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
            var result = wdoc.Inspect(new FileDocNumber()).State.ToString();
            Assert.AreEqual(newNum, result);
        }

        [TestMethod()]
        public void ValidateSopStateTest()
        {
            var newNum = "SOP-10B";
            var fixture = new Fixture();
            var wdoc = new WordDoc(fixture.WordSampleCopy);
            Assert.ThrowsException<InvalidDocPropertyStateException>(()=>wdoc.Process(new FileDocNumber(newNum)));
        }

        [TestMethod()]
        public void ValidateFormStateTest()
        {
            var newNum = "F-01B";
            var fixture = new Fixture();
            var wdoc = new ExcelDoc(fixture.ExcelSampleCopy);
            Assert.ThrowsException<InvalidDocPropertyStateException>(() => wdoc.Process(new FileDocNumber(newNum)));
        }

        [TestMethod()]
        public void WriteFormTest()
        {
            var newNum = "F-100Z";
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            doc.Process(new FileDocNumber(newNum));
            var result = doc.Inspect(new FileDocNumber()).State.ToString();
            Assert.AreEqual(newNum, result);
        }

        [TestMethod()]
        public void ReadSopTest()
        {
            var fixture = new Fixture();
            var wdoc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)wdoc.Inspect(new FileDocNumber()).State;
            Assert.AreEqual(fixture.WordSampleDocNumber, result);
        }

        [TestMethod()]
        public void ReadFormTest()
        {
            var fixture = new Fixture();

            var xldoc = new ExcelDoc(fixture.ExcelSampleCopy);
            var xlResult = xldoc.Inspect(new FileDocNumber()).State.ToString();
            Assert.AreEqual(fixture.ExcelSampleDocNumber, xlResult);

        }
    }
}
