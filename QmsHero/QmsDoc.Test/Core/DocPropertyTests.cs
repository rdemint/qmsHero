﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core.Tests
{
    [TestClass()]
    public class DocPropertyTests
    {

        [TestMethod()]
        public void DocPropertyTest()
        {
            var prop = new DocProperty();
            Assert.AreEqual(null, prop.Value);

            var prop2 = new DocProperty("hello");
            Assert.AreEqual("hello", prop2.Value);
        }

        [TestMethod()]
        public void IsSetTest()
        {
            var prop = new DocProperty();
            Assert.AreEqual(false, prop.IsSet);
            prop.Value = "4";
            Assert.AreEqual(false, prop.IsSet);
        }


        
    }
}