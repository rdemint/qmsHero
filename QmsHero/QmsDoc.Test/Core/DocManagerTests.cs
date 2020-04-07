using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Core;
using QmsDoc.Interfaces;
using QmsDoc.Docs;
using System.Collections.ObjectModel;
using QmsDoc.Test;

namespace QmsDoc.Core.Tests
{
    [TestClass()]
    public class DocManagerTests
    {
        FixtureUtil fixture = new FixtureUtil();

        [TestMethod()]
        public void ConfigDirTest()
        {
            var manager = new DocManager();
            manager.ConfigDir(fixture.ActiveQMSDocuments.FullName);

            var processDirs = fixture.FixtureDir.GetDirectories("Processing");
            Assert.IsTrue(processDirs.Length == 1);
        }
    }
}

namespace QmsDoc.Test.Core
{
    [TestClass()]
    public class DocManagerTests
    {
        [TestMethod()]
        public void ProcessDocTest()
        {
            var docEdit = new DocEdit();
            docEdit.DocPropertiesCollection = new ObservableCollection<DocProperty>
            {
                new DocProperty("Revision", "1"),
                new DocProperty("EffectiveDate", "2020-03-30"),
            };
            var fixture = new FixtureUtil();

            var manager = new DocManager();
            var doc = manager.CreateDoc(fixture.WordSample);
            doc = manager.ProcessDoc(doc, docEdit);
            Assert.AreEqual(doc.Revision, "1");
            Assert.AreEqual(doc.EffectiveDate, "2020-03-30");
        }

    }
}