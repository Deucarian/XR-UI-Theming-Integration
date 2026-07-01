using NUnit.Framework;
using UnityEngine;

namespace Deucarian.XRUI.ThemingIntegration.Tests
{
    public sealed class XrUiThemePaletteIntegrationTests
    {
        [Test]
        public void IntegrationComponentCanBeAddedWithoutCoreThemeOnObject()
        {
            var gameObject = new GameObject("XR UI Theme Integration");
            try
            {
                XrUiThemePaletteIntegration integration = gameObject.AddComponent<XrUiThemePaletteIntegration>();

                Assert.IsNotNull(integration);
            }
            finally
            {
                Object.DestroyImmediate(gameObject);
            }
        }
    }
}
