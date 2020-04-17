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
        void Process(IDocState state);
        IDoc Process(IDocState state, DirectoryInfo targetDir);

        void Process(QDocProperty prop);
        IDoc Process(QDocProperty prop, DirectoryInfo targetDir);
        QDocProperty Inspect(QDocProperty prop);
    }
}
