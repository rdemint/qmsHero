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
    public class DocManagerTests
    {
        [TestMethod()]
        public void AddDirFilesTest()
        {
            var manager = new DocManager();
            var fixture = new FixtureUtil();
            manager.ConfigDir(fixture.ActiveQMSDocuments.FullName);
            Assert.AreEqual(manager.DirFiles.Count, fixture.Files.Count);
        }
    }
}