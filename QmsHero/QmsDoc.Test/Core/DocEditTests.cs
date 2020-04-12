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
    public class DocEditTests
    {
        [TestMethod()]
        public void ToCollectionTest()
        {
            var docEdit = new DocEdit();
            var props = docEdit.ToCollection();
            Assert.IsTrue(props.Any() == false);

            docEdit.DocHeader.Revision.Value = "2";
            props = docEdit.ToCollection();
            Assert.IsTrue(props.Count == 1);

            docEdit.DocHeader.EffectiveDate.Value = "2020-03-12";
            props = docEdit.ToCollection();
            Assert.IsTrue(props.Count == 2);

            docEdit.DocHeader.EffectiveDate.Value = null;
            props = docEdit.ToCollection();
            Assert.IsTrue(props.Count == 1);
        }

        [TestMethod()]
        public void FilterCollectionTest()
        {
            var docEdit = new DocEdit();
            var props = docEdit.ToCollection();
            Assert.IsTrue(props != null);
        }
    }
}