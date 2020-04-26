using QFileUtil;
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

        public override void Write(FileInfo file, DocConfig config)
        {
            Match fileRevTextMatch = config.FileRevisionRegex.Match(file.Name);
            string fileRev = fileRevTextMatch.ToString().Replace(config.FileRevisionText, "");
            //Match fileRev = Regex.Match(fileRev, @"\d{1,2}");
            string newFileRevText = fileRevTextMatch.ToString().Replace(fileRev, (string)this.State);
            string newFileName = file.Name.Replace(fileRevTextMatch.ToString(), newFileRevText);
            FileUtil.FileRename(file, newFileName);
        }
    }
}
