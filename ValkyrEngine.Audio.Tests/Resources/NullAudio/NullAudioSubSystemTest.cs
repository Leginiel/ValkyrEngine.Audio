using ValkyrEngine.Audio.Resources.NullAudio;
using Xunit;

namespace ValkyrEngine.Audio.Tests.Resources.NullAudio
{
  public class NullAudioSubSystemTest
  {
    [Fact]
    [Trait("Category", "Unit")]
    public void TestName_NoInput_ReturnsTheRightName()
    {
      // Arrange
      NullAudioSubSystem subSystem = new NullAudioSubSystem();

      // Act
      // Assert
      Assert.Equal("No audio", subSystem.Name);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void TestName_NoInput_ReturnsTheRightResourceFactory()
    {
      // Arrange
      NullAudioSubSystem subSystem = new NullAudioSubSystem();

      // Act
      // Assert
      Assert.IsType<NullAudioResourceFactory>(subSystem.ResourceFactory);
    }
  }
}
