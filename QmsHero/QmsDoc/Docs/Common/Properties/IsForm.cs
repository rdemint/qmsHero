using QmsDoc.Core;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties
{
    public class IsForm : DocProperty
    {
        public IsForm()
        {
        }

        public IsForm(object state) : base(state)
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
            Match match = config.IsFormRegex.Match(file.Name);
            return new IsForm(match.Success);
        }
    }
}
