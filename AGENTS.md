# Deucarian XR UI Theming Integration Agent Notes

Package ID: `com.deucarian.xr-ui.theming-integration`
Repository: `Deucarian/XR-UI-Theming-Integration`

Follow the canonical Deucarian governance docs in [Package Registry](https://github.com/Deucarian/Package-Registry/blob/main/ARCHITECTURE.md), especially capability ownership and dependency rules.

## Ownership

This package owns:

- The optional bridge that maps Deucarian Theming color roles into Deucarian XR UI runtime palettes.

Registered capabilities:
- None. This package is an integration package between `com.deucarian.xr-ui` and `com.deucarian.theming`.

This package must not own:

- XR UI controls, Theming color role governance, generic UI framework behavior, diagnostics dashboards, or package installation.

## Dependencies

Allowed dependency shape:

- Must depend on XR UI and Theming because this integration composes those package-owned capabilities.

Required dependencies and why:

- `com.deucarian.xr-ui`: target XR UI palette model and runtime palette API.
- `com.deucarian.theming`: source theme provider, theme target base behavior, and built-in color role IDs.

Optional/version-defined dependencies:

- None.

Architecture exceptions:

- None.

## Policies

- Logging: Do not add diagnostics/logging unless package behavior actually needs it; if needed, use Deucarian Logging and update all metadata together.
- Common: Do not add Common unless production code directly uses approved Common-owned cleanup/runtime primitives.
- Editor UI: No editor shell or shared editor UI ownership.
- Diagnostics: Do not become Diagnostics; this package should stay a narrow integration.
- Testing: Test fixture teardown may use `DestroyImmediate` directly.

## Validation

Run the shared validator before committing:

```powershell
python C:/Repositories/Package-Registry/Tools/deucarian_package_validator.py --registry-root C:/Repositories/Package-Registry --repository-root . --config deucarian-package.json
```

Also run existing repository tests when changing code or asmdefs. Documentation-only updates should still run `git diff --check`.

## Codex Guidance

- Inspect current files before changing anything.
- Work on `develop`; do not edit or merge `main` unless the task is promotion-only.
- Do not edit `Library/PackageCache`.
- Do not guess package versions or dependency versions.
- Do not add package dependencies casually; update asmdefs, `package.json`, `deucarian-package.json`, Package Registry, Package Installer fallback catalog, and Bootstrap fallback catalog together when a dependency is truly required.
- Do not create local copies of shared helpers.
- Keep commits focused and report exactly what changed and what was validated.

## Before Adding Code

- Confirm the change fits this package's integration boundary.
- Reuse existing local patterns and helpers.
- Avoid broad refactors without audit support.
- Preserve runtime behavior unless the task explicitly asks to change it.

## Before Adding A Dependency

- Is the capability already owned by that package?
- Is it used by production code, editor code, sample code, or tests?
- Does the asmdef reference match `package.json`?
- Does `deucarian-package.json` need updating?
- Does Package Registry need updating?
- Does Package Installer fallback catalog need updating?
- Does Bootstrap fallback catalog need updating?
- Are exact versions propagated without guessing?

## Before Adding A Helper

- Is this package the capability owner?
- Is this behavior repeated in at least three production packages?
- Is there an existing owner package?
- Should this remain local?
- Has the audit been updated?

## Debug And Unity Object Lifetime

- Direct Unity Debug calls are forbidden in production code.
- This integration currently does not own production Unity object cleanup.
- Test fixture teardown may use `DestroyImmediate` directly.
