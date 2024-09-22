
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using visSubDownLoader.Models;
namespace visSubDownLoader.ViewModel;


    public partial class SettingsViewModel: ObservableObject
    {

        public SettingsViewModel()
        {
        User = Preferences.Default.Get("user", "Unknown");
        Pass = Preferences.Default.Get("user", "Unknown");
        Userkey = Preferences.Default.Get("userkey", "Unknown");

        }


    [ObservableProperty]
    string user;

    [ObservableProperty]
    string pass;

    [ObservableProperty]
    string userkey; 

    [RelayCommand]
        public void SavePreferences()
        {
            Preferences.Default.Set("user", User);
            Preferences.Default.Set("pass", Pass);
            Preferences.Default.Set("userkey", Userkey);

    }
    }

