using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;

namespace QmsDocXml.Tests
{
    [TestClass]
    public class HeaderLogoTests
    {
        [TestMethod]
        public void ReadExcelTest()
        {

            var fixture = new Fixture();
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)xl.Inspect(new HeaderLogo()).State;
            Assert.AreEqual("GT Medical Logo II", result);
        }

        [TestMethod]
        public void ReadWordTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = (string)doc.Inspect(new HeaderLogo()).State;
            Assert.AreEqual("GT Medical Logo II.jpg", result);
        }

        [TestMethod]
        public void WriteWordTest()
        {
            var fixture = new Fixture();

            var logo = new HeaderLogo(fixture.LogoSampleJpgCopy.FullName);
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(logo);
            var result = (string)doc.Inspect(new HeaderLogo()).State;
            Assert.AreEqual(fixture.LogoSampleJpgCopy.Name, result);
        }

        [TestMethod]
        public void WriteExcelTest()
        {
            var fixture = new Fixture();
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            xl.Process(new HeaderLogo(fixture.LogoSampleJpgCopy.FullName));
            var xlResult = (string)xl.Inspect(new HeaderLogo()).State;
            Assert.AreEqual(fixture.LogoSampleJpgCopy.Name, xlResult);
        }
    }
}
