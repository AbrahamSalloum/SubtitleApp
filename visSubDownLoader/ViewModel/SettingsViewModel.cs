
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace visSubDownLoader.ViewModel;

    public partial class SettingsViewModel: ObservableObject
    {

        public SettingsViewModel()
        {
            User = Preferences.Default.Get("user", "");
            Pass = Preferences.Default.Get("user", "");
            Userkey = Preferences.Default.Get("userkey", "");

        }


        [ObservableProperty]
        string user;

        [ObservableProperty]
        string pass;

        [ObservableProperty]
        string userkey; 

        [RelayCommand]
        async Task SavePreferences()
        {
            Preferences.Default.Set("user", User);
            Preferences.Default.Set("pass", Pass);
            Preferences.Default.Set("userkey", Userkey);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }

