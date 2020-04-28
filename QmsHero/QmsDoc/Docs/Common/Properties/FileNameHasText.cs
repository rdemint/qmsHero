using QmsDoc.Core;
using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties
{
    class FileNameHasText: DocProperty, IReadFileInfo
    {
        public FileNameHasText()
        {

        }

        public FileNameHasText(object state) : base(state)
        {
        }

        public override DocProperty Read(FileInfo file, DocConfig config)
        {
            return base.Read(file, config);
        }
    }
}
