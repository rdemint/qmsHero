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
        void Process(QDocState state);
        IQDoc Process(QDocState state, DirectoryInfo targetDir);
        QDocState Inspect(bool filter = false);
    }
}
