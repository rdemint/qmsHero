using QDoc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Interfaces
{
    public interface IQDoc
    {
        void Process(DocState state);
        IQDoc Process(DocState state, DirectoryInfo targetDir);
        DocState Inspect(bool filter = false);
    }
}
