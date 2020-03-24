using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs
{
    public class QmsDocBaseConfig
    {
        QmsDocBase doc;
        public QmsDocBase Doc { get => doc; set => doc = value; }

        public QmsDocBaseConfig()
        {
            this.Initialize();
        }

        public void Initialize(){
        }
    }
}
