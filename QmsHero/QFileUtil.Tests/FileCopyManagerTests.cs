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
        public void UpdateFilesTest()
        {
            var fixture = new Fixture();
            Assert.IsTrue(fixture.ReferenceFiles.Count >= 1);
            Assert.AreEqual(fixture.ProcessingFiles.Count, fixture.ReferenceFiles.Count);
        }

        [TestMethod]
        public void IsCleanTest()
        {
            var fixture = new Fixture();
            Assert.AreEqual(false, fixture.ProcessingDirIsClean());
        }
    }
}