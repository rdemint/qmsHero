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

            var fixture = new XmlFixture();
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = xl.Inspect(new HeaderLogo());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual("GT Medical Logo II", (string)result.Value);
        }

        [TestMethod]
        public void ReadWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(new HeaderLogo());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual("GT Medical Logo II.jpg", (string)result.Value);
        }

        [TestMethod]
        public void WriteWordTest()
        {
            var fixture = new XmlFixture();

            var logo = new HeaderLogo(fixture.LogoSampleJpgCopy.FullName);
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Process(logo);
            Assert.IsTrue(result.IsSuccess);

            result = doc.Inspect(new HeaderLogo());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(fixture.LogoSampleJpgCopy.Name, (string)result.Value);
        }

        [TestMethod]
        public void WriteExcelTest()
        {
            var fixture = new XmlFixture();
            var xl = new ExcelDoc(fixture.ExcelSampleCopy);
            xl.Process(new HeaderLogo(fixture.LogoSampleJpgCopy.FullName));
            var result = xl.Inspect(new HeaderLogo());
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(fixture.LogoSampleJpgCopy.Name, (string)result.Value);
        }
    }
}
