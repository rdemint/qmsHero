using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Test;
using QmsDoc.Core;
using LadderFileUtils;
using QmsDoc.Word;

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
            var targetDoc = doc.Process(state, fixture.ProcessingDir);
            string result = (string)targetDoc.Inspect().EffectiveDate.Value;
            Assert.AreEqual(effDate, result);
        }

        [TestMethod()]
        public void RevisionTest()
        {
            string rev = "60";
            var fixture = new FixtureUtil();
            WordDoc doc = new WordDoc(fixture.WordSample);
            string initialRev = doc.Inspect(new Revision()).Value;
            Assert.AreEqual("4", initialRev);

            newRev = new Revision("4");
            var targetDoc = doc.Process(state, fixture.ProcessingDir);
            string result = (string)targetDoc.Inspect().Revision.Value;
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