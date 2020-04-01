using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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
    public partial class FolderPicker : UserControl

    {
        RelayCommand openFolderDialog;

        public FolderPicker()
        {
            InitializeComponent();
        }

        public static DependencyProperty ControlValueProperty =
   DependencyProperty.Register(
"ControlValue",
typeof(string),
typeof(FolderPicker),
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
                typeof(FolderPicker),
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

        public RelayCommand OpenFileDialog
        {
            get
            {
                if (this.openFolderDialog == null)
                {
                    this.openFolderDialog = new RelayCommand(
                        () =>
                        {
                            var fd = new System.Windows.Forms.FolderBrowserDialog();
                            var result = fd.ShowDialog();
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                ControlValue = fd.SelectedPath;
                            }
                        }, 
                        ()=> true
                        );
                }
                return this.openFolderDialog;
            }

        }
    }
}
