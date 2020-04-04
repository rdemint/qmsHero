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
        Boolean ProcessFiles(Dictionary<string, object> action_dict, bool test = false);
        bool ProcessFiles(DocEdit docEdit, bool test = false);
    }
}
