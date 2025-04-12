using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using DotNetEnv;


HttpClient client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("GumroadStarTracker");

Env.Load();
string token = Environment.GetEnvironmentVariable("gumroad_access_token");
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
var starCountGlobal = 0;

while (true){
    var response = client.GetAsync("https://api.github.com/repos/antiwork/gumroad").Result;
    string jsonResult = await response.Content.ReadAsStringAsync();

    
    using var doc = JsonDocument.Parse(jsonResult);
    var starCount = doc.RootElement.GetProperty("stargazers_count").GetInt32();

    Console.WriteLine("starCount " + starCount);

    starCountGlobal = starCount;
    await Task.Delay(5000);

    var hasStarred = false;
    if(starCount == 4949 && !hasStarred){
        var startResponse = await client.PutAsync("https://api.github.com/user/starred/antiwork/gumroad" , new StringContent(""));
        Console.WriteLine("Starred status " + startResponse.StatusCode);
        hasStarred = true;
        break;
    }

}
Console.WriteLine("Congrats You are the " + starCountGlobal +"th Stargazer...");