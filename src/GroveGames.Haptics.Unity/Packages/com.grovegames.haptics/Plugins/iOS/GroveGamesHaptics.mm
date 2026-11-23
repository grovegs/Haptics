#import <CoreHaptics/CoreHaptics.h>
#import <UIKit/UIKit.h>

static const float GroveGamesHapticsTransientDuration = 0.05f;

static CHHapticEngine *s_engine = nil;
static BOOL s_engineRunning = NO;
static UIImpactFeedbackGenerator *s_impactGenerator = nil;

static BOOL GroveGamesHapticsSupportsCoreHaptics(void)
{
    static BOOL s_checked = NO;
    static BOOL s_supported = NO;

    if (!s_checked)
    {
        s_supported = CHHapticEngine.capabilitiesForHardware.supportsHaptics;
        s_checked = YES;
    }

    return s_supported;
}

static BOOL GroveGamesHapticsStartEngine(void)
{
    if (s_engine == nil)
    {
        NSError *error = nil;
        s_engine = [[CHHapticEngine alloc] initAndReturnError:&error];

        if (s_engine == nil)
        {
            return NO;
        }

        s_engine.playsHapticsOnly = YES;
        s_engine.stoppedHandler = ^(__unused CHHapticEngineStoppedReason reason) {
            s_engineRunning = NO;
        };
        s_engine.resetHandler = ^{
            s_engineRunning = NO;
        };
    }

    if (!s_engineRunning)
    {
        s_engineRunning = [s_engine startAndReturnError:nil];
    }

    return s_engineRunning;
}

static void GroveGamesHapticsPlayCoreHaptics(float intensity, float sharpness, float duration)
{
    if (!GroveGamesHapticsStartEngine())
    {
        return;
    }

    NSArray<CHHapticEventParameter *> *parameters = @[
        [[CHHapticEventParameter alloc] initWithParameterID:CHHapticEventParameterIDHapticIntensity value:intensity],
        [[CHHapticEventParameter alloc] initWithParameterID:CHHapticEventParameterIDHapticSharpness value:sharpness]
    ];

    CHHapticEvent *event = duration <= GroveGamesHapticsTransientDuration
        ? [[CHHapticEvent alloc] initWithEventType:CHHapticEventTypeHapticTransient parameters:parameters relativeTime:0]
        : [[CHHapticEvent alloc] initWithEventType:CHHapticEventTypeHapticContinuous parameters:parameters relativeTime:0 duration:duration];

    NSError *error = nil;
    CHHapticPattern *pattern = [[CHHapticPattern alloc] initWithEvents:@[event] parameters:@[] error:&error];

    if (pattern == nil)
    {
        return;
    }

    id<CHHapticPatternPlayer> player = [s_engine createPlayerWithPattern:pattern error:&error];

    if (player == nil)
    {
        return;
    }

    if (![player startAtTime:CHHapticTimeImmediate error:&error])
    {
        s_engineRunning = NO;
    }
}

static void GroveGamesHapticsPlayImpact(float intensity)
{
    if (s_impactGenerator == nil)
    {
        s_impactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
    }

    [s_impactGenerator impactOccurredWithIntensity:intensity];
}

extern "C" bool GroveGamesHaptics_IsSupported(void)
{
    return GroveGamesHapticsSupportsCoreHaptics();
}

extern "C" void GroveGamesHaptics_Play(float intensity, float sharpness, float duration)
{
    if (GroveGamesHapticsSupportsCoreHaptics())
    {
        GroveGamesHapticsPlayCoreHaptics(intensity, sharpness, duration);
        return;
    }

    GroveGamesHapticsPlayImpact(intensity);
}
