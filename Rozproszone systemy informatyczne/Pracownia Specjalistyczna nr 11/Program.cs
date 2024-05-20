using System.Text.Json;
using System.Net.Http;
using System.Text;

string apiUrl = "http://localhost:5244/api/";

try
{
    using HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(apiUrl+"message/2");

    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine($"{responseBody}");

    response = await client.PostAsync(apiUrl+"message", 
        new StringContent(JsonSerializer.Serialize(new PostMessage
        {
            Author = "ja",
            MessageText = "siemka",
        }), Encoding.UTF8, "application/json"));

    responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine($"{responseBody}");

    response = await client.GetAsync(apiUrl+"product/all");

    response.EnsureSuccessStatusCode();

    responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine($"{responseBody}");
}
catch (HttpRequestException e)
{
    Console.WriteLine("\nException Caught!");
    Console.WriteLine("Message :{0} ", e.Message);
}