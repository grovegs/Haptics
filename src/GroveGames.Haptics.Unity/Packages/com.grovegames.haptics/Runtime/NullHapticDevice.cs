namespace GroveGames.Haptics.Unity
{
    internal sealed class NullHapticDevice : IHapticDevice
    {
        public bool IsSupported => false;

        public void Play(float intensity, float sharpness, float duration)
        {
        }
    }
}
