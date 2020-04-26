using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Word;
using QmsDoc.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Tests.Docs.Common.Properties
{
    [TestClass()]
    public class IsFormTests
    {
        [TestMethod()]
        public void IsFormTest()
        {
            var isForm = new IsForm(true);
            Assert.AreEqual(isForm.State, true);
            Assert.ThrowsException<ArgumentException>(() => new IsForm("false"));
        }

        [TestMethod]
        public void ReadTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.CopyToProcessingDir(fixture.WordSampleCopy));
            bool result = (bool)doc.Inspect(new IsForm()).State;
            Assert.AreEqual(false, result);
            //var xl = new ExcelDoc(fixture.CopyToProcessingDir(fixture.ExcelSample));
            //bool xlResult = (bool)xl.Inspect(new IsForm(), xl.FileInfo).State;
            //Assert.AreEqual(false, xlResult);
        }
    }
}