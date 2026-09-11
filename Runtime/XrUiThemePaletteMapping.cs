using Deucarian.Theming;
using Deucarian.XRUI.Controls;
using UnityEngine;

namespace Deucarian.XRUI.ThemingIntegration
{
    internal static class XrUiThemePaletteMapping
    {
        internal static void Apply(DeucarianTheme theme, XrUiColorPalette source, XrUiColorPalette destination, bool useMultipliers)
        {
            destination.UseInteractionStateMultipliers = useMultipliers && source.UseInteractionStateMultipliers;
            destination.NormalMultiplier = source.NormalMultiplier;
            destination.HighlightedMultiplier = source.HighlightedMultiplier;
            destination.PressedMultiplier = source.PressedMultiplier;
            destination.SelectedMultiplier = source.SelectedMultiplier;
            destination.DisabledMultiplier = source.DisabledMultiplier;
            destination.Primary = Resolve(theme, DeucarianBuiltinColorRoleIds.Primary, source.Primary);
            destination.Secondary = Resolve(theme, DeucarianBuiltinColorRoleIds.Secondary, source.Secondary);
            destination.Success = Resolve(theme, DeucarianBuiltinColorRoleIds.Success, source.Success);
            destination.Danger = Resolve(theme, DeucarianBuiltinColorRoleIds.Error, source.Danger);
            destination.Warning = Resolve(theme, DeucarianBuiltinColorRoleIds.Warning, source.Warning);
            destination.Info = Resolve(theme, DeucarianBuiltinColorRoleIds.Info, source.Info);
            destination.Background = Resolve(theme, DeucarianBuiltinColorRoleIds.Surface, source.Background);
            destination.Disabled = Resolve(theme, DeucarianBuiltinColorRoleIds.UiDisabled, source.Disabled);
            destination.BodyText = Resolve(theme, DeucarianBuiltinColorRoleIds.TextPrimary, source.BodyText);
            destination.SmallText = Resolve(theme, DeucarianBuiltinColorRoleIds.TextSecondary, source.SmallText);
            destination.MutedText = Resolve(theme, DeucarianBuiltinColorRoleIds.TextMuted, source.MutedText);
            destination.SocketGhost = Resolve(theme, DeucarianControlColorRoleIds.SocketGhost,
                Resolve(theme, "deucarian.text.primary", source.SocketGhost));
            destination.TitleText = Resolve(theme, DeucarianControlColorRoleIds.TitleText,
                Resolve(theme, "deucarian.primary", source.TitleText));
            destination.InputText = Resolve(theme, DeucarianControlColorRoleIds.InputText,
                Resolve(theme, "deucarian.text.primary", source.InputText));
            destination.PlaceholderText = Resolve(theme, DeucarianControlColorRoleIds.PlaceholderText,
                Resolve(theme, "deucarian.text.disabled", source.PlaceholderText));
            destination.Icon = Resolve(theme, DeucarianControlColorRoleIds.Icon,
                Resolve(theme, "deucarian.text.primary", source.Icon));
            destination.Image = Resolve(theme, DeucarianControlColorRoleIds.Image,
                Resolve(theme, "deucarian.text.primary", source.Image));
            destination.ImageMuted = Resolve(theme, DeucarianControlColorRoleIds.ImageMuted,
                Resolve(theme, "deucarian.text.muted", source.ImageMuted));
            destination.ImageSubtle = Resolve(theme, DeucarianControlColorRoleIds.ImageSubtle,
                Resolve(theme, "deucarian.surface.raised", source.ImageSubtle));
            destination.SliderTrack = Resolve(theme, DeucarianControlColorRoleIds.SliderTrack,
                Resolve(theme, "deucarian.secondary", source.SliderTrack));
            destination.Outline = Resolve(theme, DeucarianControlColorRoleIds.Outline,
                Resolve(theme, "deucarian.text.secondary", source.Outline));
            destination.ErrorText = Resolve(theme, DeucarianControlColorRoleIds.ErrorText,
                Resolve(theme, "deucarian.error", source.ErrorText));
            destination.KeyboardAccent = Resolve(theme, DeucarianControlColorRoleIds.KeyboardAccent,
                Resolve(theme, "deucarian.accent", source.KeyboardAccent));
            destination.KeyboardBackground = Resolve(theme, DeucarianControlColorRoleIds.KeyboardBackground,
                Resolve(theme, "deucarian.surface.raised", source.KeyboardBackground));
            destination.KeyboardOutline = Resolve(theme, DeucarianControlColorRoleIds.KeyboardOutline,
                Resolve(theme, "deucarian.primary", source.KeyboardOutline));
            destination.KeyboardInputText = Resolve(theme, DeucarianControlColorRoleIds.KeyboardInputText,
                Resolve(theme, "deucarian.text.primary", source.KeyboardInputText));
            destination.ControlSubtleBackground = Resolve(theme, DeucarianControlColorRoleIds.ControlSubtleBackground,
                Resolve(theme, "deucarian.surface", source.ControlSubtleBackground));
            destination.ControlDarkBorder = Resolve(theme, DeucarianControlColorRoleIds.ControlDarkBorder,
                Resolve(theme, "deucarian.secondary", source.ControlDarkBorder));
            destination.SliderHandle = Resolve(theme, DeucarianControlColorRoleIds.SliderHandle,
                Resolve(theme, "deucarian.primary", source.SliderHandle));
            destination.LoadingIndicator = Resolve(theme, DeucarianControlColorRoleIds.LoadingIndicator,
                Resolve(theme, "deucarian.accent", source.LoadingIndicator));
            destination.DropdownInvalidState = Resolve(theme, DeucarianControlColorRoleIds.DropdownInvalidState,
                Resolve(theme, "deucarian.error", source.DropdownInvalidState));
            destination.KeyboardContentAccent = Resolve(theme, DeucarianControlColorRoleIds.KeyboardContentAccent,
                Resolve(theme, "deucarian.accent", source.KeyboardContentAccent));
            destination.Transparent = Resolve(theme, DeucarianControlColorRoleIds.Transparent,
                Resolve(theme, "deucarian.ui.normal", source.Transparent));
            destination.SetResolvedInteractionColors(
                Resolve(theme, DeucarianBuiltinColorRoleIds.UiNormal, destination.GetAuthoredInteractionColor(CustomButtonVisualState.Normal)),
                Resolve(theme, DeucarianBuiltinColorRoleIds.UiHighlighted, destination.GetAuthoredInteractionColor(CustomButtonVisualState.Highlighted)),
                Resolve(theme, DeucarianBuiltinColorRoleIds.UiPressed, destination.GetAuthoredInteractionColor(CustomButtonVisualState.Pressed)),
                Resolve(theme, DeucarianBuiltinColorRoleIds.UiSelected, destination.GetAuthoredInteractionColor(CustomButtonVisualState.Selected)),
                Resolve(theme, DeucarianBuiltinColorRoleIds.UiDisabled, destination.GetAuthoredInteractionColor(CustomButtonVisualState.Disabled)));
        }

        private static Color Resolve(DeucarianTheme theme, string roleId, Color fallback) =>
            theme.TryGetColorById(roleId, out var value) ? value : fallback;
    }
}
