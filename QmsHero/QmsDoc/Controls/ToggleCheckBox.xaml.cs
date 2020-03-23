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
        bool controlIsEnabled;
        public ToggleCheckBox()
        {
            InitializeComponent();
            this.displayName = "some label";
            this.controlValue = "initial value";
            this.controlIsEnabled = true;
        }

        public ToggleCheckBox(string name="SomeDocProperty", string displayName="Some Doc Property", string val="3")
        {
            InitializeComponent();
            this.ControlName = name;
            this.DisplayName = displayName;
            this.ControlValue = val;
        }

        public static DependencyProperty ControlValueProperty =
            DependencyProperty.Register(
        "ControlValue",
        typeof(string),
        typeof(ToggleCheckBox),
        new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }
        );

        public string ControlValue {
            get {
                return (string)GetValue(ControlValueProperty);
            }
            set {
                this.controlValue = value;
                OnPropertyChanged();
                SetValue(ControlValueProperty, value);
            } }



        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register(
                "DisplayName",
                typeof(string),
                typeof(ToggleCheckBox),
                new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }
           );

        public string DisplayName {
            get {
                return (string)GetValue(DisplayNameProperty);
            }
            set {
                this.displayName = value;
                OnPropertyChanged();
                SetValue(DisplayNameProperty, value);
            } }



        public string ControlName { get => controlName;
            set {
                this.controlName = value;
                this.OnPropertyChanged();
            } }
        public bool ControlIsEnabled { 
            get => controlIsEnabled;
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
