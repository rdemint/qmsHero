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
using System.IO;

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
            Assert.AreEqual(fixture.ActiveQMSDocuments.FullName, manager.Dir.FullName);
            Assert.AreEqual(
                Path.Combine(fixture.Dir.FullName, "Processing"),
                manager.ProcessingDir.FullName
                );
            Assert.AreEqual(
                manager.Dir.FullName,
                fixture.ActiveQMSDocuments.FullName
                );
            manager.Dispose();
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
            var docEdit = new DocState();
            var fixture = new FixtureUtil();
            var manager = new DocManager();

            docEdit.DocHeader.Revision.Value = "1";
            docEdit.DocHeader.EffectiveDate.Value = "2020-03-30";
            manager.ConfigDir(fixture.Sop1Documents.FullName);
            manager.ProcessFiles(docEdit);
            var wordDoc = (WordDoc)manager.CreateDoc(manager.ProcessingDirFiles[0]);
            var rev = wordDoc.FetchRevision();
            var date = wordDoc.FetchEffectiveDate();
            Assert.AreEqual(rev, "1");
            Assert.AreEqual(date, "2020-03-30");
            manager.Dispose();

        }

    }
}