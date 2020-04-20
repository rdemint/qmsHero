using QFileUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests
{
    class Fixture: FileCopyManager
    {
        public FileInfo WordSample;
        public FileInfo ExcelSample;
        DirectoryInfo activeQMSDocuments;
        DirectoryInfo sop1Documents;
        string defaultReferenceDirName = "Reference";
        string defaultProcessingDirName = "Processing";

        public Fixture(): base()
        {
            var topdir = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent;
            ReferenceDir = FileUtil.SearchSubDirectory(topdir, "Reference");
            ProcessingDir = FileUtil.SearchSubDirectory(topdir, "Processing");
            this.ActiveQMSDocuments = this.ReferenceDir.GetDirectories("Active QMS Documents").ToList()[0];
            this.Sop1Documents = this.ActiveQMSDocuments.GetDirectories("SOP-001 Quality Manual Documents")[0];
            var files = Sop1Documents.GetFiles().ToList();
            this.WordSample = this.Sop1Documents.GetFiles("SOP-001*").ToList()[0];
            this.ExcelSample = this.Sop1Documents.GetFiles("F-001B*").ToList()[0];
        }

        public DirectoryInfo ActiveQMSDocuments { get => activeQMSDocuments; set => activeQMSDocuments = value; }
        public DirectoryInfo Sop1Documents { get => sop1Documents; set => sop1Documents = value; }
        public string DefaultProcessingDirName { get => defaultProcessingDirName; set => defaultProcessingDirName = value; }
        public string DefaultReferenceDirName { get => defaultReferenceDirName; set => defaultReferenceDirName = value; }
    }
}
