using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsHero.Services
{
    
    internal interface IAsyncDialogService
    {
        Task<MessageDialogResult> AskQuestionAsync(string title, string message);
        Task<ProgressDialogController> ShowProgressAsync(string title, string message);
        Task ShowMessageAsync(string title, string message);
    }
    
    internal class DialogService: IAsyncDialogService
    {
        private readonly MetroWindow metroWindow;

        public DialogService()
        {
            this.metroWindow = (MetroWindow)System.Windows.Application.Current.MainWindow;
        }

        //public DialogService(MetroWindow metroWindow)
        //{
        //    this.metroWindow = metroWindow;
        //}

        

        
        public Task<MessageDialogResult> AskQuestionAsync(string title, string message)
        {

            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                NegativeButtonText = "Cancel"
            };
            return metroWindow.ShowMessageAsync(
                title, 
                message, 
                MessageDialogStyle.AffirmativeAndNegative, 
                settings);
        }
        
        public Task<ProgressDialogController> ShowProgressAsync(string title, string message)
        {
            return metroWindow.ShowProgressAsync(title, message);
        }
        
        public Task ShowMessageAsync(string message, string title)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                NegativeButtonText = "Cancel"
            };
            return metroWindow.ShowMessageAsync(
                title, 
                message, 
                MessageDialogStyle.AffirmativeAndNegative, 
                settings);


        }
    }
}
