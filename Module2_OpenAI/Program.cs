using OpenAI;

namespace JatinSanghvi.Module2_OpenAI;

public static class Program
{
    public static async Task Main()
    {
        // WriteHeading("1. Connect with OpenAI using HTTP");
        // await Demo1_ConnectWithHttp.CreateChatCompletionHttp();
        // await Demo1_ConnectWithHttp.CreateResponseHttp();

        var client = new OpenAIClient(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        // WriteHeading("2. Chat completions");
        // var chatCompletions = new Demo2_ChatCompletions(client);
        // await chatCompletions.ChatCompletion();
        // await chatCompletions.ChatCompletionWithStreaming();
        // await chatCompletions.ChatCompletionWithJsonOutput();
        // await chatCompletions.InteractiveChat();

        // WriteHeading("3. Image generations");
        // var imageGenerations = new Demo3_ImageGenerations(client);
        // await imageGenerations.ImageGeneration();
        // await imageGenerations.ImageEdit();

        // WriteHeading("4. Audio transcriptions");
        // await Demo4_AudioTransciptions.TranscribeAudio(client);

        // WriteHeading("5. Function calling");
        // await Demo5_FunctionCalling.ChatCompletionWithFunctionCalling(client);

        WriteHeading("6. Response");
        var responses = new Demo6_Responses(client);
        // await chatCompletions.Response();
        // await chatCompletions.ResponseWithStreaming();
        await responses.InteractiveChat();
    }

    private static void WriteHeading<T>(T message)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }
        finally
        {
            Console.ResetColor();
        }
    }
}
