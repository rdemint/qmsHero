using QDoc.Interfaces;
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
            if(!IsValid(config))
            {
                throw new InvalidDocPropertyStateException();
            }
            
            Match matchForm = config.FileFormNumberRegex.Match(file.Name);
            Match matchSop = config.FileSopNumberRegex.Match(file.Name);
            string newName;

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

        public override bool IsValid(IDocConfig iconfig)
        {
            DocConfig config = iconfig as DocConfig;
            Match matchFormState = config.FileFormNumberRegex.Match(this.State.ToString());
            Match matchSopState = config.FileSopNumberRegex.Match(this.State.ToString());

            if (matchFormState.Success == false && matchSopState.Success == false)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
