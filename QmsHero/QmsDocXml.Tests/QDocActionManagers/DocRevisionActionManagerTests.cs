using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Assert.AreEqual(1, docsWithErrors.Count); //F-001D Rev. 2
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
            var docsWithErrors = docCollection.DocumentsWithErrors();
            Assert.IsFalse(docCollection.HasErrors());
        }

        [TestMethod]
        public void RegexText()
        {
            var rx = new Regex(@":\s*\d{1,2}s*$");
            var dateText = "Effective Date: 2018-12-03";
            var revText = "Rev: 3";
            var revText2 = "Rev:3";
            var dateMatch = rx.Match(dateText);
            Assert.IsFalse(dateMatch.Success);
            var revMatch = rx.Match(revText);
            Assert.IsTrue(revMatch.Success);
            var revMatch2 = rx.Match(revText2);
            Assert.IsTrue(revMatch2.Success);

        }
    }
}