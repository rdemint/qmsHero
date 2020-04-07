using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Test;
using QmsDoc.Core;

namespace QmsDoc.Docs.Tests
{
    [TestClass()]
    public class WordDocTests
    {
        [TestMethod()]
        public void GetEffectiveDateTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            var doc = (WordDoc)manager.CreateDoc(fixture.WordSample);
            Assert.AreEqual("Effective Date: 2018-11-26", doc.GetEffectiveDate());

            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual("EffectiveDate: 2018-11-26", wbk.GetEffectiveDate());
        }

        [TestMethod()]
        public void GetRevisionTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            var doc = (WordDoc)manager.CreateDoc(fixture.WordSample);
            Assert.AreEqual("Revision: 2", doc.GetRevision());

            var wbk = (ExcelDoc)manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual("Revision: 2", wbk.GetRevision());

        }

        [TestMethod()]
        public void OpenDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            Assert.AreEqual(0, manager.WordApp.Documents.Count);
            Assert.AreEqual(0, manager.ExcelApp.Workbooks.Count);
            var doc = manager.CreateDoc(fixture.WordSample);
            Assert.AreEqual(1, manager.WordApp.Documents.Count);
            manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual(1, manager.ExcelApp.Workbooks.Count);
        }

        [TestMethod()]
        public void CloseDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            
            var doc = manager.CreateDoc(fixture.WordSample);
            Assert.AreEqual(1, manager.WordApp.Documents.Count);
            doc.CloseDocument();
            Assert.AreEqual(0, manager.WordApp.Documents.Count);
            
            var wbk = manager.CreateDoc(fixture.ExcelSample);
            Assert.AreEqual(1, manager.ExcelApp.Workbooks.Count);
            wbk.CloseDocument();
            Assert.AreEqual(0, manager.ExcelApp.Workbooks.Count);


        }

        //[TestMethod()]
        //public void SaveAsPdfTest()
        //{
        //    foreach(WordDoc in )
        //}
    }
}