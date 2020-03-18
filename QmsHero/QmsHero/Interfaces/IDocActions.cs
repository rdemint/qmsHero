using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocLib
{
    public interface IDocActions
    {
        string LogoPath { get; set; }

        string LogoText { get; set; }
        string EffectiveDate { get; set; }
        string Revision { get; set; }


    }
}
