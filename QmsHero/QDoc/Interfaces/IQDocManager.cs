﻿using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Interfaces
{
    public interface IQDocManager
    {
        bool ProcessFiles(QDocState docEdit);
    }
}