using System.Net.Http.Headers;
using AvatarChat.Abstractions;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace AvatarChat.Core.Services;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Members)]
public class AvatarBackendOptions
{
    public required string Url { get; init; }
    public required int TimeoutSeconds { get; init; }
    public required EndpointsSection Endpoints { get; init; }

    [UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Members)]
    public class EndpointsSection
    {
        public required string InferencePath { get; init; }
    }
}

public class PythonAvatarInferenceService(HttpClient httpClient, IOptions<AvatarBackendOptions> options)
    : IAvatarInferenceService
{
    private readonly AvatarBackendOptions _options = options.Value;

    public async Task<Stream> DriveAvatarAsync(Stream audioStream, CancellationToken cancellationToken = default)
    {
        using var content = new StreamContent(audioStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

        var response = await httpClient.PostAsync(_options.Endpoints.InferencePath, content, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync(cancellationToken);
    }
}