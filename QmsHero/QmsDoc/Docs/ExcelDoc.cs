
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using FileInfo = System.IO.FileInfo;

namespace QmsDoc.Docs

{
    public class ExcelDoc : QmsDocBase
    {
        public System.IO.FileInfo file_info;
        public Excel.Application app;
        public string password;
        public Boolean save_changes;
        public Boolean auto_close;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;
        Boolean autoClose;
        Boolean saveChanges;

        public string Revision { get => revision; set => revision = value; }
        public string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public string LogoPath { get => logoPath; set => logoPath = value; }
        public string LogoText { get => logoText; set => logoText = value; }

        public ExcelDoc() { }
        
        public ExcelDoc(Excel.Application app, System.IO.FileInfo file_info, Boolean save_changes=true, Boolean auto_close=true)
        {
            this.app = app;
            this.file_info = file_info;
            this.password = "QMSpwd";
            this.save_changes = save_changes;
            this.auto_close = auto_close;
            this.OpenDocument(file_info);
        }


        public Excel.Workbook OpenDocument(FileInfo file_info)
        {
       
            if (this.file_info.Name.StartsWith("~"))
            {
                return null;
            }

            try
            {

              Excel.Workbook workbook = this.app.Workbooks.Open(file_info.FullName, IgnoreReadOnlyRecommended: true, Password: this.password);
              return workbook;
            }

            catch (Exception e)
            {
                this.CloseDocument();
                throw e;
            }
            
        }

        public override int CloseDocument()
        {
            try
            {
                var workbooks = this.app.Workbooks;
                var count = workbooks.Count;
                this.app.Workbooks[this.file_info.Name].Close(SaveChanges: this.save_changes);

                var result = this.app.Workbooks.Count - count;
                return result;
            }

            catch (Exception e)
            {
                throw e;
            }
        }
        //public override void HeaderRev(string rev)
        //{
        //    Console.WriteLine($"ExcelDoc changing HeaderRev to {rev}");
        //}

        //public override void HeaderEffectiveDate(string eff_date)
        //{
        //    Console.WriteLine($"ExcelDoc changing Effective Date to {eff_date}");
        //}

        //public override void HeaderLogo(string logo_path)
        //{
        //    Console.WriteLine($"ExcelDoc changing header logo to picture at to {logo_path}");
        //}
    }
}
