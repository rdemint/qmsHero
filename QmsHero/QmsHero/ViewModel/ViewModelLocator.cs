/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:QmsHero"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

namespace QmsHero.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        MainViewModel mainViewModel;
        CustomProcessingViewModel customProcessingViewModel;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MainViewModel MainViewModel {
            get {
                if (this.mainViewModel == null)
                {
                    this.mainViewModel = new MainViewModel();
                }
                    return this.mainViewModel;
                }
            set { 
                this.mainViewModel = value;
                }
        }

        internal CustomProcessingViewModel CustomProcessingViewModel {
            get {
                if (this.customProcessingViewModel == null)
                {
                    this.customProcessingViewModel = new CustomProcessingViewModel();
                }
                return this.customProcessingViewModel;
            } 
            set => customProcessingViewModel = value; }

        public static void Cleanup()
        {
            
        }
    }
}