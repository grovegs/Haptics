namespace GroveGames.Haptics.Tests;

public sealed class HapticPlayerTests
{
    [Fact]
    public void Constructor_NullDevice_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new HapticPlayer(null!));
    }

    [Fact]
    public void Constructor_Device_StartsEnabled()
    {
        var player = new HapticPlayer(new FakeHapticDevice(true));

        Assert.True(player.IsEnabled);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsSupported_Device_ReturnsTheDeviceValue(bool isSupported)
    {
        var player = new HapticPlayer(new FakeHapticDevice(isSupported));

        Assert.Equal(isSupported, player.IsSupported);
    }

    [Fact]
    public void Play_Values_ReachTheDevice()
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);

        player.Play(0.3f, 0.5f, 0.03f);

        Assert.Equal(1, device.PlayCount);
        Assert.Equal(0.3f, device.Intensity);
        Assert.Equal(0.5f, device.Sharpness);
        Assert.Equal(0.03f, device.Duration);
    }

    [Fact]
    public void Play_ValuesOutOfRange_AreClamped()
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);

        player.Play(2f, -1f, -0.5f);

        Assert.Equal(1f, device.Intensity);
        Assert.Equal(0f, device.Sharpness);
        Assert.Equal(0f, device.Duration);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-0.2f)]
    public void Play_NoIntensity_DoesNotReachTheDevice(float intensity)
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);

        player.Play(intensity, 0.5f, 0.03f);

        Assert.Equal(0, device.PlayCount);
    }

    [Fact]
    public void Play_PlayerDisabled_DoesNotReachTheDevice()
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);
        player.SetEnabled(false);

        player.Play(0.3f, 0.5f, 0.03f);
        player.Play(new Haptic(0.3f, 0.5f, 0.03f));

        Assert.False(player.IsEnabled);
        Assert.Equal(0, device.PlayCount);
    }

    [Fact]
    public void Play_PlayerEnabledAgain_ReachesTheDevice()
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);
        player.SetEnabled(false);
        player.SetEnabled(true);

        player.Play(new Haptic(0.3f, 0.5f, 0.03f));

        Assert.Equal(1, device.PlayCount);
    }

    [Fact]
    public void Play_Haptic_PassesItsValues()
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);

        player.Play(new Haptic(0.8f, 0.2f, 0.15f));

        Assert.Equal(0.8f, device.Intensity);
        Assert.Equal(0.2f, device.Sharpness);
        Assert.Equal(0.15f, device.Duration);
    }

    [Fact]
    public void Play_DisabledHaptic_DoesNotReachTheDevice()
    {
        var device = new FakeHapticDevice(true);
        var player = new HapticPlayer(device);

        player.Play(new Haptic(false, 0.8f, 0.2f, 0.15f));
        player.Play(default(Haptic));

        Assert.Equal(0, device.PlayCount);
    }

    private sealed class FakeHapticDevice : IHapticDevice
    {
        private readonly bool _isSupported;
        private int _playCount;
        private float _intensity;
        private float _sharpness;
        private float _duration;

        public bool IsSupported => _isSupported;
        public int PlayCount => _playCount;
        public float Intensity => _intensity;
        public float Sharpness => _sharpness;
        public float Duration => _duration;

        public FakeHapticDevice(bool isSupported)
        {
            _isSupported = isSupported;
        }

        public void Play(float intensity, float sharpness, float duration)
        {
            _playCount++;
            _intensity = intensity;
            _sharpness = sharpness;
            _duration = duration;
        }
    }
}
