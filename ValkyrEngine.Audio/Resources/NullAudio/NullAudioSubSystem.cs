namespace ValkyrEngine.Audio.Resources.NullAudio
{
  /// <summary>
  /// Defines an audio subsystem which do not play any audio.
  /// </summary>
  public class NullAudioSubSystem : IAudioSubSystem
  {
    private readonly NullAudioResourceFactory resourceFactory = new NullAudioResourceFactory();
    /// <inheritdoc/>
    public IAudioResourceFactory ResourceFactory => resourceFactory;

    /// <inheritdoc/>
    public string Name => "No audio";
  }
}
