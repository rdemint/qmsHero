using QmsDoc.Core;
using QmsDoc.Docs.Common;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    interface IReadFileInfoOnly
    {
        DocProperty Read(FileInfo file, DocConfig config);
        //DocProperty Read(FileInfo file, WordDocConfig config);
    }
}
