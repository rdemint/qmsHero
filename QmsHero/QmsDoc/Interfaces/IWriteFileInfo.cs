using FluentResults;
using QDoc.Core;
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
        Result<int> Write(FileInfo file, DocConfig config);
    }
}
