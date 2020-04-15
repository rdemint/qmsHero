using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    public interface IDocProperty
    {
        string Name { get; set; }
        string Value { get; set; }

        virtual string Get(object doc, IDocConfig config);
        virtual void Set(object doc, IDocConfig config);
        bool IsValid();
    }
}
