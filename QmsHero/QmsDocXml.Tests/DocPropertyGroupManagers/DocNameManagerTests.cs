using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.DocPropertyGroupManagers;
using QmsDocXml.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests.Actions
{
    [TestClass()]
    public class DocNameManagerTests
    {
        [TestMethod()]
        public void InspectExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(DocNameManager.Create(fixture.ExcelSampleDocName));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.ResultCollection.First().Value.State.ToString(), fixture.ExcelSampleDocName);
            Assert.AreEqual(result.Value.ResultCollection.Last().Value.State.ToString(), fixture.ExcelSampleDocName);
        }

        [TestMethod()]
        public void InspectWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(DocNameManager.Create(fixture.WordSampleFileDocName));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.ResultCollection.First().Value.State.ToString(), fixture.WordSampleFileDocName);
            Assert.AreEqual(result.Value.ResultCollection.Last().Value.State.ToString(), fixture.WordSampleFileDocName);
        }

        [TestMethod]
        public void ProcessWordTest()
        {
            string newName = "My New SOP";
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Process(DocNameManager.Create("Document Control Index", "Better Index"));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.ResultCollection.First().Value.State.ToString(), newName);
            Assert.AreEqual(result.Value.ResultCollection.Last().Value.State.ToString(), newName);
        }

        [TestMethod]
        public void ProcessExcelTest()
        {
            string newName = "My New SOP";
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Process(DocNameManager.Create("Document Control Index", "Better Index"));
        }
    }
}