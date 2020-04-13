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
        //[TestMethod()]
        //public void ProcessDocTest()
        //{

        //}

    }
}