using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common
{
    public class DocConfig: IDocConfig
    {
        string fileRevisionText;
        Regex fileRevisionRegex;
        Regex isSopRegex;
        Regex isFormRegex;

        public DocConfig()
        {
            this.FileRevisionText = "Rev";
            this.FileIsSopRegex = new Regex(@"SOP-");
            this.FileIsFormRegex = new Regex(@"F-");
        }

        public Regex FileIsSopRegex { get => isSopRegex; set => isSopRegex = value; }
        public Regex FileIsFormRegex { get => isFormRegex; set => isFormRegex = value; }
        public string FileRevisionText { get => fileRevisionText;
            set { 
                
                fileRevisionText = value;
                this.FileRevisionRegex = new Regex(Regex.Escape(fileRevisionText) + @"\d{1,2}");
            }
        }
        public Regex FileRevisionRegex { get => fileRevisionRegex; set => fileRevisionRegex = value; }
    }
}
