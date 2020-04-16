
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using LadderFileUtils;
using QDoc.Core;
using QDoc.Docs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directory = System.IO.Directory;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;

namespace QDoc.Test
{
    public class FixtureUtil
    {
        
        DirectoryInfo unittest_dir;
        DirectoryInfo processingDir;
        DirectoryInfo dir;
        DirectoryInfo sop1_dir;
        DirectoryInfo qmsHero_dir;
        FileInfo open_file;
        List<FileInfo> files;
        private List<FileInfo> safeFiles;
        string wordSampleName;
        string excelSampleName;
        public FileInfo WordSample;
        public FileInfo ExcelSample;
        DirectoryInfo activeQMSDocuments;
        DirectoryInfo sop1Documents;

        public DirectoryInfo ActiveQMSDocuments { get => activeQMSDocuments; set => activeQMSDocuments = value; }
        public DirectoryInfo Sop1Documents { get => sop1Documents; set => sop1Documents = value; }
        public List<FileInfo> Files { get => files; set => files = value; }
        public List<FileInfo> SafeFiles { get => safeFiles; set => safeFiles = value; }
        public DirectoryInfo ProcessingDir { get => processingDir; set => processingDir = value; }
        public DirectoryInfo Dir { get => dir; set => dir = value; }
        public string ExcelSampleName { get => excelSampleName; set => excelSampleName = value; }
        public string WordSampleName { get => wordSampleName; set => wordSampleName = value; }

        public FixtureUtil()
        {
            this.WordSampleName = "F-001A Document Change Notice Rev1";
            this.ExcelSampleName = "F-001B Document Control Index Rev1";
            var unittest_dir_path = Directory.GetCurrentDirectory();
            this.unittest_dir = new DirectoryInfo(unittest_dir_path);
            var parent1 = this.unittest_dir.Parent;
            var parent = parent1.Parent;
            this.qmsHero_dir = parent;
            this.Dir = new DirectoryInfo(Path.Combine(this.qmsHero_dir.FullName, "Fixtures"));
            this.ProcessingDir = QFileUtil.CreateOrCleanSubDirectory(Dir, "Processing");
            Contract.Requires(ProcessingDir.Exists);
            this.ActiveQMSDocuments = this.Dir.GetDirectories("Active QMS Documents").ToList()[0];
            this.Sop1Documents = this.ActiveQMSDocuments.GetDirectories("SOP-001 Quality Manual Documents")[0];
            this.Files = this.ActiveQMSDocuments.GetFiles("*", SearchOption.AllDirectories).ToList();
            this.SafeFiles = this.GetSafeFiles(this.files);
            this.WordSample = this.Sop1Documents.GetFiles("SOP-001*").ToList()[0];
            this.ExcelSample = this.Sop1Documents.GetFiles("F-001B*").ToList()[0];
        }
        
        public List<FileInfo> GetSafeFiles(List<FileInfo> files)
        {
            var result = files.Where((file) => file.Name.StartsWith("~") == false).ToList();
            return result;
        }

        public DocState WordProcessingSampleState()
        {
            var doc = new WordDoc(this.WordSample);
            return doc.Inspect();
        }

        public DocState ExcelProcessingSampleState()
        {
            var doc = new ExcelDoc(this.ExcelSample);
            return doc.Inspect();
        }

        public object TestExcelDocMethod(FileInfo file, string methodName)
        {
            using(SpreadsheetDocument doc = SpreadsheetDocument.Open(file.FullName, true))
            {
                WorkbookPart wbPart = doc.WorkbookPart;
                WorksheetPart wsPart = wbPart.WorksheetParts.First();
                Worksheet ws = wsPart.Worksheet;
                HeaderFooter hf = ws.Descendants<HeaderFooter>().FirstOrDefault();
                if (hf!=null)
                {
                    var x = hf.InnerText;
                }
                return null;

            }
        }
    }
}
