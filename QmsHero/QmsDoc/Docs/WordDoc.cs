using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using QmsDoc.Interfaces;
using System.IO;
using QmsDoc.Core;
using QmsDoc.Exceptions;
using GalaSoft.MvvmLight.Ioc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LadderFileUtils;

namespace QmsDoc.Docs
{
    public class WordDoc : QmsDocBase, IDocActions, INotifyPropertyChanged
    {
        WordDocConfig docConfig;
        WordprocessingDocument doc;
        MainDocumentPart mainDocumentPart;
        FileInfo fileInfo;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;


        public WordDoc()
        {

        }

        public WordDoc(System.IO.FileInfo fileInfo):base()
        {
            this.FileInfo = fileInfo;
            this.DocConfig = new WordDocConfig();
        }


        #region Config
        public WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public FileInfo FileInfo { get => fileInfo;
            set { fileInfo = value; } }
        #endregion  
        
        #region Header

        public Table FetchHeaderFooterTable()
        {
            return this.mainDocumentPart?.HeaderParts.FirstOrDefault().RootElement.Elements<Table>().FirstOrDefault();
        }

        public TableCell FetchHeaderFooterTableCell(int row, int col)
        {
            var table = FetchHeaderFooterTable();
            TableRow r = table.Elements<TableRow>().ElementAt(row);
            TableCell cell = r.Elements<TableCell>().ElementAt(col);
            return cell;
        }
        public Paragraph FetchEffectiveDatePart()
        {
 
            TableCell cell = FetchHeaderFooterTableCell(DocConfig.EffectiveDateRow, DocConfig.EffectiveDateCol);
            Paragraph p = cell.Elements<Paragraph>().First();
            return p;
        }
        public string FetchEffectiveDate()
        {
            Paragraph p = FetchEffectiveDatePart();
            //
            Match match = Regex.Match(p.InnerText, @"\d\d\d\d-\d\d-\d\d");
            this.effectiveDate = match.ToString();
            return match.ToString();
        }
        public override string EffectiveDate
        {
            get {
                return this.effectiveDate;
               }
            
            set
            {
                //https://stackoverflow.com/questions/32075170/how-to-replace-the-innertext-of-a-comment
                var par = FetchEffectiveDatePart();
                par.RemoveAllChildren();
                Run run = new Run();
                Text text = new Text();
                text.Text = DocConfig.EffectiveDateText + value;
                run.Append(text);
                par.Append(run);
                this.effectiveDate = value;
                this.OnPropertyChanged();

            }
        }

        public Paragraph FetchRevisionPart()
        {
            TableCell cell = this.FetchHeaderFooterTableCell(this.DocConfig.RevisionRow, DocConfig.RevisionCol);
            return cell.Elements<Paragraph>().First();
        }
        public string FetchRevision()
        {
            Paragraph par = FetchRevisionPart();
            //Match match = Regex.Match(par.InnerText, @"[0-9][0-9]|[0-9]");
            Match match = Regex.Match(par.InnerText, @"\d{0,2}");
            return match.ToString();
        }

        public override string Revision
        {
            get
            {
                return this.revision;
            }

            set
            {
                Paragraph par = FetchRevisionPart();
                par.RemoveAllChildren();
                Run run = new Run();
                Text text = new Text();
                text.Text = DocConfig.RevisionText + value;
                run.Append(text);
                par.Append(run);
                this.revision = value;
            }
        }
        public override string LogoPath
        {
            get => this.logoPath;
            set { 
                //this.logoPath = value;
                //var cell = this.HeaderFooterTable
                //    .Cell(
                //    this.DocConfig.LogoRow,
                //    this.DocConfig.LogoCol
                //    );
                //cell.Range.Delete();
                //var picture = cell.Range.InlineShapes.AddPicture(value, false, true);
                //picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                //picture.Height = 28;
                this.logoPath = value;
                this.OnPropertyChanged(); }
        }
        public override string LogoText
        {
            get => this.logoText;
            set
            {
                //var cell = this.HeaderFooterTable
                //    .Cell(
                //    this.DocConfig.LogoRow,
                //    this.DocConfig.LogoCol
                //    );
                //cell.Range.Delete();
                //cell.Range.Text = "Effective Date: " + value;
                this.logoText = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public WordDoc Process(DocState docState, DirectoryInfo targetDir)
        {
            var targetFile = this.CopyDocToTargetDir(this.FileInfo, targetDir);
            var targetDoc = new WordDoc(targetFile);
            targetDoc.Process(docState);
            return targetDoc;
        }
        
        public override void Process(DocState docState)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.mainDocumentPart = doc.MainDocumentPart;
                var docProps = docState.ToCollection();
                foreach (DocProperty docProp in docProps)
                {
                    var propertyInfo = this.GetType().GetProperty(docProp.Name);
                    propertyInfo?.SetValue(this, docProp.Value);
                }
            }
        }

        public override DocState Inspect(bool filter=false)
        {
            DocState state = new DocState();
            var docProps = state.ToCollection(filter:false);
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.mainDocumentPart = doc.MainDocumentPart;
                foreach(DocProperty docProp in docProps)
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

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public override void SaveAsPdf()
        {
            base.SaveAsPdf();
        }

    }
}
