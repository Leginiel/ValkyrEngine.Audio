namespace ValkyrEngine.Audio.Resources.NullAudio
{
  /// <summary>
  /// Create an audio buffer without any functionality.
  /// </summary>
  public class NullAudioBuffer : IAudioBuffer
  {
    /// <inheritdoc/>
    public void BufferData<T>(T[] data, BufferAudioFormat format) { }
    /// <inheritdoc/>
    public void Dispose() { }
  }
}
