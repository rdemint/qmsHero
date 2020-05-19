using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
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
    public class DocumentPropertyCompanyTests
    {
        [TestMethod()]
        public void WriteWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(new DocumentPropertyCompany("QA LADDER LLC"));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.State, "QA LADDER LLC");
        }

        [TestMethod()]
        public void ReadExcelTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = doc.Inspect(new DocumentPropertyCompany());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Value.State, "");
        }

        [TestMethod]
        public void WriteDirTest()
        {
            var fixture = new XmlFixture();
            var manager = new DocManager(fixture);
            var resultDocCollection = manager.Process(new DocumentPropertyCompany("Lean RAQA Systems"));
            Assert.IsFalse(resultDocCollection.HasErrors());
            foreach (var doc in resultDocCollection)
            {
                Assert.IsFalse(doc.UpdatePropertyProcessingErrors());
            }
            foreach (var docFileInfo in manager.FileManager.ProcessingFiles)
            {
                var docResult = manager.DocFactory.CreateDoc(docFileInfo);
                if (docResult.IsSuccess)
                {
                    var resultInspect = docResult.Value.Inspect(new DocumentPropertyCompany());
                    Assert.AreEqual("Lean RAQA Systems", resultInspect.Value.State);
                }
            }
        }

        //[TestMethod]
        //public void ReadDirTest()
        //{
        //    var fixture = new XmlFixture();
        //    var manager = new DocManager(fixture);
        //    var resultDocCollection = manager.Process(new DocumentPropertyCompany("Lean RAQA Systems"));
        //    Assert.IsFalse(resultDocCollection.HasErrors());
        //    foreach (var doc in resultDocCollection)
        //    {
        //        Assert.IsFalse(doc.HasPropertyProcessingErrors());
        //    }
        //    foreach (var docFileInfo in manager.FileManager.ProcessingFiles)
        //    {
        //        var docResult = manager.DocFactory.CreateDoc(docFileInfo);
        //        if(docResult.IsSuccess)
        //        {
        //            var resultInspect = docResult.Value.Inspect(new DocumentPropertyCompany());
        //            Assert.AreEqual("Lean RAQA Systems", resultInspect.Value.State);
        //        }
        //    }
        //}


    }
}