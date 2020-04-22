using QDoc.Interfaces;
using QmsDoc.Core;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml.Docs.Common.Properties
{
    public class IsSop : DocProperty
    {
        public IsSop()
        {
        }

        public IsSop(object state) : base(state)
        {
            if(state is bool)
            {
                this.State = (bool)state;
            }

            else
            {
                throw new ArgumentException();
            }

        }

        public override DocProperty Read(FileInfo file, WordDocConfig config)
        {
            Match match = config.IsSopRegex.Match(file.Name);
            return new IsSop(match.Success);
        }
    }
}
