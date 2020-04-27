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
        void Process(QDocPropertyCollection state);

        void Process(QDocProperty prop);
        QDocProperty Inspect(QDocProperty prop);

        QDocPropertyCollection Inspect(QDocPropertyCollection state);
    }
}
