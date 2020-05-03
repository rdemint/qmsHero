using FluentResults;
using QDoc.Interfaces;
using System;
using System.ComponentModel;

namespace QDoc.Core
{
    [DefaultProperty("State")]
    public abstract class QDocProperty: INotifyPropertyChanged, IEquatable<QDocProperty>
    {
        string name;
        object state;

        public QDocProperty()
        {
            this.name = this.GetType().Name;
        }

        public QDocProperty(object state): this()
        {
            this.state = state;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object State
        {
            get => state;
            set => this.state = value;

        }
        public string Name { get => name; }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Result<QDocProperty> Read(object doc)
        {
            throw new NotImplementedException();
        }

        public virtual Result<QDocProperty> Write(object doc)
        {
            throw new NotImplementedException();
        }

        public virtual Result<QDocProperty> Read(object doc, object config)
        {
            throw new NotImplementedException();
        }


        public virtual Result<QDocProperty> Write(object doc, object config)
        {
            throw new NotImplementedException();
        }
        

        public virtual bool IsValid(object config)
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

        public bool Equals(QDocProperty other)
        {
            if(this.Name == other.Name && this.State == other.State)
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