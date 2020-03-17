using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using IPageViewModel = QmsHero.Interfaces.IPageViewModel;
using QmsHero.ViewModel;

namespace QmsHero.Navigation
{
    public class MainNavService: INotifyPropertyChanged
    {
        IPageViewModel activeViewModel;
        IPageViewModel mainViewModel;
        IPageViewModel customProcessingViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public IPageViewModel ActiveViewModel { 
            get => activeViewModel;
            set {
                OnPropertyChanged();
                this.activeViewModel = value;
            } }

        public IPageViewModel NavigateTo(IPageViewModel viewModel)
        {
            this.activeViewModel = viewModel;
            return this.activeViewModel;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
