using Deucarian.Theming;
using UnityEngine;

namespace Deucarian.XRUI.ThemingIntegration.Samples
{
    public sealed class ThemedControlsDemo : MonoBehaviour
    {
        [SerializeField] private DeucarianThemeProvider provider;
        [SerializeField] private DeucarianThemeFamily family;
        private void Start() => provider.SetThemeFamily(family != null ? family : DeucarianVisualDefaults.LoadFamily(), DeucarianThemeMode.Dark);
        public void UseLight() => provider.SetThemeMode(DeucarianThemeMode.Light);
        public void UseDark() => provider.SetThemeMode(DeucarianThemeMode.Dark);
    }
}
