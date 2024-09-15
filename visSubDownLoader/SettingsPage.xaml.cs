using visSubDownLoader.ViewModel;

namespace visSubDownLoader;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
      
        InitializeComponent();
        BindingContext = vm;

    }
}