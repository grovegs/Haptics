namespace GroveGames.Haptics.Unity
{
    public sealed class UnityHapticDevice : IHapticDevice
    {
        private readonly IHapticDevice _device;

        public bool IsSupported => _device.IsSupported;

        public UnityHapticDevice()
        {
            _device = CreateDevice();
        }

        public void Play(float intensity, float sharpness, float duration)
        {
            _device.Play(intensity, sharpness, duration);
        }

        private static IHapticDevice CreateDevice()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return new IosHapticDevice();
#elif UNITY_ANDROID && !UNITY_EDITOR
            return new AndroidHapticDevice();
#else
            return new NullHapticDevice();
#endif
        }
    }
}
