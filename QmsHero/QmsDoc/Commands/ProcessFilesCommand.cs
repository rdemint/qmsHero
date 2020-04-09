using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QmsDoc.Commands
{
    class ProcessFilesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            DocManager manager = (DocManager)parameter;
            return manager.CanProcessFiles;
        }

        public void Execute(object parameter)
        {
            DocManager manager = (DocManager)parameter;

        }
    }
}
