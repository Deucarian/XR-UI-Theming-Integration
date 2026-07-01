using Deucarian.Theming;
using Deucarian.XRUI;
using UnityEngine;

namespace Deucarian.XRUI.ThemingIntegration
{
    public sealed class XrUiThemePaletteIntegration : DeucarianThemeTargetBehaviour
    {
        [SerializeField] private XrUiColorPalette targetPalette;
        [SerializeField] private bool applyAsRuntimePalette = true;
        [SerializeField] private bool useInteractionStateMultipliers = true;

        private XrUiColorPalette _runtimePalette;
        private XrUiColorPalette ResolvedPalette
        {
            get
            {
                if (targetPalette != null)
                {
                    return targetPalette;
                }

                if (_runtimePalette == null)
                {
                    _runtimePalette = ScriptableObject.CreateInstance<XrUiColorPalette>();
                    _runtimePalette.hideFlags = HideFlags.HideAndDontSave;
                }

                return _runtimePalette;
            }
        }

        protected override void ApplyResolvedTheme(DeucarianTheme theme)
        {
            if (theme == null)
            {
                return;
            }

            XrUiColorPalette palette = ResolvedPalette;
            palette.UseInteractionStateMultipliers = useInteractionStateMultipliers;

            Apply(theme, DeucarianBuiltinColorRoleIds.Core.Primary, palette.Primary, value => palette.Primary = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Core.Secondary, palette.Secondary, value => palette.Secondary = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Status.Success, palette.Success, value => palette.Success = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Status.Error, palette.Danger, value => palette.Danger = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Status.Warning, palette.Warning, value => palette.Warning = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Status.Info, palette.Info, value => palette.Info = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.UI.Normal, palette.Background, value => palette.Background = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.UI.Disabled, palette.Disabled, value => palette.Disabled = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.UI.Highlighted, palette.Secondary, value => palette.Secondary = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.UI.Pressed, palette.Primary, value => palette.Primary = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Text.Primary, palette.BodyText, value => palette.BodyText = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Text.Secondary, palette.SmallText, value => palette.SmallText = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Text.Muted, palette.MutedText, value => palette.MutedText = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Text.Disabled, palette.PlaceholderText, value => palette.PlaceholderText = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Core.Accent, palette.KeyboardAccent, value => palette.KeyboardAccent = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Core.Surface, palette.ControlSubtleBackground, value => palette.ControlSubtleBackground = value);
            Apply(theme, DeucarianBuiltinColorRoleIds.Core.SurfaceRaised, palette.KeyboardBackground, value => palette.KeyboardBackground = value);

            palette.NotifyPaletteChanged();

            if (applyAsRuntimePalette)
            {
                XrUiColorPalette.SetRuntimePalette(palette);
            }
        }

        protected override void OnDisable()
        {
            if (applyAsRuntimePalette)
            {
                XrUiColorPalette.ClearRuntimePalette();
            }

            base.OnDisable();
        }

        private static void Apply(DeucarianTheme theme, string roleId, Color fallback, System.Action<Color> assign)
        {
            if (theme != null && theme.TryGetColorById(roleId, out Color color))
            {
                assign(color);
                return;
            }

            assign(fallback);
        }
    }
}
