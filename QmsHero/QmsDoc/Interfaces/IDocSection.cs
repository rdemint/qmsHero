using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    
    public enum SectionType
    {
        HeaderFooter,
        Body
    }
    
    interface IDocSection

    {
        SectionType SectionType { get; set; }
    }
}
