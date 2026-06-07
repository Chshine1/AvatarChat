using JetBrains.Annotations;

namespace AvatarChat.Host.Options;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Members)]
public class ChatClientOptions
{
    public required string Endpoint { get; init; }
    public required string ApiKey { get; init; }
    public required string Model { get; init; }
}