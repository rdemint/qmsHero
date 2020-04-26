using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;

namespace QmsDocXml.Tests
{
    [TestClass]
    public class LogoTests
    {
        [TestMethod]
        public void ReadTest()
        {
            var fixture = new Fixture();
            
            //word
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)doc.Inspect(new Logo()).State;
            Assert.AreEqual("GT Medical Logo II.jpg", result);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            result = (string)xl.Inspect(new Logo()).State;
            Assert.AreEqual("GT Medical Logo II", result);
        }

        [TestMethod]
        public void WriteTest()
        {
            var fixture = new Fixture();

            //word
            //var doc = new WordDoc(fixture.WordSampleCopy);
            //var initial = (string)doc.Inspect(new Logo()).State;
            //Assert.AreEqual("GT Medical Logo II.jpg", initial);

            //var logo = new Logo(fixture.LogoSampleJpgCopy.FullName);
            //doc.Process(logo);
            //var result = (string)doc.Inspect(new Logo()).State;
            //Assert.AreEqual(fixture.LogoSampleJpgCopy.Name, result);

            //excel
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            xl.Process(new Logo(fixture.LogoSampleJpgCopy));
            var xlResult = (string)xl.Inspect(new Logo()).State;
            Assert.AreEqual(fixture.LogoSampleJpgCopy.Name, xlResult);
        }

        [TestMethod]
        public void ImageStyleRegexTest()
        {
            string myS = "position:absolute;margin-left:0;margin-top:0;width:79.5pt;height:28.5pt;   z-index:1";
            Match widthMatch = Regex.Match(myS, @"width:.*pt");
            Match heightMatch = Regex.Match(myS, @"height:.*pt");
            Assert.AreEqual(widthMatch.Success, true);
            Assert.AreEqual(heightMatch.Success, true);

        }

        [TestMethod]
        public void StyleParseTest()
        {
            string hS = "height:23.5pt";
            string wS = "width:32.0pt";
            string h = "23.5";
            double dh = 23.5;
            Assert.AreEqual(double.Parse(h), dh);
            
        }
    }
}
