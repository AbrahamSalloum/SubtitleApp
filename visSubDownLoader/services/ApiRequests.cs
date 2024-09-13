using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 
namespace visSubDownLoader.services;
using visSubDownLoader.Creds;
using visSubDownLoader.Models;

using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
class ApiRequests
{
    private string? apikey;
    private string? username;
    private string? password;
    private string? token;
    private HttpClient client;

    
    public ApiRequests()
    {

        Credentials? p = CredentailsReader.ReadCredentials();
        apikey = p.key;
        username = p.username;
        password = p.password;
        client = ClientSetup();
    }
    public static string BaseAPIUrl = "https://api.opensubtitles.com/api/v1";
    public static ApiRequests? _apiRequests;

    public static ApiRequests Instance()
    {
        if(_apiRequests == null)
        {
            _apiRequests = new ApiRequests();
        }

        return _apiRequests;
    }

    private HttpClient ClientSetup()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Api-Key", $"{apikey}");
        client.DefaultRequestHeaders.Add("User-Agent", "1234");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }

    public async Task<LoginInfo?> Login()
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseAPIUrl}/login");
        object jsonObj = new
        {
            username,
            password
        };
        requestMessage.Content = new StringContent(JsonConvert.SerializeObject(jsonObj));
        HttpResponseMessage response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
        LoginInfo? logininfo = JsonConvert.DeserializeObject<LoginInfo>(responseAsString);
        token = logininfo?.token;
        return logininfo;
    }
    public async Task<LogoutInfo?> Logout(string token = "string")
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseAPIUrl}/logout");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        HttpResponseMessage response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
        LogoutInfo? logoutinfo = JsonConvert.DeserializeObject<LogoutInfo>(responseAsString);
        return logoutinfo;
    }
    public async Task GetRecentList()
    {

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseAPIUrl}/discover/latest");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        HttpResponseMessage response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
    }

    public async Task<SubtitleResults?> SubtitleSearch(string jsonString)
    {

        QueryParams? q = JsonConvert.DeserializeObject<QueryParams>(jsonString);
        if (q is null)
        {
            return null;
        }
        JObject searchterms = JObject.FromObject(q);
        string urlquery = "";
        foreach (var p in searchterms)
        {
            if (p.Value?.Type != JTokenType.Null)
                urlquery += $"{p.Key}={p.Value}&";
        }
        if (urlquery is null)
        {
            return null;
        }
        SubtitleResults? x = await runSearch(urlquery);

        return x;
    }

    public async Task<SubtitleResults?> SubtitleSearch(QueryParams q)
    {

        JObject searchterms = JObject.FromObject(q);
        string urlquery = "";
        foreach (var p in searchterms)
        {
            if (p.Value?.Type != JTokenType.Null)
                urlquery += $"{p.Key}={p.Value}&";
        }
        Console.Write(urlquery);
        SubtitleResults? x = await runSearch(urlquery);

        return x;
    }

    public async Task<SubtitleResults?> runSearch(string urlquery)
    {
        try
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseAPIUrl}/subtitles?languages=en&{urlquery}");
            requestMessage.Headers.Add("Authorization", $"Bearer {token}");
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            string responseAsString = await response.Content.ReadAsStringAsync();
            SubtitleResults? x = JsonConvert.DeserializeObject<SubtitleResults>(responseAsString);

            return x;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
            return null;
        }
    }

    public async Task<DownLoadLinkData?> RequestDownloadURL(int subid)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseAPIUrl}/download");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        string s = $"{{\n  \"file_id\": {subid}\n}}"; // the wrong spaces and newlines here will cause errrors. 
        requestMessage.Content = new StringContent(s, Encoding.ASCII, "application/json");

        HttpResponseMessage response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
        DownLoadLinkData? x = JsonConvert.DeserializeObject<DownLoadLinkData>(responseAsString);
        return x;
    }

    public async Task DownloadSubFile(DownLoadLinkData info, string path)
    {
        Stream fileStream = await client.GetStreamAsync(info.link);

        FileStream outputFileStream = new FileStream($"{path}\\{info.file_name}.sub", FileMode.CreateNew);
        await fileStream.CopyToAsync(outputFileStream);
    }

}

