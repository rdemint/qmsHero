using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Controls
{
    public class ControlComboBox : ControlBase
    {
        List<IDocActionControl> itemList;
        IDocActionControl selectedControl;
        string myLabel;
        string myText;

        public ControlComboBox() : base()
        {

        }

        public ControlComboBox(string label, List<IDocActionControl> itemList) : base()
        {
            this.MyLabel = label;
            this.ItemList = itemList;
            this.SelectedControl = this.ItemList[0];
        }

        public List<IDocActionControl> ItemList { get => itemList; set => itemList = value; }
        public string MyText { get => myText; set => myText = value; }
        public string MyLabel { 
            get => myLabel;
            set { Set<string>(()=> this.MyLabel, ref this.myLabel, value); } 
        }
        public IDocActionControl SelectedControl
        {
            get => selectedControl;
            set
                {
                    Set<IDocActionControl>(() => this.SelectedControl, ref selectedControl, value);
                    this.DocActionName = value.DocActionName;
                    this.DocActionVal = value.DocActionVal;
                }
        }
    }
         
}
