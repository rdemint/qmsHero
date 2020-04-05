
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using FileInfo = System.IO.FileInfo;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;

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
            this.OpenDocument(fileInfo);
        }

        public ExcelDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public DocManagerConfig ManagerConfig { get => managerConfig; set => managerConfig = value; }
        public FileInfo FileInfo { get => fileInfo; set => fileInfo = value; }
        #region Header
        public string Revision { get => revision; set => revision = value; }
        public string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public string LogoPath { get => logoPath; set => logoPath = value; }
        public string LogoText { get => logoText; set => logoText = value; }
        
        #endregion



        public Excel.Workbook OpenDocument(FileInfo file_info)
        {
       
            if (this.FileInfo.Name.StartsWith("~"))
            {
                return null;
            }

            try
            {

              Excel.Workbook workbook = this.app.Workbooks.Open(file_info.FullName, IgnoreReadOnlyRecommended: true, Password: this.ManagerConfig.DocPassword);
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
