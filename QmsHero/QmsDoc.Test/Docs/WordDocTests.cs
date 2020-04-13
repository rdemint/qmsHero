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
            string initial = (string)state.EffectiveDate.Value;
            Assert.AreEqual("2019-11-05", initial);

            state.EffectiveDate.Value = effDate;
            doc.Process(state, fixture.ProcessingDir);
            string result = (string)doc.Inspect().EffectiveDate.Value;
            Assert.AreEqual(effDate, result);
        }

        [TestMethod()]
        public void RevisionTest()
        {
            string rev = "20";
            var fixture = new FixtureUtil();
            WordDoc doc = new WordDoc(fixture.WordSample);
            var state = doc.Inspect();
            string initial = (string)state.Revision.Value;
            Assert.AreEqual("2", initial);

            state.Revision.Value = rev;
            doc.Process(state, fixture.ProcessingDir);
            string result = (string)doc.Inspect().Revision.Value;
            Assert.AreEqual(rev, result);
        }

        //[TestMethod()]
        //public void InspectTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SaveAsPdfTest()
        //{
        //    foreach(WordDoc in )
        //}
    }
}