namespace ValkyrEngine.Audio.Resources
{
  /// <summary>
  /// Represents an object which provide functionality to create audio resources.
  /// </summary>
  public abstract class AudioResourceFactory : IAudioResourceFactory
  {
    /// <inheritdoc/>
    public abstract IAudioSource CreateAudioSource();
    /// <inheritdoc/>
    public abstract IAudioBuffer CreateAudioBuffer();
    /// <inheritdoc/>
    public abstract IAudioEngine CreateAudioEngine();
  }
}
