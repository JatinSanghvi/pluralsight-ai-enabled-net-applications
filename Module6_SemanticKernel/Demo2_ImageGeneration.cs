#pragma warning disable SKEXP0001 // Suppress evaluation-only API warning
#pragma warning disable SKEXP0010 // Suppress evaluation-only API warning

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;

namespace JatinSanghvi.Module6_SemanticKernel;

internal class Demo2_ImageGeneration
{
    public readonly Kernel _kernel;
    public readonly ITextToImageService _imageService;

    public Demo2_ImageGeneration()
    {
        _kernel = Kernel
            .CreateBuilder()
            .AddOpenAITextToImage(modelId: "dall-e-3", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")!)
            .Build();

        _imageService = _kernel.GetRequiredService<ITextToImageService>();
    }

    public async Task ImageGeneration()
    {
        Console.WriteLine("\n# Image Generation\n");

        string description =
            "A cozy and inviting pie shop that blends the warmth of rustic charm with the elegance of modern design, " +
            "creating a space that feels both nostalgic and contemporary. The ambiance is centered around a palette " +
            "of warm, earthy tones—think rich browns, soft creams, and muted greens—complemented by natural wood " +
            "finishes that exude a sense of comfort and tradition.\n\n" +

            "The shop features handcrafted wooden tables with subtle details, surrounded by comfortable seating that " +
            "encourages customers to linger and enjoy their treats. Large windows allow natural light to flood the " +
            "space, creating an airy and welcoming environment.\n\n" +

            "On the walls, you'll find a mix of vintage-inspired artwork and shelves displaying artisanal products and " +
            "fresh flowers, which add a personal touch and a connection to nature. The counter, where pies are " +
            "displayed, is the focal point, with a clean, minimalist design that highlights the vibrant colors and " +
            "textures of the baked goods.\n\n" +

            "Incorporating soft textiles, such as woven rugs and cushions in natural fabrics, adds warmth and coziness, " +
            "making the space feel like a home away from home. The overall atmosphere invites customers to relax, " +
            "savor their pie, and enjoy a moment of simple pleasure in a beautifully crafted setting.";

        // Returns image URL. Example:
        // https://oaidalleapiprodscus.blob.core.windows.net/private/org-4VMBr6geZfFxgpVKueC4Gt22/
        // user-ytWVFF0MsqcKKq3OSAoimeS8/img-PqsIQqgyzIBnkvgMN88tOwSC.png?st=2025-08-17T05%3A37%3A25Z
        // &se=2025-08-17T07%3A37%3A25Z&sp=r&sv=2024-08-04&sr=b&rscd=inline&rsct=image/png
        // &skoid=cc612491-d948-4d2e-9821-2683df3719f5&sktid=a48cca56-e6da-484e-a814-9c849652bcb3
        // &skt=2025-08-16T11%3A23%3A43Z&ske=2025-08-17T11%3A23%3A43Z&sks=b&skv=2024-08-04
        // &sig=/r1rATZSLwcyaWZ3zgtycl70U3adv80hb1Jf7L7FXhc%3D

        var image = await _imageService.GenerateImageAsync(description, width: 1792, height: 1024);
        Console.WriteLine($"Image URL: {image}");
    }
}
