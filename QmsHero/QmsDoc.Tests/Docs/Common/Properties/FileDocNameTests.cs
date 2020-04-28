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
            var result = doc.Inspect(new FileDocName());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(fixture.ExcelSampleFileDocName, (string)result.Value.State);
        }

        [TestMethod]
        public void WriteSopTest()
        {
            string newName = "Important Quality Booklet";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var processResult = doc.Process(new FileDocName(newName));
            Assert.IsTrue(processResult.IsSuccess);

            var result = doc.Inspect(new FileDocName());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newName, (string)result.Value.State);

        }

        [TestMethod]
        public void WriteFormTest()
        {
            string newName = "Really-Important-Form";
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);

            var processResult = doc.Process(new FileDocName(newName));
            Assert.IsTrue(processResult.IsSuccess);

            var result = doc.Inspect(new FileDocName());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newName, (string)result.Value.State);

        }
    }
}
