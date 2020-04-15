using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    public interface IDocConfig
    {
        string EffectiveDateText { get; set; }
        string RevisionText { get; set; }
        
    }
}
