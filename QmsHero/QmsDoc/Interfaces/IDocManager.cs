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
        void ProcessFiles(List<IDocActionControl> actionControls, bool test = false);
    }
}
