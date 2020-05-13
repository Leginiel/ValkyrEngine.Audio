using System.Numerics;

namespace ValkyrEngine.Audio.Resources.NullAudio
{
  /// <summary>
  /// Defines an audio source without any functionality.
  /// </summary>
  public class NullAudioSource : IAudioSource
  {
    /// <inheritdoc/>
    public Vector3 Direction { get; set; }
    /// <inheritdoc/>
    public float Gain { get; set; }
    /// <inheritdoc/>
    public float Pitch { get; set; }
    /// <inheritdoc/>
    public bool IsLooping { get; set; }
    /// <inheritdoc/>
    public Vector3 Position { get; set; }
    /// <inheritdoc/>
    public AudioPositionKind PositionKind { get; set; }
    /// <inheritdoc/>
    public float PlaybackPosition => 1f;
    /// <inheritdoc/>
    public bool IsPlaying => false;

    /// <inheritdoc/>
    public void Dispose()
    {
    }
    /// <inheritdoc/>
    public void Stop()
    {
      throw new System.NotImplementedException();
    }
  }
}