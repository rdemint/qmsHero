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
    public class FileDocName : DocProperty, IReadFileInfo, IWriteFileInfo
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
            string docNumbertext = null;
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            if (matchForm.Success)
            {
                docNumbertext = matchForm.ToString();
            }
            else if (matchSop.Success)
            {
                docNumbertext = matchSop.ToString();
            }

            else
            {
                throw new DocReadException();
            }


            Match matchRev = config.FileRevisionRegex.Match(file.Name);
            if (!matchRev.Success)
            {
                throw new DocReadException();
            }

            string nameText = file.Name
                .Replace(docNumbertext, "")
                .Replace(matchRev.ToString(), "")
                .Replace(file.Extension, "")
                .Trim();
            return new FileDocName(nameText);
        }
    }
}
