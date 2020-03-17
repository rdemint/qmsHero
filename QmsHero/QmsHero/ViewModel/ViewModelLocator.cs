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
using System.ComponentModel;


namespace QmsHero.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private ViewModelBase mainViewModel;
        private CustomProcessingViewModel customProcessingViewModel;
        private TestViewModel1 testViewModel1;
        
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
            SimpleIoc.Default.Register<CustomProcessingViewModel>();
            SimpleIoc.Default.Register<TestViewModel1>();
        }

        public ViewModelBase MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }


        //public MainViewModel MainViewModel {
        //    get {
        //        if (this.mainViewModel == null)
        //        {
        //            this.mainViewModel = new MainViewModel();
        //        }
        //            return this.mainViewModel;
        //        }
        //    set { 
        //        this.mainViewModel = value;
        //        }
        //}

    

        public CustomProcessingViewModel CustomProcessingViewModel {
            get {
                //if (this.customProcessingViewModel == null)
                //{
                //    this.customProcessingViewModel = new CustomProcessingViewModel();
                //}
                //return this.customProcessingViewModel;
                return ServiceLocator.Current.GetInstance<CustomProcessingViewModel>();
            }
        }

        public TestViewModel1 TestViewModel1 { 
            get => ServiceLocator.Current.GetInstance<TestViewModel1>(); 
            set => testViewModel1 = value; }

        public static void Cleanup()
        {
            
        }
    }
}