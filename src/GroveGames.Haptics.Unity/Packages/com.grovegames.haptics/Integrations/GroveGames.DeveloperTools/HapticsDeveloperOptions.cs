using GroveGames.DeveloperTools;

namespace GroveGames.Haptics.Unity.DeveloperTools
{
    public sealed class HapticsDeveloperOptions : IDeveloperOptions
    {
        private readonly IHapticPlayer _player;

        [DeveloperOption(Header = "Test", Min = 0f, Max = 1f)] private float _intensity;
        [DeveloperOption(Min = 0f, Max = 1f)] private float _sharpness;
        [DeveloperOption(Min = 0.01f, Max = 1f)] private float _duration;

        public string Category => "Haptics";

        public HapticsDeveloperOptions(IHapticPlayer player)
        {
            _player = player;
            _intensity = 1f;
            _sharpness = 0.5f;
            _duration = 0.1f;
        }

        [DeveloperOption]
        private void PlayHaptic()
        {
            _player.Play(_intensity, _sharpness, _duration);
        }
    }
}
