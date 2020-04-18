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
        Fixture fixture = new Fixture();

        [TestMethod()]
        public void ConfigDirTest()
        {
            var fixture = new Fixture();
            var manager = new QDocManager();
            manager.ConfigDir(fixture.ReferenceDir.FullName);
            Assert.AreEqual(fixture.ReferenceDir.FullName, manager.Dir.FullName);
            Assert.AreEqual(
                fixture.ProcessingDir.FullName,
                manager.ProcessingDir.FullName
                );
        }
    }
}

