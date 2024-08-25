

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using visSubDownLoader.Models;
namespace visSubDownLoader.ViewModel;


[QueryProperty("Items", "Items")]
    public partial class DetailViewModel : ObservableObject
{
    public DetailViewModel()
    {
        subs = [];
    }

    [ObservableProperty]
    SubItems? items;

    [ObservableProperty]
    ObservableCollection<SubData> subs;


    partial void OnItemsChanged(SubItems? value)
    {
        SubData[]? data = value?.results?.data;
        if (data is null) return; 
        foreach (SubData? sub in data)
        {
            if(sub != null)
            {

                sub.r = sub.attributes?.related_links?[0];
                Subs.Add(sub);


            }


            
        }

    }

    [RelayCommand]
    static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    }

