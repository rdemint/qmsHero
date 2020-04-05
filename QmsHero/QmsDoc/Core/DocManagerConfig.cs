using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocManagerConfig
    {
        bool saveChanges;
        bool closeDocs;
        string docPassword;
        int logoHeight;

        public DocManagerConfig()
        {
            Initialize();
        }

        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public string DocPassword { get => docPassword; set => docPassword = value; }
        public bool SaveChanges { get => saveChanges; set => saveChanges = value; }
        public bool CloseDocs { get => closeDocs; set => closeDocs = value; }

        public void Initialize() {
            
            this.DocPassword = "QMSpwd";
            this.CloseDocs = true;
            this.SaveChanges = true;
            this.LogoHeight = 28;
        }
    }
}
