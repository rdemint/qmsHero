﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;
using QmsDoc.Controls;

namespace QmsDoc.Core
{
    
    public class DocActionControlManager
    {
        ControlFolderPicker logoPath;
        ControlCheckTextBox logoText;
        ControlCheckTextBox effectiveDate;
        ControlCheckTextBox revision;

        public ControlFolderPicker LogoPath { get => logoPath; set => logoPath = value; }
        public ControlCheckTextBox LogoText { get => logoText; set => logoText = value; }
        public ControlCheckTextBox EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public ControlCheckTextBox Revision { get => revision; set => revision = value; }

        public DocActionControlManager()
        {
            this.Initialize();
        }
 

        private void Initialize()
        {
            this.LogoPath = new ControlFolderPicker("LogoPath", null);
            this.LogoText = new ControlCheckTextBox("LogoText", null, false);
            this.EffectiveDate = new ControlCheckTextBox("EffectiveDate", null);
            this.Revision = new ControlCheckTextBox("Revision", null);

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

        public List<IDocActionControl> ToControlList()
        {
            var actionList = new List<IDocActionControl>();
            var properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                actionList.Add((IDocActionControl)property.GetValue(this));
            }

            return actionList;
        }


        public List<IDocActionControl> ToControlList(Dictionary<string, object> filterDict)
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
            var actionControlVal = (IDocActionControl)propertyInfo.GetValue(this);

            if (actionControlVal.DocActionVal != null && (string)actionControlVal.DocActionVal != "") { return true; }
            else { return false; }
        }
    }
}
