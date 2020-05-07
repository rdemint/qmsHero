using FluentResults;
using QDoc.Core;
using QDoc.Interfaces;
using QFileUtil;
using QmsDoc.Core;
using QmsDoc.Exceptions;
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
    public class FileDocNumber: DocProperty, IReadFileInfo, IWriteFileInfo
    {
        public FileDocNumber()
        {
        }

        public FileDocNumber(object state) : base(state)
        {
        }

        public override Result<QDocProperty> Write(FileInfo file, DocConfig config)
        {
            Match matchFile;
            string pattern = (string)this.state;
            Match matchForm = config.FileFormNumberRegex.Match(pattern);
            Match matchSop = config.FileSopNumberRegex.Match(pattern);
            Match matchBothFormAndSop = config.FileNumberRegex.Match(pattern);
            string newName;

            if (matchForm.Success)
            {
                matchFile = config.FileFormNumberRegex.Match(file.Name);
            }
            else if (matchSop.Success)
            {
                matchFile = config.FileSopNumberRegex.Match(file.Name);
            }

            else if (matchBothFormAndSop.Success)
            {
                matchFile = config.FileNumberRegex.Match(file.Name);
            } 
            
            else
            {
                return Results.Fail(new Error("Could not identify the current document number to replace."));
            }

            newName = file.Name.Replace(matchFile.ToString(), this.State.ToString());
            FileUtil.FileRename(file, newName);
            return Results.Ok<QDocProperty>(new FileDocNumber((string)this.State));


        }

        public override Result<QDocProperty> Read(FileInfo file, DocConfig config)
        {
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            if(matchForm.Success)
            {
                return Results.Ok<QDocProperty>(new FileDocNumber(matchForm.ToString()));
            }
            else if (matchSop.Success) {
                return Results.Ok<QDocProperty>(new FileDocNumber(matchSop.ToString()));
            }

            else
            {
                return Results.Fail<QDocProperty>(new Error("Could not identify the current document number to replace."));
            }
            
        }
    }
}
