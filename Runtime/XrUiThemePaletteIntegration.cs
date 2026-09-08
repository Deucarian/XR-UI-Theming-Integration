using Deucarian.Common;
using Deucarian.Theming;
using Deucarian.XRUI;
using UnityEngine;

namespace Deucarian.XRUI.ThemingIntegration
{
    public sealed class XrUiThemePaletteIntegration : DeucarianThemeTargetBehaviour
    {
        [SerializeField] private XrUiColorPalette targetPalette;
        [SerializeField, Tooltip("Optional local destination. Leave empty only for the legacy application-wide palette.")]
        private XrUiPaletteScope paletteScope;
        [SerializeField] private bool applyAsRuntimePalette = true;
        [SerializeField] private bool useInteractionStateMultipliers = true;

        private XrUiColorPalette _runtimePalette;
        private System.IDisposable _paletteRegistration;
        private XrUiColorPalette _registeredPalette;
        private XrUiPaletteContext _paletteContext;
        private XrUiPaletteContext _registeredContext;

        /// <summary>Uses an explicit palette scope. Null preserves the global compatibility destination.</summary>
        public void SetPaletteContext(XrUiPaletteContext context)
        {
            if (ReferenceEquals(_paletteContext, context)) return;
            XrUiColorPalette palette = _registeredPalette;
            ReleaseRegistration();
            _paletteContext = context;
            if (isActiveAndEnabled && applyAsRuntimePalette && palette != null) RegisterPalette(palette);
        }
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

            if (applyAsRuntimePalette && isActiveAndEnabled) RegisterPalette(palette);
            else ReleaseRegistration();
        }

        protected override void OnDisable()
        {
            ReleaseRegistration();
            base.OnDisable();
            UnityObjectUtility.DestroySafely(_runtimePalette);
            _runtimePalette = null;
        }

        protected override void OnDestroy()
        {
            ReleaseRegistration();
            UnityObjectUtility.DestroySafely(_runtimePalette);
            _runtimePalette = null;
            base.OnDestroy();
        }

        private void RegisterPalette(XrUiColorPalette palette)
        {
            XrUiPaletteContext destination = _paletteContext ?? (paletteScope != null ? paletteScope.Context : null);
            if (_registeredPalette == palette && _paletteRegistration != null && ReferenceEquals(destination, _registeredContext)) return;
            ReleaseRegistration();
            _paletteRegistration = destination == null
                ? XrUiColorPalette.RegisterRuntimePalette(palette)
                : destination.Register(palette);
            _registeredPalette = palette;
            _registeredContext = destination;
        }

        private void ReleaseRegistration()
        {
            _paletteRegistration?.Dispose();
            _paletteRegistration = null;
            _registeredPalette = null;
            _registeredContext = null;
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
