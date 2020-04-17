using QDoc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Interfaces
{
    public interface IDoc
    {
        FileInfo FileInfo { get; set; }
        void Process(IQDocState state);
        IDoc Process(IQDocState state, DirectoryInfo targetDir);

        void Process(QDocProperty prop);
        IDoc Process(QDocProperty prop, DirectoryInfo targetDir);
        QDocProperty Inspect(QDocProperty prop);
    }
}
