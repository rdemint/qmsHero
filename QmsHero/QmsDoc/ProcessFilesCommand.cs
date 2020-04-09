using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc
{
    class ProcessFilesCommand: ICommand

    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object obj)
        {
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
