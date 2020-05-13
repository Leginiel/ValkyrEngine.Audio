using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ValkyrEngine.Audio.Messages;
using ValkyrEngine.Audio.Resources;
using ValkyrEngine.MessageSystem;

namespace ValkyrEngine.Audio
{
  /// <summary>
  /// 
  /// </summary>
  public class AudioSystem : System<AudioSettings>, IAudioSystem
  {
    private AudioSourceManager audioSourceManager;

    private IAudioSubSystem AudioSubSystem => (IAudioSubSystem)ActiveSubSystem;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messageSystem"></param>
    public AudioSystem(IMessageSystem messageSystem)
      : base(messageSystem) { }

    /// <inheritdoc />
    public override void CleanUp()
    {
      base.CleanUp();
    }

    /// <inheritdoc/>
    protected override void SubSystemChanged()
    {
      if (audioSourceManager != null)
        audioSourceManager.Dispose();

      audioSourceManager = new AudioSourceManager(AudioSubSystem.ResourceFactory);
    }
    /// <inheritdoc/>
    protected override IEnumerable<ISubSystem> SetupSubSystems()
    {
      return FindSubSystems();
    }

    /// <inheritdoc/>
    protected override void Setup()
    {
      ActivateSubSystem(SubSystems.Where((_) => _.Name.Equals(SystemSettings.AudioSourceProvider)).First());
    }
    /// <inheritdoc/>
    protected override void SetupMessageHandler()
    {
      MessageSystem.RegisterReceiver<PlaySoundMessage>(PlaySound);
    }

    /// <inheritdoc/>
    protected override void CleanUpMessageHandler()
    {
      MessageSystem.UnregisterReceiver<PlaySoundMessage>(PlaySound);
    }

    #region MessageHandler
    private Task PlaySound(PlaySoundMessage message)
    {
      return Task.Run(() =>
      {
        if (audioSourceManager.TryGetAudioSource(out IAudioSource audioSource))
        {
          IAudioBuffer buffer = AudioSubSystem.ResourceFactory.CreateAudioBuffer();
          buffer.BufferData(message.Asset.RawData, SystemSettings.AudioFormat);

          audioSource.Gain = message.Volume;
          audioSource.Pitch = message.Pitch;
          audioSource.Play(buffer);
          audioSource.PositionKind = message.AudioPositionKind;
          audioSource.Position = message.Position;
        }
      });
    }
    #endregion

    #region Load Subsystems
    private IEnumerable<IAudioSubSystem> FindSubSystems()
    {
      List<Assembly> assemblies = LoadAssemblies();
      return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IAudioSubSystem).IsAssignableFrom(t))
            .Select(type => (IAudioSubSystem)Activator.CreateInstance(type));
    }
    private List<Assembly> LoadAssemblies()
    {
      List<Assembly> assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };

      foreach (string file in GetAllFiles())
      {
        assemblies.Add(Assembly.LoadFile(file));
      }
      return assemblies;
    }
    private string[] GetAllFiles()
    {
      return Directory.GetFiles(Assembly.GetExecutingAssembly().Location, "*.dll");
    }
    #endregion
  }
}
