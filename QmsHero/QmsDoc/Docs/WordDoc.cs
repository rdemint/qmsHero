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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LadderFileUtils;
using System.Reflection;

namespace QmsDoc.Docs
{
    public class WordDoc : IQmsDoc, INotifyPropertyChanged
    {
        WordDocConfig docConfig;
        WordprocessingDocument doc;
        MainDocumentPart mainDocumentPart;
        FileInfo fileInfo;
        string logoText;
        string logoPath;
        string revision;


        public WordDoc()
        {

        }

        public WordDoc(System.IO.FileInfo fileInfo) : base()
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

        public Paragraph FetchRevisionPart()
        {
            TableCell cell = this.FetchHeaderFooterTableCell(this.DocConfig.RevisionRow, DocConfig.RevisionCol);
            return cell.Elements<Paragraph>().First();
        }
        public string FetchRevision()
        {
            Paragraph par = FetchRevisionPart();
            Match match = Regex.Match(par.InnerText, @"\d{1,2}");
            var result = match.ToString().Replace(DocConfig.RevisionText, "");
            return result;
        }

        public string Revision
        {
            get
            {
                return this.revision;
            }

            set
            {
                Paragraph par = FetchRevisionPart();
                //par.Elements<Run>().Elements<Text>().First()
                par.RemoveAllChildren();
                Run run = new Run();
                Text text = new Text();
                text.Text = DocConfig.RevisionText + value;
                run.Append(text);
                par.Append(run);
                this.revision = value;
                OnPropertyChanged();
            }
        }
        public string LogoPath
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
        public string LogoText
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

        public IQmsDoc Process(DocState docState, DirectoryInfo targetDir)
        {
            var targetFile = QFileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new WordDoc(targetFile);
            targetDoc.Process(docState);
            return targetDoc;
        }

        public void Process(DocState docState)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                var docProps = docState.ToCollection();
                foreach (DocProperty docProp in docProps)
                {
                    Process(docProp);
                }
            }
        }

        public IQmsDoc Process(DocProperty prop, DirectoryInfo targetDir)
        {
            var targetFile = QFileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new WordDoc(targetFile);
            targetDoc.Process(prop);
            return targetDoc;
        }
        public void Process(DocProperty prop)
        {
            string propRef = DocConfig.PropertyReferenceName(prop.Name);
            Type myPropType = Type.GetType(propRef);
            DocProperty instance = (DocProperty)Activator.CreateInstance(myPropType);
            DocProperty result = null;
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                instance.Write(doc, DocConfig, prop.Value);
            }
        }

        

        public DocProperty Inspect(DocProperty prop)
        {
            string propRef = DocConfig.PropertyReferenceName(prop.Name);
            Type myPropType = Type.GetType(propRef);
            DocProperty instance = (DocProperty)Activator.CreateInstance(myPropType);
            DocProperty result = null;
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
            {
                result = (DocProperty)instance.Read(doc, DocConfig);
            }
            return result;

        }

        public DocState Inspect(bool filter = false)
        {
            //Return a new DocState based on inspection of the WordProcessingDocument
            DocState state = new DocState();
            var docProps = state.ToCollection(filter: false);
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
            {
                object[] methodParams = new object[1];
                methodParams[0] = doc;
                methodParams[1] = DocConfig;
                throw new NotImplementedException();
                //foreach (DocProperty docProp in docProps)
                //{

                //    var getMethod = docProp.GetType().GetMethod("Get");
                //    string result = (string)getMethod?.Invoke(docProp, methodParams);
                //    var stateProperty = state.GetType().GetProperty(docProp.Name);
                //    DocProperty dp = (DocProperty)stateProperty.GetValue(state);
                //    var propertyInfoValue = dp.GetType().GetProperty("Value");
                //    propertyInfoValue.SetValue(dp, result);
                //}
            }
            return state;
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
