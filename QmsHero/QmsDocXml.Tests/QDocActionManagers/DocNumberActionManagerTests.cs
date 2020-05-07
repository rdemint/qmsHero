using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests.QDocActionManagers
{
    [TestClass()]
    public class DocNumberActionManagerTests
    {
        [TestMethod()]
        public void InspectTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var propCollection = doc.Inspect(new DocNumberActionManager(fixture.ExcelSampleDocNumber));
            var textFind = propCollection.Where(prop => prop.Value.Name == "TextFindReplace").First().Value as TextFindReplace;
            Assert.IsFalse(propCollection.HasErrors());
            Assert.AreEqual(textFind.Count, 1);
        }

        [TestMethod()]
        public void InspectWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var propCollection = doc.Inspect(new DocNumberActionManager(fixture.WordSampleDocNumber));
            var textFind = propCollection.Where(prop => prop.Value.Name == "TextFindReplace").First().Value as TextFindReplace;
            Assert.IsFalse(propCollection.HasErrors());
            Assert.AreEqual(textFind.Count, 8);
        }

        [TestMethod]
        public void ProcessWordTest()
        {
            string newNum = "-111";
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var propCollection = doc.Process(new DocNumberActionManager("-001", newNum));
            foreach (var propResult in propCollection)
            {
                Assert.IsTrue(propResult.IsSuccess);
            }

        }

        [TestMethod]
        public void ProcessExcelTest()
        {
            string newNum = "F-222Z";
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var propCollection = doc.Process(new DocNumberActionManager("F-001B", newNum));
            foreach (var propResult in propCollection)
            {
                Assert.IsTrue(propResult.IsSuccess);
            }

        }

        [TestMethod]
        public void ProcessBaseSopAndFormNumberDirTest()
        {
            string newNum = "-290";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Process(new DocNumberActionManager("-001", newNum));
            var docsWithErrors = docCollection.DocumentsWithErrors();
            Assert.IsFalse(docCollection.HasErrors());
        }

        [TestMethod]
        public void InspectDirTest()
        {
            string newNum = "F-001B";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var docNameManager = new DocNumberActionManager(newNum);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Inspect(docNameManager);
            Assert.IsFalse(docCollection.HasErrors());
        }

        [TestMethod]
        public void ProcessDirTest()
        {
            string currentNum = "F-001B";
            string newNum = "F-012Z";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var docNameManager = new DocNumberActionManager(currentNum, newNum);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Process(docNameManager);
            Assert.IsFalse(docCollection.HasErrors());
        }
    }
}