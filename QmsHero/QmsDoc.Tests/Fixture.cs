using QFileUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Tests
{
    class Fixture: FixtureUtil
    {
        string wordSampleName;
        string excelSampleName;
        public FileInfo WordSample;
        public FileInfo ExcelSample;
        DirectoryInfo activeQMSDocuments;
        DirectoryInfo sop1Documents;

        public Fixture(): base()
        {
        }

        public DirectoryInfo ActiveQMSDocuments { get => activeQMSDocuments; set => activeQMSDocuments = value; }
        public DirectoryInfo Sop1Documents { get => sop1Documents; set => sop1Documents = value; }

        public override void Initialize(string fixtureDirName="Fixtures", string processingDirName="Processing")
        {
            base.Initialize();
            this.ActiveQMSDocuments = this.Dir.GetDirectories("Active QMS Documents").ToList()[0];
            this.Sop1Documents = this.ActiveQMSDocuments.GetDirectories("SOP-001 Quality Manual Documents")[0];
            this.WordSample = this.Sop1Documents.GetFiles("SOP-001*").ToList()[0];
            this.ExcelSample = this.Sop1Documents.GetFiles("F-001B*").ToList()[0];
        }


    }
}
