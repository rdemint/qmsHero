using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QmsDoc.Interfaces;

namespace QmsDoc.Controls
{
    public class ControlBase : ObservableObject
    {
        bool controlIsValid;
        bool qInitial;
        string displayValue;
        string controlState;
        string controlOutput;

        public ControlBase()
        {

        }
        
        public string DisplayValue
        {
            get => displayValue;
            set
            {
                Set<string>(() => this.DisplayValue, ref displayValue, value);
            }
        }
        public bool ControlIsValid
        {
            get => ControlIsValid;
            set
            {
                Set<bool>(() => this.ControlIsValid, ref controlIsValid, value);
            }
        }

        public string ControlState
        {
            get => controlState;
            set
            {
                Set<string>(() => this.ControlState, ref controlState, value);
            }
        }

        public string ControlOutput {
            get { 
                    if (this.ControlIsValid)
                    {
                        return this.ControlState;
                    }
                    else
                    {
                        return null;
                    }
            } 
            set {
                Set<string>(() => this.ControlState, ref controlState, value);
            } 
        }

        public bool QInitial {
            get => qInitial;
            }
    }
}
