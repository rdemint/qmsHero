using Microsoft.VisualStudio.TestTools.UnitTesting;
using QFileUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Assert.AreEqual(true, fixture.IsValid());
        }

        [TestMethod]
        public void IsCleanTest()
        {
            var fixture = new FixtureUtil();
            Assert.AreEqual(true, fixture.IsClean());
        }
    }
}