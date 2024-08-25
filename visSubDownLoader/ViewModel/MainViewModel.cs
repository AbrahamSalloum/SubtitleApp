
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using visSubDownLoader.Creds;
using visSubDownLoader.Models;
using visSubDownLoader.services;
 
//using Microsoft.Maui.Graphics;

namespace visSubDownLoader.ViewModel;

[QueryProperty("Query", "Query")]
public partial class MainViewModel : ObservableObject
{
    readonly IConnectivity connectivity;
    readonly ApiRequests subfetch;

    public MainViewModel(IConnectivity connectivity)
    {

        Items = [];
        Languagelist = [];
        Yearlist = new ObservableCollection<YearLabel>(CreateYears());
        Bgcolor = "blue";
        this.connectivity = connectivity;

        Credentials? p = CredentailsReader.ReadCredentials();


        this.subfetch = new ApiRequests(p?.key, p?.username, p?.password);

        Languagelist.Add("en");
        Languagelist.Add("ar");
        Languagelist.Add("cn");

        

    }

    [ObservableProperty]
    ObservableCollection<SubItems> items;

    [ObservableProperty]
    YearLabel? selectedYear;

    [ObservableProperty]
    ObservableCollection<string> languagelist;

    [ObservableProperty]
    ObservableCollection<YearLabel> yearlist;

    [ObservableProperty]
    string? text;

    [ObservableProperty]
    public QueryParams? query;

    [ObservableProperty]
    public string? sparams;

    [ObservableProperty]
    public string bgcolor;
    [RelayCommand]
    async Task Add()
    {

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("oh no", "no internnet", "OK");
                return;
            }

            QueryParams TextSearchObject;
            if (string.IsNullOrEmpty(Text)) return;
            if (SelectedYear?.Year == 0) SelectedYear.Year = null;

            if(Query is not null)
            {
                 TextSearchObject = Query;

            } else
            {
                TextSearchObject = new QueryParams();
            }

            TextSearchObject.query = Text;
            TextSearchObject.year = SelectedYear?.Year;

            JObject searchterms = JObject.FromObject(TextSearchObject);
            string urlquery = "";
            foreach (var p in searchterms)
            {
                if (p.Value?.Type != JTokenType.Null)
                    urlquery += $"{p.Key}={p.Value}&";
            }
            if(urlquery is not null)
            {
                Sparams = urlquery;
            }
            

            SubtitleResults? r = await TextSearch(TextSearchObject);
            if (r is null)
            {
                return;
            }
            r.total_count ??= 0;
            SubItems s = new()
            {
                Name = Text,
                Rating = r.total_count,
                InternalId = Text,
                results = r,
                Sparams = urlquery
            };

            Items.Add(s);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
            throw;
        }
    }

    [RelayCommand]
    async Task Advanced()
    {
        Console.WriteLine($"{nameof(AdvancedSearchPage)}");
        await Shell.Current.GoToAsync($"{nameof(AdvancedSearchPage)}");
    }

    [RelayCommand]
    async Task Tap(SubItems s)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { "Items", s }
        };
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}", navigationParameter);
    }

    public async Task<SubtitleResults?> TextSearch(QueryParams SearchString)
    {

        try
        {
            if (SearchString is null)
            {
                return null;
            }

            SubtitleResults? subtitleResults = await subfetch.SubtitleSearch(SearchString);
            if (subtitleResults is null)
            {
                return null;
            }
            return subtitleResults;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
            throw;
        }
    }

    private static List<YearLabel> CreateYears()
    {

        List<YearLabel> yyyy =
        [
            new YearLabel
            {
                Year = 0,
                Labelyear = "year"
            },
        ];
        int year = 2024;

        while (year > 1880)
        {

            YearLabel y = new()
            {
                Year = year,
                Labelyear = year.ToString()
            };


            yyyy.Add(y);
            year--;
        }

        return yyyy;
    }

    partial void OnQueryChanged(QueryParams? value)
    {
        if(Query is not null)
        {
            Bgcolor = "red";
        }
    }

}