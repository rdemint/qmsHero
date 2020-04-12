
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using FileInfo = System.IO.FileInfo;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace QmsDoc.Docs

{
    public class ExcelDoc : QmsDocBase
    {
        Excel.Application app;
        Excel.Workbook doc;
        ExcelDocConfig docConfig;
        DocManagerConfig managerConfig;
        FileInfo fileInfo;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;

        public ExcelDoc() { }

        public ExcelDoc(Excel.Application app, System.IO.FileInfo fileInfo, ExcelDocConfig docConfig, DocManagerConfig docManagerConfig): base()
        {
            this.app = app;
            this.FileInfo = fileInfo;
            this.DocConfig = docConfig;
            this.ManagerConfig = docManagerConfig;
            this.Doc  = this.OpenDocument(fileInfo);
        }

        public ExcelDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public DocManagerConfig ManagerConfig { get => managerConfig; set => managerConfig = value; }
        public FileInfo FileInfo { get => fileInfo; set => fileInfo = value; }
        #region Header
        
        public string GetRevision()
        {
            string rightHeaders = this.Doc.Worksheets[1].PageSetup.RightHeader;
            //var myArr = new string[] { };
            //myArr.Append(this.DocConfig.RevisionText);
            //var result = rightHeaders.Split(myArr, StringSplitOptions.RemoveEmptyEntries);
            var match = Regex.Match(rightHeaders, Regex.Escape(this.DocConfig.RevisionText) + @"\d");
            var result = match.ToString();
            var match2 = Regex.Match(result, @"\d");
            return match2.ToString();
        }
        
        public override string Revision { 
            get => revision;
            set { 
                if(this.EffectiveDate == null)
                {
                    this.EffectiveDate = GetEffectiveDate();
                }

                foreach(Worksheet wksht in this.doc.Worksheets)
                {
                    wksht.PageSetup.RightHeader =
                        this.DocConfig.EffectiveDateText +
                        value +
                        this.DocConfig.RevisionEffectiveDateSeparator +
                        this.Revision;
                }
                this.revision = value;
            }
     
        }

        public string GetEffectiveDate() {
            string rightHeaders = this.Doc.Worksheets[1].PageSetup.RightHeader;
            Regex rx = new Regex(@"\d\d\d\d-\d\d-\d\d", RegexOptions.None);
            Match match = rx.Match(rightHeaders);
            return match.ToString();
        }
        public override string EffectiveDate { 
            get => effectiveDate; 
            set {
                if(this.Revision == null)
                {
                    this.Revision = GetRevision();
                }

                foreach (Worksheet wkst in this.Doc.Worksheets)
                {
                    wkst.PageSetup.RightHeader =
                        this.DocConfig.EffectiveDateText +
                        value +
                        this.DocConfig.RevisionEffectiveDateSeparator +
                        this.Revision;
                }
                this.effectiveDate = value;
            }
        }
        public string GetLogoPath()
        {
            return this.Doc.Worksheets[1].PageSetup.LeftHeaderPicture.Filename;
        }
        public override string LogoPath { 
            get => logoPath;
            set { 
                foreach(Worksheet wkst in this.Doc.Worksheets)
                {
                    var picture = wkst.PageSetup.LeftHeaderPicture;
                    picture.Filename = value;
                    picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    picture.Height = 28;
                    wkst.PageSetup.LeftHeader = "&G";
                 }
                this.logoPath = value;
            } }

        
        public string GetLogoText()
        {
            return this.Doc.Worksheets[1].PageSetup.LeftHeader;
        }
        public override string LogoText { 
            get => logoText;
            set {
                foreach (Worksheet wkst in this.Doc.Worksheets)
                {
                    wkst.PageSetup.LeftHeader = value;
                }
                this.logoText = value;
            } }
        public Workbook Doc { get => doc; set => doc = value; }
        public Excel.Application App {
            get
            {
                return this.app;
            }
            set => app = value; 
            }

        #endregion



        public Excel.Workbook OpenDocument(FileInfo file_info)
        {
       
            if (this.FileInfo.Name.StartsWith("~"))
            {
                return null;
            }

            try
            {

                int count = this.App.Workbooks.Count;
                Excel.Workbook workbook = this.App.Workbooks.Open(file_info.FullName, IgnoreReadOnlyRecommended: true, Password: this.ManagerConfig.DocPassword, WriteResPassword: this.ManagerConfig.DocPassword);
                int count2 = this.App.Workbooks.Count;
                var active = this.App.ActiveWorkbook;

              return workbook;
            }

            catch (Exception e)
            {
                this.CloseDocument();
                throw e;
            }
            
        }

        public override void CloseDocument()
        {
            try
            {
                var workbooks = this.app.Workbooks;
                var count = workbooks.Count;
                this.app.Workbooks[this.FileInfo.Name].Close(SaveChanges: this.ManagerConfig.SaveChanges);
            }

            catch (Exception e)
            {
                throw e;
            }

        }

        public override void SaveAsPdf()
        {
            base.SaveAsPdf();
        }
    }
}
