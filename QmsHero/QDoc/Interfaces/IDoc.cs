using FluentResults;
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
        IDocConfig DocConfig { get; set; }
        FileInfo FileInfo { get; set; }
        QDocPropertyResultCollection PropertyResultCollection { get; set; }
        QDocPropertyResultCollection Process(QDocPropertyCollection state);

        Result<QDocProperty> Process(QDocProperty prop);
        Result<QDocProperty> Inspect(QDocProperty prop);

        QDocPropertyResultCollection Inspect(QDocPropertyCollection state);

        QDocPropertyResultCollection Process(QDocActionManager actionManager);

        QDocPropertyResultCollection Inspect(QDocActionManager actionManager);
    }
}
