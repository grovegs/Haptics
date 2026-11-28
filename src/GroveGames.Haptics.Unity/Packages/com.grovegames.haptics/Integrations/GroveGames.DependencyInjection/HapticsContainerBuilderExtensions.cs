using GroveGames.DependencyInjection;
#if GROVEGAMES_DEVELOPERTOOLS
using GroveGames.DeveloperTools.Unity;
using GroveGames.Haptics.Unity.DeveloperTools;
#endif

namespace GroveGames.Haptics.Unity
{
    public static class HapticsContainerBuilderExtensions
    {
        public static IContainerBuilder AddHapticPlayer(this IContainerBuilder builder)
        {
#if GROVEGAMES_DEVELOPERTOOLS
            builder.AddDeveloperOptions<HapticsDeveloperOptions>();
#endif
            return builder.AddSingleton<IHapticPlayer>(_ => new HapticPlayer(new UnityHapticDevice()));
        }
    }
}
