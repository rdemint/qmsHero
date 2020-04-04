using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Core;
using QmsDoc.Interfaces;
using QmsDoc.Docs;
using System.Collections.ObjectModel;

namespace QmsDoc.Test.Core
{
    [TestClass()]
    public class DocManagerTests
    {
        [TestMethod()]
        public void ProcessDocTest()
        {
            var docEdit = new DocEdit();
            docEdit.DocPropertiesCollection = new ObservableCollection<DocProperty>
            {
                new DocProperty("Revision", "1"),
                new DocProperty("EffectiveDate", "2020-03-30"),
            };
            var manager = new DocManager();
            var doc = (QmsDocBase)new WordDoc();
            doc = manager.ProcessDoc(doc, docEdit);
            Assert.AreEqual(doc.Revision, "1");
            Assert.AreEqual(doc.EffectiveDate, "2020-03-12");
        }

    }
}