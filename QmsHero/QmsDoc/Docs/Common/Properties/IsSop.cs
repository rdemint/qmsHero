using QDoc.Interfaces;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties
{
    public class IsSop : DocProperty, IReadFileInfoOnly
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

        public override DocProperty Read(FileInfo file, DocConfig config)
        {
            Match match = config.FileIsSopRegex.Match(file.Name);
            return new IsSop(match.Success);
        }

        //public override DocProperty Read(FileInfo file, ExcelDocConfig config)
        //{
        //    Match match = config.FileIsSopRegex.Match(file.Name);
        //    return new IsSop(match.Success);
        //}
    }
}
