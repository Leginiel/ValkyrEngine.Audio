using System.Numerics;

namespace ValkyrEngine.Audio.Resources.NullAudio
{
  /// <summary>
  /// Defines an audio engine without any functionality.
  /// </summary>
  public class NullAudioEngine : IAudioEngine
  {
    /// <inheritdoc/>
    public void SetListenerOrientation(Vector3 forward, Vector3 up) { }
    /// <inheritdoc/>
    public void SetListenerPosition(Vector3 position) { }
  }
}
