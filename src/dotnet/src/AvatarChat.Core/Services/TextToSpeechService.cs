using AvatarChat.Abstractions;

namespace AvatarChat.Core.Services;

public class TextToSpeechService : ITextToSpeechService
{
    public Task<Stream> SynthesizeAsync(string text, string emotion)
    {
        throw new NotImplementedException();
    }
}