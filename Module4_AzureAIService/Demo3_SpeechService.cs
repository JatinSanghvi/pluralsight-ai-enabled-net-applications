using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace JatinSanghvi.Module4_AzureAIService;

internal class Demo3_SpeechService
{
    private readonly SpeechConfig _speechConfig;

    public Demo3_SpeechService()
    {
        var subscriptionKey = Environment.GetEnvironmentVariable("SPEECH_KEY")!;
        var region = Environment.GetEnvironmentVariable("SPEECH_REGION")!;
        _speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);
        _speechConfig.SpeechRecognitionLanguage = "en-US";
    }

    public async Task SpeechRecognitionFromFile()
    {
        Console.WriteLine("\n# Speech Recognition From File\n");

        using var audioConfig = AudioConfig.FromWavFileInput("Assets/audio-gill.wav");
        using var recognizer = new SpeechRecognizer(_speechConfig, audioConfig);
        SpeechRecognitionResult recognition = await recognizer.RecognizeOnceAsync();

        Console.WriteLine($"Speech recognized: [{recognition.Text}].");
    }

    public async Task SpeechRecognitionFromMicrophone()
    {
        Console.WriteLine("\n# Speech Recognition From Microphone\n");

        using var recognizer = new SpeechRecognizer(_speechConfig);
        SpeechRecognitionResult recognition = await recognizer.RecognizeOnceAsync();
        Console.WriteLine("Please speak into the microphone...");

        switch (recognition.Reason)
        {
            case ResultReason.RecognizedSpeech:
                Console.WriteLine($"Speech recognized: [{recognition.Text}].");
                break;
            case ResultReason.NoMatch:
                Console.WriteLine("Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                Console.WriteLine($"Speech recognition was canceled: [{CancellationDetails.FromResult(recognition).Reason}].");
                break;
            default:
                Console.WriteLine("Encountered unexpected speech recognition result.");
                break;
        }
    }
}