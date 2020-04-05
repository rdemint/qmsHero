using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.ComponentModel;
using QmsDoc.Interfaces;
using System.IO;
using QmsDoc.Core;
using QmsDoc.Exceptions;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;

namespace QmsDoc.Docs
{
    public class WordDoc : QmsDocBase, IDocActions, INotifyPropertyChanged
    {
        Application app;
        Document doc;
        WordDocConfig docConfig;
        DocManagerConfig docManagerConfig;
        FileInfo fileInfo;
        HeaderFooter headerFooter;
        bool headerFootersChecked;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;
        ObservableCollection<IDocSection> sections;


        public WordDoc()
        {

        }

        public WordDoc(Application app, System.IO.FileInfo file_info, WordDocConfig docConfig, DocManagerConfig docManagerConfig) : base()
        {
            this.app = app;
            this.FileInfo = file_info;
            this.DocConfig = docConfig;
            this.DocManagerConfig = docManagerConfig;
            this.OpenDocument();
        }

        #region Config
        public WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public DocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public FileInfo FileInfo { get => fileInfo;
            set { this.headerFootersChecked = false; this.fileInfo = value; } }
        #endregion  

        #region Header

        public HeaderFooter HeaderFooter
        {
            get
            {
                if (this.headerFootersChecked == false)
                {
                    for (int i = 1; i <= this.doc.Sections.Count; i++)
                    {
                        var section = this.doc.Sections[i];
                        if (i != DocConfig.HeaderFooterSection)
                        {
                            var evenHF = section.Headers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].Exists;
                            var firstHF = section.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Exists;
                            var primHF = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Exists;

                            if (evenHF || firstHF)
                            {
                                System.Windows.MessageBox.Show($"{this.FileInfo.Name} has multiple headers in Section {section}.  This must be fixed manually");
                            }

                        }

                    }
                    this.headerFootersChecked = true;
                }

                var hfSection = doc.Sections[DocConfig.HeaderFooterSection];
                return hfSection.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];

            }
        }
        public string EffectiveDate
        {
            get {
                return this.HeaderFooter.Range.Tables[0]
                    .Cell(
                        this.DocConfig.EffectiveDateRow,
                        this.DocConfig.EffectiveDateCol
                    )
                    .Range.Text;
               }
            
            set
            {
                this.HeaderFooter.Range.Tables[0]
                    .Cell(
                        this.DocConfig.EffectiveDateRow,
                        this.DocConfig.EffectiveDateCol
                    )
                    .Range.Text = value;
                this.OnPropertyChanged();
            }
        }

        public string Revision
        {
            get
            {
                return this.HeaderFooter.Range.Tables[0]
                    .Cell(
                        this.DocConfig.RevisionRow,
                        this.DocConfig.RevisionCol
                    )
                    .Range.Text;
            }

            set
            {
                this.HeaderFooter.Range.Tables[0]
                    .Cell(
                        this.DocConfig.RevisionRow,
                        this.DocConfig.RevisionCol
                    )
                    .Range.Text = value;
                this.OnPropertyChanged();
            }
        }
        public string LogoPath
        {
            get => this.logoPath;
            set { 
                this.logoPath = value;
                var cell = this.HeaderFooter.Range.Tables[0]
                    .Cell(
                    this.DocConfig.LogoRow,
                    this.DocConfig.LogoCol
                    );
                cell.Range.Delete();
                var picture = cell.Range.InlineShapes.AddPicture(value, false, true);
                picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                picture.Height = 28;
                this.OnPropertyChanged(); }
        }
        public string LogoText
        {
            get => this.logoText;
            set
            {
                this.logoText = value;
                var cell = this.HeaderFooter.Range.Tables[0]
                    .Cell(
                    this.DocConfig.LogoRow,
                    this.DocConfig.LogoCol
                    );
                cell.Range.Delete();
                cell.Range.Text = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public override void OpenDocument()
        {
         
            try
            {
                this.doc = this.app.Documents.Open(this.fileInfo.FullName, PasswordDocument: this.DocManagerConfig.DocPassword, WritePasswordDocument: this.DocManagerConfig.DocPassword);
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
                var documents = this.app.Documents;
                var count = documents.Count;
                if (this.DocManagerConfig.SaveChanges)
                {
                    this.app.Documents[this.fileInfo.Name].Close(SaveChanges:WdSaveOptions.wdSaveChanges);
                }
                else
                {
                    //this.app.Documents.Close(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges);
                    this.app.Documents[this.fileInfo.Name].Close(SaveChanges:WdSaveOptions.wdDoNotSaveChanges);
                }
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
