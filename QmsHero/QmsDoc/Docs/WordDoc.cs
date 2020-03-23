using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.ComponentModel;
using QmsDoc.Interfaces;
using System.IO;
using QmsDoc.Core;

namespace QmsDoc.Docs
{
    public class WordDoc : QmsDocBase, IDocActions, INotifyPropertyChanged
    {
        Word.Application app;
        Word.Document doc;
        WordDocConfig docConfig;
        DocManagerConfig managerConfig;
        FileInfo fileInfo;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;

        public WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public DocManagerConfig ManagerConfig { get => managerConfig; set => managerConfig = value; }

        public string Header { get => Header; set => Header = value; }
        public string EffectiveDate
        {
            get { return effectiveDate; }
            set
            {
                effectiveDate = value;
                this.OnPropertyChanged();
            }
        }

        public string Revision
        {
            get { return revision; }
            set
            {
                revision = value;
                this.OnPropertyChanged();
            }
        }
        public string LogoPath
        {
            get => this.logoPath;
            set { this.logoPath = value; this.OnPropertyChanged(); }
        }
        public string LogoText
        {
            get => this.logoText;
            set
            {
                this.logoText = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public WordDoc()
        {

        }

        public WordDoc(Word.Application app, System.IO.FileInfo file_info, Boolean save_changes = true, Boolean auto_close = true) : base()
        {
            this.app = app;
            this.fileInfo = file_info;
            this.saveChanges = save_changes;
            this.autoClose = auto_close;
            this.doc = null;
            this.password = "QMSpwd";
            this.OpenDocument();
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public Word.Document OpenDocument()
        {
            if (this.fileInfo.Name.StartsWith("~")) {
                return null;
            }

           
            try
            {
                this.doc = this.app.Documents.Open(this.fileInfo.FullName, PasswordDocument: this.password, WritePasswordDocument: this.password);
                return this.doc;
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
                var documents = this.app.Documents;
                var count = documents.Count;
                if (this.saveChanges)
                {
                    this.app.Documents[this.fileInfo.Name].Close(SaveChanges:Word.WdSaveOptions.wdSaveChanges);
                }
                else
                {
                    //this.app.Documents.Close(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges);
                    this.app.Documents[this.fileInfo.Name].Close(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges);
                }

                var result = this.app.Documents.Count - count;
                return result;
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
