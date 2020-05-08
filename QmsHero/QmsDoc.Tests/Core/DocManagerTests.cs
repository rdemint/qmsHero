using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Docs;
using QmsDoc.Core;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Common.PropertyGroups;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Tests.Core
{
    [TestClass()]
    public class DocManagerTests
    {
        
        
        
        
        [TestMethod()]
        public void ProcessTest()
        {
            string newRev = "20";
            
            var fixture = new Fixture();
            var manager = new DocManager(fixture);
            var fileGroup = new FilePropertyGroup();
            fileGroup.FileRevision.State = newRev;
            

            
            DocCollection processedDocs = manager.Process(fileGroup.ToCollection());
            var docs = manager.ToUnprocessedDocCollection();
            Assert.IsTrue(docs.Count >= 1);
            foreach(var doc in manager.ToUnprocessedDocCollection())
            {
                var result = doc?.Inspect(new FileRevision());
                Assert.IsTrue(result.IsSuccess);
                Assert.AreEqual(newRev, (string)result.Value);
            }
        }

        [TestMethod()]
        public void DocCollectionTest()
        {
            var fixture = new Fixture();
            var manager = new DocManager(fixture);
            var docs = manager.ToUnprocessedDocCollection();
            int wordCount = fixture.ProcessingFiles.Where(
                file => WordDoc.Extensions().Contains(file.Extension)
                ).Count();
            int excelCount = fixture.ProcessingFiles.Where(
                file => ExcelDoc.Extensions().Contains(file.Extension)
                ).Count();
            Assert.AreEqual(docs.Count, wordCount+excelCount);
        }
}
}