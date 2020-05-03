using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.Actions;
using QmsDocXml.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests.Actions
{
    [TestClass()]
    public class RenameDocumentTests
    {
        [TestMethod()]
        public void InspectExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(new RenameDocument());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.ResultCollection.First().Value.State.ToString(), fixture.ExcelSampleDocName);
            Assert.AreEqual(result.Value.ResultCollection.Last().Value.State.ToString(), fixture.ExcelSampleDocName);

        }
    }
}