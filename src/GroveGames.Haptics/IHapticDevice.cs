namespace GroveGames.Haptics;

public interface IHapticDevice
{
    public bool IsSupported { get; }

    public void Play(float intensity, float sharpness, float duration);
}
