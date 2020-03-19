using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Core
{
    public class DocAction
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

        public string Name { get => name; set => name = value; }
        public string Value { get => value; set => this.value = value; }
        public IDocActionControl DocActionControl { get => docActionControl; set => docActionControl = value; }

        //public IDocActionControl GetDocActionControl()
        //{
        //    var controlType = DocActionControlType.GetType();
        //    return (IDocActionControl)Activator.CreateInstance(controlType);
        //}
    }
}
