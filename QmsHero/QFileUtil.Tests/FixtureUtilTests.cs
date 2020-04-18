using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QFileUtil.Tests
{
    [TestClass]
    public class FixtureUtilTests
    {
        [TestMethod]
        public void FixtureUtilTest()
        {
            var fixture = new FixtureUtil();
            Assert.AreEqual(fixture.DefaultFixtureDirName, fixture.FixtureDir.Name);
            Assert.AreEqual(fixture.DefaultReferenceDirName, fixture.ReferenceDir.Name);
            Assert.AreEqual(fixture.DefaultProcessingDirName, fixture.ProcessingDir.Name);
        }

        [TestMethod]
        public void IsValidTest()
        {
            var fixture = new FixtureUtil();
            Assert.AreEqual(false, fixture.IsValid());
        }

        [TestMethod]
        public void IsCleanTest()
        {
            var fixture = new FixtureUtil();
            Assert.AreEqual(true, fixture.IsClean());
        }
    }
}