using DocumentFormat.OpenXml.Bibliography;
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

        private FileDocNumber(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Write(FileInfo file, DocConfig config)
        {
            string patternToInspectFile = (string)this.state;
            Match isValidFormFileMatch = config.FileFormNumberRegex.Match(file.Name);
            Match isValidSopFileMatch = config.FileSopNumberRegex.Match(file.Name);
            Match isValidNumberFile = config.FileNumberRegex.Match(file.Name);
            string newName;

            if (isValidFormFileMatch.Success)
            {
                newName = file.Name.Replace(isValidFormFileMatch.ToString(), this.State.ToString());
                    FileUtil.FileRename(file, newName);
                    return Results.Ok<QDocProperty>(new FileDocNumber((string)this.State, 1));
            }
            else if (isValidSopFileMatch.Success)
            {
                newName = file.Name.Replace(isValidSopFileMatch.ToString(), this.State.ToString());
                FileUtil.FileRename(file, newName);
                return Results.Ok<QDocProperty>(new FileDocNumber((string)this.State, 1));
            }

            else if (isValidNumberFile.Success)
            {
                newName = file.Name.Replace(isValidNumberFile.ToString(), this.State.ToString());
                FileUtil.FileRename(file, newName);
                return Results.Ok<QDocProperty>(new FileDocNumber((string)this.State, 1));
            }

            else
            {
                return Results.Fail(new Error("Could not identify the current document number to replace."));
            }


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
