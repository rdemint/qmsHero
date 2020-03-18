﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QmsDocLibTests
{
    [TestClass]
    public class FixtureUtilTests
    {
        [TestMethod]
        public void GetSafeFilesTest()
        {
            var fixture = new FixtureUtil();
            Assert.Equals(fixture.SafeFiles.Count, 28);
        }

        [TestMethod]
        public void ActiveQMSDocumentsTest()
        {
            var fixture = new FixtureUtil();
            var name = fixture.ActiveQMSDocuments.Name;
            Assert.Equals("Active QMS Documents", name);
        }

        [TestMethod]
        public void Sop1DocumentsTest()
        {
            var fixture = new FixtureUtil();
            var name = fixture.Sop1Documents.Name;
            Assert.Equals("SOP-001 Quality Manual Documents", name);
        }

        [TestMethod]
        public void WordSampleTest()
        {
            var fixture = new FixtureUtil();
            var ext = fixture.WordSample.Extension;
            Assert.Equals("docx", ext);
        }

        [TestMethod]
        public void ExcelSampleTest()
        {
            var fixture = new FixtureUtil();
            var ext = fixture.ExcelSample.Extension;
            Assert.Equals("xlsx", ext);
        }
    }
}
