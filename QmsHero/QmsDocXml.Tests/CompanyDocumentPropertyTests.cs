using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Word;
using QmsDocXml;
using QmsDocXml.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml
{
    [TestClass()]
    public class CompanyDocumentPropertyTests
    {
        [TestMethod()]
        public void WriteWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(new CompanyDocumentProperty("QA LADDER LLC"));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.State, "QA LADDER LLC");
        }

        [TestMethod()]
        public void ReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(new CompanyDocumentProperty());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.State, "");
        }
    }
}