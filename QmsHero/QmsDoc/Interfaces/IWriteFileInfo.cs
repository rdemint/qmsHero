﻿using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Docs.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    interface IWriteFileInfo
    {
        Result<QDocProperty> Write(FileInfo file, DocConfig config);
    }
}
