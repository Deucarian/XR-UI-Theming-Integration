# One visual palette owner

Author application colors in **Deucarian Theming**, not in an XR-specific copy.
Theming includes Simultria Design & Sales and Realisation & Progress presets.

Attach `XrUiThemePaletteIntegration` to the UI root and explicitly assign a local
`XrUiPaletteScope`. The bridge reads the closest theme provider, or the configured
project theme. Controls beneath that scope read its resolved palette. A null
scope remains the application-wide compatibility destination for old scenes.

The optional target palette is a read-only fallback input. Theme colors are
written to a bridge-owned transient palette, never to the authored asset.
UI Normal, Highlighted, Pressed, Selected and Disabled map independently and
are not multiplied again. Fine-grained control roles override standard semantic
fallbacks when a palette supplies them.

Turning project Visual styling off releases only this bridge's registration.
Disabling or destroying one bridge never clears another bridge's palette.
Re-enabling styling reapplies the theme without changing the authored fallback.

XR UI remains usable without Theming. Keeping this integration separate avoids
forcing visual/audio theming dependencies into applications that only need XR
controls, and preserves existing serialized assets and scene references.
