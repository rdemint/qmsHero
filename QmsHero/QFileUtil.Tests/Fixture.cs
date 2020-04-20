using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFileUtil.Tests
{
    class Fixture : FileCopyManager
    {
        public Fixture(): base()
        {
            var topdir = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent;
            ReferenceDir = FileUtil.SearchSubDirectory(topdir, "Reference");
            ProcessingDir = FileUtil.SearchSubDirectory(topdir, "Processing");
        }
    }
}
