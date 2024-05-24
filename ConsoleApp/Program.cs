using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://api.sampleapis.com/codingresources/codingResources";
        try
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };

            var resources = JsonSerializer.Deserialize<List<Resource>>(response, options);

            var uniqueTopics = new HashSet<string>();

            if (resources != null)
            {
                foreach (var resource in resources)
                {
                    if (resource.Topics != null)
                    {
                        foreach (var topic in resource.Topics)
                        {
                            uniqueTopics.Add(topic);
                        }
                    }
                }

                foreach (var topic in uniqueTopics)
                {
                    Console.WriteLine(topic);
                }
            }
            else
            {
                Console.WriteLine("No resources found.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    public class Resource
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public List<string>? Types { get; set; }
        public List<string>? Topics { get; set; }
        public List<string>? Levels { get; set; }
    }
}
