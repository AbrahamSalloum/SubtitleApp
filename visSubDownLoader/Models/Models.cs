
namespace visSubDownLoader.Models; 

class DownLoadLinkData
{
    public string? link { get; set; }
    public string? file_name { get; set; }
    public int? requests { get; set; }
    public int? remaining { get; set; }
    public string? message { get; set; }
    public string? reset_time { get; set; }
    public string? reset_time_utc { get; set; }
}

class UserLoginDetails
{
    public int? allowed_downloads { get; set; }
    public int? allowed_translations { get; set; }
    public string? level { get; set; }
    public int? user_id { get; set; }
    public bool? ext_installed { get; set; }
    public bool? vip { get; set; }
}

class LoginInfo
{
    public string? token { get; set; }
    public string? base_url { get; set; }
    public int? status { get; set; }
    public UserLoginDetails? user { get; set; }
}

class LogoutInfo
{
    public string? message { get; set; }
    public int? status { get; set; }
}

public class SubtitleResults
{
    public int? total_pages { get; set; }
    public int? total_count { get; set; }
    public int? per_page { get; set; }
    public int? page { get; set; }
    public SubData[]? data { get; set; }
}

public class SubData
{
    public string? id { get; set; }
    public string? type { get; set; }

    public SubAttributes? attributes { get; set; }

    public RelatedLinks? r { get; set; }
}

public class SubAttributes
{
    public string? subtitle_id { get; set; }
    public string? language { get; set; }
    public int? download_count { get; set; }
    public int? new_download_count { get; set; }
    public bool? hearing_impaired { get; set; }
    public bool? hd { get; set; }
    public double? fps { get; set; }
    public int? votes { get; set; }
    public double? ratings { get; set; }
    public bool? from_trusted { get; set; }
    public bool? foreign_parts_only { get; set; }
    public string? upload_date { get; set; }
    public string[]? file_hashes { get; set; }
    public bool? ai_translated { get; set; }
    public int? nb_cd { get; set; }
    public string? slug { get; set; }
    public bool? machine_translated { get; set; }
    public string? release { get; set; }
    public string? comments { get; set; }
    public int? legacy_subtitle_id { get; set; }
    public int? legacy_uploader_id { get; set; }
    public FeaturDetails? feature_details { get; set; }
    public string? url { get; set; }
    public RelatedLinks[]? related_links { get; set; }
    public SubFiles[]? files { get; set; }
}

public class RelatedLinks
{
    public string? label { get; set; }
    public string? url { get; set; }
    public string? img_url { get; set; }
}



public class SubFiles
{
    public int? file_id { get; set; }
    public int? cd_number { get; set; }
    public string? file_name { get; set; }

}
public class FeaturDetails
{
    public int? feature_id { get; set; }
    public string? feature_type { get; set; }
    public int? year { get; set; }
    public string? title { get; set; }
    public string? movie_name { get; set; }
    public int? imdb_id { get; set; }
    public int? tmdb_id { get; set; }
}

class SubInfoData
{
    public string? id { get; set; }
    public string? type { get; set; }
    public SubAttributes? attributes { get; set; }
}

class Credentials
{
    required public string username { get; set; }
    required public string password { get; set; }
    required public string key { get; set; }
}

public class QueryParams
{
    public string? ai_translated { get; set; }
    public int? episode_number { get; set; }
    public string? foreign_parts_only { get; set; }
    public string? hearing_impaired { get; set; }
    public int? id { get; set; }
    public int? imdb_id { get; set; }
    public string? languages { get; set; }
    public string? machine_translated { get; set; }
    public string? moviehash { get; set; }
    public string? moviehash_match { get; set; }
    public string? order_by { get; set; }
    public string? order_direction { get; set; }
    public int? page { get; set; }
    public int? parent_feature_id { get; set; }
    public int? parent_tmdb_id { get; set; }
    public string? query { get; set; }
    public int? season_number { get; set; }
    public int? tmdb_id { get; set; }
    public string? trusted_sources { get; set; }
    public string? type { get; set; }
    public int? uploader_id { get; set; }
    public int? year { get; set; }
}

public class SubItems
{
    public string? InternalId { get; set; }
    public string? Name { get; set; }
    public bool? IsmovieMatch { get; set; }
    public int? Rating { get; set; }

    public SubtitleResults? results { get; set; }
    public QueryParams? Sparams { get; set; }
}

public class YearLabel
{

    public int? Year { get; set; }
    public string? Labelyear { get; set; }


}