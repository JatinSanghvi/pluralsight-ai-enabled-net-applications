using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace JatinSanghvi.Module6_SemanticKernel;

internal class Demo3_Plugins
{
    public readonly Kernel _kernel;
    public readonly IChatCompletionService _chatCompletionService;

    public Demo3_Plugins()
    {
        _kernel = Kernel
            .CreateBuilder()
            .AddOpenAIChatCompletion(modelId: "gpt-4.1-nano", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")!)
            .Build();

        _kernel.ImportPluginFromType<TimePlugin>();
        _kernel.ImportPluginFromType<WeatherPlugin>();

        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public async Task DaysUntilChristmas()
    {
        Console.WriteLine("\n# Days Until Christmas\n");

        string prompt = "How many days are there until Christmas this year?";
        var settings = new OpenAIPromptExecutionSettings { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
        Console.WriteLine(await _kernel.InvokePromptAsync(prompt, new KernelArguments(settings)));
    }

    // For some reason, call to method `InvokePromptAsync` calls plugins but `GetChatMessageContentAsync` does not.
    public async Task MultiplePlugins()
    {
        Console.WriteLine("\n# Multiple Plugins\n");

        var chatHistory = new ChatHistory();

        while (true)
        {
            Console.Write("Enter message (type 'quit' to exit): ");
            string userMessage = Console.ReadLine()!;
            if (userMessage == "quit") { break; }

            chatHistory.AddUserMessage(userMessage);
            string assistantMessage = (await _chatCompletionService.GetChatMessageContentAsync(chatHistory)).Content!;
            Console.WriteLine($"Assistant: {assistantMessage}");
            chatHistory.AddAssistantMessage(assistantMessage);
        }
    }
}

public class TimePlugin
{
    [KernelFunction]
    [Description("Gets the current date and time in UTC")]
    public string GetCurrentDateAndTime()
    {
        return new DateTime(2024, 11, 11).ToString("R");
    }
}

public class WeatherPlugin
{
    [KernelFunction]
    [Description("Provides real-time weather update on a given date and location")]
    public string GetWeather(
        [Description("The date to give weather for")] string date,
        [Description("The location to give weather for")] string location)
    {
        if (location == "Brussels")
        {
            return $"The weather in {location} on {date} is sunny with a high of 75 degress Fahrenheit.";
        }
        else
        {
            return "I'm sorry. I don't have that information.";
        }
    }
}