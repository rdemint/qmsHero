using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    interface IReadDocRegex

    {
        Result<QDocProperty> Read(WordprocessingDocument doc, Regex rx);

        Result<QDocProperty> Read(SpreadsheetDocument doc, Regex rx);
    }
}
