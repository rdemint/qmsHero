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
        
        ViewModelBase activeViewModel;
        ViewModelLocator viewModelLocator;
        RelayCommand navToConfigViewModel;
        RelayCommand navToCustomProcessingViewModel;
        RelayCommand navToDCNViewModel;
        RelayCommand navToResultsViewModel;
        RelayCommand navToQuickActionsViewModel;
        RelayCommand navToFilePropertiesViewModel;
        RelayCommand openConfigFlyout;
        bool isSettingsFlyoutOpen;

        public MainViewModel()
        {
            this.navToQuickActionsViewModel = new RelayCommand(
                () => this.ActiveViewModel = this.viewModelLocator.QuickActionsViewModel
                );
            this.navToFilePropertiesViewModel = new RelayCommand(
                () => this.ActiveViewModel = this.viewModelLocator.FilePropertiesViewModel
                );
            this.navToResultsViewModel = new RelayCommand(() => this.ActiveViewModel = this.viewModelLocator.ResultsViewModel);
            this.navToConfigViewModel = new RelayCommand(() => this.ActiveViewModel = this.viewModelLocator.ConfigViewModel);

            this.viewModelLocator = App.Current.Resources["ViewModelLocator"] as ViewModelLocator;
            this.activeViewModel = viewModelLocator.FilePropertiesViewModel;
            this.openConfigFlyout = new RelayCommand(() => OpenFlyout());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.NavToCustomProcessingViewModel.RaiseCanExecuteChanged();
            this.NavToConfigViewModel.RaiseCanExecuteChanged();
            this.NavToQuickActionsViewModel.RaiseCanExecuteChanged();
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
        public RelayCommand NavToConfigViewModel {
            get => navToConfigViewModel;
            set => navToConfigViewModel = value; }

        public RelayCommand NavToDCNViewModel {
            get => navToDCNViewModel;
            set => navToDCNViewModel = value; }

        public RelayCommand NavToResultsViewModel {
            get => navToResultsViewModel;
            set => navToResultsViewModel = value; }

        public RelayCommand NavToQuickActionsViewModel { get => navToQuickActionsViewModel; set => navToQuickActionsViewModel = value; }
        public RelayCommand NavToFilePropertiesViewModel { get => navToFilePropertiesViewModel; set => navToFilePropertiesViewModel = value; }
        public bool IsSettingsFlyoutOpen { 
            get => isSettingsFlyoutOpen;
            set { 
                if (value.Equals(isSettingsFlyoutOpen)) return; 
                isSettingsFlyoutOpen = value; 
                OnPropertyChanged("IsSettingsFlyoutOpen");
            } 
        }

        private void OpenFlyout()
        {
            isSettingsFlyoutOpen = true;
        }

        public RelayCommand OpenConfigFlyout { get => openConfigFlyout; set => openConfigFlyout = value; }
    }


}