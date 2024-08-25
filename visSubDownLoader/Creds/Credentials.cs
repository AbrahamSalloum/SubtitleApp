
using System.Text.Json;
using visSubDownLoader.Models; 


namespace visSubDownLoader.Creds;


class CredentailsReader
{
    public static Credentials? ReadCredentials()
    {
        string file = "C:\\credentials.json";
        string? text = File.ReadAllText(file);
        if (text is null)
        {
            Console.WriteLine("Can not open the C:\\credentails.json file.");
            return null;
        }
        Credentials? person = JsonSerializer.Deserialize<Credentials>(text);
        if (person is null)
        {
            Console.WriteLine("Something is wrong with your credentials.json file.");
            return null;
        }

        return person;
    }
}