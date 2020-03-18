using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        ViewModelLocator viewModelLocator;
        RelayCommand navToTestViewModel1;
        RelayCommand navToCustomProcessingViewModel;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.NavToCustomProcessingViewModel.RaiseCanExecuteChanged();
            this.NavToTestViewModel1.RaiseCanExecuteChanged();
        }
        public ViewModelBase ActiveViewModel
        {
            get => activeViewModel;
            set
            {
                this.activeViewModel = value;
                OnPropertyChanged("ActiveViewModel");
            }
        }
        public RelayCommand NavToCustomProcessingViewModel {
            get {
                    if (this.navToCustomProcessingViewModel == null)
                    {
                        this.navToCustomProcessingViewModel = new RelayCommand(
                           () =>
                           {
                               this.ActiveViewModel = this.viewModelLocator.CustomProcessingViewModel;
                           },
                            () =>
                            {
                                return this.ActiveViewModel != this.viewModelLocator.CustomProcessingViewModel;
                            }
                       );
                    }
                     return this.navToCustomProcessingViewModel;
                }
            set => navToCustomProcessingViewModel = value; 
            } 
        public RelayCommand NavToTestViewModel1 {
            get {
                if (this.navToTestViewModel1 == null)
                    {
                        this.navToTestViewModel1 = new RelayCommand(
                                () => {
                                    this.ActiveViewModel = this.viewModelLocator.TestViewModel1;
                                },
                                () => this.activeViewModel != this.viewModelLocator.TestViewModel1
                                );
                    }
                return this.navToTestViewModel1;
            } 
            set => navToTestViewModel1 = value; }
    }


}