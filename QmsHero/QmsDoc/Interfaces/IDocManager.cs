using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    public interface IDocManager
    {
        bool ProcessFiles(DocState docEdit);
    }
}
