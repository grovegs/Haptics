using System;

using UnityEngine;

namespace GroveGames.Haptics.Unity
{
    [Serializable]
    public struct UnityHaptic
    {
        [SerializeField] private bool _enabled;
        [SerializeField, Range(0f, 1f)] private float _intensity;
        [SerializeField, Range(0f, 1f)] private float _sharpness;
        [SerializeField, Range(0f, 1f)] private float _duration;

        public readonly bool Enabled => _enabled;
        public readonly float Intensity => _intensity;
        public readonly float Sharpness => _sharpness;
        public readonly float Duration => _duration;

        public UnityHaptic(float intensity, float sharpness, float duration)
        {
            _enabled = true;
            _intensity = intensity;
            _sharpness = sharpness;
            _duration = duration;
        }

        public static implicit operator Haptic(UnityHaptic haptic)
        {
            return new Haptic(haptic._enabled, haptic._intensity, haptic._sharpness, haptic._duration);
        }
    }
}
