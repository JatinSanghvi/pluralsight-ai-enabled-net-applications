using Azure;
using Azure.AI.TextAnalytics;
using Azure.AI.Vision.ImageAnalysis;

namespace JatinSanghvi.Module4_AzureAIService;

internal class Demo2_ComputerVision
{
    private readonly ImageAnalysisClient _client;

    public Demo2_ComputerVision()
    {
        var endpoint = new Uri(Environment.GetEnvironmentVariable("VISION_ENDPOINT")!);
        var credential = new AzureKeyCredential(Environment.GetEnvironmentVariable("VISION_KEY")!);
        _client = new ImageAnalysisClient(endpoint, credential);
    }

    public async Task CaptionGeneration()
    {
        Console.WriteLine("\n# Caption Generation\n");

        using var imageStream = File.OpenRead("Module4_AzureAIService/Assets/photo-empty-restaurant.jpeg");

        ImageAnalysisResult analysis = await _client.AnalyzeAsync(
            imageData: await BinaryData.FromStreamAsync(imageStream),
            visualFeatures: VisualFeatures.Caption);

        Console.WriteLine($"Caption text: [{analysis.Caption.Text}], confidence: [{analysis.Caption.Confidence:0.00}].");
    }

    public async Task TagExtraction()
    {
        Console.WriteLine("\n# Tag Extraction\n");

        using var imageStream = File.OpenRead("Module4_AzureAIService/Assets/photo-empty-restaurant.jpeg");

        ImageAnalysisResult analysis = await _client.AnalyzeAsync(
            imageData: await BinaryData.FromStreamAsync(imageStream),
            visualFeatures: VisualFeatures.Tags);

        foreach (var tag in analysis.Tags.Values)
        {
            Console.WriteLine($"Tag: [{tag.Name}], confidence: [{tag.Confidence:0.00}].");
        }
    }

    public async Task ObjectDetection()
    {
        Console.WriteLine("\n# Object Detection\n");

        using var imageStream = File.OpenRead("Module4_AzureAIService/Assets/photo-empty-restaurant.jpeg");

        ImageAnalysisResult analysis = await _client.AnalyzeAsync(
            imageData: await BinaryData.FromStreamAsync(imageStream),
            visualFeatures: VisualFeatures.Objects);

        foreach (var obj in analysis.Objects.Values)
        {
            Console.WriteLine($"Tags: [{string.Join(", ", obj.Tags.Select(tag => tag.Name))}], bounding box: [{obj.BoundingBox}].");
        }
    }
}