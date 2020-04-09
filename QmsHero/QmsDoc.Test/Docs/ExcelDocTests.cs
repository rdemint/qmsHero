using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs;
using QmsDoc.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Tests
{
    [TestClass()]
    public class ExcelDocTests
    {
        [TestMethod()]
        public void RevisionTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            var rightHeader = wbk.Doc.Worksheets[1].PageSetup.RightHeader;
            Assert.AreEqual(
                "2",
                wbk.GetRevision()
                );
            manager.Dispose();
        }

        [TestMethod()]
        public void EffectiveDateTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            var rightHeader = wbk.Doc.Worksheets[1].PageSetup.RightHeader;
            Assert.AreEqual(
                "2018-11-26",
                wbk.GetEffectiveDate()
                );
            manager.Dispose();
        }

        [TestMethod()]
        public void OpenDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            Assert.AreEqual(0, manager.ExcelApp.Workbooks.Count);
            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual(1, manager.ExcelApp.Workbooks.Count);
            manager.Dispose();

        }

        [TestMethod()]
        public void CloseDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();

            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            //Assert.AreEqual(wbk.App, manager.ExcelApp);
            Assert.AreEqual(1, wbk.App.Workbooks.Count);
            Assert.AreEqual(wbk.App, manager.ExcelApp);
            wbk.CloseDocument();
            Assert.AreEqual(0, manager.ExcelApp.Workbooks.Count);
            manager.Dispose();
        }
    }
}