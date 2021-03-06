﻿using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QFileUtil;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
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
    public class FileDocName : DocProperty, IReadFileInfo, IWriteFileInfo
    {
        public FileDocName()
        {
        }

        public FileDocName(object state) : base(state)
        {
        }

        private FileDocName(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Write(FileInfo file, DocConfig config)
        {
            Result<QDocProperty> result;
            if (WordDoc.Extensions().Contains(file.Extension))
            {
                var tempDoc = new WordDoc(file, config as WordDocConfig);
                result = tempDoc.Inspect(new FileDocName());
                if(result.IsSuccess)
                {
                    string currentText = result.Value.State.ToString();
                    string newFileName = file.Name.Replace(currentText, this.State.ToString());
                    FileUtil.FileRename(file, newFileName);
                    return Results.Ok<QDocProperty>(new FileDocName((string)this.State, 1));
                }
            }

            else if (ExcelDoc.Extensions().Contains(file.Extension))
            {
                var tempDoc = new ExcelDoc(file, config as ExcelDocConfig);
                result = tempDoc.Inspect(new FileDocName());
                if (result.IsSuccess)
                {
                    string currentText = result.Value.State.ToString();
                    string newFileName = file.Name.Replace(currentText, this.State.ToString());
                    FileUtil.FileRename(file, newFileName);
                    return Results.Ok<QDocProperty>(new FileDocName((string)this.State, 1));
                }
            }

            else
            {
                return Results.Fail(
                    new Error("File did not have a valid extension to write to")
                        .CausedBy(new DocWriteException()));
            }
            return Results.Fail(new Error($"Did not match a name with pattern {this.state} in the file name {file.Name}."));
            
        }

        public override Result<QDocProperty> Read(FileInfo file, DocConfig config)
        {
            string docNumbertext = null;
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            if (matchForm.Success)
            {
                docNumbertext = matchForm.ToString();
            }
            else if (matchSop.Success)
            {
                docNumbertext = matchSop.ToString();
            }

            else
            {
                return Results.Fail(
                    new Error("Could not read the the property from the file")
                        .CausedBy(new DocReadException())
                        );
            }


            Match matchRev = config.FileRevisionRegex.Match(file.Name);
            if (!matchRev.Success)
            {
                return Results.Fail(
                    new Error("Could not read the the property from the file")
                        .CausedBy(new DocReadException())
                        );
            }

            string nameText = file.Name
                .Replace(docNumbertext, "")
                .Replace(matchRev.ToString(), "")
                .Replace(file.Extension, "")
                .Trim();
            return Results.Ok<QDocProperty>(new FileDocName(nameText, 1));
        }
    }
}
