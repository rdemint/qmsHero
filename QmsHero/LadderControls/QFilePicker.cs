using LadderControls.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LadderControls
{
    public class QFilePicker: QControlBase
    {
        ShowFileDialogCommand showDialogCommand;
        static QFilePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QFilePicker), new FrameworkPropertyMetadata(typeof(QFilePicker)));
        }

        public ShowFileDialogCommand ShowDialogCommand
        {
            get
            {
                if (this.showDialogCommand == null)
                {
                    this.showDialogCommand = new ShowFileDialogCommand();
                }
                return this.showDialogCommand;
            }
            set { this.showDialogCommand = value; }
        }
    }
}
