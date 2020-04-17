
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QDoc.Docs
{
    public class DocConfig: IDocConfig
    {
        public DocConfig():base()
        {
            this.Initialize();
        }

        public virtual void Initialize()
        {
            throw new NotImplementedException();
        }

    }
}
