using System;
using System.Collections.Generic;
using System.Linq;

namespace ValkyrEngine.Audio.Resources
{
  /// <summary>
  /// Represents an object which manages audio sources.
  /// </summary>
  public class AudioSourceManager : IDisposable
  {
    private readonly IAudioResourceFactory resourceFactory;
    private List<IAudioSource> freeAudioSources;
    private List<IAudioSource> activeAudioSources;
    private int maximumAudioSources;

    /// <summary>
    /// Returns the current available free audio sources. 
    /// </summary>
    public int FreeAudioSourcesCount => freeAudioSources.Count;
    /// <summary>
    /// Returns the current active audio sources.
    /// </summary>
    public int ActiveAudioSourcesCount => activeAudioSources.Count;
    /// <summary>
    /// Returns the total available audio sources.
    /// </summary>
    public int TotalAudioSourcesCount => FreeAudioSourcesCount + ActiveAudioSourcesCount;
    /// <summary>
    /// Creates a new instance of the <see cref="AudioSourceManager"/>. 
    /// </summary>
    /// <param name="resourceFactory">Factory, that is used to create audio resources.</param>
    public AudioSourceManager(IAudioResourceFactory resourceFactory)
    {
      this.resourceFactory = resourceFactory;
    }

    /// <summary>
    /// Initializes the audio resource manager.
    /// </summary>
    /// <param name="initialAudioSources">Number of initial audio sources.</param>
    /// <param name="maximumAudioSources">Number of maximum available audio sources.</param>
    public void Initialize(int initialAudioSources, int maximumAudioSources)
    {
      activeAudioSources = new List<IAudioSource>(maximumAudioSources);
      freeAudioSources = new List<IAudioSource>(maximumAudioSources);
      this.maximumAudioSources = maximumAudioSources;
      PrepareInitialAudioSources(initialAudioSources);
    }

    /// <summary>
    /// Clears all resources, that is currently used by the audio source manager. 
    /// </summary>
    public void Dispose()
    {
      CleanUpResources(activeAudioSources);
      CleanUpResources(freeAudioSources);
    }
    /// <summary>
    /// Tries to get the next available free audio source.
    /// </summary>
    /// <param name="source">Return parameter for the free audio source. This parameter will return as <code>null</code>, if there is no free audio source.</param>
    /// <returns>Returns <code>True</code>, if there is a free audio source, otherwise <code>False</code>.</returns>
    public bool TryGetAudioSource(out IAudioSource source)
    {
      source = freeAudioSources.FirstOrDefault();
      if (source != null)
      {
        MoveAudioResource(source, freeAudioSources, activeAudioSources);
      }
      else if (TotalAudioSourcesCount < maximumAudioSources)
      {
        source = resourceFactory.CreateAudioSource();
        activeAudioSources.Add(source);
      }
      return source != null;
    }

    /// <summary>
    /// Updates all audio sources and frees up finished audio sources.
    /// </summary>
    public void Update()
    {
      int index = 0;

      while (index < activeAudioSources.Count)
      {
        IAudioSource audioSource = activeAudioSources[index];
        if (IsPlayBackCompleted(audioSource))
        {
          MoveAudioResource(audioSource, activeAudioSources, freeAudioSources);
        }
        else
        {
          index++;
        }
      }
    }

    private void PrepareInitialAudioSources(int initialAudioSources)
    {
      for (int i = 0; i < initialAudioSources; i++)
      {
        freeAudioSources.Add(resourceFactory.CreateAudioSource());
      }
    }
    private void CleanUpResources(List<IAudioSource> audioSources)
    {
      foreach (IAudioSource audioSource in audioSources)
      {
        audioSource.Dispose();
      }
      audioSources.Clear();
    }
    private bool IsPlayBackCompleted(IAudioSource audioSource)
    {
      return (audioSource.PlaybackPosition >= 1.0f) || !audioSource.IsPlaying;
    }
    private void MoveAudioResource(IAudioSource source, IList<IAudioSource> from, IList<IAudioSource> to)
    {
      from.Remove(source);
      to.Add(source);
    }
  }
}
