using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Core
{
    public class DocActionControlManager: DocControlManagerBase
    {

        public DocActionControlManager():base()
        {
            this.Initialize();
        }
 

        private void Initialize()
        {
            //this.LogoPath = new ControlFolderPicker("LogoPath", null);
            //this.LogoText = new ControlCheckTextBox("LogoText", null, false);
            //this.EffectiveDate = new ControlCheckTextBox("EffectiveDate", null);
            //this.Revision = new ControlCheckTextBox("Revision", null);
            //this.Logo = new ControlComboBox(
            //                "Logo",
            //                new List<IDocActionControl>()
            //                    {
            //                        new ControlTextBox2("MyLogoText", null),
            //                        new ControlTextBox2("MyOtherLogoText", "Some text already here")
            //                    }
            //            );

        }

    }
}
