using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs.Word;
using QmsDocXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests
{
    [TestClass()]
    public class EffectiveDateTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            string actual = "2018-11-26";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSample);
            var prop = new EffectiveDate();
            string result = (string)doc.Inspect(prop).State;
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void WriteTest()
        {
            var manager = new DocManager();
            string actual = "2018-11-26";
            string effDate = "2020-20-20";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSample);
            Assert.AreEqual(actual, (string)doc.Inspect(new EffectiveDate()).State);
            var prop = new EffectiveDate(effDate);
            var newDoc = (WordDoc)doc.Process(prop, fixture.ProcessingDir);
            string result = newDoc.Inspect(new EffectiveDate()).State.ToString(); 
            Assert.AreEqual(result, actual);
        }

    }
}