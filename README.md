# GroveGames.Haptics

[![Build Status](https://github.com/grovegs/Haptics/actions/workflows/release.yml/badge.svg)](https://github.com/grovegs/Haptics/actions/workflows/release.yml)
[![Tests](https://github.com/grovegs/Haptics/actions/workflows/tests.yml/badge.svg)](https://github.com/grovegs/Haptics/actions/workflows/tests.yml)
[![NuGet](https://img.shields.io/nuget/v/GroveGames.Haptics)](https://www.nuget.org/packages/GroveGames.Haptics)
[![Latest Release](https://img.shields.io/github/v/release/grovegs/Haptics)](https://github.com/grovegs/Haptics/releases/latest)

Haptic feedback with intensity, sharpness and duration for .NET and Unity, with iOS and Android devices

## Features

- `IHapticPlayer` and `HapticPlayer`: play a haptic, switch all haptics off, and ask whether the device supports them
- `Haptic`: intensity, sharpness and duration in one value
- `IHapticDevice`: the one thing an engine or a platform implements
- Unity: `UnityHapticDevice` for iOS (Core Haptics) and Android (vibrator), and `UnityHaptic`, a `Haptic` you can edit in the Inspector
- Test doubles for your own tests: `FakeHapticPlayer` in the editor-only `GroveGames.Haptics.Unity.Testing` assembly

## .NET

### Installation

```bash
dotnet add package GroveGames.Haptics
```

### Usage

```csharp
using GroveGames.Haptics;

IHapticPlayer player = new HapticPlayer(device);

player.Play(new Haptic(0.3f, 0.5f, 0.03f));
player.Play(0.8f, 0.8f, 0.15f);
player.SetEnabled(false);
```

- Intensity and sharpness are clamped to 0 to 1, and duration is in seconds, up to 30. A `NaN` intensity plays nothing; a `NaN` sharpness or duration counts as 0.
- Nothing plays while the player is switched off, for an intensity of zero or less, or for a `Haptic` that is not enabled. `default(Haptic)` is not enabled.

## Unity

### Installation

1. Install the core package via [NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity):
   - Open Window > NuGet > Manage NuGet Packages
   - Search for `GroveGames.Haptics` and install

2. Add the Unity package via Git URL:
   - Open Package Manager (Window > Package Manager)
   - Click "+" > "Add package from git URL"
   - Enter: `https://github.com/grovegs/Haptics.git?path=src/GroveGames.Haptics.Unity/Packages/com.grovegames.haptics`

3. Create a `csc.rsp` file in your `Assets/` directory with the following content to enable C# 10 features:

   ```text
   -langversion:10
   -nullable:enable
   ```

### Usage

With [GroveGames.DependencyInjection](https://github.com/grovegs/DependencyInjection) installed, `AddHapticPlayer()` registers `IHapticPlayer` with the device of the current platform. Without it, create the player yourself:

```csharp
using GroveGames.Haptics;
using GroveGames.Haptics.Unity;

IHapticPlayer player = new HapticPlayer(new UnityHapticDevice());
```

Keep a haptic in a settings asset with `UnityHaptic`. It converts to `Haptic` by itself:

```csharp
[SerializeField] private UnityHaptic _clickHaptic = new(0.15f, 0.4f, 0.02f);

player.Play(_clickHaptic);
```

In the Editor and on other platforms the device does nothing and `IsSupported` is `false`. The Android build adds the `VIBRATE` permission to the manifest.

- **iOS**: `IsSupported` means Core Haptics, which supports intensity, sharpness and duration. iPhone 7 and 7 Plus have no Core Haptics but still play a basic impact at the given intensity; older iPhones and iPads play nothing. The haptic engine shuts down when idle and restarts on the next `Play`.
- **Android**: sharpness is ignored, and intensity sets the amplitude when the vibrator supports it. Up to 32 vibration effects are kept for reuse. If the vibrator cannot be set up, the error is logged and the device is unsupported. If playing throws, the first error is logged and nothing reaches game code.

## Development

### Project Structure

- `src/GroveGames.Haptics/` - Core .NET library (`net10.0`, `netstandard2.1`)
- `src/GroveGames.Haptics.Unity/` - Unity package
- `tests/` - xUnit tests
- `sandbox/` - Sample applications for testing

### Commands

```bash
dotnet build                        # Build all projects
dotnet format                       # Format code
dotnet test                         # Run tests
dotnet pack -c Release              # Create NuGet packages
```

### Requirements

- .NET 10.0 SDK

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
