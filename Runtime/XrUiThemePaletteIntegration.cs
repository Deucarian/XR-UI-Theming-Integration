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
        private XrUiColorPalette _defaultPalette;
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
            if (isActiveAndEnabled && applyAsRuntimePalette && palette != null && DeucarianThemeRuntimeResolver.UseVisualStyling) RegisterPalette(palette);
        }
        private XrUiColorPalette ResolvedPalette
        {
            get
            {
                if (_runtimePalette == null)
                {
                    _runtimePalette = ScriptableObject.CreateInstance<XrUiColorPalette>();
                    _runtimePalette.hideFlags = HideFlags.HideAndDontSave;
                }

                return _runtimePalette;
            }
        }

        private XrUiColorPalette SourcePalette
        {
            get
            {
                if (targetPalette != null) return targetPalette;
                if (_defaultPalette == null)
                {
                    _defaultPalette = ScriptableObject.CreateInstance<XrUiColorPalette>();
                    _defaultPalette.hideFlags = HideFlags.HideAndDontSave;
                }
                return _defaultPalette;
            }
        }

        protected override void ApplyResolvedTheme(DeucarianTheme theme)
        {
            if (theme == null)
            {
                return;
            }

            XrUiColorPalette palette = ResolvedPalette;
            XrUiThemePaletteMapping.Apply(theme, SourcePalette, palette, useInteractionStateMultipliers);

            if (applyAsRuntimePalette && isActiveAndEnabled) RegisterPalette(palette);
            else ReleaseRegistration();
        }

        protected override void OnDisable()
        {
            ReleaseRegistration();
            base.OnDisable();
            ReleaseOwnedPalettes();
        }

        protected override void OnDestroy()
        {
            ReleaseRegistration();
            ReleaseOwnedPalettes();
            base.OnDestroy();
        }

        private void ReleaseOwnedPalettes()
        {
            UnityObjectUtility.DestroySafely(_runtimePalette);
            _runtimePalette = null;
            UnityObjectUtility.DestroySafely(_defaultPalette);
            _defaultPalette = null;
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

        protected override void OnVisualStylingDisabled() => ReleaseRegistration();
    }
}
