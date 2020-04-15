using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    public interface IQmsDoc
    {
        void Process(DocState state);
        IQmsDoc Process(DocState state, DirectoryInfo targetDir);
        DocState Inspect(bool filter = false);
    }
}
