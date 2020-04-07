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
        public void GetRevisionTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();

            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual("Revision: 2", wbk.GetRevision());
        }

        [TestMethod()]
        public void GetEffectiveDateTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual("EffectiveDate: 2018-11-26", wbk.GetEffectiveDate());
        }

        [TestMethod()]
        public void OpenDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            Assert.AreEqual(0, manager.ExcelApp.Workbooks.Count);
            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual(1, manager.ExcelApp.Workbooks.Count);

        }

        [TestMethod()]
        public void CloseDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();

            var wbk = manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual(1, manager.ExcelApp.Workbooks.Count);
            wbk.CloseDocument();
            Assert.AreEqual(0, manager.ExcelApp.Workbooks.Count);
        }
    }
}