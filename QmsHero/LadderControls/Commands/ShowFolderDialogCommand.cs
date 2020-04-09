using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LadderControls.Commands
{
    class ShowFolderDialogCommand: ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object obj)
        {
            var cb = (QControlBase)obj;
            var d = new FolderBrowserDialog();
            var result = d.ShowDialog();
            if (result == DialogResult.OK)
            {
                cb.QState = d.SelectedPath;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
