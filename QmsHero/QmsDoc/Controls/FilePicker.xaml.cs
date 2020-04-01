using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for FolderPicker.xaml
    /// </summary>
    public partial class FilePicker : UserControl
    {
        RelayCommand openFileDialog;
        public FilePicker()
        {
            InitializeComponent();
        }

        public static DependencyProperty ControlValueProperty =
           DependencyProperty.Register(
       "ControlValue",
       typeof(string),
       typeof(FilePicker),
       new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }
       );

        public string ControlValue
        {
            get
            {
                return (string)GetValue(ControlValueProperty);
            }
            set
            {
                SetValue(ControlValueProperty, value);
            }
        }



        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register(
                "DisplayName",
                typeof(string),
                typeof(FilePicker),
                new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }
           );

        public string DisplayName
        {
            get
            {
                return (string)GetValue(DisplayNameProperty);
            }
            set
            {
                SetValue(DisplayNameProperty, value);
            }
        }

        public RelayCommand OpenFileDialog {
            get { 
                if  (this.openFileDialog == null)
                {
                    this.openFileDialog = new RelayCommand(
                        ()=>
                        {
                            var fd = new OpenFileDialog();
                            var result = fd.ShowDialog();
                            if (result == true)
                            {
                                ControlValue = fd.FileName;
                            }
                        },
                        ()=> true
                        );
                }
                return this.openFileDialog;
            }
            
        }

    }
}
