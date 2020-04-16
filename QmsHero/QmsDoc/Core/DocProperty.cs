using QmsDoc.Interfaces;
using System;
using System.ComponentModel;

namespace QmsDoc.Core
{
    public class DocProperty: INotifyPropertyChanged
    {
        string name;
        string value;
        bool isSet;

        public DocProperty()
        {

        }

        public DocProperty(string value) 
        {
            this.Value = value;
        }
        public DocProperty(string name, string value)
        {
            this.name = name;
            this.Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }
        public string Name { get => name; set => name = value; }
        public bool IsSet { get => isSet; }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            this.isSet = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual DocProperty Get(object doc, object docConfig)
        {
            throw new NotImplementedException();
        }

        public virtual void Set(object doc, IDocConfig docConfig, string value)
        {
            throw new NotImplementedException();
        }

        public virtual bool Accepts(string value, IDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsValid()
        {
            if (value != null)
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