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
            var doc = new WordDoc(fixture.WordSampleCopy);
            var result = doc.Inspect(new IsSop());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(true, result.Value.State.ToString());
        }
    }
}