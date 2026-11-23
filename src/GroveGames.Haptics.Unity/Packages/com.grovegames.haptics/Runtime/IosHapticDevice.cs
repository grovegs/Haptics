#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;

namespace GroveGames.Haptics.Unity
{
    internal sealed class IosHapticDevice : IHapticDevice
    {
        [DllImport("__Internal")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool GroveGamesHaptics_IsSupported();

        [DllImport("__Internal")]
        private static extern void GroveGamesHaptics_Play(float intensity, float sharpness, float duration);

        private readonly bool _isSupported;

        public bool IsSupported => _isSupported;

        public IosHapticDevice()
        {
            _isSupported = GroveGamesHaptics_IsSupported();
        }

        public void Play(float intensity, float sharpness, float duration)
        {
            GroveGamesHaptics_Play(intensity, sharpness, duration);
        }
    }
}
#endif
