using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Core;
using QDoc.Docs.Excel;
using QDoc.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;

namespace QDoc.Test.Docs.Excel
{
    [TestClass()]
    public class ExcelDocTests
    {
        //[TestMethod]
        //public void HeaderTest()
        //{
        //    var fixture = new FixtureUtil();
        //    using (SpreadsheetDocument document = SpreadsheetDocument.Open(fixture.ExcelSample.FullName, false))
        //    {
        //        var ws = document.WorkbookPart.WorksheetParts.First().Worksheet;
        //        var xl = new QExcelDoc(fixture.ExcelSample);
        //        var first = ws.Elements<HeaderFooter>().First().DifferentFirst;
        //        var odd = ws.Elements<HeaderFooter>().First().DifferentOddEven;
        //        var innertext = ws.Descendants<HeaderFooter>().First().OddHeader.InnerText;
        //        Assert.IsTrue(innertext != null);
        //    }


        //}
        //[TestMethod()]
        //public void InspectTest()
        //{
        //    var fixture = new FixtureUtil();
        //    var xl = new ExcelDoc(fixture.ExcelSample);
        //    var props = xl.GetType().GetProperties();
        //    var activeState = xl.Inspect();
        //    Assert.AreEqual(props.Count(), activeState.ToCollection().Count);
        //}

        //[TestMethod()]
        //public void EffectiveDateTest()
        //{
        //    string effDate = "2020-04-12";
        //    var fixture = new FixtureUtil();
        //    ExcelDoc doc = new ExcelDoc(fixture.ExcelSample);
        //    var state = doc.Inspect();
        //    var initial = state.EffectiveDate.Value;
        //    Assert.AreEqual("2018-11-26", initial);
            
        //    state.EffectiveDate.Value = effDate;
        //    var targetDoc = doc.Process(state, fixture.ProcessingDir);
        //    var result = (string)targetDoc.Inspect().EffectiveDate.Value;
        //    Assert.AreEqual(effDate, result);
        //}

        //[TestMethod()]
        //public void RevisionTest()
        //{
        //    string rev = "20";
        //    var fixture = new FixtureUtil();
        //    ExcelDoc doc = new ExcelDoc(fixture.ExcelSample);
        //    var state = doc.Inspect();
        //    var initial = state.Revision.Value;
        //    Assert.AreEqual("2", initial);

        //    state.Revision.Value = rev;
        //    var targetDoc = doc.Process(state, fixture.ProcessingDir);
        //    var result = (string)targetDoc.Inspect().Revision.Value;
        //    Assert.AreEqual(rev, result);
        //}

    }
}