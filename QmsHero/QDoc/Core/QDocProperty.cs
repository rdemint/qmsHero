﻿using QDoc.Interfaces;
using System;
using System.ComponentModel;

namespace QDoc.Core
{
    public abstract class QDocProperty: INotifyPropertyChanged
    {
        string name;
        object state;

        public QDocProperty()
        {
            this.name = this.GetType().Name;
        }

        public QDocProperty(object value) 
        {
            this.name = this.GetType().Name;
            this.state = value;
        }
        public QDocProperty(string name, object state)
        {
            this.name = this.GetType().Name;
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

        public virtual QDocProperty Read(object doc, object docConfig)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(object doc, IDocConfig docConfig, object state)
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