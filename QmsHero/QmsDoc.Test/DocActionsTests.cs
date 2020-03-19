using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Test
{
    [TestClass()]
    public class DocActionsTests
    {
        [TestMethod()]
        public void SetPropertyTest()
        {
            var docActions1 = new DocActionControls();
            docActions1.Revision = "2";
            var docActions2 = new DocActionControls();
            docActions2.SetProperty(docActions1, "Revision");
            Assert.AreEqual(docActions1.Revision, docActions2.Revision);
            Assert.AreEqual(docActions2.Revision, "2");
        }

        [TestMethod()]
        public void IsValidPropertyTest()
        {
            var docActions = new DocActionControls();
            var revInfo = docActions.GetType().GetProperty("Revision");
            Assert.IsFalse(docActions.IsValidProperty(revInfo));
            docActions.Revision = "3";
            Assert.IsTrue(docActions.IsValidProperty(revInfo));
            docActions.Revision = null;
            Assert.IsFalse(docActions.IsValidProperty(revInfo));
        }

        [TestMethod()]
        public void AsDictTest()
        {
            var docActions = new DocActionControls();
            var dict = docActions.AsDict();
            Assert.AreEqual(dict.Count, 0);
            docActions.Revision = "2";
            docActions.EffectiveDate = "2020-03-11";
            dict = docActions.AsDict();
            Assert.AreEqual(dict.Count, 2);
        }
    }
}