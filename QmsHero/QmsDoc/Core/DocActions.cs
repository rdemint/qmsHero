using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;
using QmsHero.Model;

namespace QmsDoc.Core
{
    
    public class DocActions : IDocActions
    {
        string logoPath;
        string logoText;
        string effectiveDate;
        string revision;

        public string LogoPath { get => logoPath; set => logoPath = value; }
        public string LogoText { get => logoText; set => logoText = value; }
        public string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public string Revision { get => revision; set => revision = value; }

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

        public Dictionary<string, object> AsDict()
        {
            var dict = new Dictionary<string, object>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                if (this.IsValidProperty(property))
                {
                    dict.Add(property.Name, property.GetValue(this));
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
