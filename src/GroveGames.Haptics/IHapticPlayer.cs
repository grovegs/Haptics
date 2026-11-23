namespace GroveGames.Haptics;

public interface IHapticPlayer
{
    public bool IsSupported { get; }
    public bool IsEnabled { get; }

    public void SetEnabled(bool isEnabled);
    public void Play(in Haptic haptic);
    public void Play(float intensity, float sharpness, float duration);
}
