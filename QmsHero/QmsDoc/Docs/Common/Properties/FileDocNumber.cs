using QFileUtil;
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
            ValidateState(config);
            
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            string newName = null;
            if (matchForm.Success)
            {
                newName = file.Name.Replace(matchForm.ToString(), this.State.ToString());

            }
            else if (matchSop.Success)
            {
                newName = file.Name.Replace(matchSop.ToString(), this.State.ToString());
            }

            else
            {
                throw new DocWriteException();
            }

            FileUtil.FileRename(file, newName);


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
                throw new DocReadException();
            }
            
        }

        public void ValidateState(DocConfig config)
        {
            Match matchFormState = config.FileFormNumberRegex.Match(this.State.ToString());
            Match matchSopState = config.FileSopNumberRegex.Match(this.State.ToString());

            if (matchFormState.Success == false && matchSopState.Success == false)
            {
                throw new InvalidDocPropertyStateException();
            }
        }
    }
}
