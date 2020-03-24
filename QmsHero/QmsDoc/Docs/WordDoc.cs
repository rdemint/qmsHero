﻿using System;
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

namespace QmsDoc.Docs
{
    public class WordDoc : QmsDocBase, IDocActions, INotifyPropertyChanged
    {
        Application app;
        Document doc;
        WordDocConfig docConfig;
        DocManagerConfig managerConfig;
        FileInfo fileInfo;
        HeaderFooter headerFooter;
        bool headerFootersChecked;
        string logoText;
        string logoPath;
        string effectiveDate;
        string revision;


        public WordDoc()
        {

        }

        public WordDoc(Application app, System.IO.FileInfo file_info, Boolean save_changes = true, Boolean auto_close = true) : base()
        {
            this.app = app;
            this.FileInfo = file_info;
            this.OpenDocument();
        }

        #region Config
        public WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public DocManagerConfig ManagerConfig { get => managerConfig; set => managerConfig = value; }
        public FileInfo FileInfo { get => fileInfo;
            set { this.headerFootersChecked = false; this.fileInfo = value; } }
        #endregion  

        #region Header

        public HeaderFooter HeaderFooter {
            get
            {
                if (this.headerFootersChecked == false)
                    {
                        for (int i=0; i < this.doc.Sections.Count; i++)
                            {
                                var section = this.doc.Sections[i];
                                if (i != DocConfig.HeaderFooterSection)
                                {
                                    var evenHF = section.Headers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].Exists;
                                    var firstHF = section.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Exists;
                                    var primHF = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Exists;

                                    if (evenHF || firstHF || primHF)
                                    {
                                        throw new MultipleDocHeadersException($"{this.FileInfo.Name} has multiple headers in Section {section}.  This must be fixed manually");
                                    }

                                }
                    
                            }
                        this.headerFootersChecked = true;
                    }
                
                var hfSections = this.doc.Sections;
                var hfSection = hfSections[DocConfig.HeaderFooterSection];
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

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public Document OpenDocument()
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
