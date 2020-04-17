using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocManager : QDocManager
    {
        public DocManager(): base()
        {
        }

        public override void ProcessFiles(IDocState docEdit)
        {
            throw new NotImplementedException();
        }
        public override void ProcessFiles(QDocProperty docProp)
        {
            throw new NotImplementedException();
        }
    }
}
