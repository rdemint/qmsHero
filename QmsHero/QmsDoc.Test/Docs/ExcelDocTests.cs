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
            manager.ConfigDir(fixture.Sop1Documents.FullName);
            ExcelDoc doc = (ExcelDoc)manager.CreateDoc(manager.ProcessingDir.GetFiles(fixture.ExcelSampleName).ToList()[0]);
            var initial = doc.GetEffectiveDate();
            doc.EffectiveDate = "2020-04-12";
            var result = doc.GetEffectiveDate();
            manager.Dispose();
            Assert.AreEqual("2018-11-26", initial);
            Assert.AreEqual("2020-04-12", result);
            ExcelDoc wbk = (ExcelDoc)manager.CreateDoc(manager.ProcessingDirFiles[1]);
        }

        [TestMethod()]
        public void EffectiveDateTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            manager.ConfigDir(fixture.Sop1Documents.FullName);
            ExcelDoc doc = (ExcelDoc)manager.CreateDoc(manager.ProcessingDir.GetFiles(fixture.ExcelSampleName).ToList()[0]);
            Assert.AreEqual(
                "2018-11-26",
                doc.GetEffectiveDate()
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