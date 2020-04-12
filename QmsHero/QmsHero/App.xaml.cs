using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using QmsDoc.Core;

namespace QmsHero
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            var manager = SimpleIoc.Default.GetInstance<DocManager>();
            manager.CloseApps();
            System.Windows.Forms.MessageBox.Show(exception.ToString());
            e.Handled = true;
            Shutdown();
        }
    }
}
