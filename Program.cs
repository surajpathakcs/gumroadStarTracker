using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using DotNetEnv;


class Program{

static async Task Main(string[] args)
{

    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.UserAgent.ParseAdd("GumroadStarTracker");

    Env.Load();
    string token = Environment.GetEnvironmentVariable("gumroad_access_token");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
    var starCountGlobal = 0;

    while (true)
    {
        var response = await client.GetAsync("https://api.github.com/repos/antiwork/gumroad");
        string jsonResult = await response.Content.ReadAsStringAsync();


        using var doc = JsonDocument.Parse(jsonResult);
        var starCount = doc.RootElement.GetProperty("stargazers_count").GetInt32();

        Console.WriteLine("starCount " + starCount);


        if (starCount == 4999)
        {
            var startResponse = await client.PutAsync("https://api.github.com/user/starred/antiwork/gumroad", new StringContent(""));
            Console.WriteLine("Starred status " + startResponse.StatusCode);
            starCountGlobal = starCount + 1;
            break;
        }
        await Task.Delay(719);

    }
    Console.WriteLine("Congrats You are the " + starCountGlobal + "th Stargazer...");

}
}