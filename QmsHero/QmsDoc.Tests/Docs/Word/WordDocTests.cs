using FluentResults;
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
            QDocPropertyResultCollection resultCollection = doc.Inspect(state);
            Result<int> fileDocNumber = resultCollection.Where(result => result.Value.Name == "FileDocNumber").First();
            Assert.IsTrue(fileDocNumber.IsSuccess);
           
            Result<int> fileRevision = resultCollection.Where(result => result.Value.Name == "FileRevision").First();
            Assert.IsTrue(fileRevision.IsSuccess);

            Assert.AreEqual(newNum, (string)fileDocNumber.Value.State);
            Assert.AreEqual(newRev, (string)fileRevision.Value.State);

        }

        [TestMethod()]
        public void InspectStateTest()
        {
            var fixture = new Fixture();
            var doc = new WordDoc(fixture.WordSampleCopy);
            var state = new QDocPropertyCollection() { new FileDocName(), new FileRevision() };
            QDocPropertyResultCollection resultCollection = doc.Inspect(state);

            Assert.AreEqual(resultCollection.Count, 2);

            Result<int> fileDocName = resultCollection.Where(prop => prop.Value.Name == "FileDocName").First();
            Assert.IsTrue(fileDocName.IsSuccess);

            Result<int> fileRevision = resultCollection.Where(result => result.Value.Name == "FileRevision").First();
            Assert.IsTrue(fileRevision.IsSuccess);

            Assert.AreEqual(fixture.WordSampleFileDocName, (string)fileDocName.Value.State);
            Assert.AreEqual(fixture.WordSampleRevision, (string)fileRevision.Value.State);
        }
    }
}