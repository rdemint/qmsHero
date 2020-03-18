using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Controls;
using IPageViewModel = QmsHero.Interfaces.IPageViewModel;

namespace QmsHero.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        ViewModelBase activeViewModel;
        TestViewModel1 testViewModel1;
        ViewModelLocator viewModelLocator;
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            
            //this.viewModelLocator = new ViewModelLocator();
            // OR
            this.viewModelLocator = App.Current.Resources["ViewModelLocator"] as ViewModelLocator;
            this.activeViewModel = viewModelLocator.CustomProcessingViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "ActiveViewModel")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Windows.MessageBox.Show("Changing property"); 
        }

        public RelayCommand NavToTestViewModel1 => new RelayCommand(
            () => this.ActiveViewModel = this.viewModelLocator.TestViewModel1,
            () => this.activeViewModel != this.viewModelLocator.TestViewModel1
            );

        public RelayCommand ShowMessageBox => new RelayCommand(
            () => this.ActiveViewModel = this.viewModelLocator.CustomProcessingViewModel,
            () => this.activeViewModel != this.viewModelLocator.CustomProcessingViewModel
            );

        public ViewModelBase ActiveViewModel { 
            get => activeViewModel;
            set {
                this.activeViewModel = value;
                OnPropertyChanged("ActiveViewModel");
            } }
    }

}