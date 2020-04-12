
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileInfo = System.IO.FileInfo;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using System.Text.RegularExpressions;

namespace QmsDoc.Docs

{
    public class ExcelDoc : QmsDocBase
    {
        ExcelDocConfig docConfig;
        FileInfo fileInfo;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;

        public ExcelDoc() { }

        public ExcelDoc(System.IO.FileInfo fileInfo, ExcelDocConfig docConfig, DocManagerConfig docManagerConfig): base()
        {
            this.FileInfo = fileInfo;
            this.DocConfig = docConfig;
        }

        public ExcelDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public FileInfo FileInfo { get => fileInfo; set => fileInfo = value; }
        #region Header
        
        public string GetRevision()
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

        public string GetEffectiveDate() {
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

        #endregion




        public void Process(DocState docState)
        {

            //using (ExcelprocessingDocument doc = ExcelprocessingDocument.Open(this.FileInfo.FullName, true))
            //{
            //    var docProps = docState.ToCollection();
            //    foreach (DocProperty docProp in docProps)
            //    {
            //        var propertyInfo = doc.GetType().GetProperty(docProp.Name);
            //        propertyInfo?.SetValue(this, docProp.Value);
            //    }
            //}
            throw new NotImplementedException();
        }
        
        public override void SaveAsPdf()
        {
            base.SaveAsPdf();
        }
    }
}
