using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDocXml.Docs.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Docs.Common.Properties.Tests
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
    }
}