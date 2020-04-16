
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileInfo = System.IO.FileInfo;
using QDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.ComponentModel;
using DocumentFormat.OpenXml;
using LadderFileUtils;
using QDoc.Interfaces;

namespace QDoc.Docs.Excel

{
    public class QExcelDoc : IQDoc, INotifyPropertyChanged
    {
        QExcelDocConfig docConfig;
        FileInfo fileInfo;
        SpreadsheetDocument doc;
        WorkbookPart workbookPart;

        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;

        public QExcelDoc() { }

        public QExcelDoc(System.IO.FileInfo fileInfo): base()
        {
            this.FileInfo = fileInfo;
            this.DocConfig = new QExcelDocConfig();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public QExcelDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public FileInfo FileInfo { get => fileInfo; set => fileInfo = value; }
        #region Header
        
        public Worksheet FetchFirstHeaderWorkSheet()
        {
            return this.workbookPart.WorksheetParts.First().Worksheet;
        }

        public HeaderFooter FetchHeaderParts()
        {
            var ws = FetchFirstHeaderWorkSheet();
            var h = ws.Descendants<HeaderFooter>().First();
            return h;
        }
        public string FetchRevision()
        {
            var xml = this.FetchHeaderParts().InnerXml;
            Match match = Regex.Match(xml, DocConfig.RevisionText + @"\d{1,2}");
            var result = match.ToString().Replace(DocConfig.RevisionText, "");
            return result.ToString();

        }

        public string Revision { 
            get => revision;
            set {
                var temp = DocConfig.RevisionText + FetchRevision();
                var xml = FetchHeaderParts().OuterXml;
                var changed = DocConfig.RevisionText + value;

                var header = FetchFirstHeaderWorkSheet().Elements<HeaderFooter>().First();
                var odd = header.Elements<OddHeader>().First();
                var text = odd.Text;
                odd.Text = text.Replace(temp, changed);
                this.revision = value;
                OnPropertyChanged();
            }
     
        }

        public string FetchEffectiveDate() {
            var xml = this.FetchHeaderParts().InnerXml;
            Match match = Regex.Match(xml, DocConfig.EffectiveDateText + @"\d\d\d\d-\d\d-\d\d");
            var result = match.ToString().Replace(DocConfig.EffectiveDateText, "");
            return result;
        }
        public string EffectiveDate { 
            get => effectiveDate; 
            set {
                //For excel docs, FetchEffective() must be called, because header text must be replaced
                // rather than set like it can be in word documents.
                var temp = DocConfig.EffectiveDateText + FetchEffectiveDate();
                var xml = FetchHeaderParts().InnerXml;
                var changed = DocConfig.EffectiveDateText + value;

                var header = FetchFirstHeaderWorkSheet().Elements<HeaderFooter>().First();
                var odd = header.Elements<OddHeader>().First();
                var text = odd.Text;
                odd.Text = text.Replace(temp, changed);
                //FetchFirstHeaderWorkSheet().RemoveChild<HeaderFooter>(header);
                //var newHeader = new HeaderFooter(result);
                //FetchFirstHeaderWorkSheet().AppendChild<HeaderFooter>(newHeader);
                this.effectiveDate = value;
                OnPropertyChanged();
            }
        }
        public string GetLogoPath()
        {
            throw new NotImplementedException();
        }
        public string LogoPath { 
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
        public string LogoText { 
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


        public IQDoc Process(QDocState docState, DirectoryInfo targetDir)
        {
            var targetFile = QFileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new QExcelDoc(targetFile);
            targetDoc.Process(docState);
            return targetDoc;
        }

        public void Process(QDocState docState)
        {

            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.workbookPart = doc.WorkbookPart;
                var docProps = docState.ToCollection();
                foreach (QDocProperty docProp in docProps)
                {
                    var propertyInfo = this.GetType().GetProperty(docProp.Name);
                    propertyInfo?.SetValue(this, docProp.Value);
                }
            }
        }

        public QDocState Inspect(bool filter=false)
        {
            QDocState state = new QDocState();
            var docProps = state.ToCollection(filter);
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, false))
            {
                this.doc = doc;
                this.workbookPart = doc.WorkbookPart;
                //
                foreach (QDocProperty docProp in docProps)
                {
                    var methodInfo = this.GetType().GetMethod("Fetch" + docProp.Name);
                    string result = (string)methodInfo?.Invoke(this, null);
                    var propertyInfo = state.GetType().GetProperty(docProp.Name);
                    QDocProperty dp = (QDocProperty)propertyInfo.GetValue(state);
                    var propertyInfoValue = dp.GetType().GetProperty("Value");
                    propertyInfoValue.SetValue(dp, result);
                }
            }
            return state;
        }
        public void SaveAsPdf()
        {
            
        }
    }
}
