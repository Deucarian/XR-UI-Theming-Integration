using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Deucarian.Theming;
using Deucarian.XRUI.Controls;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Deucarian.XRUI.ThemingIntegration.Tests
{
    public sealed class XrUiThemeOwnershipTests
    {
        private readonly List<Object> objects = new List<Object>();
        [TearDown] public void Cleanup()
        {
            for (int i = objects.Count - 1; i >= 0; i--) if (objects[i] != null) Object.DestroyImmediate(objects[i]);
            objects.Clear();
        }

        [UnityTest]
        public IEnumerator ThemeMapsToScopedTransientOutputWithoutMutatingAuthoredPalette()
        {
            yield return new EnterPlayMode();
            try
            {
                var fallback = Asset<XrUiColorPalette>();
                fallback.Primary = Color.magenta;
                var context = new XrUiPaletteContext(fallback);
                var theme = Theme();
                var integration = Bridge(theme, fallback, context);
                Assert.That(context.Current, Is.Not.SameAs(fallback));
                Assert.That(fallback.Primary, Is.EqualTo(Color.magenta));
                Assert.That(context.Current.Primary, Is.EqualTo(Color.red));
                Assert.That(context.Current.TitleText, Is.EqualTo(Color.cyan));
                Assert.That(context.Current.GetInteractionColor(CustomButtonVisualState.Pressed), Is.EqualTo(Color.green));
                Assert.That(context.Current.GetInteractionColor(CustomButtonVisualState.Selected), Is.EqualTo(Color.blue));
                integration.enabled = false;
                Assert.That(context.Current, Is.SameAs(fallback));
                Assert.That(fallback.Primary, Is.EqualTo(Color.magenta));
            }
            finally { Cleanup(); }
            yield return new ExitPlayMode();
        }

        [UnityTest]
        public IEnumerator ReleasingOneBridgeVisualOverridePreservesTheOtherAndCanReapply()
        {
            yield return new EnterPlayMode();
            try
            {
                var fallback = Asset<XrUiColorPalette>();
                var context = new XrUiPaletteContext(fallback);
                var older = Bridge(Theme(), fallback, context);
                var olderOutput = context.Current;
                var newer = Bridge(Theme(), fallback, context);
                Assert.That(context.Current, Is.Not.SameAs(olderOutput));
                typeof(XrUiThemePaletteIntegration).GetMethod("OnVisualStylingDisabled", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(newer, null);
                Assert.That(context.Current, Is.SameAs(olderOutput));
                newer.ApplyTheme();
                Assert.That(context.Current, Is.Not.SameAs(olderOutput));
                older.enabled = false;
                Assert.That(context.Current, Is.Not.SameAs(fallback));
                newer.enabled = false;
                Assert.That(context.Current, Is.SameAs(fallback));
            }
            finally { Cleanup(); }
            yield return new ExitPlayMode();
        }

        private XrUiThemePaletteIntegration Bridge(DeucarianTheme theme, XrUiColorPalette source, XrUiPaletteContext context)
        {
            var go = new GameObject("Theme bridge test"); objects.Add(go); go.SetActive(false);
            var bridge = go.AddComponent<XrUiThemePaletteIntegration>();
            var serialized = new SerializedObject(bridge);
            serialized.FindProperty("targetPalette").objectReferenceValue = source;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            bridge.ThemeOverride = theme;
            bridge.SetPaletteContext(context);
            go.SetActive(true);
            return bridge;
        }

        private DeucarianTheme Theme()
        {
            var palette = Asset<DeucarianColorPalette>();
            Add(palette, DeucarianBuiltinColorRoleIds.Primary, Color.red);
            Add(palette, DeucarianBuiltinColorRoleIds.UiPressed, Color.green);
            Add(palette, DeucarianBuiltinColorRoleIds.UiSelected, Color.blue);
            Add(palette, DeucarianControlColorRoleIds.TitleText, Color.cyan);
            var theme = Asset<DeucarianTheme>(); theme.Configure("test.theme", "Test", palette); return theme;
        }
        private void Add(DeucarianColorPalette palette, string id, Color color)
        {
            var role = Asset<DeucarianColorRole>(); role.Configure(id, id, "", "", color, false); palette.SetColor(role, color);
        }
        private T Asset<T>() where T : ScriptableObject { var value = ScriptableObject.CreateInstance<T>(); objects.Add(value); return value; }
    }
}
