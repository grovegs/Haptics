using GroveGames.Haptics;

namespace GroveGames.Haptics.Testing
{
    public sealed class FakeHapticPlayer : IHapticPlayer
    {
        private bool _isEnabled = true;
        private int _playCount;

        public bool IsSupported => true;
        public bool IsEnabled => _isEnabled;
        public int PlayCount => _playCount;

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        public void Play(in Haptic haptic)
        {
            if (!haptic.Enabled || !_isEnabled || haptic.Intensity <= 0f)
            {
                return;
            }

            _playCount++;
        }

        public void Play(float intensity, float sharpness, float duration)
        {
        }
    }
}
