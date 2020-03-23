using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;
using GalaSoft.MvvmLight;

namespace QmsDoc.Core
{
    public class DocAction: ObservableObject
    {
        string name;
        string value;
        IDocActionControl docActionControl;
        public DocAction(string name, string value, IDocActionControl docActionControl)
        {
            this.Name = name;
            this.Value = value;
            this.DocActionControl = docActionControl;
        }

        public string Name { 
            get => name;
            set { Set<string>(this.Name, ref this.name, value); } 
        }
        public string Value { 
            get => value;
            set { Set<string>(this.Value, ref this.value, value); } 
        }
        public IDocActionControl DocActionControl { get => docActionControl; set => docActionControl = value; }
    }
}
