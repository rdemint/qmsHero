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
            if(!IsValid(config))
            {
                return Results.Fail(new Error("Could not write to the file."));
            }
            
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            string newName;

            if (matchForm.Success)
            {
                newName = file.Name.Replace(matchForm.ToString(), this.State.ToString());

            }
            else if (matchSop.Success)
            {
                newName = file.Name.Replace(matchSop.ToString(), this.State.ToString());
            }

            else
            {
                return Results.Fail(new Error("Could not identify the current document number to replace."));
            }

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

        public override bool IsValid(object iConfig)
        {
            DocConfig config = iConfig as DocConfig;
            Match matchFormState = config.FileFormNumberRegex.Match(this.State.ToString());
            Match matchSopState = config.FileSopNumberRegex.Match(this.State.ToString());

            if (matchFormState.Success == false && matchSopState.Success == false)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

    }
}
