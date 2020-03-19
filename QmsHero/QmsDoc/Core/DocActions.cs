using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Core
{
    
    public class DocActions
    {
        DocAction logoPath;
        DocAction logoText;
        DocAction effectiveDate;
        DocAction revision;

        public DocActions()
        {
            this.Initialize();
        }
        public DocAction LogoPath { get => logoPath; set => logoPath = value; }
        public DocAction LogoText { get => logoText; set => logoText = value; }
        public DocAction EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public DocAction Revision { get => revision; set => revision = value; }

        private void Initialize()
        {
            this.LogoPath = new DocAction("LogoPath", null, new DocActionControlFolderPicker("LogoPath", null));
            this.LogoText = new DocAction("LogoText", "Awesome Company", new DocActionControlTextBox("LogoText", null));
            this.EffectiveDate = new DocAction("EffectiveDate", "2020-03-03", new DocActionControlTextBox("EffectiveDate", null));
            this.Revision = new DocAction("Revision", "1", new DocActionControlTextBox("Revision", null));

        }
        public System.Reflection.PropertyInfo GetPropertyInfo(string propertyName)
        {
            return this.GetType().GetProperty(propertyName);
        }

        public void SetProperty(object obj, string propertyName)
        {
            var objPropInfo = obj.GetType().GetProperty(propertyName);
            var objPropValue = objPropInfo.GetValue(obj);
            var thisPropInfo = this.GetType().GetProperty(propertyName);
            thisPropInfo.SetValue(this, objPropValue);
        }

        public Dictionary<string, DocAction> AsDict()
        {
            var dict = new Dictionary<string, DocAction>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                if (this.IsValidProperty(property))
                {
                    dict.Add(property.Name, (DocAction)property.GetValue(this));
                }
            }
            return dict;
        }

        public List<DocActionControlBase> ToDocActionControlList()
        {
            var actionList = new List<DocActionControlBase>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                actionList.Add(new DocActionControlBase(property.Name, property.GetValue(this)));
            }

            return actionList;
        }

        public List<DocAction> ToDocActionList()
        {
            var actionList = new List<DocAction>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                actionList.Add((DocAction)property.GetValue(this));
            }

            return actionList;
        }
        public List<DocActionControlBase> ToDocActionControlList(Dictionary<string, object> filterDict)
        {
            var actionList = new List<DocActionControlBase>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                if (filterDict.Keys.Contains(property.Name))
                {
                    actionList.Add(new DocActionControlBase(property.Name, property.GetValue(this)));
                }
            }

            return actionList;
        }



        public Boolean IsValidProperty(System.Reflection.PropertyInfo propertyInfo) 
        {
            if (propertyInfo.GetValue(this) != null && propertyInfo.GetValue(this).ToString() != "") { return true; }
            else { return false; }
        }
    }
}
