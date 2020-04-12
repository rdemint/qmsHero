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

namespace QmsDoc.Docs
{
    public class WordDoc : QmsDocBase, IDocActions, INotifyPropertyChanged
    {
        WordDocConfig docConfig;
        DocManagerConfig docManagerConfig;
        WordprocessingDocument doc;
        MainDocumentPart mainDocumentPart;
        FileInfo fileInfo;
        object headerFooter;
        bool headerFootersChecked;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;


        public WordDoc()
        {

        }

        public WordDoc(System.IO.FileInfo file_info, WordDocConfig docConfig, DocManagerConfig docManagerConfig) : base()
        {
            this.FileInfo = file_info;
            this.DocConfig = docConfig;
            this.DocManagerConfig = docManagerConfig;
        }

        #region Config
        public WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public DocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public FileInfo FileInfo { get => fileInfo;
            set { this.headerFootersChecked = false; this.fileInfo = value; } }
        #endregion  

        #region Header

        public Table HeaderFooterTable
        {
            get
            {
                return this.mainDocumentPart.HeaderParts.FirstOrDefault().RootElement.Elements<Table>().FirstOrDefault();
            }
        }

        public TableCell FetchHeaderFooterTableCell(int row, int col)
        {
            var table = this.HeaderFooterTable;
            TableRow r = table.Elements<TableRow>().ElementAt(row);
            TableCell cell = r.Elements<TableCell>().ElementAt(col);
            return cell;
        }
        public Text FetchEffectiveDatePart()
        {
            var table = this.HeaderFooterTable;
            TableRow row = table.Elements<TableRow>().ElementAt(DocConfig.EffectiveDateRow);
            TableCell cell = row.Elements<TableCell>().ElementAt(DocConfig.EffectiveDateCol);
            Paragraph p = cell.Elements<Paragraph>().First();
            Run r = p.Elements<Run>().First();
            Text text = r.Elements<Text>().First();
            return text;
        }
        public string FetchEffectiveDate()
        {
            Text text = FetchEffectiveDatePart();
            Match match = Regex.Match(text.ToString(), @"\d\d\d\d-\d\d-\d\d");
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
                var part = FetchEffectiveDatePart();
                part.Text = value;
                this.effectiveDate = value;
                this.OnPropertyChanged();

            }
        }

        public Text FetchRevisionPart()
        {
            TableCell cell = this.FetchHeaderFooterTableCell(this.DocConfig.RevisionRow, DocConfig.RevisionCol);
            Paragraph p = cell.Elements<Paragraph>().First();
            Run r = p.Elements<Run>().First();
            Text text = r.Elements<Text>().First();
            return text;

        }
        public string FetchRevision()
        {
            Text text = FetchRevisionPart();
            Match match = Regex.Match(text.ToString(), @"\d");
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
                TableCell cell = FetchHeaderFooterTableCell(DocConfig.RevisionRow, DocConfig.RevisionCol);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text text = r.Elements<Text>().First();
                text.Text = value;
                this.revision = value;
            }
        }
        public override string LogoPath
        {
            get => this.logoPath;
            set { 
                this.logoPath = value;
                var cell = this.HeaderFooterTable
                    .Cell(
                    this.DocConfig.LogoRow,
                    this.DocConfig.LogoCol
                    );
                cell.Range.Delete();
                var picture = cell.Range.InlineShapes.AddPicture(value, false, true);
                picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                picture.Height = 28;
                this.logoPath = value;
                this.OnPropertyChanged(); }
        }
        public override string LogoText
        {
            get => this.logoText;
            set
            {
                this.logoText = value;
                var cell = this.HeaderFooterTable
                    .Cell(
                    this.DocConfig.LogoRow,
                    this.DocConfig.LogoCol
                    );
                cell.Range.Delete();
                cell.Range.Text = "Effective Date: " + value;
                this.logoText = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void Process(DocState docState)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                this.doc = doc;
                this.mainDocumentPart = doc.MainDocumentPart;
                var docProps = docState.ToCollection();
                foreach (DocProperty docProp in docProps)
                {
                    var propertyInfo = doc.GetType().GetProperty(docProp.Name);
                    propertyInfo?.SetValue(this, docProp.Value);
                }
            }
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
