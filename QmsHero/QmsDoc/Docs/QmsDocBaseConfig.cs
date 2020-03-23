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

        }

        public void Initialize()
        {

        }

        public object GetProperty(QmsDocBase doc, string propertyName)
        {
            this.Doc = doc;
            var propInfo = this.GetType().GetProperty(propertyName);
            return propInfo.GetValue(this);
        }
    }
}
