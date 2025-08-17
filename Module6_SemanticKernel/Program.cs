namespace JatinSanghvi.Module6_SemanticKernel;

public static class Program
{
    public static async Task Main()
    {
        // WriteHeading("1. Basics");
        // var basics = new Demo1_Basics();
        // await basics.ChatCompletion();
        // await basics.ChatHistory();
        // await basics.Streaming();
        // await basics.ExecutionSettings();

        // WriteHeading("2. Image Generation");
        // var imageGeneration = new Demo2_ImageGeneration();
        // await imageGeneration.ImageGeneration();

        WriteHeading("3. Plugins");
        var plugins = new Demo3_Plugins();
        await plugins.DaysUntilChristmas();
        await plugins.MultiplePlugins();
    }

    private static void WriteHeading<T>(T message)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
        }
        finally
        {
            Console.ResetColor();
        }
    }
}
