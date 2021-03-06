﻿using FluentResults;
using QDoc.Core;
using QDoc.Docs;
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

        private FileRevision(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(FileInfo file, DocConfig config)
        {
            Match match = config.FileRevisionRegex.Match(file.Name);
            if (match.Success)
            {
                string result = match.ToString().Replace(config.FileRevisionText, "");
                return Results.Ok<QDocProperty>(new FileRevision(result, 1));

            }

            else
            {
                return Results.Fail(new Error($"Could not find a text match with the pattern '{this.state}' in the file name {file.Name}."));
            } 
        }

        public override Result<QDocProperty> Write(FileInfo file, DocConfig config)
        {
            Match fileRevTextMatch = config.FileRevisionRegex.Match(file.Name);
            if(fileRevTextMatch.Success)
        {
            string fileRev = fileRevTextMatch.ToString().Replace(config.FileRevisionText, "");
            string newFileRevText = fileRevTextMatch.ToString().Replace(fileRev, (string)this.State);
            string newFileName = file.Name.Replace(fileRevTextMatch.ToString(), newFileRevText);
            FileUtil.FileRename(file, newFileName);
            return Results.Ok<QDocProperty>(new FileRevision((string)this.State, 1));

        }
            else
            {
                return Results.Fail(new Error($"Could not find a text match with the pattern '{this.state}' to replace in the file name {file.Name}."));
            }
        }

    }
}
