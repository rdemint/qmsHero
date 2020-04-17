
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Core;
using QDoc.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Test.Core
{
    [TestClass()]
    public class QDocPropertyTests
    {

        [TestMethod()]
        public void DocPropertyTest()
        {
            var prop = new QDocPropertyTestClass();
            Assert.AreEqual(null, prop.State);

            Assert.AreEqual("QDocPropertyTestClass", prop.Name);

            var prop2 = new QDocPropertyTestClass("hello");
            Assert.AreEqual("hello", prop2.State);


        }



        
    }
}