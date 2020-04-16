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
    public class DocEditTests
    {
        [TestMethod()]
        public void ToCollectionTest()
        {
            var docEdit = new QDocState();
            var props = docEdit.ToCollection();
            Assert.IsTrue(props.Any() == false);

            docEdit.Revision.Value = "2";
            props = docEdit.ToCollection();
            Assert.IsTrue(props.Count == 1);

            docEdit.EffectiveDate.Value = "2020-03-12";
            props = docEdit.ToCollection();
            Assert.IsTrue(props.Count == 2);

            docEdit.EffectiveDate.Value = null;
            props = docEdit.ToCollection();
            Assert.IsTrue(props.Count == 1);
        }

        [TestMethod()]
        public void FilterCollectionTest()
        {
            var docEdit = new QDocState();
            var props = docEdit.ToCollection();
            Assert.IsTrue(props != null);
        }
    }
}