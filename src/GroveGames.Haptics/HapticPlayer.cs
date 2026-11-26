namespace GroveGames.Haptics;

public sealed class HapticPlayer : IHapticPlayer
{
    private readonly IHapticDevice _device;
    private bool _isEnabled;

    public bool IsSupported => _device.IsSupported;
    public bool IsEnabled => _isEnabled;

    public HapticPlayer(IHapticDevice device)
    {
        ArgumentNullException.ThrowIfNull(device);

        _device = device;
        _isEnabled = true;
    }

    public void SetEnabled(bool isEnabled)
    {
        _isEnabled = isEnabled;
    }

    public void Play(in Haptic haptic)
    {
        if (!haptic.Enabled)
        {
            return;
        }

        Play(haptic.Intensity, haptic.Sharpness, haptic.Duration);
    }

    public void Play(float intensity, float sharpness, float duration)
    {
        if (!_isEnabled || !(intensity > 0f))
        {
            return;
        }

        _device.Play(Math.Min(intensity, 1f), sharpness > 0f ? Math.Min(sharpness, 1f) : 0f, duration > 0f ? duration : 0f);
    }
}
