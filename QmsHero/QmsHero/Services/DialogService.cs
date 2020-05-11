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
    class DialogService : IDialogService
    {
        private readonly MetroWindow metroWindow;
        public DialogService(MetroWindow metroWindow)
        {
            this.metroWindow = metroWindow;
        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                NegativeButtonText = "Cancel"
            };
            return metroWindow.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative, settings);


        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessageBox(string message, string title)
        {
            throw new NotImplementedException();
        }

        public Task<ProgressDialogController> ShowProgressAsync(string title, string message)
        {
            return metroWindow.ShowProgressAsync(title, message);
        }
    }
}
