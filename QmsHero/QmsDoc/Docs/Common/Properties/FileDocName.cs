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
    class FileDocName : DocProperty, IReadFileInfo, IWriteFileInfo
    {
        public FileDocName()
        {
        }

        public FileDocName(object state) : base(state)
        {
        }

        public override void Write(FileInfo file, DocConfig config)
        {
            base.Write(file, config);
        }

        public override DocProperty Read(FileInfo file, DocConfig config)
        {
            return base.Read(file, config);
        }
    }
}
