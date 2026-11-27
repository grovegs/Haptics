---
name: grovegames-haptics
description: Rules for haptic feedback in game code with GroveGames.Haptics. Use when writing or reviewing code that plays a haptic, adds a haptic to a settings class, or switches haptics on or off for the player.
---

# Using GroveGames.Haptics

Rules for game code that uses this package. They hold in every game. The API is in the package's `README.md`.

- Take `IHapticPlayer` through the constructor. Never call a platform vibration API.
- A haptic is a tunable: it lives in the feature's settings, not as numbers in a controller. Play it with `_haptics.Play(_settings.XHaptic)`.
- Play a haptic from the controller, at the moment the player sees the result: a click, an upgrade, a reward landing. Never from a service, and never per frame or per hit.
- The player's haptics preference is `SetEnabled`. Do not check the preference yourself before every `Play`.
- Do not check `IsSupported` before `Play`; an unsupported device does nothing. Use it only to hide the haptics toggle.
- You cannot verify how a haptic feels. Say so and ask the user to check it on a device.

## Unity

- `IHapticPlayer`, `HapticPlayer` and `Haptic` are in the `GroveGames.Haptics` namespace. `GroveGames.Haptics.Unity` holds `UnityHaptic` and `UnityHapticDevice`.
- A settings class declares `[SerializeField] private UnityHaptic _xHaptic = new(0.3f, 0.5f, 0.03f);` and exposes `public UnityHaptic XHaptic => _xHaptic;`. Never serialize the core `Haptic`; Unity does not save it.
- Register with `builder.AddHapticPlayer()` in the root installer.
- Haptics do nothing in the Editor. Test them on a device.
- An asmdef that overrides references lists `GroveGames.Haptics.dll` in its precompiled references.
- On iOS, iPhone 7 and 7 Plus report `IsSupported` as `false` but still play a basic impact, so a hidden toggle there cannot turn haptics off.
