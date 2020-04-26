using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties
{
    public class FileRevision : DocProperty, IReadFileInfoOnly

    {
        public FileRevision()
        {
        }

        public FileRevision(object state) : base(state)
        {
        }

        public override DocProperty Read(FileInfo file, ExcelDocConfig config)
        {
            Match match = config.
        }

        public override DocProperty Read(FileInfo file, WordDocConfig config)
        {
            return base.Read(file, config);
        }
    }
}
