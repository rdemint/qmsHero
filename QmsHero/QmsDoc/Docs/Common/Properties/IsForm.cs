using FluentResults;
using QDoc.Core;
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
    public class IsForm : DocProperty, IReadFileInfo
    {
        public IsForm()
        {
        }

        public IsForm(object state) : base(state)
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
            Match match = config.FileFormNumberRegex.Match(file.Name);
            return Results.Ok<QDocProperty>(new IsForm(match.Success));
        }

    }
}
