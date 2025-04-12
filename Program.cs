using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using DotNetEnv;


HttpClient client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("GumroadStarTracker");

Env.Load();
string token = Environment.GetEnvironmentVariable("gumroad_access_token");
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);


while (true){
    var response = client.GetAsync("https://api.github.com/repos/antiwork/gumroad").Result;
    string jsonResult = await response.Content.ReadAsStringAsync();

    
    using var doc = JsonDocument.Parse(jsonResult);
    var starCount = doc.RootElement.GetProperty("stargazers_count").GetInt32();

    Console.WriteLine("starCount "+starCount);

    await Task.Delay(10000);

    if(starCount >= 4997 ){
        var startResponse = await client.GetAsync("https://api.github.com/user/starred/gumroad/gumroad" , new StringContent());
    }

}