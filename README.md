# Deucarian XR UI Theming Integration

## What this is

Deucarian XR UI Theming Integration is an optional package for projects that use both `com.deucarian.xr-ui` and `com.deucarian.theming`. It maps Deucarian Theming color roles into an XR UI runtime palette while keeping the XR UI core package fully functional on its own.

Package ID: `com.deucarian.xr-ui.theming-integration`

Current package version: `0.3.1` (Theming `1.10.0`, XR UI `0.4.0`).

## When to use it

- You use Deucarian XR UI and want project themes to drive XR control colors.
- You already have a `DeucarianThemeProvider` in the scene or app shell.
- You want missing theme roles to fall back to the neutral XR UI palette instead of hard failing.

## When not to use it

- You only need neutral XR UI controls; install `com.deucarian.xr-ui` alone.
- You need to define new theme roles or theme governance; that belongs to `com.deucarian.theming`.
- You need custom app-specific XR interaction flows; keep those in the consuming project.

## Install

Stable:

```json
"com.deucarian.xr-ui.theming-integration": "https://github.com/Deucarian/XR-UI-Theming-Integration.git#main"
```

Development:

```json
"com.deucarian.xr-ui.theming-integration": "https://github.com/Deucarian/XR-UI-Theming-Integration.git#develop"
```

## Unity compatibility

Requires Unity `2022.3` or newer.

## 60-second quick start

1. Install `com.deucarian.xr-ui`.
2. Install `com.deucarian.theming`.
3. Install this integration package.
4. Add `XrUiThemePaletteIntegration` beneath a `DeucarianThemeProvider`, and explicitly assign an `XrUiPaletteScope` at the UI root.
5. Leave `Apply As Runtime Palette` enabled. Enable Visual styling in Theming Project setup and choose the project family (including built-in Simultria DS or RP presets).

The bridge uses a transient output and does not rewrite its target palette.
[Palette ownership and compatibility](Documentation~/PALETTE_OWNERSHIP.md) explains
scopes, feature switches and legacy fallback behavior.

## Samples

- `Themed Pressable Controls`: setup notes for applying Deucarian Theming colors to XR UI controls.

## Public API map

- `XrUiThemePaletteIntegration`: theme target behavior that resolves built-in Deucarian color roles into an `XrUiColorPalette` and optionally installs it as the runtime palette.

## Integrations

Works with:

- `com.deucarian.xr-ui`: target palette and runtime palette API.
- `com.deucarian.theming`: source theme provider, built-in role IDs, and theme target base behavior.

Optional integrations:

- None.

Does not own:

- XR UI controls.
- Theme role definitions or governance.
- Generic UI framework behavior.
- Diagnostics dashboards.

## Troubleshooting

- If colors do not update, confirm a `DeucarianThemeProvider` is active above or near the integration component.
- If some colors stay neutral, check whether the active theme defines the built-in role IDs used by the integration.
- Disabling the component or Visual styling intentionally reveals the previous scope registration or authored fallback. One bridge never clears another bridge's colors.

## Validation

Run the shared package validator:

```powershell
python C:/Repositories/Package-Registry/Tools/deucarian_package_validator.py --registry-root C:/Repositories/Package-Registry --repository-root . --config deucarian-package.json
```

Run Unity EditMode tests when changing runtime code or asmdefs.

## Architecture / Contributor Notes

See [AGENTS.md](AGENTS.md) for ownership, dependency, and validation guidance.

## License

See [LICENSE.md](LICENSE.md).
