using QDoc.Interfaces;
using System;
using System.ComponentModel;

namespace QDoc.Core
{
    public class QDocProperty: INotifyPropertyChanged
    {
        string name;
        string value;
        bool isSet;

        public QDocProperty()
        {

        }

        public QDocProperty(string value) 
        {
            this.Value = value;
        }
        public QDocProperty(string name, string value)
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

        public virtual QDocProperty Read(object doc, object docConfig)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(object doc, IQDocConfig docConfig, string value)
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