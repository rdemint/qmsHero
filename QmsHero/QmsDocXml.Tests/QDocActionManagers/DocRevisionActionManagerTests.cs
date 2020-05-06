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
    public class DocRevisionActionManagerTests
    {
        [TestMethod()]
        public void InspectTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var propCollection = doc.Inspect(new DocRevisionActionManager(fixture.ExcelSampleDocName));
            Assert.IsFalse(propCollection.HasErrors());
        }

        [TestMethod()]
        public void InspectWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var propCollection = doc.Inspect(new DocRevisionActionManager(fixture.WordSampleFileDocName));
            Assert.IsFalse(propCollection.HasErrors());
        }
        [TestMethod]
        public void InspectDirTest()
        {
            string newNum = "1";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var docNameManager = new DocRevisionActionManager(newNum);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Inspect(docNameManager);
            var docsWithErrors = docCollection.DocumentsWithErrors();
            Assert.AreEqual(0, docsWithErrors.Count);
            Assert.IsFalse(docCollection.HasErrors());
        }

        [TestMethod]
        public void ProcessDirTest()
        {
            string currentNum = "1";
            string newNum = "12";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var docNameManager = new DocRevisionActionManager(currentNum, newNum);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Process(docNameManager);
            Assert.IsFalse(docCollection.HasErrors());
        }
    }
}