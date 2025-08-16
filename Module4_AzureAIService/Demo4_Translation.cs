using Azure;
using Azure.AI.Translation.Text;

namespace JatinSanghvi.Module4_AzureAIService;

internal class Demo4_Translation
{
    private readonly TextTranslationClient _client;

    public Demo4_Translation()
    {
        var credential = new AzureKeyCredential(Environment.GetEnvironmentVariable("TRANSLATION_KEY")!);
        var region = Environment.GetEnvironmentVariable("TRANSLATION_REGION")!;
        _client = new TextTranslationClient(credential, region);
    }

    public async Task TextTranslation()
    {
        Console.WriteLine("\n# Text Translation\n");

        string text =
            "This cheesecake is a classic dessert with a rich, creamy filling and a buttery graham cracker crust. " +
            "The smooth, velvety texture of the cream cheese blend is perfectly balanced with a hint of vanilla and " +
            "a slight tang from sour cream, creating a delightful flavor profile. The crust, made from crushed " +
            "graham crackers and melted butter, provides a sweet, crunchy base that complements the silky filling. " +
            "Baked to perfection and chilled until set, this cheesecake is ideal for any occasion. Top it with fresh " +
            "berries or a drizzle of fruit preserves for an added burst of flavor and a beautiful presentation.";

        Console.WriteLine($"Input text: [{text}].");
        IReadOnlyList<TranslatedTextItem> textItems = (await _client.TranslateAsync(targetLanguage: "nl", text)).Value;
        TranslatedTextItem textItem = textItems.Single();

        Console.WriteLine($"\nDetected language: [{textItem.DetectedLanguage.Language}] with confidence: [{textItem.DetectedLanguage.Confidence}].");
        Console.WriteLine($"\nTranslated Text: [{textItem.Translations.Single().Text}].");
    }
}