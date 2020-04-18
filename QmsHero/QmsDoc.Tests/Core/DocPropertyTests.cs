using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Tests.Core;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Tests.Core
{
    [TestClass()]
    public class DocPropertyTests
    {
        [TestMethod()]
        public void DocPropertyTest()
        {
            DocProperty prop = new DocPropertyTestClass();
            Assert.AreEqual("DocPropertyTestClass", prop.Name);
            Assert.AreEqual(null, prop.State);

            DocProperty prop2 = new DocPropertyTestClass("4");
            Assert.AreEqual("4", prop2.State.ToString());

            DocProperty prop3 = new DocPropertyTestClass(true);
            Assert.AreEqual(true, (bool)prop3.State);

        }
    }
}