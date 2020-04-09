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
            var initial = doc.GetEffectiveDate();
            doc.EffectiveDate = "2020-04-12";
            var result = doc.GetEffectiveDate();
            manager.Dispose();
            Assert.AreEqual("2018-11-26", initial);
            Assert.AreEqual("2020-04-12", result);

        }

        [TestMethod()]
        public void GetRevisionTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            var doc = (WordDoc)manager.CreateDoc(fixture.WordSample);
            var initial = doc.GetRevision();
            doc.Revision = "3";
            var changeResult = doc.GetRevision();
            manager.Dispose();
            Assert.AreEqual("2", initial);
            Assert.AreEqual("3", changeResult);
        }

        [TestMethod()]
        public void OpenDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            int preCount = manager.WordApp.Documents.Count;
            manager.CreateDoc(fixture.WordSample);
            int postCount = manager.WordApp.Documents.Count;
            manager.Dispose();
            Assert.AreEqual(0, preCount);
            Assert.AreEqual(1, postCount);
        }

        [TestMethod()]
        public void CloseDocumentTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            
            var doc = manager.CreateDoc(fixture.WordSample);
            int preCount = manager.WordApp.Documents.Count;
            doc.CloseDocument();
            int postCount = manager.WordApp.Documents.Count;
            manager.Dispose();
            Assert.AreEqual(1, preCount);
            Assert.AreEqual(0, postCount);
        }

        //[TestMethod()]
        //public void SaveAsPdfTest()
        //{
        //    foreach(WordDoc in )
        //}
    }
}