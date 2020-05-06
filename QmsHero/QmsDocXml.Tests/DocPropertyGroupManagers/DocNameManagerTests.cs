using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.QDocActionManagers;
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
            var propCollection = doc.Inspect(DocNameManager.Create(fixture.ExcelSampleDocName));
            //Assert.AreEqual(
            //    propCollection.Where(prop=> prop.Value.Name == "HeaderName").First().Value.State.ToString(),
            //    fixture.ExcelSampleDocName
            //    );
            var textFind = propCollection.Where(prop => prop.Value.Name == "TextFindReplace").First().Value as TextFindReplace;
            Assert.IsFalse(propCollection.HasErrors());
            Assert.AreEqual(textFind.Count, 1);
        }

        [TestMethod()]
        public void InspectWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var propCollection = doc.Inspect(DocNameManager.Create(fixture.WordSampleFileDocName));
            //Assert.AreEqual(
            //    propCollection.Where(prop => prop.Value.Name == "HeaderName").First().Value.State.ToString(),
            //    fixture.WordSampleFileDocName
            //    );
            var textFind = propCollection.Where(prop => prop.Value.Name == "TextFindReplace").First().Value as TextFindReplace;
            Assert.IsFalse(propCollection.HasErrors());
            Assert.AreEqual(textFind.Count, 19);
        }

        [TestMethod]
        public void ProcessWordTest()
        {
            string newName = "My New SOP";
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var propCollection = doc.Process(DocNameManager.Create("Quality Manual", newName));
            foreach(var propResult in propCollection)
            {
                Assert.IsTrue(propResult.IsSuccess);
            }

        }

        [TestMethod]
        public void ProcessExcelTest()
        {
            string newName = "Better Index" ;
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var propCollection = doc.Process(DocNameManager.Create("Document Control Index", newName));
            foreach (var propResult in propCollection)
            {
                Assert.IsTrue(propResult.IsSuccess);
            }

        }

        [TestMethod]
        public void InspectDirTest()
        {
            string newName = "Better Index";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var docNameManager = DocNameManager.Create(newName);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Inspect(docNameManager);
            Assert.IsFalse(docCollection.HasErrors());
        }

        [TestMethod]
        public void ProcessDirTest()
        {
            string currentName = "Document Control Index";
            string newName = "Better Index";
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var docNameManager = DocNameManager.Create(currentName, newName);
            Assert.IsTrue(manager.CanProcessFiles());
            var docCollection = manager.Process(docNameManager);
            Assert.IsFalse(docCollection.HasErrors());
        }
    }
}