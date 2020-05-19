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
    public class DocumentPropertyCreatorTests
    {
        [TestMethod()]
        public void ReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(new DocumentPropertyCreator());
            Assert.AreEqual("leanRAQAsystems", result.Value.State);
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(new DocumentPropertyCreator());
            Assert.AreEqual("Maria", result.Value.State);
        }

        [TestMethod]
        public void WriteExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Process(new DocumentPropertyCreator("Lean RAQA Systems"));
            Assert.IsTrue(result.IsSuccess);
            var resultInspect = doc.Inspect(new DocumentPropertyCreator());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Lean RAQA Systems", result.Value.State);
        }

        [TestMethod]
        public void WriteWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(new DocumentPropertyCreator("Lean RAQA Systems"));
            Assert.IsTrue(result.IsSuccess);
            var resultInspect = doc.Inspect(new DocumentPropertyCreator());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Lean RAQA Systems", result.Value.State);
        }

        [TestMethod]
        public void WriteDirTest()
        {
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var resultDocCollection = manager.Process(new DocumentPropertyCreator("Lean RAQA Systems"));
            Assert.IsFalse(resultDocCollection.HasErrors());
            foreach(var doc in resultDocCollection)
            {
                Assert.IsFalse(doc.UpdatePropertyProcessingErrors());
            }
            foreach (var doc in resultDocCollection)
            {
                var resultInspect = doc.Inspect(new DocumentPropertyCreator());
                Assert.AreEqual("Lean RAQA Systems", resultInspect.Value.State);
            }
        }
    }
}