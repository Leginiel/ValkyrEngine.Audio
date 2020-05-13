namespace ValkyrEngine.Audio.Resources.NullAudio
{
  /// <summary>
  /// Represents an object that provides methods to create the different audio reosurces. All Audio object provided by <see cref="NullAudioResourceFactory"/>
  /// doesn't provide any functionality. 
  /// </summary>
  public class NullAudioResourceFactory : AudioResourceFactory
  {
    /// <inheritdoc/>
    public override IAudioBuffer CreateAudioBuffer()
    {
      return new NullAudioBuffer();
    }
    /// <inheritdoc/>
    public override IAudioSource CreateAudioSource()
    {
      return new NullAudioSource();
    }
    /// <inheritdoc/>
    public override IAudioEngine CreateAudioEngine()
    {
      return new NullAudioEngine();
    }
  }
}
