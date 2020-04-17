using QDoc.Interfaces;
using System;
using System.ComponentModel;

namespace QDoc.Core
{
    public class QDocProperty: INotifyPropertyChanged
    {
        string name;
        string state;
        bool isSet;

        public QDocProperty()
        {

        }

        public QDocProperty(string value) 
        {
            this.state = value;
        }
        public QDocProperty(string name, string value)
        {
            this.name = name;
            this.state = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string State
        {
            get => state;
            set => this.state = value;

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

        public virtual void Write(object doc, IDocConfig docConfig, string value)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsValid(IDocConfig config)
        {
            if (state != null)
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