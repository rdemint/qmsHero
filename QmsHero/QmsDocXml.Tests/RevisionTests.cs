
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml;

namespace QmsDocXml.Tests
{
    [TestClass]
    public class RevisionTests
    {
        [TestMethod]
        public void ReadTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var prop = new Revision();
            string result = (string)doc.Inspect(prop).State;
            Assert.AreEqual(result, fixture.WordSampleRevision);

            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = (string)xl.Inspect(new Revision()).State;
            Assert.AreEqual(result, fixture.ExcelSampleRevision);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new Fixture();
            
            //word
            var doc = new WordDoc(fixture.WordSampleCopy);

            string rev = "20";
            Assert.AreEqual(fixture.WordSampleRevision, (string)doc.Inspect(new Revision()).State);
            var prop = new Revision(rev);
            doc.Process(prop);
            string result = (string)doc.Inspect(new Revision()).State;
            Assert.AreEqual(result, rev);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = (string)xl.Inspect(new Revision()).State;
            Assert.AreEqual(result, fixture.ExcelSampleRevision);

            rev = "12";
            xl.Process(new Revision(rev));
            result = (string)xl.Inspect(new Revision()).State;
            Assert.AreEqual(rev, result);

        }
    }
}
