using DocumentFormat.OpenXml.Bibliography;
using FluentResults;
using QDoc.Core;
using QDoc.Docs;
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
            Match fileMatch;
            string patternToInspectFile = (string)this.state;
            Match isValidFormPatternMatch = config.FileFormNumberRegex.Match(patternToInspectFile);
            Match isValidSopPatternMatch = config.FileSopNumberRegex.Match(patternToInspectFile);
            Match isValidNumberPatternMatch = config.FileNumberRegex.Match(patternToInspectFile);

            if (isValidFormPatternMatch.Success)
            {
                fileMatch = config.FileFormNumberRegex.Match(file.Name);
                return WriteBase(file, fileMatch);

            }
            else if (isValidSopPatternMatch.Success)
            {
                fileMatch = config.FileSopNumberRegex.Match(file.Name);
                return WriteBase(file, fileMatch);
            }

            else if (isValidNumberPatternMatch.Success)
            {
                fileMatch = config.FileNumberRegex.Match(file.Name);
                return WriteBase(file, fileMatch);
            }

            else
            {
                return Results.Fail(new Error("Could not identify the current document number to replace."));
            }
        }

        public Result<QDocProperty> WriteBase(FileInfo file, Match fileMatch)
        {
            if (fileMatch.Success)
            {
                string newName = file.Name.Replace(fileMatch.ToString(), this.State.ToString());
                FileUtil.FileRename(file, newName);
                return Results.Ok<QDocProperty>(new FileDocNumber((string)this.State, 1));
            }

            else
            {
                return Results.Fail(new Error($"Was not able to find the pattern {this.state.ToString()} in the file name '{file.Name}'."));
            }
        }

        public override Result<QDocProperty> Read(FileInfo file, DocConfig config)
        {
            //Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            //Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            //if(matchForm.Success)
            //{
            //    return Results.Ok<QDocProperty>(new FileDocNumber(matchForm.ToString()));
            //}
            //else if (matchSop.Success) {
            //    return Results.Ok<QDocProperty>(new FileDocNumber(matchSop.ToString()));
            //}

            //else
            //{
            //    return Results.Fail<QDocProperty>(new Error("Could not identify the current document number to replace."));
            //}
            Match isValidFormFileMatch = config.FileFormNumberRegex.Match(file.Name);
            Match isValidSopFileMatch = config.FileSopNumberRegex.Match(file.Name);
            Match isValidNumberFile = config.FileNumberRegex.Match(file.Name);
            string newName;

            if (isValidFormFileMatch.Success)
            {
                return Results.Ok<QDocProperty>(new FileDocNumber(isValidFormFileMatch.ToString(), 1));
            }
            else if (isValidSopFileMatch.Success)
            {
                return Results.Ok<QDocProperty>(new FileDocNumber(isValidSopFileMatch.ToString(), 1));
            }

            else if (isValidNumberFile.Success)
            {
                return Results.Ok<QDocProperty>(new FileDocNumber(isValidNumberFile.ToString(), 1));
            }

            else
            {
                return Results.Fail(new Error("Could not identify the current document number to replace."));
            }
        }
    }
}
