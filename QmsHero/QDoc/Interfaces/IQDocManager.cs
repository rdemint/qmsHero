using QDoc.Core;
using QFileUtil;
using System.Collections.Generic;
using System.IO;

namespace QDoc.Interfaces
{
    public interface IQDocManager
    {
        IQDocManagerConfig DocManagerConfig { get; set; }
        IFileCopyManager FileManager { get; set; }
        IQDocFactory DocFactory { get; set; }

        bool CanProcessFiles();

        void Process(FileInfo file, QDocProperty docProp);
        void Process(QDocPropertyCollection docEdit);
        void Process(QDocProperty docProp);
    }
}