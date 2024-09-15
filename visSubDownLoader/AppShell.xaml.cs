namespace visSubDownLoader
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(AdvancedSearchPage), typeof(AdvancedSearchPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}
