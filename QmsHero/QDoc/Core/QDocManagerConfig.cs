
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class DocManagerConfig
    {
        bool saveChanges;
        bool closeDocs;
        string docPassword;
        int logoHeight;
        int safeProcessingLength;
        List<string> wordDocExtensions;
        List<string> excelDocExtensions;

        public DocManagerConfig()
        {
            Initialize();
        }

        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public string DocPassword { get => docPassword; set => docPassword = value; }
        public bool SaveChanges { get => saveChanges; set => saveChanges = value; }
        public bool CloseDocs { get => closeDocs; set => closeDocs = value; }
        public int SafeProcessingLength { get => safeProcessingLength; set => safeProcessingLength = value; }
        public List<string> ExcelDocExtensions { get => excelDocExtensions; set => excelDocExtensions = value; }
        public List<string> WordDocExtensions { get => wordDocExtensions; set => wordDocExtensions = value; }

        public void Initialize() {
            
            this.DocPassword = "QMSpwd";
            this.CloseDocs = true;
            this.SaveChanges = true;
            this.LogoHeight = 28;
            this.SafeProcessingLength = 15;
            this.WordDocExtensions = new List<string> { ".docx", ".doc" };
            this.ExcelDocExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };
        }
    }
}

