using Newtonsoft.Json;
using WebAPIClient;

// --- Question 1 ---
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=Morocco,ma&APPID=8aba9d73b0944ea50196ad82b055430c")
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    string body = await response.Content.ReadAsStringAsync();
    // Console.WriteLine(body);
    Root morocco = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("\n");
    string moroccoWeather = morocco.weather[0].description;
    Console.WriteLine("What’s the weather like in Morocco? : " + moroccoWeather + "\n");
}
// How to call API in different classes ?
// --- Question 2 ---

var molo = new HttpClient();
var sunstuff = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=Oslo,no&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response1 = await client.SendAsync(sunstuff))
{
    response1.EnsureSuccessStatusCode();
    string body1 = await response1.Content.ReadAsStringAsync();
    Console.WriteLine(body1 + "\n");
    Root oslo = JsonConvert.DeserializeObject<Root>(body1) ?? throw new InvalidOperationException();
    Console.WriteLine("\n");
    int osloSunRise = oslo.sys.sunrise;
    int osloSunSet = oslo.sys.sunset;
    Console.WriteLine(osloSunRise + "\n" + osloSunSet);
}

/*static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
{
    // Unix timestamp is seconds past epoch
    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
    dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
    return dtDateTime;
}*/