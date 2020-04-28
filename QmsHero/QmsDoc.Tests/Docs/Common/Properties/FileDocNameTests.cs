using QmsDoc.Docs.Common.Properties;
using System;
using FluentResults;
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
            var result = doc.Inspect(new FileDocName());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(fixture.WordSampleFileDocName, (string)result.Value.State);
        }

        [TestMethod]
        public void ReadFormTest()
        {
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new FileDocName()).State;
            Assert.AreEqual(fixture.ExcelSampleFileDocName, result);
        }

        [TestMethod]
        public void WriteSopTest()
        {
            string name = "Important Quality Booklet";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(new FileDocName(name));
            string result = doc.Inspect(new FileDocName()).State.ToString();
            Assert.AreEqual(name, result);

        }

        [TestMethod]
        public void WriteFormTest()
        {
            string name = "Really-Important-Form";
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            doc.Process(new FileDocName(name));
            string result = doc.Inspect(new FileDocName()).State.ToString();
            Assert.AreEqual(name, result);

        }
    }
}
