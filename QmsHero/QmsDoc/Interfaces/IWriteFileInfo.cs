﻿using QmsDoc.Docs.Common;
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
        void Write(FileInfo file, DocConfig config);
    }
}