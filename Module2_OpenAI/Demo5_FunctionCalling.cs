using System.Text.Json;
using OpenAI;
using OpenAI.Chat;

namespace JatinSanghvi.Module2_OpenAI;

internal static class Demo5_FunctionCalling
{
    // https://github.com/openai/openai-dotnet?tab=readme-ov-file#how-to-use-chat-completions-with-tools-and-function-calling
    public static async Task ChatCompletionWithFunctionCalling(OpenAIClient client)
    {
        ChatClient chatClient = client.GetChatClient("gpt-4.1-nano");

        ChatTool getCurrentLocationTool = ChatTool.CreateFunctionTool(
            functionName: nameof(GetCurrentLocation),
            functionDescription: "Get the user's current location");

        ChatTool getCurrentWeatherTool = ChatTool.CreateFunctionTool(
            functionName: nameof(GetWeather),
            functionDescription: "Get the current weather in a given location",
            functionParameters: BinaryData.FromString("""
                {
                    "type": "object",
                    "properties": {
                        "location": {
                            "type": "string",
                            "description": "The city and state, e.g. Boston, MA"
                        },
                        "unit": {
                            "type": "string",
                            "enum": [ "celsius", "fahrenheit" ],
                            "description": "The temperature unit to use. Infer this from the specified location."
                        }
                    },
                    "required": [ "location" ]
                }
                """));

        var options = new ChatCompletionOptions
        {
            Tools = { getCurrentLocationTool, getCurrentWeatherTool },
        };

        IReadOnlyList<ChatMessage> messages = await HaveConversation(chatClient, options);

        // Print all chat messages.
        foreach (var message in messages)
        {
            Console.Write($"[{message.GetType().Name}]: ");

            if (message.Content.Count > 0)
            {
                Console.WriteLine(string.Join(", ", message.Content.Select(content => content.Text)));
            }

            if (message is AssistantChatMessage assistantMessage && assistantMessage.ToolCalls.Count > 0)
            {
                Console.WriteLine(string.Join(", ",
                    assistantMessage.ToolCalls.Select(call => $"{call.FunctionName}({call.FunctionArguments.ToString()})")));
            }
        }
    }

    private static string GetCurrentLocation()
    {
        // Call the current location API here.
        return "San Francisco";
    }

    private static string GetWeather(string location, string unit = "celsius")
    {
        // Call the current weather API here.
        return $"Expect light showers and 30 degrees {unit} today in {location}.";
    }

    private static async Task<IReadOnlyList<ChatMessage>> HaveConversation(ChatClient chatClient, ChatCompletionOptions options)
    {
        var messages = new List<ChatMessage>
        {
            new UserChatMessage("What clothes should I wear and items I should carry considering today's weather?"),
        };

        while (true)
        {
            ChatCompletion completion = await chatClient.CompleteChatAsync(messages, options);

            switch (completion.FinishReason)
            {
                case ChatFinishReason.Stop:
                    // Add the assistant message to the conversation history.
                    messages.Add(new AssistantChatMessage(completion));
                    return messages;

                case ChatFinishReason.ToolCalls:
                    // First, add the assistant message with tool calls to the conversation history.
                    messages.Add(new AssistantChatMessage(completion));

                    // Then, add a new tool message for each tool call that is resolved.
                    foreach (ChatToolCall toolCall in completion.ToolCalls)
                    {
                        messages.Add(new ToolChatMessage(toolCall.Id, GetToolCallContent(toolCall)));
                    }

                    break;

                case ChatFinishReason.Length:
                    throw new NotImplementedException("Incomplete model output due to MaxTokens parameter or token limit exceeded.");

                case ChatFinishReason.ContentFilter:
                    throw new NotImplementedException("Omitted content due to a content filter flag.");

                case ChatFinishReason.FunctionCall:
                    throw new NotImplementedException("Deprecated in favor of tool calls.");

                default:
                    throw new NotImplementedException(completion.FinishReason.ToString());
            }
        }
    }

    private static string GetToolCallContent(ChatToolCall toolCall)
    {
        switch (toolCall.FunctionName)
        {
            case nameof(GetCurrentLocation):
                return GetCurrentLocation();

            case nameof(GetWeather):
                {
                    // The arguments that the model wants to use to call the function are specified as a
                    // stringified JSON object based on the schema defined in the tool definition. Note that
                    // the model may hallucinate arguments too. Consequently, it is important to do the
                    // appropriate parsing and validation before calling the function.
                    using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                    bool hasLocation = argumentsJson.RootElement.TryGetProperty("location", out JsonElement location);
                    bool hasUnit = argumentsJson.RootElement.TryGetProperty("unit", out JsonElement unit);

                    if (!hasLocation)
                    {
                        throw new ArgumentNullException(nameof(location), "The location argument is required.");
                    }

                    return hasUnit
                        ? GetWeather(location.GetString()!, unit.GetString()!)
                        : GetWeather(location.GetString()!);
                }

            default:
                // Handle other unexpected calls.
                throw new NotImplementedException();
        }
    }
}