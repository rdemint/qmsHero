using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Test;
using QmsDoc.Docs.Word;
using QmsDocXml.Docs.Word.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Docs.Word.Properties.Tests
{
    [TestClass()]
    public class EffectiveDateTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            string actual = "2019-11-05";
            string effDate = "2020-20-20";
            var fixture = new FixtureUtil();
            var doc = new WordDoc(fixture.WordSample);
            var prop = new EffectiveDate();
            string result = doc.Inspect(prop).Value;
            Assert.AreEqual(result, actual);
        }
    }
}