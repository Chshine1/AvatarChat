using AvatarChat.Abstractions;
using JetBrains.Annotations;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AvatarChat.Core.Agents;

public class DigitalHumanAgent
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public AIAgent Agent { get; }

    public DigitalHumanAgent(IChatClient chatClient, ISpeechToTextService asrService, ITextToSpeechService ttsService)
    {
        var asrTool =
            AIFunctionFactory.Create(async (Stream audioStream) => await asrService.TranscribeAsync(audioStream),
                "transcribe_audio", "Transcribe user speech to text");

        var ttsTool =
            AIFunctionFactory.Create(
                async (string text, string emotion) => await ttsService.SynthesizeAsync(text, emotion),
                "synthesize_speech", "Synthesize reply speech from texts, support emotion parameter");

        Agent = new ChatClientAgent(chatClient, "You are a friendly, enthusiastic digital human", "DigitalHuman", null,
            [asrTool, ttsTool]);
    }
}