using System.Collections.Generic;

namespace QDoc.Core
{
    public interface IQDocManagerConfig
    {
        int SafeProcessingLength { get; set; }

        void Initialize();
    }
}