using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace JatinSanghvi.Module6_SemanticKernel;

internal class Demo1_Basics
{
    public readonly Kernel _kernel;
    public readonly IChatCompletionService _chatCompletionService;

    public Demo1_Basics()
    {
        _kernel = Kernel
            .CreateBuilder()
            .AddOpenAIChatCompletion(modelId: "gpt-4.1-nano", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")!)
            .Build();

        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public async Task ChatCompletion()
    {
        Console.WriteLine("\n# Chat Completion\n");

        while (true)
        {
            Console.Write("Enter message (type 'quit' to exit): ");
            string prompt = Console.ReadLine()!;
            if (prompt == "quit") { break; }

            Console.WriteLine("\n" + await _kernel.InvokePromptAsync(prompt));
        }
    }

    public async Task ChatHistory()
    {
        Console.WriteLine("\n# Chat History\n");

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

    public async Task Streaming()
    {
        Console.WriteLine("\n# Streaming\n");

        var chatHistory = new ChatHistory(systemMessage: "You are a pie creator and you should come up with magnificient and delicious recipes for pies.");
        chatHistory.AddAssistantMessage("Welcome to the pie creator chat. How can I help you today?");
        chatHistory.AddUserMessage("I want to create a pumpkin pie.");

        foreach (var message in chatHistory)
        {
            Console.WriteLine($"{message.Role}: {message.Content}");
        }

        var chatMessages = _chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);
        Console.WriteLine();

        await foreach (StreamingChatMessageContent message in chatMessages)
        {
            Console.Write(message.Content);
        }

        Console.WriteLine();
    }

    public async Task ExecutionSettings()
    {
        Console.WriteLine("\n# Execution Settings\n");

        string prompt = "Tell me a story about Bethany's Pie Shop, a pie store located in Brussels which is known for its delicious pies. Keep it short.";
        var executionSettings = new OpenAIPromptExecutionSettings { MaxTokens = 500 };

        executionSettings.Temperature = 0.0;
        Console.WriteLine("\nTemperature 0:");
        Console.WriteLine(await _kernel.InvokePromptAsync(prompt, new KernelArguments(executionSettings)));

        executionSettings.Temperature = 1.0;
        Console.WriteLine("\nTemperature 1:");
        Console.WriteLine(await _kernel.InvokePromptAsync(prompt, new KernelArguments(executionSettings)));
    }
}
