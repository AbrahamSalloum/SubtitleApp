


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using visSubDownLoader.Models;
using visSubDownLoader.services;
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
    static async Task Download(SubAttributes selectedSub)
    {
        if(selectedSub == null || selectedSub.files == null) return;


        var subfetchinfo = ApiRequests.Instance();
        DownLoadLinkData? DownloadInfo =  await subfetchinfo.RequestDownloadURL(selectedSub.files[0].file_id);
        if (DownloadInfo != null)
        {
            string temppath = "C:\\Users\\abrah\\OneDrive\\Desktop"; //Path.GetTempPath();
            await subfetchinfo.DownloadSubFile(DownloadInfo, selectedSub, temppath);
        }
    }

    [RelayCommand]
    static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    }

