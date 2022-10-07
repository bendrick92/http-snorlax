using Microsoft.Extensions.Configuration;

namespace HttpSnorlax;

public class Program
{
    private static IConfiguration _configuration;
    private static readonly HttpClient Client = new HttpClient();
    
    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json");

        _configuration = builder.Build();
        
        await Process();
    }

    private static async Task Process()
    {
        var url = _configuration["Url"];
        
        // Uncomment if you want to use as base URL
        //Client.BaseAddress = new Uri(url);

        var result = await Client.GetAsync(url);

        if (result.IsSuccessStatusCode)
        {
            Console.WriteLine("Request successful!");
        }
        else
        {
            Console.WriteLine("Request failed! HTTP {0}", result.StatusCode);
        }
    }
}
