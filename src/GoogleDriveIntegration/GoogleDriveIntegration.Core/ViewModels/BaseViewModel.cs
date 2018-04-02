using Prism.Mvvm;
using Prism.Navigation;

namespace GoogleDriveIntegration.Core.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        public BaseViewModel()
        {
        }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public void Destroy()
        {
            //Add your logic to handle when viewmodel is destroyed
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //Add your logic to handle when your viewmodel has navigated to other viewmodel
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //Add your logic to handle when your viewmodel has been navigated successfully
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //Add your logic to handle when your viewmodel has started getting focus
        }
    }
}
