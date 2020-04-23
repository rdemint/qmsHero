using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Word;
using QmsDoc.Docs.Excel;

namespace QmsDoc.Tests.Docs.Common.Properties
{
    [TestClass()]
    public class IsSopTests
    {
        [TestMethod()]
        public void IsSopTest()
        {
            var sop = new IsSop(true);
            Assert.AreEqual(sop.State, true);
            Assert.ThrowsException<ArgumentException>(()=> new IsSop("true"));
        }

        [TestMethod]
        public void ReadTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.CopyToProcessingDir(fixture.WordSample));
            bool result = (bool)doc.Inspect(new IsSop()).State;
            Assert.AreEqual(true, result);
            //var xl = new ExcelDoc(fixture.CopyToProcessingDir(fixture.ExcelSample));
            //bool xlResult = (bool)xl.Inspect(new IsSop(), xl.FileInfo).State;
            //Assert.AreEqual(false, xlResult);
        }
    }
}