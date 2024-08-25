using visSubDownLoader.ViewModel;

namespace visSubDownLoader;

public partial class AdvancedSearchPage : ContentPage
{
	public AdvancedSearchPage(AdvancedSearchModel vm )
	{
		InitializeComponent();
		BindingContext = vm;
	}
}