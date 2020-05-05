using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QControls.Commands
{
    public class ShowFileDialogCommand: ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object obj)
        {
            var cb = (QControlBase)obj;
            var d = new OpenFileDialog();
            var result = d.ShowDialog();
            if (result == DialogResult.OK)
            {
                cb.QState = d.FileName;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
