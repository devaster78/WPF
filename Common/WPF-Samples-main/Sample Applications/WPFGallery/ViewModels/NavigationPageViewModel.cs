using WPFGallery.Navigation;
using WPFGallery.Views;
using WPFGallery.Models;

namespace WPFGallery.ViewModels
{
    public partial class NavigationPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pageTitle = "Navigation";

        [ObservableProperty]
        private string _pageDescription = "Controls for navigation and actions";

        [ObservableProperty]
        private ICollection<ControlInfoDataItem> _navigationCards = ControlsInfoDataSource.Instance.GetControlsInfo("Navigation");

        private readonly INavigationService _navigationService;

        public NavigationPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        public void Navigate(object pageType){
            if (pageType is Type page)
            {
                _navigationService.NavigateTo(page);
            }
        }

        
    }
}