using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("GumroadStarTracker");

while (true){
    var response = client.GetAsync("https://api.github.com/repos/antiwork/gumroad").Result;
    string jsonResult = await response.Content.ReadAsStringAsync();

    using var doc = JsonDocument.Parse(jsonResult);
    Console.WriteLine(jsonResult);

}