namespace GroveGames.Haptics;

public readonly struct Haptic
{
    private readonly bool _enabled;
    private readonly float _intensity;
    private readonly float _sharpness;
    private readonly float _duration;

    public bool Enabled => _enabled;
    public float Intensity => _intensity;
    public float Sharpness => _sharpness;
    public float Duration => _duration;

    public Haptic(float intensity, float sharpness, float duration) : this(true, intensity, sharpness, duration)
    {
    }

    public Haptic(bool enabled, float intensity, float sharpness, float duration)
    {
        _enabled = enabled;
        _intensity = intensity;
        _sharpness = sharpness;
        _duration = duration;
    }
}
