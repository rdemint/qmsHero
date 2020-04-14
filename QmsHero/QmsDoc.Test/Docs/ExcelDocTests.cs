using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using QmsDoc.Docs;
using QmsDoc.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;

namespace QmsDoc.Docs.Tests
{
    [TestClass()]
    public class ExcelDocTests
    {
        [TestMethod]
        public void HeaderTest()
        {
            var fixture = new FixtureUtil();
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fixture.ExcelSample.FullName, false))
            {
                var ws = document.WorkbookPart.WorksheetParts.First().Worksheet;
                var xl = new ExcelDoc(fixture.ExcelSample);
                var first = ws.Elements<HeaderFooter>().First().DifferentFirst;
                var odd = ws.Elements<HeaderFooter>().First().DifferentOddEven;
                var innerxml = ws.Descendants<HeaderFooter>().First().OddHeader.InnerXml;
                var innertext = ws.Descendants<HeaderFooter>().First().OddHeader.InnerText;
                Match match = Regex.Match(innerxml, xl.DocConfig.RevisionText + @"\d{0,2}");
                Match matchEff = Regex.Match(innerxml, @"\d\d\d\d-\d\d-\d\d");
                var result = match.ToString();
                Assert.IsTrue(innertext != null);
            }


        }
        //[TestMethod()]
        //public void InspectTest()
        //{
        //    var fixture = new FixtureUtil();
        //    var xl = new ExcelDoc(fixture.ExcelSample);
        //    var props = xl.GetType().GetProperties();
        //    var activeState = xl.Inspect();
        //    Assert.AreEqual(props.Count(), activeState.ToCollection().Count);
        //}

        [TestMethod()]
        public void EffectiveDateTest()
        {
            string effDate = "2020-04-12";
            var fixture = new FixtureUtil();
            ExcelDoc doc = new ExcelDoc(fixture.ExcelSample);
            var state = doc.Inspect();
            var initial = state.EffectiveDate.Value;
            state.EffectiveDate.Value = effDate;

            doc.Process(state, fixture.ProcessingDir);
            var result = doc.Inspect().EffectiveDate.Value;
            Assert.AreEqual("2019-11-05", initial);
            Assert.AreEqual(effDate, result);
        }

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