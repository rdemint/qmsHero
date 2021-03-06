﻿using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;
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
    public class IsSop : DocProperty, IReadFileInfo
    {
        public IsSop()
        {
        }

        public IsSop(object state) : base(state)
        {
            if(state is bool)
            {
                this.State = (bool)state;
            }

            else
            {
                throw new ArgumentException();
            }

        }

        public override Result<QDocProperty> Read(FileInfo file, DocConfig config)
        {
            Match match = config.FileSopNumberRegex.Match(file.Name);
            if(match.Success)
            {
                return Results.Ok<QDocProperty>(new IsSop(match.Success));
            }
            else
            {
                return Results.Fail(new Error($"File {file.Name} did not match the criteria {config.FileNumberRegex};"));
            }
        }

    }
}
