﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace QmsDoc.Core
{
    public class DocActionControlBase: ObservableObject
    {
        bool controlIsEnabled;
        string docActionName;
        object docActionVal;

        public DocActionControlBase(string docActionName)
        {
            this.DocActionName = docActionName;
        }

        public DocActionControlBase(string docActionName, object docActionVal)
        {
            this.DocActionVal = docActionVal;
            this.DocActionName = docActionName;
        }

        public string DocActionName { 
            get => docActionName;
            set {
                Set<string>(()=>this.DocActionName, ref docActionName, value);
            } }
        public bool ControlIsEnabled { 
            get => controlIsEnabled;
            set {
                Set<bool>(() => this.ControlIsEnabled, ref controlIsEnabled, value);
            } }

        public object DocActionVal { get => docActionVal; set => docActionVal = value; }

        public static DocActionControlBase GetControl(DocActionControlBase currentVal, [CallerMemberName] string actionName=null)
        {
            if(currentVal != null)
            {
                return currentVal;
            }
            else
            {
                return new DocActionControlBase(actionName);
            }
        }
    }
}
