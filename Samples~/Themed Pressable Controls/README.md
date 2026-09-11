# Themed Pressable Controls

Open **ThemedPressableControls.unity** and press Play. Hold Light theme / Dark
theme briefly to switch the bundled Deucarian family through real XR controls.
The scene includes its camera, world-space canvas, provider, local palette scope,
bridge and serialized events. Select **Example canvas** to inspect the wiring.

The bridge targets this local scope, not a global runtime palette. Visual styling
must be enabled in Theming > Project setup. The scene uses the built-in input
module; use InputSystemUIInputModule for Input-System-only projects.

1. Install `com.deucarian.xr-ui`.
2. Install `com.deucarian.theming`.
3. Install this integration package.
4. Add `XrUiThemePaletteIntegration` below a `DeucarianThemeProvider`.

The integration maps built-in Deucarian UI/text/status roles to the XR UI palette and leaves missing role IDs on the core XR UI fallback colors.
