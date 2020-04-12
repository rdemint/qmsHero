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
            manager.ConfigDir(fixture.Sop1Documents.FullName);
            WordDoc doc = (WordDoc)manager.CreateDoc(fixture.WordSample);
            var initial = doc.FetchEffectiveDate();
            doc.EffectiveDate = "2020-04-12";
            var result = doc.FetchEffectiveDate();
            manager.Dispose();
            Assert.AreEqual("2019-11-05", initial);
            Assert.AreEqual("2020-04-12", result);
        }

        [TestMethod()]
        public void GetRevisionTest()
        {
            var fixture = new FixtureUtil();
            var manager = new DocManager();
            manager.ConfigDir(fixture.Sop1Documents.FullName);
            WordDoc doc = (WordDoc)manager.CreateDoc(fixture.WordSample);
            var initial = doc.FetchRevision();
            doc.Revision = "3";
            var changeResult = doc.FetchRevision();
            manager.Dispose();
            Assert.AreEqual("4", initial);
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