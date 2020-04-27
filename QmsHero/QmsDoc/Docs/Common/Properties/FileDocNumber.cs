using QmsDoc.Core;
using QmsDoc.Exceptions;
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
    public class FileDocNumber: DocProperty, IReadFileInfo, IWriteFileInfo
    {
        public FileDocNumber()
        {
        }

        public FileDocNumber(object state) : base(state)
        {
        }

        public override void Write(FileInfo file, DocConfig config)
        {
            base.Write(file, config);
        }

        public override DocProperty Read(FileInfo file, DocConfig config)
        {
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            if(matchForm.Success)
            {
                return new FileDocNumber(matchForm.ToString());
            }
            else if (matchSop.Success) {
                return new FileDocNumber(matchSop.ToString());
            }

            else
            {
                throw new DocProcessingException();
            }
            
        }
    }
}
