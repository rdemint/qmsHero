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

namespace QmsDoc.Core
{
    public class DocManager: IDocManager
    {
        System.IO.DirectoryInfo topDir;
        DocManagerConfig managerConfig;
        ExcelDocConfig excelConfig;
        WordDocConfig wordConfig;
        List<FileInfo> dirFilesUnsafe;
        List<FileInfo> dirFiles;
        List<string> wordDocExtensions;
        List<string> excelDocExtensions;
        Word.Application wordApp;
        Excel.Application excelApp;
        string doc_password;
        string dirPath;
        Boolean auto_close_doc;

        public List<FileInfo> DirFiles {
            get => this.GetSafeFiles(dirFilesUnsafe); }


        private List<FileInfo> GetSafeFiles(List<FileInfo> files)
        {
            var result = files.Where((file) => file.Name.StartsWith("~") == false).ToList();
            return result;
        }
 
        public DocManager()
        {
            this.wordDocExtensions = new List<string> { ".docx", ".doc" };
            this.excelDocExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };
            //wordApp = new Word.Application();
            //excelApp = new Excel.Application();
            this.dirFilesUnsafe = new List<FileInfo>();
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

        public void ConfigDir(string dir_path)
        {
            this.dirPath = dir_path;
            this.topDir = new System.IO.DirectoryInfo(dir_path);
            this.AddDirFiles(dir_path);
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

        public QmsDocBase ProcessDoc(QmsDocBase doc, List<IDocActionControl> actionControls)
        {
            foreach (IDocActionControl actionControl in actionControls)
            {
                var propertyInfo = doc.GetType().GetProperty(actionControl.DisplayValue);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(doc, actionControl.DocActionVal);
                }
                else
                {
                    throw new Exception("The DocActionName passed is not part of IDocActions.");
                }
            }
            return doc;
        }

        private void ProcessFilesPrep(bool test)
        {
            if (this.wordApp == null && test==false || this.excelApp == null && test==false)
            {
                this.CreateApps();
            }
        }
        public Boolean ProcessFiles(Dictionary<string, object> action_dict, bool test=false)
        {
            Contract.Requires(this.managerConfig != null);
            Contract.Requires(this.dirFilesUnsafe.Count >= 1);
            Contract.Requires(action_dict.Count >= 1);
            
            this.ProcessFilesPrep(test);
            foreach (FileInfo file_info in this.DirFiles)
            {
                try
                {
                    QmsDocBase doc = this.CreateDoc(file_info);
                    this.ProcessDoc(doc, action_dict);
                    if (this.auto_close_doc)
                    {
                        doc.CloseDocument();
                    }

                }
                catch (Exception e)
                {
                    this.CloseApps();
                    throw e;
                }
            }

            this.CloseApps();
            return true;
        }

        public void ProcessFiles(List<IDocActionControl> actionControls, bool test=false)
        {
            Contract.Requires(this.managerConfig != null);
            Contract.Requires(this.dirFilesUnsafe.Count >= 1);
            Contract.Requires(actionControls.Count >= 1);

            this.ProcessFilesPrep(test);
            var filteredControls = this.FilterControls(actionControls);

            foreach (FileInfo file_info in this.DirFiles)
            {
                
                try
                {
                    QmsDocBase doc = this.CreateDoc(file_info, test);
                    this.ProcessDoc(doc, filteredControls);
                    if (SimpleIoc.Default.GetInstance<DocManagerConfig>().CloseDocs == true && test == false)
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
            this.CloseApps();
        }

        public List<IDocActionControl> FilterControls(List<IDocActionControl> controls)
        {
            var query = controls
                .Where(control => control.ControlIsValid == true)
                .Where(control => control.DocActionVal != null)
                .Where(control=> (string)control.DocActionVal != "");
            return query.ToList();

        }
        public QmsDocBase CreateDoc(FileInfo file_info, bool test=false)
        {
            if (this.wordDocExtensions.Contains(file_info.Extension))
            {
                if (test == false)
                {
                    QmsDocBase doc = new WordDoc(this.wordApp, file_info);
                    return doc;
                }
                else
                {
                    QmsDocBase doc = new WordDoc();
                    return doc;
                }
                // create word doc and process
            }
            else if (this.excelDocExtensions.Contains(file_info.Extension))
            {
                // create excel doc and process
                if (test == false)
                {
                    QmsDocBase doc = new ExcelDoc(
                        this.excelApp, 
                        file_info);
                    return doc;
                }
                else
                {
                    QmsDocBase doc = new ExcelDoc();
                    return doc;
                }
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
                    this.wordApp.Documents.Close(
                            Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges,
                            Microsoft.Office.Interop.Word.WdOriginalFormat.wdOriginalDocumentFormat
                        );
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
                    this.excelApp.Workbooks.Close();
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
    }
}
