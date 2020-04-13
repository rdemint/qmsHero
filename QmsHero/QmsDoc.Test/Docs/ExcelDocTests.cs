using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs;
using QmsDoc.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Tests
{
    [TestClass()]
    public class ExcelDocTests
    {
        //[TestMethod()]
        //public void RevisionTest()
        //{

        //    string effDate = "2020-04-12";
        //    var fixture = new FixtureUtil();
        //    ExcelDoc doc = new ExcelDoc(fixture.ExcelSample);
        //    var initial = doc.FetchEffectiveDate();
        //    DocState docEdit = new DocState();

        //    docEdit.DocHeader.EffectiveDate.Value = effDate;

        //    doc.Process(docEdit, fixture.ProcessingDir);
        //    var result = doc.FetchEffectiveDate();
        //    Assert.AreEqual("2019-11-05", initial);
        //    Assert.AreEqual(effDate, result);
        //}

        //[TestMethod()]
        //public void EffectiveDateTest()
        //{
        //    var fixture = new FixtureUtil();
        //    var manager = new DocManager();
        //    manager.ConfigDir(fixture.Sop1Documents.FullName);

        //    Assert.AreEqual(
        //        "2018-11-26",
        //        doc.FetchEffectiveDate()
        //        );
        //    manager.Dispose();
        //}

    }
}