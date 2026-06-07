namespace AvatarChat.Abstractions;

public interface IAvatarInferenceService
{
    Task<Stream> DriveAvatarAsync(Stream audioStream, CancellationToken cancellationToken = default);
}