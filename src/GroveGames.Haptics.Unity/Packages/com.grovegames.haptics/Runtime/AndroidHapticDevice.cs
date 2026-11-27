#if UNITY_ANDROID && !UNITY_EDITOR
using System;
using System.Collections.Generic;

using UnityEngine;

namespace GroveGames.Haptics.Unity
{
    internal sealed class AndroidHapticDevice : IHapticDevice
    {
        private const int VibrationEffectApiLevel = 26;
        private const int VibratorManagerApiLevel = 31;
        private const int DefaultAmplitude = -1;
        private const int MaxAmplitude = 255;
        private const int MaxCachedEffects = 32;

        private readonly AndroidJavaObject _vibrator;
        private readonly AndroidJavaClass _vibrationEffect;
        private readonly Dictionary<long, AndroidJavaObject> _effects;
        private readonly bool _hasAmplitudeControl;
        private bool _isSupported;
        private bool _hasLoggedPlayError;

        public bool IsSupported => _isSupported;

        public AndroidHapticDevice()
        {
            _effects = new Dictionary<long, AndroidJavaObject>();

            try
            {
                using var version = new AndroidJavaClass("android.os.Build$VERSION");
                var apiLevel = version.GetStatic<int>("SDK_INT");

                using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                using var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                if (apiLevel >= VibratorManagerApiLevel)
                {
                    using var vibratorManager = activity.Call<AndroidJavaObject>("getSystemService", "vibrator_manager");
                    _vibrator = vibratorManager?.Call<AndroidJavaObject>("getDefaultVibrator");
                }
                else
                {
                    _vibrator = activity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                }

                _isSupported = _vibrator != null && _vibrator.Call<bool>("hasVibrator");

                if (_isSupported && apiLevel >= VibrationEffectApiLevel)
                {
                    _vibrationEffect = new AndroidJavaClass("android.os.VibrationEffect");
                    _hasAmplitudeControl = _vibrator.Call<bool>("hasAmplitudeControl");
                }
            }
            catch (Exception exception)
            {
                _isSupported = false;
                Debug.LogException(exception);
            }
        }

        public void Play(float intensity, float sharpness, float duration)
        {
            if (!_isSupported)
            {
                return;
            }

            var milliseconds = duration > 0f ? (long)Mathf.Max(1f, Mathf.Round(duration * 1000f)) : 1L;

            try
            {
                if (_vibrationEffect == null)
                {
                    _vibrator.Call("vibrate", milliseconds);
                    return;
                }

                var amplitude = _hasAmplitudeControl ? Mathf.Clamp(Mathf.RoundToInt(intensity * MaxAmplitude), 1, MaxAmplitude) : DefaultAmplitude;
                _vibrator.Call("vibrate", GetEffect(milliseconds, amplitude));
            }
            catch (Exception exception)
            {
                if (!_hasLoggedPlayError)
                {
                    _hasLoggedPlayError = true;
                    Debug.LogException(exception);
                }
            }
        }

        private AndroidJavaObject GetEffect(long milliseconds, int amplitude)
        {
            var key = (milliseconds << 16) | (uint)(amplitude & 0xFFFF);

            if (!_effects.TryGetValue(key, out var effect))
            {
                if (_effects.Count >= MaxCachedEffects)
                {
                    foreach (var cached in _effects.Values)
                    {
                        cached.Dispose();
                    }

                    _effects.Clear();
                }

                effect = _vibrationEffect.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, amplitude);
                _effects.Add(key, effect);
            }

            return effect;
        }
    }
}
#endif
