using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Common.PropertyGroups
{
    class DocumentPropertyPropertyGroup : QDocPropertyGroup
    {
        DocumentPropertyCompany documentPropertyCompany;
        DocumentPropertyCreator documentPropertyCreator;
        DocumentPropertyCreatedTime documentPropertyCreatedTime;
        DocumentPropertyLastModifiedBy documentPropertyLastModifiedBy;
        public DocumentPropertyPropertyGroup()
        {
        }

        public DocumentPropertyCompany DocumentPropertyCompany { get => documentPropertyCompany; set => documentPropertyCompany = value; }
        internal DocumentPropertyLastModifiedBy DocumentPropertyLastModifiedBy { get => documentPropertyLastModifiedBy; set => documentPropertyLastModifiedBy = value; }
        internal DocumentPropertyCreatedTime DocumentPropertyCreatedTime { get => documentPropertyCreatedTime; set => documentPropertyCreatedTime = value; }
        internal DocumentPropertyCreator DocumentPropertyCreator { get => documentPropertyCreator; set => documentPropertyCreator = value; }
    }


}
