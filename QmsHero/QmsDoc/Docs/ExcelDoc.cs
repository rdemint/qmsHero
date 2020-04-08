
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
            return rightHeaders.TakeWhile(char.IsDigit).Last().ToString();
        }
        
        public override string Revision { 
            get => revision;
            set { 
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
            string result = rightHeaders.Remove(this.DocConfig.EffectiveDateText.Length);
            return (string)result.Take(10);
        }
        public override string EffectiveDate { 
            get => effectiveDate; 
            set {
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
                if (this.app == null)
                {
                    this.app = new Excel.Application();
                }

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

              Excel.Workbook workbook = this.App.Workbooks.Open(file_info.FullName, IgnoreReadOnlyRecommended: true, Password: this.ManagerConfig.DocPassword);
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
