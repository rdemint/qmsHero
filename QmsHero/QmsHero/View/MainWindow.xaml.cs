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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using QmsHero.ViewModel;

namespace QmsHero.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        //private async void ShowMessageDialog(object sender, RoutedEvent e)
        //{
        //    MetroDialogOptions.ColorScheme = MahApps.Metro.Controls.Dialogs.MetroDialogColorScheme.Accented;
        //    var dialogSettings = new MetroDialogSettings()
        //    {
        //        AffirmativeButtonText = "Apply",
        //        NegativeButtonText = "Cancel",
        //        ColorScheme = MetroDialogColorScheme.Accented
        //    };
        //    MessageDialogResult result = await this.ShowMessageAsync("Hello world!", "This is the message text.", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
        //    if(result!= MessageDialogResult.Affirmative)
        //    {
        //        //Do something
        //    }
            
        //}

    }
}
