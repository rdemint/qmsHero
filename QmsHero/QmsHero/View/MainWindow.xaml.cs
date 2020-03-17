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
using QmsHero.ViewModel;

namespace QmsHero.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TestPage1 testPage1;


        public MainWindow()
        {
            InitializeComponent();
            var viewModelLocator = App.Current.Resources["ViewModelLocator"] as ViewModelLocator;
            //this.DataContext = new MainViewModel();
            this.testPage1 = new TestPage1();
            this.DataContext = this;

        }
        public TestPage1 MyTestPage1 { get => testPage1; set => testPage1 = value; }

        private void TestPage1Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CustomProcessingButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
