using OpenAI;
using OpenAI.Audio;

namespace JatinSanghvi.Module2_OpenAI;

internal static class D4_AudioTransciptions
{
    // https://github.com/openai/openai-dotnet/tree/main?tab=readme-ov-file#how-to-transcribe-audio
    // https://platform.openai.com/docs/api-reference/usage/audio_transcriptions
    public static async Task TranscribeAudio(OpenAIClient client)
    {
        AudioClient audioClient = client.GetAudioClient("whisper-1");
        string audioFilePath = Path.Combine("Assets", "audio_houseplant_care.mp3");

        var options = new AudioTranscriptionOptions()
        {
            ResponseFormat = AudioTranscriptionFormat.Verbose,
            TimestampGranularities = AudioTimestampGranularities.Word | AudioTimestampGranularities.Segment,
        };

        AudioTranscription transcription = await audioClient.TranscribeAudioAsync(audioFilePath, options);

        Console.WriteLine("\n[Transcription]:");
        Console.WriteLine(transcription.Text);

        Console.WriteLine("\n\n[Words]:");
        foreach (TranscribedWord word in transcription.Words)
        {
            Console.WriteLine($"  {word.Word,15} : {word.StartTime.TotalMilliseconds,5:0} - {word.EndTime.TotalMilliseconds,5:0}");
        }

        Console.WriteLine("\n\n[Segments]:");
        foreach (TranscribedSegment segment in transcription.Segments)
        {
            Console.WriteLine($"  {segment.Text,90} : {segment.StartTime.TotalMilliseconds,5:0} - {segment.EndTime.TotalMilliseconds,5:0}");
        }
    }
}