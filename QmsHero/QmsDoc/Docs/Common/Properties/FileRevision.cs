using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties
{
    public class FileRevision : DocProperty, IReadFileInfo, IWriteFileInfo

    {
        public FileRevision()
        {
        }

        public FileRevision(object state) : base(state)
        {
        }

        //public override DocProperty Read(FileInfo file, ExcelDocConfig config)
        //{
        //    Match match = config.FileRevisionRegex.Match(file.Name);
        //    string result = match.ToString().Replace(config.FileRevisionText, "");
        //    return new FileRevision(result);
        //}

        public override DocProperty Read(FileInfo file, DocConfig config)
        {
            Match match = config.FileRevisionRegex.Match(file.Name);
            string result = match.ToString().Replace(config.FileRevisionText, "");
            return new FileRevision(result);
        }

        public void Write(FileInfo file, DocConfig config)
        {
            Match match = config.FileRevisionRegex.Match(file.Name);
            string result = match.ToString().Replace(config.FileRevisionText, "");
        }
    }
}
