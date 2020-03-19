using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Core;
using QmsDoc.Interfaces;
using QmsDoc.Docs;

namespace QmsDoc.Test.Core
{
    [TestClass()]
    public class DocManagerTests
    {
        [TestMethod()]
        public void ProcessDocTest()
        {
            var l = new List<IDocActionControl>();
            l.Add(new ControlTextBox("Revision", "1"));
            l.Add(new ControlTextBox("EffectiveDate", "2020-03-12"));
            var manager = new DocManager();
            var doc = new WordDoc();
            manager.ProcessDoc(doc, l);
            Assert.AreEqual(doc.Revision, "1");
            Assert.AreEqual(doc.EffectiveDate, "2020-03-12");
        }

        [TestMethod()]
        public void FilterControlsTest()
        {
            var l = new List<IDocActionControl>();
            l.Add(new ControlTextBox("Revision", null));
            l.Add(new ControlTextBox("EffectiveDate", ""));
            l.Add(new ControlTextBox("EffectiveDate", "2020-03-12"));
            var manager = new DocManager();
            var controls = manager.FilterControls(l);
            Assert.IsTrue(controls.Count == 1);
        }
    }
}