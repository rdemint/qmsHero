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
        public void EffectiveDateTest()
        {
            string effDate = "2020-04-12";
            var fixture = new FixtureUtil();
            WordDoc doc = new WordDoc(fixture.WordSample);
            var state = doc.Inspect();
            var initial = state.DocHeader.EffectiveDate.Value;
            Assert.AreEqual("2019-11-05", initial);

            state.DocHeader.EffectiveDate.Value = effDate;
            doc.Process(state, fixture.ProcessingDir);
            var result = doc.Inspect();
            Assert.AreEqual(effDate, result);
        }

        [TestMethod()]
        public void RevisionTest()
        {
            string rev = "20";
            var fixture = new FixtureUtil();
            WordDoc doc = new WordDoc(fixture.WordSample);
            var initial = doc.FetchEffectiveDate();
            DocState docEdit = new DocState();

            docEdit.DocHeader.Revision.Value = rev;

            doc.Process(docEdit, fixture.ProcessingDir);
            var result = doc.FetchRevision();
            Assert.AreEqual("2", initial);
            Assert.AreEqual(rev, result);
        }

        [TestMethod()]
        public void InspectTest()
        {
            Assert.Fail();
        }

        //[TestMethod()]
        //public void SaveAsPdfTest()
        //{
        //    foreach(WordDoc in )
        //}
    }
}