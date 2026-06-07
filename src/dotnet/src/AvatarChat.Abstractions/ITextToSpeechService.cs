namespace AvatarChat.Abstractions;

public interface ITextToSpeechService
{
    Task<Stream> SynthesizeAsync(string text, string emotion);
}