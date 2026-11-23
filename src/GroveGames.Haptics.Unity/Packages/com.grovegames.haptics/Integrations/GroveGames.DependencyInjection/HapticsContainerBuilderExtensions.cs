using GroveGames.DependencyInjection;

namespace GroveGames.Haptics.Unity
{
    public static class HapticsContainerBuilderExtensions
    {
        public static IContainerBuilder AddHapticPlayer(this IContainerBuilder builder)
        {
            return builder.AddSingleton<IHapticPlayer>(_ => new HapticPlayer(new UnityHapticDevice()));
        }
    }
}
