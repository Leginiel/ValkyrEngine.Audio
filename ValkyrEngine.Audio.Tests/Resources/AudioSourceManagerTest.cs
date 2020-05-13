using Moq;
using ValkyrEngine.Audio.Resources;
using Xunit;

namespace ValkyrEngine.Audio.Tests.Resources
{
  public class AudioSourceManagerTest
  {
    [Fact]
    [Trait("Category", "Unit")]
    public void Test_Initialize_AudioSourceManagerIsInitialized()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);

      // Act
      manager.Initialize(5, 10);

      // Assert
      Assert.Equal(5, manager.FreeAudioSourcesCount);
      Assert.Equal(5, manager.TotalAudioSourcesCount);
      Assert.Equal(0, manager.ActiveAudioSourcesCount);
      audioResourceFactorMock.Verify((_) => _.CreateAudioSource(), Times.Exactly(5));
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void Test_Dispose_AudioSourceManagerCleansUpAudioSources()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      audioSourceMock.Setup((_) => _.Dispose())
                     .Verifiable();

      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);
      manager.Initialize(5, 10);

      // Act
      manager.Dispose();

      // Assert
      audioSourceMock.Verify((_) => _.Dispose(), Times.Exactly(5));
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void Test_TryGetAudioSource_TrueAndAudioSource()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      audioSourceMock.Setup((_) => _.Dispose())
                     .Verifiable();

      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);
      manager.Initialize(5, 10);

      // Act
      bool result = manager.TryGetAudioSource(out IAudioSource source);

      // Assert
      Assert.True(result);
      Assert.NotNull(source);
      Assert.Equal(4, manager.FreeAudioSourcesCount);
      Assert.Equal(5, manager.TotalAudioSourcesCount);
      Assert.Equal(1, manager.ActiveAudioSourcesCount);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Test_TryGetAudioSourceCreateNewFreeSource_TrueAndAudioSource()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      audioSourceMock.Setup((_) => _.Dispose())
                     .Verifiable();

      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);
      manager.Initialize(1, 2);
      manager.TryGetAudioSource(out IAudioSource source);

      // Act
      bool result = manager.TryGetAudioSource(out source);

      // Assert
      Assert.True(result);
      Assert.NotNull(source);
      Assert.Equal(0, manager.FreeAudioSourcesCount);
      Assert.Equal(2, manager.ActiveAudioSourcesCount);
      Assert.Equal(2, manager.TotalAudioSourcesCount);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Test_TryGetAudioSourceNoFreeResources_FalseAndNull()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      audioSourceMock.Setup((_) => _.Dispose())
                     .Verifiable();

      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);
      manager.Initialize(1, 1);
      manager.TryGetAudioSource(out IAudioSource source);


      // Act
      bool result = manager.TryGetAudioSource(out source);

      // Assert
      Assert.False(result);
      Assert.Null(source);
      Assert.Equal(0, manager.FreeAudioSourcesCount);
      Assert.Equal(1, manager.ActiveAudioSourcesCount);
      Assert.Equal(1, manager.TotalAudioSourcesCount);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Test_UpdateStoppedPlaying_AudioSourceIsFreed()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      audioSourceMock.Setup((_) => _.Dispose())
                     .Verifiable();

      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);
      manager.Initialize(1, 1);
      manager.TryGetAudioSource(out IAudioSource source);
      source.Stop();
      // Act
      manager.Update();

      // Assert
      Assert.Equal(1, manager.FreeAudioSourcesCount);
      Assert.Equal(0, manager.ActiveAudioSourcesCount);
      Assert.Equal(1, manager.TotalAudioSourcesCount);
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void Test_UpdateEndOfPlayBackReached_AudioSourceIsFreed()
    {
      // Arrange
      Mock<IAudioResourceFactory> audioResourceFactorMock = new Mock<IAudioResourceFactory>();
      Mock<IAudioSource> audioSourceMock = new Mock<IAudioSource>();

      audioResourceFactorMock.Setup((_) => _.CreateAudioSource())
                             .Returns(audioSourceMock.Object)
                             .Verifiable();

      audioSourceMock.Setup((_) => _.Dispose())
                     .Verifiable();
      audioSourceMock.Setup((_) => _.PlaybackPosition)
                     .Returns(1.0f);


      AudioSourceManager manager = new AudioSourceManager(audioResourceFactorMock.Object);
      manager.Initialize(1, 1);
      manager.TryGetAudioSource(out IAudioSource source);

      // Act
      manager.Update();

      // Assert
      Assert.Equal(1, manager.FreeAudioSourcesCount);
      Assert.Equal(0, manager.ActiveAudioSourcesCount);
      Assert.Equal(1, manager.TotalAudioSourcesCount);
    }
  }
}
