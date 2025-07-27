# AI Enabled .NET Applications

This repository was created by following PluralSight course: [Building AI-enabled .NET Applications](https://app.pluralsight.com/library/courses/semantic-kernel-c-sharp-building-ai-applications) by Gill Cleeren.

## Projects

### Module2_OpenAI

Contains C# source files for different OpenAI API features:

- **Project files**
    - `Demo1_ConnectWithHttp.cs` - Calling OpenAI REST APIs directly.
    - `Demo2_ChatCompletions.cs` - Chat completions for text generation.
    - `Demo3_ImageGenerations.cs` - Image generation and editing.
    - `Demo4_AudioTransciptions.cs`: Audio transcription.
    - `Demo5_FunctionCalling.cs`: Chat completion with tools and function calling.
    - `Demo6_Responses.cs`: Responses API for text generation.
- **Requirement**: OpenAI API key.
- **Setup**: Add your key in `Module2_OpenAI/Properties/launchSettings.json`
- **Running project**: `dotnet run --project Module2_OpenAI/Module2_OpenAI.csproj`
- **References**
    - [OpenAI Platform Guide](https://platform.openai.com/docs/guides)
    - [OpenAI .NET SDK Documentation](https://github.com/openai/openai-dotnet)
