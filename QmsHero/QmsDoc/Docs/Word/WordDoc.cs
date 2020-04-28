﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;
using QmsDoc.Core;
using QmsDoc.Exceptions;
using QmsDoc.Interfaces;

namespace QmsDoc.Docs.Word
{
    public class WordDoc : Doc
    {
        WordDocConfig docConfig;
        static List<string> fileExtensions = new List<string> { ".docx", ".doc", ".docm", ".dotm" };
        public WordDoc()
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

        public override Result<QDocProperty> Process(QDocProperty qprop)
        {
            if (qprop as IWriteFileInfo != null)
            {
                return qprop.Write(FileInfo, DocConfig);
            }

            else
            {
                Result<QDocProperty> result;
                using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
                {
                    //var prop = qprop as DocProperty;
                    result = qprop.Write(doc, DocConfig);
                }
                return result;
            }
        }


        public override Result<QDocProperty> Inspect(QDocProperty prop)
        {

            Result<QDocProperty> result;

            if (prop as IReadFileInfo != null)
            {
                result = prop.Read(FileInfo, DocConfig);
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
