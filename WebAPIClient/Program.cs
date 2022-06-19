using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using Newtonsoft.Json;
using WebAPIClient;

static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
{
    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
    dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
    return dtDateTime;
}

// --- Question 1 ---
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=Morocco,ma&exclude=lon&APPID=8aba9d73b0944ea50196ad82b055430c")
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    string body = await response.Content.ReadAsStringAsync();
    // Console.WriteLine(body);
    Root morocco = JsonConvert.DeserializeObject<Root>(body);
    string moroccoWeather = morocco.weather[0].description;
    Console.WriteLine("\nWhat’s the weather like in Morocco? : " + moroccoWeather + "\n");
}

// --- Question 2 ---

var molo = new HttpClient();
var sunstuff = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?&lat=59.91&lon=10.75&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response1 = await molo.SendAsync(sunstuff))
{
    response1.EnsureSuccessStatusCode();
    string body = await response1.Content.ReadAsStringAsync();
    Root oslo = JsonConvert.DeserializeObject<Root>(body);
    var osloSunRiseUTC = UnixTimeStampToDateTime(oslo.sys.sunrise);
    var osloSunSetUTC = UnixTimeStampToDateTime(oslo.sys.sunset);
    Console.WriteLine("Oslo Sunrise : " + osloSunRiseUTC + "\nOslo Sunset : " + osloSunSetUTC + "\n");
}

// Question 3 

var jaja = new HttpClient();
var temperature = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?&lat=-6.21&lon=106.84&units=metric&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response2 = await jaja.SendAsync(temperature))
{
    response2.EnsureSuccessStatusCode();
    string body = await response2.Content.ReadAsStringAsync();
    //Console.WriteLine(body);
    Root jakarta = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("Temperature in Jakarta is : "+jakarta.main.temp+"°C\n");
}
//question 4
var wind = new HttpClient();
var question3 = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/group?id=5128581,1850147,2968815&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response3 = await wind.SendAsync(question3))
{
    response3.EnsureSuccessStatusCode();
    string body = await response3.Content.ReadAsStringAsync();
    var win = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("Wind Speed in New York is : " + win.list[0].wind.speed);
    Console.WriteLine("Wind Speed in Tokyo is : " + win.list[1].wind.speed);
    Console.WriteLine("Wind Speed in Paris is : " + win.list[2].wind.speed);
    double high = new[] { win.list[0].wind.speed, win.list[1].wind.speed, win.list[2].wind.speed }.Max();
    Console.WriteLine("The highest wind Speed is : " + high +"\n");
}

//question 5 
var quest5 = new HttpClient();
var question5 = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://api.openweathermap.org/data/2.5/group?id=703448,524901,2950158&APPID=8aba9d73b0944ea50196ad82b055430c")
};

using (var response5 = await quest5.SendAsync(question5))
{
    response5.EnsureSuccessStatusCode();
    string body = await response5.Content.ReadAsStringAsync();
    var hum = JsonConvert.DeserializeObject<Root>(body);
    Console.WriteLine("Humidity and Pressure in Kiev is respectively : " + hum.list[0].main.humidity + " - "+ hum.list[0].main.pressure);
    Console.WriteLine("Humidity and Pressure in Moscow is respectively : " + hum.list[1].main.humidity + " - "+ hum.list[1].main.pressure);
    Console.WriteLine("Humidity and Pressure in Berlin is respectively : : " + hum.list[2].main.humidity + " - "+ hum.list[2].main.pressure);
    
}
