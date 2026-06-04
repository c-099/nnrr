# nnrr - nine.nines recoil reducer/reproducer

nnrr is a recoil reducer or recoil reproducer utility for Windows. It lets you tune horizontal and vertical recoil with optional staged recoil changes and pattern repeating.

You can use this to reduce the recoil in a shooting game, or use the values in reverse for aim training. This utility uses some randomness to reduce automatic detection of its use. This program does not attempt to hide itself from anti-cheat in any way so use with caution.

## Features

- Primary recoil controls for horizontal, vertical, and delay values.
- Stage editor for timed recoil changes.
- Repeat markers that loop after the current stage runs.
- Configurable activation key, defaulting to `Insert`.
- Optional right-click activation requirement.
- Saved profile support via `saveprofiles.txt`.
- Automatic last-used profile restore via `settings.txt`.

## Activation

By default, activation is enabled at startup. Hold right click and press left click outside the app window to run recoil. Press the configured activation key to toggle activation on or off.

The activation key can be changed in Settings. Supported keys are `Insert`, `Home`, `End`, `Page Up`, `Page Down`, `F8`, `F9`, `F10`, `F11`, and `F12`.

## Save Data

The app stores local data beside the executable:

- `saveprofiles.txt` stores manually saved profiles.
- `settings.txt` stores global settings and the last-used temporary profile.

## Build

Requirements:

- Windows
- .NET 8 SDK

Build from the repository root:

```powershell
dotnet build nnrr.sln
```

Run the app from Visual Studio or launch the built executable from:

```text
nnrr\bin\Debug\net8.0-windows\nnrr.exe
```

## Credits
https://github.com/rvknth043/Global-Low-Level-Key-Board-And-Mouse-Hook

https://github.com/J-Yaghoubi/Anti-Recoil
