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
        Regex FileNumberRegex { get; set; }
        Regex FileSopNumberRegex { get; set; }
        Regex FileFormNumberRegex { get; set; }
    }
}
