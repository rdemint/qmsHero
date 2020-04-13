
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileInfo = System.IO.FileInfo;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace QmsDoc.Docs

{
    public class ExcelDoc : QmsDocBase
    {
        ExcelDocConfig docConfig;
        FileInfo fileInfo;
        FileInfo targetFile;
        SpreadsheetDocument doc;
        WorkbookPart workbookPart;

        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;

        public ExcelDoc() { }

        public ExcelDoc(System.IO.FileInfo fileInfo): base()
        {
            this.FileInfo = fileInfo;
        }

        public ExcelDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public FileInfo FileInfo { get => fileInfo; set => fileInfo = value; }
        #region Header
        
        public string FetchRevision()
        {
            //string rightHeaders = this.Doc.Worksheets[1].PageSetup.RightHeader;
            //var match = Regex.Match(rightHeaders, Regex.Escape(this.DocConfig.RevisionText) + @"\d");
            //var result = match.ToString();
            //var match2 = Regex.Match(result, @"\d");
            //return match2.ToString();
            return null;
        }
        
        public override string Revision { 
            get => revision;
            set { 
                //if(this.effectiveDate == null)
                //{
                //    this.effectiveDate = GetEffectiveDate();
                //}

                //foreach(Worksheet wksht in this.doc.Worksheets)
                //{
                //    wksht.PageSetup.RightHeader =
                //        this.DocConfig.EffectiveDateText +
                //        value +
                //        this.DocConfig.RevisionEffectiveDateSeparator +
                //        this.revision;
                //}
                this.revision = value;
            }
     
        }

        public string FetchEffectiveDate() {
            //string rightHeaders = this.Doc.Worksheets[1].PageSetup.RightHeader;
            //Regex rx = new Regex(@"\d\d\d\d-\d\d-\d\d", RegexOptions.None);
            //Match match = rx.Match(rightHeaders);
            //return match.ToString();
            return null;
        }
        public override string EffectiveDate { 
            get => effectiveDate; 
            set {
                //if(this.revision == null)
                //{
                //    this.revision = GetRevision();
                //}

                //foreach (Worksheet wkst in this.Doc.Worksheets)
                //{
                //    wkst.PageSetup.RightHeader =
                //        this.DocConfig.EffectiveDateText +
                //        value +
                //        this.DocConfig.RevisionEffectiveDateSeparator +
                //        this.Revision;
                //}
                this.effectiveDate = value;
            }
        }
        public string GetLogoPath()
        {
            throw new NotImplementedException();
        }
        public override string LogoPath { 
            get => logoPath;
            set { 
                //foreach(Worksheet wkst in this.Doc.Worksheets)
                //{
                //    var picture = wkst.PageSetup.LeftHeaderPicture;
                //    picture.Filename = value;
                //    picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                //    picture.Height = 28;
                //    wkst.PageSetup.LeftHeader = "&G";
                // }
                this.logoPath = value;
            } }

        
        public string GetLogoText()
        {
            return null;
        }
        public override string LogoText { 
            get => logoText;
            set {
                //foreach (Worksheet wkst in this.Doc.Worksheets)
                //{
                //    wkst.PageSetup.LeftHeader = value;
                //}
                this.logoText = value;
            } }

        public WorkbookPart WorkBookPart { get => workbookPart; set => workbookPart = value; }

        #endregion


        public void Process(DocState docState, DirectoryInfo targetDir)
        {
            targetFile = this.CopyDocToTargetDir(this.FileInfo, targetDir);
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.workbookPart = doc.WorkbookPart;
                var docProps = docState.ToCollection();
                foreach (DocProperty docProp in docProps)
                {
                    var propertyInfo = this.GetType().GetProperty(docProp.Name);
                    propertyInfo?.SetValue(this, docProp.Value);
                }
            }
        }

        public void Process(DocState docState)
        {

            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.workbookPart = doc.WorkbookPart;
                var docProps = docState.ToCollection();
                foreach (DocProperty docProp in docProps)
                {
                    var propertyInfo = doc.GetType().GetProperty(docProp.Name);
                    propertyInfo?.SetValue(this, docProp.Value);
                }
            }
        }

        public override DocState Inspect()
        {
            DocState state = new DocState();
            var docProps = state.ToCollection(filter: false);
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.workbookPart = doc.WorkbookPart;
                //
                foreach (DocProperty docProp in docProps)
                {
                    var methodInfo = this.GetType().GetMethod("Fetch" + docProp.Name);
                    string result = (string)methodInfo?.Invoke(this, null);
                    var propertyInfo = state.GetType().GetProperty(docProp.Name);
                    DocProperty dp = (DocProperty)propertyInfo.GetValue(state);
                    var propertyInfoValue = dp.GetType().GetProperty("Value");
                    propertyInfoValue.SetValue(dp, result);
                }
            }
            return state;
        }
        public override void SaveAsPdf()
        {
            base.SaveAsPdf();
        }
    }
}
