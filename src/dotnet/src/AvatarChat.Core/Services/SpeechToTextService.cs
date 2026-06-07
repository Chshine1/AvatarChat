using AvatarChat.Abstractions;

namespace AvatarChat.Core.Services;

public class SpeechToTextService : ISpeechToTextService
{
    public Task<string> TranscribeAsync(Stream audioStream, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}