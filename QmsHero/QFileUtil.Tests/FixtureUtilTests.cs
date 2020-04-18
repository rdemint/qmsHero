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
            Assert.AreEqual("Fixtures", fixture.FixtureDir.Name);
            Assert.AreEqual("Reference", fixture.ReferenceDir.Name);
            Assert.AreEqual("Processing", fixture.ProcessingDir.Name);
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