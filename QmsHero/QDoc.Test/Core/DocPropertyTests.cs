﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core.Tests
{
    [TestClass()]
    public class DocPropertyTests
    {

        [TestMethod()]
        public void DocPropertyTest()
        {
            var prop = new QDocProperty();
            Assert.AreEqual(null, prop.Value);

            var prop2 = new QDocProperty("hello");
            Assert.AreEqual("hello", prop2.Value);
        }

        [TestMethod()]
        public void IsSetTest()
        {
            var prop = new QDocProperty();
            Assert.AreEqual(false, prop.IsSet);
            prop.Value = "4";
            Assert.AreEqual(false, prop.IsSet);
        }


        
    }
}