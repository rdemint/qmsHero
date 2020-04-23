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
            string actual = "3";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSample);
            var prop = new Revision();
            string result = (string)doc.Inspect(prop).State;
            Assert.AreEqual(result, actual);

            actual = "2";
            var xl = new ExcelDoc(fixture.CopyToProcessingDir(fixture.ExcelSample));
            result = (string)xl.Inspect(new Revision()).State;
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.CopyToProcessingDir(fixture.WordSample));

            string actual = "3";
            string rev = "20";
            Assert.AreEqual(actual, (string)doc.Inspect(new Revision()).State);
            var prop = new Revision(rev);
            doc.Process(prop);
            string result = (string)doc.Inspect(new Revision()).State;
            Assert.AreEqual(result, rev);
        }
    }
}
