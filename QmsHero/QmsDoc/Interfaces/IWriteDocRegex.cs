﻿using DocumentFormat.OpenXml.Packaging;
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
    public interface IWriteDocRegex
    {
        Regex Regex { get; }
        Result<QDocProperty> Write(WordprocessingDocument doc);

        Result<QDocProperty> Write(SpreadsheetDocument doc);
    }
}
