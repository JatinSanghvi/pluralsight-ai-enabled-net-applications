using Azure;
using Azure.AI.TextAnalytics;

namespace JatinSanghvi.Module4_AzureAIService;

internal class Demo1_Language
{
    private readonly TextAnalyticsClient _client;

    public Demo1_Language()
    {
        var endpoint = new Uri(Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT")!);
        var credential = new AzureKeyCredential(Environment.GetEnvironmentVariable("LANGUAGE_KEY")!);
        _client = new TextAnalyticsClient(endpoint, credential);
    }

    public async Task LanguageDetection()
    {
        Console.WriteLine("\n# Language Detection\n");

        string description =
            "Un cheesecake crémeux et onctueux, délicatement posé sur une base croustillante de biscuits Graham " +
            "dorés et légèrement sucrés. Sa garniture riche et veloutée est confectionnée à partir de fromage frais, " +
            "offrant une texture fondante et un goût subtilement acidulé. Pour parfaire cette douceur, le gâteau " +
            "est nappé d'un coulis de fruits rouges frais, ajoutant une touche de fraîcheur et d'acidité.";

        Console.WriteLine($"Input text: [{description}].");
        DetectedLanguage language = await _client.DetectLanguageAsync(description);
        Console.WriteLine($"\nDetected language: [{language.Name}], ISO name: [{language.Iso6391Name}], confidence score: [{language.ConfidenceScore}].");
    }

    public async Task SentimentAnalysis()
    {
        Console.WriteLine("\n# Sentiment Analysis\n");

        string feedback =
            "The cheesecake from this website was a huge disappointment. The texture was grainy, not creamy, and " +
            "the flavor was bland with an overpowering artificial taste. The crust was soggy and stale. The " +
            "packaging was inadequate, resulting in a damaged cake. Not worth the price. One positive note: " +
            "the delivery was on time.";

        Console.WriteLine($"Input text: [{feedback}].");
        DocumentSentiment sentiment = await _client.AnalyzeSentimentAsync(feedback);

        Console.WriteLine($"\nGeneral sentiment: [{sentiment.Sentiment}].");
        Console.WriteLine($"Scores (positive/neutral/negative): [{sentiment.ConfidenceScores.Positive:0.00}/{sentiment.ConfidenceScores.Neutral:0.00}/{sentiment.ConfidenceScores.Negative:0.00}].");
        Console.WriteLine($"Sentences:");

        foreach (var sentence in sentiment.Sentences)
        {
            Console.WriteLine($"    Sentence: [{sentence.Text}].");
            Console.WriteLine($"    Sentiment: [{sentence.Sentiment}].");
            Console.WriteLine($"    Scores (positive/neutral/negative): [{sentence.ConfidenceScores.Positive:0.00}/{sentence.ConfidenceScores.Neutral:0.00}/{sentence.ConfidenceScores.Negative:0.00}].");
            Console.WriteLine();
        }
    }

    public async Task SummaryExtraction()
    {
        Console.WriteLine("\n# Summary Extraction\n");

        var story =
            "Nestled in the heart of Brussels, Belgium, Bethany's Pie Shop has been a beloved local gem since its " +
            "founding in 1997. The story of this charming pie store begins with Bethany DeWitt, a passionate baker " +
            "with a dream of bringing her family's cherished pie recipes to life in a bustling European city. " +
            "Growing up in a small village in Flanders, Bethany learned the art of pie-making from her grandmother, " +
            "whose pies were legendary in their community. Inspired by the rich flavors and time-honored techniques " +
            "passed down through generations, Bethany decided to share her family's culinary heritage with the world.\r\n\r\n" +
            "With a suitcase full of recipes and a heart full of hope, Bethany moved to Brussels and opened her quaint " +
            "shop on a picturesque cobblestone street near the Grand Place. From day one, the shop was a hit. Locals " +
            "and tourists alike were drawn to the shop's warm, inviting atmosphere and the mouthwatering aroma of " +
            "freshly baked pies wafting through the air. Bethany's dedication to using only the finest, locally " +
            "sourced ingredients set her pies apart. She made everything from scratch, from the flaky, buttery crusts " +
            "to the rich, decadent fillings. Her menu featured a delightful mix of sweet and savory pies, from classic " +
            "apple and cherry to innovative creations like Belgian chocolate pecan and leek and gruyère.\r\n\r\n" +
            "Over the years, Bethany's Pie Shop has grown from a small, humble bakery into a cherished institution. " +
            "Bethany herself has become a local legend, known for her warm hospitality and unwavering commitment to " +
            "quality. Today, the shop remains family-owned, with Bethany's daughter, Clara, taking the reins to carry " +
            "on her mother's legacy. The shop continues to delight customers with its delicious pies and welcoming " +
            "atmosphere, making every visit to Bethany's Pie Shop a truly memorable experience in the heart of Brussels.";

        Console.WriteLine($"Input text: [{story}].");
        ExtractiveSummarizeOperation summaryOperation = await _client.ExtractiveSummarizeAsync(WaitUntil.Completed, [story]);

        await foreach (var summaryPage in summaryOperation.Value)
        {
            foreach (var summary in summaryPage)
            {
                Console.WriteLine($"\nSummary extracted with [{summary.Sentences.Count}] sentence(s):");
                foreach (var sentence in summary.Sentences)
                {
                    Console.WriteLine($"    - {sentence.Text}");
                }
            }
        }
    }

    public async Task EntityRecognition()
    {
        Console.WriteLine("\n# Entity Recognition\n");

        string description =
            "This cheesecake is a classic dessert with a rich, creamy filling and a buttery graham cracker crust. " +
            "The smooth, velvety texture of the cream cheese blend is perfectly balanced with a hint of vanilla " +
            "and a slight tang from sour cream, creating a delightful flavor profile. The crust, made from crushed " +
            "graham crackers and melted butter, provides a sweet, crunchy base that complements the silky filling. " +
            "Baked to perfection and chilled until set, this cheesecake is ideal for any occasion. Top it with fresh " +
            "berries or a drizzle of fruit preserves for an added burst of flavor and a beautiful presentation.";

        Console.WriteLine($"Input text: [{description}].");
        CategorizedEntityCollection extractedEntities = await _client.RecognizeEntitiesAsync(description);

        Console.WriteLine("\nExtracted Entities:");

        foreach (var entity in extractedEntities)
        {
            Console.WriteLine($"    Text: [{entity.Text}].");
            Console.WriteLine($"    Entity length: [{entity.Length}], category: [{entity.Category}], sub category: [{entity.SubCategory}], confidence score: [{entity.ConfidenceScore}].");
            Console.WriteLine();
        }
    }
}