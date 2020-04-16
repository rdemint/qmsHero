using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Test;
using QmsDoc.Core;
using QWordDoc;

namespace QmsDoc.Docs.Tests
{
    [TestClass()]
    public class WordDocTests
    {
        [TestMethod()]
        public void EffectiveDateTest()
        {
            string expected = "2019-11-05";
            string setValue = "2020-20-20";
            var fixture = new FixtureUtil();
            WordDoc doc = new WordDoc(fixture.WordSample);
            //Get
            string initial = doc.Inspect(new EffectiveDate()).Value;
            Assert.AreEqual(expected, initial);
            //Set
            EffectiveDate newDate = new EffectiveDate(setValue);
            WordDoc newDoc = (WordDoc)doc.Process(newDate, fixture.ProcessingDir);
            string result = newDoc.Inspect(new EffectiveDate()).Value;
            Assert.AreEqual(setValue, result);

            //var targetDoc = doc.Process(state, fixture.ProcessingDir);
            //string result = (string)targetDoc.Inspect().Revision.Value;
            //Assert.AreEqual(rev, result);
        }

        [TestMethod()]
        public void InspectTest()
        {
            var fixture = new FixtureUtil();
            WordDoc doc = new WordDoc(fixture.WordSample);
            EffectiveDate rev = new EffectiveDate();
            var result = (EffectiveDate)doc.Inspect(rev);
            Assert.AreEqual(typeof(EffectiveDate), result.GetType());
        }

        [TestMethod()]
        public void QmsWordDocPropertiesTest()
        {
            WordDocConfig config = new WordDocConfig();

            string propRef = config.PropertyReferenceName("EffectiveDate");
            Type myType = Type.GetType(propRef);
            EffectiveDate rev = new EffectiveDate();
            Assert.AreEqual(rev.GetType(), myType); 
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