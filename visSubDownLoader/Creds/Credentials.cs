

using visSubDownLoader.Models;
namespace visSubDownLoader.Creds;


class CredentailsReader
{
    public static Credentials? ReadCredentials()
    {

		try
		{
            string user = Preferences.Default.Get("user", "Unknown");
            string pass = Preferences.Default.Get("pass", "Unknown");
            string userkey = Preferences.Default.Get("userkey", "Unknown");

            Credentials person = new() { username = user, password = pass, key = userkey };

            return person;
        }
		catch (Exception e)
		{

			Console.WriteLine($"{e.Message}");  
            return null;    
		}

        
    }
}