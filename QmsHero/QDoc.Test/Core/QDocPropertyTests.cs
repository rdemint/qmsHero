using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core.Tests
{
    [TestClass()]
    public class QDocPropertyTests
    {

        [TestMethod()]
        public void DocPropertyTest()
        {
            var prop = new QDocProperty();
            Assert.AreEqual(null, prop.State);
            //Assert.AreEqual("QDocProperty")

            var prop2 = new QDocProperty("hello");
            Assert.AreEqual("hello", prop2.State);
        }



        
    }
}