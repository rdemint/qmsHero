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

namespace QmsDoc.Core
{
    public class DocManager
    {
        System.IO.DirectoryInfo topDir;
        DocManagerConfig config;
        List<FileInfo> dirFiles;
        List<string> wordDocExtensions;
        List<string> excelDocExtensions;
        Word.Application wordApp;
        Excel.Application excelApp;
        string doc_password;
        string dirPath;
        Boolean auto_close_doc;

        public List<FileInfo> DirFiles { get => dirFiles; set => dirFiles = value; }

        public DocManager()
        {
            this.wordDocExtensions = new List<string> { ".docx", ".doc" };
            this.excelDocExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };
            wordApp = new Word.Application();
            excelApp = new Excel.Application();
            this.dirFiles = new List<FileInfo>();
        }
        public void AddDirFiles(string dir_path)
        {
            var dir = new System.IO.DirectoryInfo(dir_path);
            var dir_files = dir.GetFiles();
            this.dirFiles.AddRange(dir_files);
            var sub_dirs = Directory.GetDirectories(dir_path);

            foreach (string sub_dir_path in Directory.EnumerateDirectories(dir_path))
            {
                this.AddDirFiles(sub_dir_path);
            }
        }

        public void Config(DocManagerConfig config)
        {
            this.config = config;
        }

        public void ConfigDir(string dir_path)
        {
            this.dirPath = dir_path;
            this.topDir = new System.IO.DirectoryInfo(dir_path);
            this.AddDirFiles(dir_path);
        }


        public void DocMethodInfo() { }
        public void PreProcessing()
        {
            Word.Documents documents = this.wordApp.Documents;
            Excel.Workbooks workbooks = this.excelApp.Workbooks;
        }

        public void ProcessDoc(QmsDocBase doc, Dictionary<string, object> action_dict)
        {
            foreach (string propertyName in action_dict.Keys)
            {
                var propertyInfo = doc.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(doc, action_dict[propertyName]);
                }
            }
            var closed = doc.CloseDocument();
        }

        public void ProcessDoc(QmsDocBase doc, List<IDocActionControl> actionControls)
        {
            foreach (IDocActionControl actionControl in actionControls)
            {
                var propertyInfo = doc.GetType().GetProperty(actionControl.DocActionName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(doc, actionControl.DocActionVal);
                }
                else
                {
                    throw new Exception("The DocActionName passed is not part of IDocActions.");
                }
            }
        }
        public Boolean ProcessFiles(Dictionary<string, object> action_dict)
        {
            Contract.Requires(this.config != null);
            Contract.Requires(this.dirFiles.Count >= 1);
            Contract.Requires(action_dict.Count >= 1);

            foreach (FileInfo file_info in this.dirFiles)
            {
                QmsDocBase doc = this.CreateDoc(file_info);
                this.ProcessDoc(doc, action_dict);
                if (this.auto_close_doc)
                {
                    doc.CloseDocument();
                }
            }


            return true;
        }

        public void ProcessFiles(List<IDocActionControl> actionControls, bool test=false)
        {
            Contract.Requires(this.config != null);
            Contract.Requires(this.dirFiles.Count >= 1);
            Contract.Requires(actionControls.Count >= 1);


            foreach (FileInfo file_info in this.dirFiles)
            {
                QmsDocBase doc = this.CreateDoc(file_info, test);
                this.ProcessDoc(doc, actionControls);
                if (this.auto_close_doc)
                {
                    doc.CloseDocument();
                }
            }
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
                    QmsDocBase doc = new ExcelDoc(this.excelApp, file_info);
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
