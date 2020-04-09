using LadderControls.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace LadderControls
{
    public class QFolderPicker: QControlBase
    {
        ICommand showDialogCommand;

        

        static QFolderPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QFolderPicker), new FrameworkPropertyMetadata(typeof(QFolderPicker)));
        }

    
        public ICommand ShowDialogCommand
        {
            get
            {
                if (this.showDialogCommand == null)
                {
                    this.showDialogCommand = new ShowFolderDialogCommand();  
                }
                return this.showDialogCommand;
            }
            set => showDialogCommand = value;
        }

       
    }


}
