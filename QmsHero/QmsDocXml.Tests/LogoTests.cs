using System;
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
        }
    }
}
