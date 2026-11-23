namespace GroveGames.Haptics.Tests;

public sealed class HapticTests
{
    [Fact]
    public void Constructor_Values_IsEnabled()
    {
        var haptic = new Haptic(0.3f, 0.5f, 0.03f);

        Assert.True(haptic.Enabled);
        Assert.Equal(0.3f, haptic.Intensity);
        Assert.Equal(0.5f, haptic.Sharpness);
        Assert.Equal(0.03f, haptic.Duration);
    }

    [Fact]
    public void Constructor_Disabled_KeepsTheValues()
    {
        var haptic = new Haptic(false, 0.3f, 0.5f, 0.03f);

        Assert.False(haptic.Enabled);
        Assert.Equal(0.3f, haptic.Intensity);
    }

    [Fact]
    public void Default_NoValues_IsDisabled()
    {
        var haptic = default(Haptic);

        Assert.False(haptic.Enabled);
    }
}
