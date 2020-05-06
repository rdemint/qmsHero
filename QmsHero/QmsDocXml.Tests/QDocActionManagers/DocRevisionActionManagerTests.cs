﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests.QDocActionManagers
{
    [TestClass()]
    public class DocRevisionActionManagerTests
    {
        [TestMethod()]
        public void InspectTest()
        {
            var fixture = new XmlFixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var propCollection = doc.Inspect(DocRevisionActionManager.Create(fixture.ExcelSampleDocName));
            var textFind = propCollection.Where(prop => prop.Value.Name == "TextFindReplace").First().Value as TextFindReplace;
            Assert.IsFalse(propCollection.HasErrors());
            Assert.AreEqual(textFind.Count, 1);
        }

        [TestMethod()]
        public void InspectWordTest()
        {
            var fixture = new XmlFixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var propCollection = doc.Inspect(DocNameActionManager.Create(fixture.WordSampleFileDocName));
            var textFind = propCollection.Where(prop => prop.Value.Name == "TextFindReplace").First().Value as TextFindReplace;
            Assert.IsFalse(propCollection.HasErrors());
            Assert.AreEqual(textFind.Count, 19);
        }
    }
}