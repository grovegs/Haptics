## Unity

- `IHapticPlayer`, `HapticPlayer` and `Haptic` are in the `GroveGames.Haptics` namespace. `GroveGames.Haptics.Unity` holds `UnityHaptic` and `UnityHapticDevice`.
- A settings class declares `[SerializeField] private UnityHaptic _xHaptic = new(0.3f, 0.5f, 0.03f);` and exposes `public UnityHaptic XHaptic => _xHaptic;`. Never serialize the core `Haptic`; Unity does not save it.
- Register with `builder.AddHapticPlayer()` in the root installer.
- Haptics do nothing in the Editor. Test them on a device.
- An asmdef that overrides references lists `GroveGames.Haptics.dll` in its precompiled references.
- On iOS, iPhone 7 and 7 Plus report `IsSupported` as `false` but still play a basic impact, so a hidden toggle there cannot turn haptics off.
