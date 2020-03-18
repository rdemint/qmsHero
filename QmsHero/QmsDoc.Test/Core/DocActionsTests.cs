using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core.Tests
{
    [TestClass()]
    public class DocActionsTests
    {
        [TestMethod()]
        public void ToDocActionControlListTest()
        {
            var filterDict = new Dictionary<string, object>();
            filterDict.Add("Revision", "1");
            filterDict.Add("EffectiveDate", "2020-03-01");
            var docActions = new DocActions();
            var actionControls = docActions.ToDocActionControlList(filterDict);
            Assert.AreEqual(actionControls.Count, 2);
        }
    }
}