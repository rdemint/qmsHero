using QDoc.Core;
using QDoc.Interfaces;
using QFileUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocManager : QDocManager
    {
        public DocManager(): base()
        {
            this.FileManager = new FileCopyManager();
            this.DocManagerConfig = new DocManagerConfig();
            this.DocFactory = new DocFactory();
        }

        public DocManager(IFileCopyManager fManager)
        {
            //This Constructor useful in unit tests where concrete fixture can be passed
            this.FileManager = fManager;
            this.DocManagerConfig = new DocManagerConfig();
            this.DocFactory = new DocFactory();

        }

        public override void Process(IDocState docEdit)
        {
            throw new NotImplementedException();
        }
        public override void Process(QDocProperty docProp)
        {
            throw new NotImplementedException();
        }

        public override void Process(FileInfo file, QDocProperty docProp)
        {
            var fileCopy = FileManager.CopyToProcessingDir(file);
            var doc = DocFactory.CreateDoc(file);
            doc.Process(docProp);
        }
    }
}
