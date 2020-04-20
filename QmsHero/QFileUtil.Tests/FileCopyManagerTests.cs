using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QFileUtil.Tests
{
    [TestClass]
    public class FileCopyManagerTests
    {
        [TestMethod]
        public void FileCopyManagerTest()
            
        {
            var fixture = new Fixture();
            Assert.AreEqual(fixture.ReferenceDir.Name, "Reference");
            Assert.AreEqual(fixture.ProcessingDir.Name, "Processing");

        }

        [TestMethod]
        public void IsValidTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(false, fixture.IsReadyToCopy());
        }

        [TestMethod]
        public void IsCleanTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(true, fixture.ProcessingDirIsClean());
        }
    }
}