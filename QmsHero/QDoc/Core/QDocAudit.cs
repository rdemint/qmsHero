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

        protected QDocAudit()
        {
            this.errors = new ObservableCollection<Exception>();
        }
        
        public ObservableCollection<Exception> Errors { get => errors; }

        public abstract void Audit(object doc, object config);

        protected internal void Add(Exception e)
        {
            this.errors.Add(e);
        }

        protected internal bool HasErrors()
        {
            if(this.errors.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
