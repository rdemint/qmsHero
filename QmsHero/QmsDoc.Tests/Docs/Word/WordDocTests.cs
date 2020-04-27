using Microsoft.VisualStudio.TestTools.UnitTesting;
using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Tests.Docs.Word
{
    [TestClass()]
    public class WordDocTests
    {
        [TestMethod()]
        public void ProcessStateTest()
        {
            var newRev = "12";
            var newNum = "F-012B";

            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var state = new QDocPropertyCollection() { new FileDocNumber(), new FileRevision() };
            var targetState = new QDocPropertyCollection() { new FileDocNumber(newNum), new FileRevision(newRev)};

            doc.Process(targetState);
            var inspectedState = doc.Inspect(state);
            DocProperty fileDocNumber = inspectedState.Where(prop => prop.Name == "FileDocNumber").First() as DocProperty;
            DocProperty fileRevision = inspectedState.Where(prop => prop.Name == "FileRevision").First() as DocProperty;
            Assert.AreEqual(newNum, (string)fileDocNumber.State);
            Assert.AreEqual(newRev, (string)fileRevision.State);

        }

        [TestMethod()]
        public void InspectStateTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var state = new QDocPropertyCollection() { new FileDocName(), new FileRevision() };
            QDocPropertyCollection result = doc.Inspect(state);
            Assert.AreEqual(result.Count, 2);
            DocProperty fileDocName = result.Where(prop => prop.Name == "FileDocName").First() as DocProperty;
            DocProperty fileRevision = result.Where(prop => prop.Name == "FileRevision").First() as DocProperty;
            Assert.AreEqual(fixture.WordSampleFileDocName, (string)fileDocName.State);
            Assert.AreEqual(fixture.WordSampleRevision, (string)fileRevision.State);
        }
    }
}