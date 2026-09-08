using System.Collections;
using Deucarian.Theming;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Deucarian.XRUI.ThemingIntegration.PlayModeTests
{
    public sealed class XrUiThemePaletteLifetimeTests
    {
        [UnityTest]
        public IEnumerator DisablingOneBridgePreservesTheOtherAndDestroysOnlyItsTransientPalette()
        {
            var theme = ScriptableObject.CreateInstance<DeucarianTheme>();
            var firstObject = new GameObject("First bridge");
            var secondObject = new GameObject("Second bridge");
            firstObject.SetActive(false);
            secondObject.SetActive(false);
            try
            {
                var first = firstObject.AddComponent<XrUiThemePaletteIntegration>();
                var second = secondObject.AddComponent<XrUiThemePaletteIntegration>();
                first.ThemeOverride = theme;
                second.ThemeOverride = theme;
                firstObject.SetActive(true);
                first.ApplyTheme();
                var firstPalette = XrUiColorPalette.Global;
                secondObject.SetActive(true);
                second.ApplyTheme();
                var secondPalette = XrUiColorPalette.Global;
                Assert.That(firstPalette, Is.Not.SameAs(secondPalette));
                firstObject.SetActive(false);
                yield return null;
                Assert.That(XrUiColorPalette.Global, Is.SameAs(secondPalette));
                Assert.That(firstPalette == null, Is.True, "The bridge's transient palette must be released.");
                Assert.That(secondPalette != null, Is.True);
                secondObject.SetActive(false);
                yield return null;
                Assert.That(secondPalette == null, Is.True);
                Assert.That(theme != null, Is.True, "Supplied theme assets are never owned by the bridge.");
            }
            finally
            {
                Object.DestroyImmediate(firstObject);
                Object.DestroyImmediate(secondObject);
                Object.DestroyImmediate(theme);
            }
        }

    }
}
