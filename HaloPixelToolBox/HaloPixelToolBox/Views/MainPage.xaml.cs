using Microsoft.UI.Xaml.Navigation;

namespace HaloPixelToolBox.Views
{
    /// <summary>
    /// ��ҳ
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage? Current { get; set; }
        public MainPageViewModel ViewModel { get; set; } = new();
        public MainPage()
        {
            Current = this;
            InitializeComponent();
            ViewModel.AutoNavigationParameterService.Initialize(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.AutoNavigationParameterService.OnParameterChange(e.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
    }
}
