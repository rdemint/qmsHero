using QFileUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Tests
{
    public class Fixture: FileCopyManager
    {
        public FileInfo WordSampleCopy;
        public FileInfo ExcelSampleCopy;
        string wordSampleEffectiveDate;
        string wordSampleRevision;
        string wordSampleHeaderName;
        string wordSampleDocNumber;
        string wordSampleFileDocName;

        string excelSampleEffectiveDate;
        string excelSampleRevision;
        string excelSampleHeaderName;
        string excelSampleDocNumber;
        string excelSampleFileDocName;
        DirectoryInfo activeQMSDocuments;
        DirectoryInfo sop1Documents;
        string defaultReferenceDirName = "Reference";
        string defaultProcessingDirName = "Processing";
        FileInfo logoSampleJpg;

        public Fixture(): base()
        {
            var topdir = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent;
            SetProcessingDir(FileUtil.SearchSubDirectory(topdir, "Processing"));
            FileUtil.CleanDirectoryAndChildren(ProcessingDir);
            SetReferenceDir(FileUtil.SearchSubDirectory(topdir, "Reference"));

            this.ActiveQMSDocuments = this.ReferenceDir.GetDirectories("Active QMS Documents").ToList()[0];
            this.Sop1Documents = this.ActiveQMSDocuments.GetDirectories("SOP-001 Quality Manual Documents")[0];
            var files = Sop1Documents.GetFiles().ToList();

            this.WordSampleCopy = this.ProcessingDir.GetFiles("SOP-001*", SearchOption.AllDirectories).First();
            this.WordSampleEffectiveDate = "2018-11-26";
            this.WordSampleRevision = "3";
            this.WordSampleHeaderName = "Quality Manual (SOP-001)";
            this.WordSampleDocNumber = "SOP-001";
            this.WordSampleFileDocName = "Quality Manual";


            this.ExcelSampleCopy = this.ProcessingDir.GetFiles("F-001B*", SearchOption.AllDirectories).First();
            this.ExcelSampleEffectiveDate = "2018-11-26";
            this.ExcelSampleRevision = "2";
            this.ExcelSampleHeaderName = "Document Control Index (F-001B)";
            this.ExcelSampleDocNumber = "F-001B";
            this.ExcelSampleFileDocName = "Document Control Index";

            this.LogoSampleJpgCopy = this.ProcessingDir.GetFiles("*.jpg").First();

        }

        public DirectoryInfo ActiveQMSDocuments { get => activeQMSDocuments; set => activeQMSDocuments = value; }
        public DirectoryInfo Sop1Documents { get => sop1Documents; set => sop1Documents = value; }
        public string DefaultProcessingDirName { get => defaultProcessingDirName; set => defaultProcessingDirName = value; }
        public string DefaultReferenceDirName { get => defaultReferenceDirName; set => defaultReferenceDirName = value; }
        public FileInfo LogoSampleJpgCopy { get => logoSampleJpg; set => logoSampleJpg = value; }
        public string WordSampleEffectiveDate { get => wordSampleEffectiveDate; set => wordSampleEffectiveDate = value; }
        public string ExcelSampleEffectiveDate { get => excelSampleEffectiveDate; set => excelSampleEffectiveDate = value; }
        public string ExcelSampleRevision { get => excelSampleRevision; set => excelSampleRevision = value; }
        public string WordSampleRevision { get => wordSampleRevision; set => wordSampleRevision = value; }
        public string ExcelSampleHeaderName { get => excelSampleHeaderName; set => excelSampleHeaderName = value; }
        public string WordSampleHeaderName { get => wordSampleHeaderName; set => wordSampleHeaderName = value; }
        public string ExcelSampleDocNumber { get => excelSampleDocNumber; set => excelSampleDocNumber = value; }
        public string ExcelSampleDocName { get => excelSampleFileDocName; set => excelSampleFileDocName = value; }
        public string WordSampleDocNumber { get => wordSampleDocNumber; set => wordSampleDocNumber = value; }
        public string WordSampleFileDocName { get => wordSampleFileDocName; set => wordSampleFileDocName = value; }
        public string ExcelSampleFileDocName { get => excelSampleFileDocName; set => excelSampleFileDocName = value; }
    }
}
