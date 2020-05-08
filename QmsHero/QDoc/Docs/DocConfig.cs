using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QDoc.Docs
{
    public class DocConfig: IDocConfig
    {
        string fileRevisionText;
        Regex fileRevisionRegex;
        Regex fileSopNumberRegex;
        Regex fileFormNumberRegex;
        Regex fileNumberRegex;

        public DocConfig()
        {
            this.FileRevisionText = "Rev";
            this.FileSopNumberRegex = new Regex(@"SOP-\d\d\d");
            this.FileFormNumberRegex = new Regex(@"F-\d\d\d\w");
            this.FileNumberRegex = new Regex(@"-\d\d\d");
        }

        public Regex FileSopNumberRegex { get => fileSopNumberRegex; set => fileSopNumberRegex = value; }
        public Regex FileFormNumberRegex { get => fileFormNumberRegex; set => fileFormNumberRegex = value; }
        public string FileRevisionText { get => fileRevisionText;
            set { 
                
                fileRevisionText = value;
                this.FileRevisionRegex = new Regex(Regex.Escape(fileRevisionText) + @"\d{1,2}");
            }
        }
        public Regex FileRevisionRegex { get => fileRevisionRegex; set => fileRevisionRegex = value; }
        public Regex FileNumberRegex { get => fileNumberRegex; set => fileNumberRegex = value; }
    }
}
