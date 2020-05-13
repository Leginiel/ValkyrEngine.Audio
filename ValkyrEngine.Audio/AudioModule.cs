using Autofac;

namespace ValkyrEngine.Audio
{
  /// <summary>
  /// A class that is used to setup autofac.
  /// </summary>
  public class AudioModule : Autofac.Module
  {
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AudioSystem>()
             .As<IAudioSystem>()
             .SingleInstance();
    }
  }
}
