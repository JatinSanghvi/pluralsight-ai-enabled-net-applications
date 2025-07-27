using OpenAI;
using OpenAI.Images;

namespace JatinSanghvi.Module2_OpenAI;

internal class D3_ImageGenerations
{
    private readonly ImageClient _imageClient;

    public D3_ImageGenerations(OpenAIClient client)
    {
        _imageClient = client.GetImageClient("gpt-image-1");
    }

    // https://github.com/openai/openai-dotnet?tab=readme-ov-file#how-to-generate-images
    // https://platform.openai.com/docs/api-reference/images/create
    public async Task ImageGeneration()
    {
        string prompt = "The concept for a living room that blends Scandinavian simplicity with Japanese minimalism for"
            + " a serene and cozy atmosphere. It's a space that invites relaxation and mindfulness, with natural light"
            + " and fresh air. Using neutral tones, including colors like white, beige, gray, and black, that create a"
            + " sense of harmony. Featuring sleek wood furniture with clean lines and subtle curves to add warmth and"
            + " elegance. Plants and flowers in ceramic pots adding color and life to a space. They can serve as focal"
            + " points, creating a connection with nature. Soft textiles and cushions in organic fabrics adding comfort"
            + " and softness to a space. They can serve as accents, adding contrast and texture.";

#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates.
        var options = new ImageGenerationOptions
        {
            Quality = GeneratedImageQuality.Medium,
            Size = GeneratedImageSize.W1536xH1024,
            ModerationLevel = GeneratedImageModerationLevel.Low,
        };
#pragma warning restore OPENAI001

        GeneratedImage image = await _imageClient.GenerateImageAsync(prompt, options);

        using FileStream stream = File.OpenWrite(Path.Combine("Assets", "image-living-room-generated.png"));
        await image.ImageBytes.ToStream().CopyToAsync(stream);
    }

    // https://platform.openai.com/docs/api-reference/images/createVariation
    public async Task ImageEdit()
    {
        string imageFilePath = Path.Combine("Assets", "image-happy-color-source.png");
        string prompt = "Make the image photo-realistic with absolutely minimum edits. Make sure the women looks like a beauty model.";

        var options = new ImageEditOptions
        {
            Size = GeneratedImageSize.W1024xH1024,
        };

        GeneratedImage image = await _imageClient.GenerateImageEditAsync(imageFilePath, prompt, options);

        using FileStream outStream = File.OpenWrite(Path.Combine("Assets", "image-happy-color-generated.png"));
        await image.ImageBytes.ToStream().CopyToAsync(outStream);
    }
}