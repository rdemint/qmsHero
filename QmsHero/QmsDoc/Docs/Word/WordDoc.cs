using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Core;
using QmsDoc.Interfaces;

namespace QmsDoc.Docs.Word
{
    public class WordDoc : Doc
    {
        WordDocConfig docConfig;
        static List<string> fileExtensions = new List<string> { ".docx", ".doc", ".docm", ".dotm" };
        public WordDoc(): base()
        {
        }

        public WordDoc(FileInfo fileInfo) : this()
        {
            this.FileInfo = fileInfo;
            DocConfig = new WordDocConfig();
        }

        public WordDoc(FileInfo fileInfo, WordDocConfig docConfig) : this()
        {

            this.FileInfo = fileInfo;
            this.DocConfig = docConfig;
        }

        public new WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public List<string> FileExtensions { get => fileExtensions; }

        
        public override Result<int> Process(QDocProperty qprop)
        {
           Result<int> result;
            if (qprop as IWriteFileInfo != null)
                {
                    return qprop.Write(FileInfo, DocConfig);
                }

                else if(qprop as IWriteDocRegex !=null)
            {

                var prop = qprop as IWriteDocRegex;
                using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
                {
                    result = prop.Write(doc);
                }
                return result;
            }    
            
            else
                {
                    using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
                    {
                        result = qprop.Write(doc, DocConfig);
                    }
                    return result;
                }
        }


       
        public override Result<int> Inspect(QDocProperty prop)
        {

            Result<int> result;

            //try
            //{
            if (prop as IReadFileInfo != null)
            {
                result = prop.Read(FileInfo, DocConfig);
            }

            else if(prop as IReadDocRegex != null) {

                using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
                {
                    result = prop.Read(doc);
                }
            }

            else
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
                {
                    result = prop.Read(doc, DocConfig);
                }
            }
            return result;

        }
            
        public static List<string> Extensions()
        {
            return fileExtensions;
        }
    }
}
