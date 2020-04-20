﻿using System.Collections.Generic;
using System.IO;

namespace QFileUtil
{
    public interface IFileCopyManager
    {
        DirectoryInfo ProcessingDir { get; }
        DirectoryInfo ReferenceDir { get; }
        List<FileInfo> ProcessingFiles { get; }
        List<FileInfo> ReferenceFiles { get; }

        int CleanProcessingDir();
        bool ProcessingDirIsClean();
        bool IsReadyToCopy();
    }
}