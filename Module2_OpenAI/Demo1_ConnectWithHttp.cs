using System.Net.Http.Json;
using Newtonsoft.Json;

namespace JatinSanghvi.Module2_OpenAI;

internal static class Demo1_ConnectWithHttp
{
    // https://platform.openai.com/docs/api-reference/chat/create
    public static async Task CreateChatCompletionHttp()
    {
        Console.WriteLine("\n# Create Chat Completion - HTTP\n");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY")}");

        var requestBody = new
        {
            model = "gpt-4.1-nano",
            messages = new[]
            {
                new { role = "developer", content = "You are a helpful assistant." },
                new { role = "user", content = "Hello!" }
            }
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            dynamic responseBody = JsonConvert.DeserializeObject<dynamic>(responseContent)!;
            var role = (string)responseBody.choices[0].message.role;
            var content = (string)responseBody.choices[0].message.content;

            foreach (var message in requestBody.messages.Append(new { role, content }))
            {
                Console.WriteLine($"{message.role}: {message.content}");
            }
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine(responseContent);
        }
    }

    // https://platform.openai.com/docs/api-reference/responses/create
    public static async Task CreateResponseHttp()
    {
        Console.WriteLine("\n# Create Response - HTTP\n");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY")}");

        var requestBody = new
        {
            model = "gpt-4.1-nano",
            input = "Tell me a three sentence bedtime story about a unicorn.",
        };

        Console.WriteLine($"Input message: {requestBody.input}");
        HttpResponseMessage response = await client.PostAsJsonAsync("https://api.openai.com/v1/responses", requestBody);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            dynamic responseBody = JsonConvert.DeserializeObject<dynamic>(responseContent)!;
            var message = (string)responseBody.output[0].content[0].text;
            Console.WriteLine($"Response message: {message}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            Console.WriteLine(responseContent);
        }
    }
}