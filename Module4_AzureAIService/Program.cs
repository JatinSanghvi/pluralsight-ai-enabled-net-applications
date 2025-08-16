namespace JatinSanghvi.Module4_AzureAIService;

public static class Program
{
    public static async Task Main()
    {
        WriteHeading("1. Language Service");
        var language = new Demo1_Language();
        await language.LanguageDetection();
        await language.SentimentAnalysis();
        await language.SummaryExtraction();
        await language.EntityRecognition();

        WriteHeading("2. Computer Vision");
        var vision = new Demo2_ComputerVision();
        await vision.CaptionGeneration();
        await vision.TagExtraction();
        await vision.ObjectDetection();

        WriteHeading("3. Speech Service");
        var speech = new Demo3_SpeechService();
        await speech.SpeechRecognitionFromFile();
        await speech.SpeechRecognitionFromMicrophone();

        WriteHeading("4. Translation");
        var translation = new Demo4_Translation();
        await translation.TextTranslation();
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
