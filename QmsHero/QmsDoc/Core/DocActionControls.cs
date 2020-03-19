using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Core
{
    
    public class DocActionControls
    {
        DocActionControlFolderPicker logoPath;
        DocActionControlTextBox logoText;
        DocActionControlTextBox effectiveDate;
        DocActionControlTextBox revision;

        public DocActionControlFolderPicker LogoPath { get => logoPath; set => logoPath = value; }
        public DocActionControlTextBox LogoText { get => logoText; set => logoText = value; }
        public DocActionControlTextBox EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public DocActionControlTextBox Revision { get => revision; set => revision = value; }

        public DocActionControls()
        {
            this.Initialize();
        }
 

        private void Initialize()
        {
            this.LogoPath = new DocActionControlFolderPicker("LogoPath", null);
            this.LogoText = new DocActionControlTextBox("LogoText", "Some text here", false);
            this.EffectiveDate = new DocActionControlTextBox("EffectiveDate", "2020-03-03");
            this.Revision = new DocActionControlTextBox("Revision", "1");

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

        public List<IDocActionControl> ToDocActionControlList()
        {
            var actionList = new List<IDocActionControl>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                actionList.Add((IDocActionControl)property.GetValue(this));
            }

            return actionList;
        }


        public List<IDocActionControl> ToDocActionControlList(Dictionary<string, object> filterDict)
        {
            var actionList = new List<IDocActionControl>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                if (filterDict.Keys.Contains(property.Name))
                {
                    actionList.Add((IDocActionControl)property.GetValue(this));
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
