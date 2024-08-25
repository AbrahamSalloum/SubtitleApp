

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using visSubDownLoader.Models;
namespace visSubDownLoader.ViewModel;


public partial class AdvancedSearchModel : ObservableObject
{
    public AdvancedSearchModel()
    {
        FilemediaType = [];
        FilemediaType.Add("All");
        FilemediaType.Add("Movie");
        FilemediaType.Add("Episode");

        AitranslatedType = [];
        AitranslatedType.Add("include");
        AitranslatedType.Add("exclude");

        ForeignpartsonlyType = [];
        ForeignpartsonlyType.Add("include");
        ForeignpartsonlyType.Add("exclude");


        HearingImpairedType = [];
        HearingImpairedType.Add("include");
        HearingImpairedType.Add("exclude");

        TrustedSourcesType = [];
        TrustedSourcesType.Add("include");
        TrustedSourcesType.Add("only");

        MachineTranslatedType = [];
        MachineTranslatedType.Add("include");
        MachineTranslatedType.Add("exclude");



    }

    [ObservableProperty]
    ObservableCollection<string> filemediaType;

    [ObservableProperty]
    string? mtype;

    [ObservableProperty]
    ObservableCollection<string> aitranslatedType;

    [ObservableProperty]
    string? aitranslated;

    [ObservableProperty]
    ObservableCollection<string> foreignpartsonlyType;

    [ObservableProperty]
    string? foreignpartsonly;

    [ObservableProperty]
    ObservableCollection<string> hearingImpairedType;

    [ObservableProperty]
    string? hearingImpaired;

    [ObservableProperty]
    ObservableCollection<string> machineTranslatedType;

    [ObservableProperty]
    string? machineTranslated;

    [ObservableProperty]
    ObservableCollection<string> trustedSourcesType;

    [ObservableProperty]
    string? trustedSources;

    [ObservableProperty]
    int? episodeNumber;

    [ObservableProperty]
    int? imdbID;

    [ObservableProperty]
    int? seasonNumber;
    
    [RelayCommand]
     async Task Apply()
    {
        var query = new QueryParams
        {
            type = Mtype,
            ai_translated = Aitranslated,
            foreign_parts_only = Foreignpartsonly,
            hearing_impaired = HearingImpaired,
            machine_translated = MachineTranslated,
            trusted_sources = TrustedSources,
            episode_number = EpisodeNumber,
            imdb_id = ImdbID,
            season_number = SeasonNumber
        };
        var navigationParameter = new Dictionary<string, object>
        {
            {"Query" , query }
        };
        await Shell.Current.GoToAsync($"..", navigationParameter);

    }
}
