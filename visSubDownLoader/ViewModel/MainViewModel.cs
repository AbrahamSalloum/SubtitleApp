
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using visSubDownLoader.Models;
using visSubDownLoader.services;
using services;
using Newtonsoft.Json.Linq;

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
        MovieHashLabel = "";
        this.connectivity = connectivity;
        ShowOptions = true;
        this.subfetch = ApiRequests.Instance();


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
    public QueryParams? sparams;

    [ObservableProperty]
    public string bgcolor;


    [ObservableProperty]
    public String movieHashLabel;

    [ObservableProperty]
    public Boolean showOptions;

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

            if (SelectedYear?.Year == 0) SelectedYear.Year = null;

            if (Query is not null)
            {
                TextSearchObject = Query;

            }
            else
            {
                TextSearchObject = new QueryParams();
            }

            TextSearchObject.query = Text;
            TextSearchObject.year = SelectedYear?.Year;

            if (string.IsNullOrEmpty(MovieHashLabel) is false)
            {
                TextSearchObject = new QueryParams();
                TextSearchObject.moviehash = MovieHashLabel;
            }

            SubtitleResults? r = await TextSearch(TextSearchObject);
            if (r is null)
            {
                return;
            }
            MovieHashLabel = "";
            r.total_count ??= 0;


            string qq = "";
            foreach (var p in JObject.FromObject(TextSearchObject))
            {
                if (p.Value?.Type != JTokenType.Null)
                    qq += $"<b>{p.Key}</b>: {p.Value} ";
            }

            SubItems s = new()
            {
                Name = Text,
                Rating = r.total_count,
                InternalId = Text,
                results = r,
                Sparams = TextSearchObject,
                QueryLabel = qq
            };

            Items.Add(s);
            Query = new QueryParams();


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
        if (Query is not null)
        {
            Bgcolor = "red";
        }
    }

    [RelayCommand]
    public async Task MovieHashSearch()
    {

        var result = await FilePicker.PickAsync(new PickOptions
        {

            PickerTitle = "pick movie file",
            FileTypes = FilePickerFileType.Videos

        });

        if (result is null) return;

        string path = result.FullPath;

        string moviehash = MovieHasher.ComputeMovieHashString(path);
        MovieHashLabel = moviehash;
        Text = result.FileName;
    }

}