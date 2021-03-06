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
using QmsDoc.Core;
using QmsDoc.Docs.Word;
using QmsDoc.Docs.Excel;
using QmsHero.Model;
using QmsHero.Services;
using GalaSoft.MvvmLight.Views;

namespace QmsHero.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    // Create design time view services and models
            //}
            //else
            //{
                // Create run time view services and models
                SimpleIoc.Default.Register<DocManager>();
                SimpleIoc.Default.Register<ProcessingResultsStore>();
                SimpleIoc.Default.Register<DocManagerConfig>();
                SimpleIoc.Default.Register<WordDocConfig>();
                SimpleIoc.Default.Register<ExcelDocConfig>();

                SimpleIoc.Default.Register<IAsyncDialogService, DialogService>();

                SimpleIoc.Default.Register<ConfigViewModel>();
                SimpleIoc.Default.Register<CustomProcessingViewModel>();
                SimpleIoc.Default.Register<ResultsViewModel>();
                SimpleIoc.Default.Register<QuickActionsViewModel>();
                SimpleIoc.Default.Register<FilePropertiesViewModel>();
                SimpleIoc.Default.Register<ManagerProcessingViewModel>();
                SimpleIoc.Default.Register<MainViewModel>();

                
            //}

        }
        public DocManager DocManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DocManager>();
            }
        }

        public IAsyncDialogService IAsyncDialogService
        {
            get {
                return ServiceLocator.Current.GetInstance<IAsyncDialogService>();
            }
        }

        public ViewModelBase MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

   

        public ConfigViewModel ConfigViewModel {
            get {
                return ServiceLocator.Current.GetInstance<ConfigViewModel>();
            }
        }

        public CustomProcessingViewModel CustomProcessingViewModel { 
            get => ServiceLocator.Current.GetInstance<CustomProcessingViewModel>(); 
            }


        public ResultsViewModel ResultsViewModel
        {
            get => ServiceLocator.Current.GetInstance<ResultsViewModel>();
        }

        public QuickActionsViewModel QuickActionsViewModel
        {
            get => ServiceLocator.Current.GetInstance<QuickActionsViewModel>();
        }

        public FilePropertiesViewModel FilePropertiesViewModel
        {
            get => ServiceLocator.Current.GetInstance<FilePropertiesViewModel>();
        }

        public static void Cleanup()
        {
            
        }
    }
}