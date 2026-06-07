using System.ClientModel;
using AvatarChat.Abstractions;
using AvatarChat.Core.Agents;
using AvatarChat.Core.Services;
using AvatarChat.Host.Options;
using JetBrains.Annotations;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OpenAI;

namespace AvatarChat.Host;

[UsedImplicitly]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<ISpeechToTextService, SpeechToTextService>();
        builder.Services.AddSingleton<ITextToSpeechService, TextToSpeechService>();

        builder.Services.Configure<AvatarBackendOptions>(builder.Configuration.GetSection("AvatarBackend"));
        builder.Services.AddHttpClient<IAvatarInferenceService, PythonAvatarInferenceService>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AvatarBackendOptions>>().Value;
            
            client.BaseAddress = new Uri(options.Url);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        });

        builder.Services.Configure<ChatClientOptions>(builder.Configuration.GetSection("ChatClient"));
        builder.Services.AddSingleton<IChatClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ChatClientOptions>>().Value;

            var credential = new ApiKeyCredential(options.ApiKey);
            var openAiClient = new OpenAIClient(credential, new OpenAIClientOptions
            {
                Endpoint = new Uri(options.Endpoint)
            }).GetChatClient(options.Model);

            return openAiClient.AsIChatClient();
        });

        builder.Services.AddSingleton<DigitalHumanAgent>();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}