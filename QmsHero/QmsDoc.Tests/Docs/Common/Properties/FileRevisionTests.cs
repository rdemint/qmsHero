﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Tests.Docs.Common.Properties
{
    [TestClass()]
    public class FileRevisionTests
    {

        [TestMethod()]
        public void ReadTest()
        {
            var fixture = new Fixture();
            var doc = new ExcelDoc(fixture.ExcelSampleCopy);
            var result = (string)doc.Inspect(new FileRevision()).State;
            Assert.AreEqual(fixture.ExcelSampleRevision, result);
        }

        [TestMethod()]
        public void WriteTest()
        {
            string newRev = "12";
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            doc.Process(new FileRevision(newRev));
            var result = (string)doc.Inspect(new FileRevision()).State;
            Assert.AreEqual(newRev, result);
        }


    }
}