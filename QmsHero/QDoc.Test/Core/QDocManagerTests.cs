using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
using QDoc.Interfaces;
using QDoc.Docs;
using System.Collections.ObjectModel;
using QDoc.Test;
using System.IO;

namespace QDoc.Test.Core
{
    [TestClass()]
    public class QDocManagerTests
    {
        FixtureUtil fixture = new FixtureUtil();

        [TestMethod()]
        public void ConfigDirTest()
        {
            var manager = new QDocManager();
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

namespace QDoc.Test.Core
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