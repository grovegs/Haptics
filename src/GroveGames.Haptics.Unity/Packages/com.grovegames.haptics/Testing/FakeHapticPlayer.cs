using GroveGames.Haptics;

namespace GroveGames.Haptics.Testing
{
    public sealed class FakeHapticPlayer : IHapticPlayer
    {
        private bool _isEnabled;
        private int _playCount;

        public bool IsSupported => true;
        public bool IsEnabled => _isEnabled;
        public int PlayCount => _playCount;

        public FakeHapticPlayer()
        {
            _isEnabled = true;
            _playCount = 0;
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

            _playCount++;
        }
    }
}
