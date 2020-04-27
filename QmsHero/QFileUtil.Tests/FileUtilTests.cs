using Microsoft.VisualStudio.TestTools.UnitTesting;
using QFileUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFileUtil.Tests
{
    [TestClass()]
    public class FileUtilTests
    {
        [TestMethod()]
        public void FileRenameTest()
        {
            string newName = "SOP-001 Quality Book Rev1.docx";
            var fixture = new Fixture();
            FileInfo newFile = FileUtil.FileRename(fixture.WordSampleCopy, newName);
            Assert.AreEqual(newName, newFile.Name);
        }

        [TestMethod]
        public void DirectoryCopyTest()
        {
            var fixture = new Fixture();
            Assert.IsTrue(fixture.ReferenceFiles.Count >= 1);
            Assert.AreEqual(fixture.ReferenceFiles.Count, fixture.ProcessingFiles.Count);
        }
    }
}