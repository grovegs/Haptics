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
- Do not check `IsSupported` before `Play`; an unsupported device does nothing. Use it only to hide the haptics toggle, and remember that older iPhones report `false` but still play a basic impact.
- You cannot verify how a haptic feels. Say so and ask the user to check it on a device.
