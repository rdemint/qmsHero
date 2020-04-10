using ListDictionary = System.Collections.Specialized.ListDictionary;
using PropertyInfo = System.Reflection.PropertyInfo;
using ParameterInfo = System.Reflection.ParameterInfo;
using System;
using Directory = System.IO.Directory;
using FileInfo = System.IO.FileInfo;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using Contract = System.Diagnostics.Contracts.Contract;
using MethodBase = System.Reflection.MethodBase;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Docs;
using QmsDoc.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Runtime.Serialization;

namespace QmsDoc.Core
{
    public class DocManager : IDocManager, IDisposable
    {
        System.IO.DirectoryInfo topDir;
        string originalDirPath;
        DocManagerConfig docManagerConfig;
        ExcelDocConfig excelConfig;
        WordDocConfig wordConfig;
        List<FileInfo> dirFilesUnsafe;
        List<FileInfo> dirFiles;
        Word.Application wordApp;
        Excel.Application excelApp;
        string dirPath;
        bool disposed = false;
        bool canProcess;

        public DocManager()
        {
           
            this.excelConfig = new ExcelDocConfig();
            this.wordConfig = new WordDocConfig();
            this.docManagerConfig = new DocManagerConfig();
            this.dirFilesUnsafe = new List<FileInfo>();
        }
        
        #region Properties
        public DocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public List<FileInfo> DirFiles {
            get => this.GetSafeFiles(dirFilesUnsafe); }
        
        public Word.Application WordApp {
            get {
                if (wordApp == null)
                {
                    wordApp = new Word.Application();
                }
                return wordApp;
            }
            set => wordApp = value; }

         public Excel.Application ExcelApp {
            get { 
                if(excelApp == null)
                {
                    excelApp = new Excel.Application();
                }
                return excelApp;
            }
            set => excelApp = value; }

        public string OriginalDirPath { get => originalDirPath; set => originalDirPath = value; }



        #endregion


        #region Methods

        public bool CanProcessFiles(string path)
        {
            if (path!=null && path.Length > this.DocManagerConfig.SafeProcessingLength)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private List<FileInfo> GetSafeFiles(List<FileInfo> files)
        {
            var result = files.Where((file) => file.Name.StartsWith("~") == false).ToList();
            return result;
        }
 
        private void AddDirFiles(string dir_path)
        {
            var dir = new System.IO.DirectoryInfo(dir_path);
            var dir_files = dir.GetFiles();
            this.dirFilesUnsafe.AddRange(dir_files);

            foreach (string sub_dir_path in Directory.EnumerateDirectories(dir_path))
            {
                this.AddDirFiles(sub_dir_path);
            }
        }

        public void ConfigDir(string dir_path, string processingDirName="Processing")
        {
            this.originalDirPath = dir_path;
            this.dirPath = DirectoryCopy(new DirectoryInfo(dir_path), processingDirName, true).FullName;
            this.topDir = new System.IO.DirectoryInfo(this.dirPath);
            this.AddDirFiles(dir_path);
        }

        private DirectoryInfo DirectoryCopy(DirectoryInfo dir, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var destDirPath = Path.Combine(dir.Parent.FullName, destDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + dir.FullName);
            }


            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirPath))
            {
                Directory.CreateDirectory(destDirPath);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            if (files.Length >= 30)
            {
                throw new Exception("The number of files is conspicuously large.  An error has been thrown to ensure the directory is correct.");
            }

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirPath, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirPath, subdir.Name);
                    DirectoryCopy(subdir, temppath, copySubDirs);
                }
            }

            return new DirectoryInfo(destDirPath);
        }

        public QmsDocBase ProcessDoc(QmsDocBase doc, Dictionary<string, object> action_dict)
        {
            foreach (string propertyName in action_dict.Keys)
            {
                var propertyInfo = doc.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(doc, action_dict[propertyName]);
                }
            }
            return doc;
        }

            public QmsDocBase ProcessDoc(QmsDocBase doc, DocEdit docEdit)
        {
            var docProps = docEdit.ToCollection();
            foreach (DocProperty docProp in docProps)
            {
                var propertyInfo = doc.GetType().GetProperty(docProp.Name);
                if (propertyInfo != null)
                {
                        propertyInfo.SetValue(doc, docProp.Value);
                }
                else
                {
                    throw new Exception("The DocActionName passed is not part of IDocActions.");
                }
            }
            return doc;
        }
        public Boolean ProcessFiles(DocEdit docEdit) 
        {
            Contract.Requires(this.OriginalDirPath != null);
            Contract.Requires(this.docManagerConfig != null);
            Contract.Requires(this.dirFilesUnsafe.Count >= 1);

            if(this.DirFiles == null)
            {
                throw new ProjectDirNotSetException();
            }

            foreach (FileInfo file_info in this.DirFiles)
            {

                try
                {
                    QmsDocBase doc = this.CreateDoc(file_info);
                    this.ProcessDoc(doc, docEdit);
                    if (SimpleIoc.Default.GetInstance<DocManagerConfig>().CloseDocs == true)
                    {
                        doc.CloseDocument();
                    }
                    System.Windows.Forms.MessageBox.Show("Finished Processing Files");
                }

                catch (Exception e)
                {
                    this.CloseApps();
                    throw e;

                }
            }
            this.dirFilesUnsafe = new List<FileInfo>();
            this.CloseApps();
            return true;

        }
        public QmsDocBase CreateDoc(FileInfo file_info)
        {
            if (this.DocManagerConfig.WordDocExtensions.Contains(file_info.Extension))
            {
                    QmsDocBase doc = new WordDoc(this.WordApp, file_info, this.wordConfig, this.docManagerConfig);
                    return doc;
            }
 
                // create word doc and process
            else if (this.DocManagerConfig.ExcelDocExtensions.Contains(file_info.Extension))
            {
                // create excel doc and process
                    QmsDocBase doc = new ExcelDoc(
                        this.ExcelApp, 
                        file_info,
                        this.excelConfig,
                        this.docManagerConfig);
                    return doc;

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void CreateApps()
        {
            if (this.wordApp == null)
            {
                this.wordApp = new Word.Application();
            }

            if (this.excelApp == null)
            {
                this.excelApp = new Excel.Application();
            }
        }

        public void CloseApps()
        {
            if (this.wordApp != null)
            {
                try
                {
                    this.wordApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
                    if (this.wordApp.Documents.Count > 0 )
                    {
                        this.wordApp.Documents.Close(
                            Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges,
                            Microsoft.Office.Interop.Word.WdOriginalFormat.wdOriginalDocumentFormat
                        );
                    }
                    this.wordApp.Quit();
                }
                catch (Exception e)
                {
                    //Do something
                    throw e;
                }
            }
            
            
            if (this.excelApp != null)
            {
                try
                {
                    this.excelApp.DisplayAlerts = false;
                    this.excelApp.Workbooks?.Close();
                    this.excelApp.Quit();
                }

                catch (Exception e)
                {
                    //Do something
                    throw e;
                }
            }
            
        }

        public Boolean HasOpenFilePath(System.IO.FileInfo file_info)
        {
            return this.HasOpenFilePath(file_info.Name);
        }

        public Boolean HasOpenFilePath(string file_name)
        {
            if (file_name.StartsWith("~"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    CloseApps();
                }
            }
        }
        #endregion
    }

}
