namespace AvatarChat.Abstractions;

public interface ISpeechToTextService
{
    Task<string> TranscribeAsync(Stream audioStream, CancellationToken cancellationToken = default);
}