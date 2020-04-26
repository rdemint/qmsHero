using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public abstract class QDocAudit
    {
        ObservableCollection<Exception> errors;

        public QDocAudit()
        {
            this.Errors = new ObservableCollection<Exception>();
        }
        
        public ObservableCollection<Exception> Errors { get => errors; set => errors = value; }

        public abstract void Audit(object doc, object config);
    }
}
