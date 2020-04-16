using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QDoc.Interfaces
{
    public interface IDocConfig
    {
        string EffectiveDateText { get; set; }
        Regex EffectiveDateRegex { get; set; }
        string RevisionText { get; set; }
        
    }
}
