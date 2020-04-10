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
            
            var processDirs = fixture.FixtureProcessingDir.GetDirectories("Processing");
            Assert.IsTrue(processDirs.Length == 1);
            var dirFiles = manager.DirFiles;
            var parentDir = dirFiles[0].Directory;
            Assert.AreEqual(parentDir, fixture.FixtureProcessingDir);
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
            var fixture = new FixtureUtil();
            var manager = new DocManager();

            docEdit.DocHeader.Revision.Value = "1";
            docEdit.DocHeader.EffectiveDate.Value = "2020-03-30";
            var doc = manager.CreateDoc(fixture.WordSample);
            doc = manager.ProcessDoc(doc, docEdit);
            var worddoc = (WordDoc)doc;
            var rev = worddoc.GetRevision();
            var date = worddoc.GetEffectiveDate();
            manager.Dispose();

            Assert.AreEqual(rev, "1");
            Assert.AreEqual(date, "2020-03-30");
        }

    }
}