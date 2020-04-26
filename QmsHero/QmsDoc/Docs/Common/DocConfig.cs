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
        Regex isSopRegex;
        Regex isFormRegex;

        public DocConfig()
        {
            this.FileRevisionText = "Rev";
            this.IsSopRegex = new Regex(@"SOP-");
            this.IsFormRegex = new Regex(@"F-");
        }

        public Regex IsSopRegex { get => isSopRegex; set => isSopRegex = value; }
        public Regex IsFormRegex { get => isFormRegex; set => isFormRegex = value; }
        public string FileRevisionText { get => fileRevisionText; set => fileRevisionText = value; }


    }
}
