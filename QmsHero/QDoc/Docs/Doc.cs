using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using QDoc.Interfaces;
using System.IO;
using QDoc.Core;
using QDoc.Exceptions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QFileUtil;

namespace QDoc.Docs
{
    public class Doc : IDoc
    {
        FileInfo fileInfo;
        IQDocConfig docConfig;


        public Doc()
        {

        }

        public Doc(System.IO.FileInfo fileInfo) : base()
        {
            this.FileInfo = fileInfo;
        }

        public Doc(FileInfo fileInfo, )

        #region Config
        public FileInfo FileInfo { 
            get => fileInfo;
            set { fileInfo = value; } }

        public IQDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        #endregion

        public virtual IDoc Process(IQDocState docState, DirectoryInfo targetDir)
        {
            var targetFile = FileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new Doc(targetFile);
            targetDoc.Process(docState);
            return targetDoc;
        }

        public virtual void Process(IQDocState docState)
        {
           var docProps = docState.ToCollection();
           foreach (QDocProperty docProp in docProps)
                {
                    Process(docProp);
                }
        }

        public IDoc Process(QDocProperty prop, DirectoryInfo targetDir)
        {
            var targetFile = FileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new Doc(targetFile);
            targetDoc.Process(prop);
            return targetDoc;
        }
        public virtual void Process(QDocProperty prop)
        {
            throw new NotImplementedException();
        }


        public virtual IQDocState Inspect(IQDocState docState)
        {
            throw new NotImplementedException();
        }
            
        public virtual QDocProperty Inspect(QDocProperty prop)
        {
            throw new NotImplementedException();
        }
    }
}
