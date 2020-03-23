using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QmsDoc.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ToggleCheckBox : UserControl, INotifyPropertyChanged
    {
        string controlName;
        string displayName;
        string controlValue;
        string controlIsEnabled;
        public ToggleCheckBox()
        {
            InitializeComponent();
        }

        public ToggleCheckBox(string name="SomeDocProperty", string displayName="Some Doc Property", string val="3")
        {
            InitializeComponent();
            this.ControlName = name;
            this.DisplayName = displayName;
            this.ControlValue = val;
        }


        public string ControlValue { get => controlValue;
            set { 
                this.controlValue = value;
                this.OnPropertyChanged();
            } }
        public string DisplayName { get => displayName;
            set {
                this.displayName = value;
                this.OnPropertyChanged();
            } }
        public string ControlName { get => controlName;
            set {
                this.controlName = value;
                this.OnPropertyChanged();
            } }
        public string ControlIsEnabled { get => controlIsEnabled;
            set {
                this.controlIsEnabled = value;
                this.OnPropertyChanged();
            } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
