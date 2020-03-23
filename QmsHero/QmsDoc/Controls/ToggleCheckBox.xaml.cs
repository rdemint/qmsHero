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
        //string displayName;
        //string controlValue;
        public ToggleCheckBox()
        {
            InitializeComponent();
            //this.displayName = "some label";
            //this.controlValue = "initial value";
            this.ControlIsEnabled = true;

        }

        public ToggleCheckBox(string name="SomeDocProperty", string displayName="Some Doc Property", string val="3")
        {
            InitializeComponent();
            //this.ControlName = name;
            //this.DisplayName = displayName;
            //this.ControlValue = val;
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
                //this.controlValue = value;
                //OnPropertyChanged();
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
                //this.displayName = value;
                //OnPropertyChanged();
                SetValue(DisplayNameProperty, value);
            } }



        public string ControlName { get => controlName;
            set {
                this.controlName = value;
                this.OnPropertyChanged();
            } }



        public bool ControlIsEnabled
        {
            get { return (bool)GetValue(ControlIsEnabledProperty); }
            set { SetValue(ControlIsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlIsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlIsEnabledProperty =
            DependencyProperty.Register("ControlIsEnabled", typeof(bool), typeof(ToggleCheckBox), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });

        public override string ToString()
        {
            return this.DisplayName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
