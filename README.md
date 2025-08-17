# AI Enabled .NET Applications

This repository was created by following PluralSight course: [Building AI-enabled .NET Applications](https://app.pluralsight.com/library/courses/semantic-kernel-c-sharp-building-ai-applications) by Gill Cleeren.

## Projects

### Module2_OpenAI

Contains source files for different OpenAI API features.

- **Project files**
    - `Demo1_ConnectWithHttp.cs` - Calling OpenAI REST APIs directly.
    - `Demo2_ChatCompletions.cs` - Chat completions for text generation.
    - `Demo3_ImageGenerations.cs` - Image generation and editing.
    - `Demo4_AudioTransciptions.cs`: Audio transcription.
    - `Demo5_FunctionCalling.cs`: Chat completion with tools and function calling.
    - `Demo6_Responses.cs`: Responses API for text generation.
- **Requirement**: OpenAI account with credit balance.
- **Setup**: Add your OpenAI API key in `Module2_OpenAI/Properties/launchSettings.json`
- **Running project**: `dotnet run --project Module2_OpenAI/Module2_OpenAI.csproj`
- **References**
    - [OpenAI Platform Guide](https://platform.openai.com/docs/guides)
    - [OpenAI .NET SDK Documentation](https://github.com/openai/openai-dotnet)

### Module4_AzureAIService

Contains source files for working with various Azure AI Services.

- **Project files**
    - `Demo1_Language.cs` - Language detection, sentiment analysis, and text summarization.
    - `Demo2_ComputerVision.cs` - Image caption generation, and object recognition.
    - `Demo3_SpeechService.cs` - Text-to-speech functionality.
    - `Demo4_Translator.cs` - Text translation between languages.
- **Requirements**: Azure subscription with the following AI Services:
    - Azure AI Language (formerly Text Analytics)
    - Azure AI Vision (formerly Computer Vision)
    - Azure AI Speech (formerly Speech Services)
    - Azure AI Translator (formerly Translator Text)
- **Setup**: Add your Azure AI Services endpoints and keys in `Module4_AzureAIService/Properties/launchSettings.json`
- **Running project**: `dotnet run --project Module4_AzureAIService/Module4_AzureAIService.csproj`
- **References**
    - [Azure AI Language Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/)
    - [Azure AI Vision Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/)
    - [Azure AI Speech Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/)
    - [Azure AI Translator Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/translator/)

### Module6_SemanticKernel

Contains source files for working with Microsoft Semantic Kernel SDK.

- **Project files**
    - `Demo1_ChatCompletion.cs` - Chat completion using Semantic Kernel.
    - `Demo2_ImageGeneration.cs` - Image generation with Semantic Kernel.
    - `Demo3_Plugins.cs` - Using plugins with Semantic Kernel.
- **Setup**: Add your API keys and endpoints in `Module6_SemanticKernel/Properties/launchSettings.json` as required by the demos.
- **Running project**: `dotnet run --project Module6_SemanticKernel/Module6_SemanticKernel.csproj`
- **References**
    - [Microsoft Semantic Kernel Documentation](https://learn.microsoft.com/en-us/semantic-kernel/)
    - [Semantic Kernel GitHub Repository](https://github.com/microsoft/semantic-kernel/)
