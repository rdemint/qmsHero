﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core.Test
{
    [TestClass()]
    public class DocActionControlsTests
    {

        [TestMethod()]
        public void SetPropertyTest()
        {
            var docActions1 = new DocActionControlManager();
            docActions1.Revision = new ControlCheckTextBox("Revision", "2");
            var docActions2 = new DocActionControlManager();
            docActions2.SetProperty(docActions1, "Revision");
            Assert.AreEqual(docActions1.Revision, docActions2.Revision);
            Assert.AreEqual(docActions2.Revision.DocActionVal, "2");
        }

        [TestMethod()]
        public void IsValidPropertyTest()
        {
            var docActions = new DocActionControlManager();
            var revInfo = docActions.GetType().GetProperty("Revision");
            Assert.IsFalse(docActions.IsValidProperty(revInfo));
            docActions.Revision = new ControlCheckTextBox("Revision", "2");
            Assert.IsTrue(docActions.IsValidProperty(revInfo));
            docActions.Revision = new ControlCheckTextBox("Revision", null); ;
            Assert.IsFalse(docActions.IsValidProperty(revInfo));
        }

        //[TestMethod()]
        //public void AsDictTest()
        //{
        //    var docActions = new DocActionControls();
        //    var dict = docActions.AsDict();
        //    Assert.AreEqual(dict.Count, 0);
        //    docActions.Revision = "2";
        //    docActions.EffectiveDate = "2020-03-11";
        //    dict = docActions.AsDict();
        //    Assert.AreEqual(dict.Count, 2);
        //}

        [TestMethod()]
        public void ToDocActionControlListTest()
        {
            var actionControls = new DocActionControlManager();
            var actionControlsList = actionControls.ToControlList();
            Assert.IsTrue(actionControlsList.Count >= 4);

            var filterDict = new Dictionary<string, object>();
            filterDict.Add("Revision", "1");
            filterDict.Add("EffectiveDate", "2020-03-01");
            actionControlsList = actionControls.ToControlList(filterDict);
            Assert.AreEqual(actionControlsList.Count, 2);
        }
    }
}