using ValkyrEngine.Audio.Resources;
using ValkyrEngine.Audio.Resources.NullAudio;
using Xunit;

namespace ValkyrEngine.Audio.Tests.Resources.NullAudio
{
  public class NullAudioResourceFactoryTest
  {
    [Fact]
    [Trait("Category", "Unit")]
    public void TestCreateAudioBuffer_ReturnNullAudioBuffer()
    {
      // Arrange
      NullAudioResourceFactory factory = new NullAudioResourceFactory();

      // Act
      IAudioBuffer buffer = factory.CreateAudioBuffer();

      // Assert
      Assert.IsType<NullAudioBuffer>(buffer);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void TestCreateAudioSource_ReturnNullAudioSource()
    {
      // Arrange
      NullAudioResourceFactory factory = new NullAudioResourceFactory();

      // Act
      IAudioSource source = factory.CreateAudioSource();

      // Assert
      Assert.IsType<NullAudioSource>(source);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void TestCreateAudioEngine_ReturnNullAudioEngine()
    {
      // Arrange
      NullAudioResourceFactory factory = new NullAudioResourceFactory();

      // Act
      IAudioEngine engine = factory.CreateAudioEngine();

      // Assert
      Assert.IsType<NullAudioEngine>(engine);
    }
  }
}
