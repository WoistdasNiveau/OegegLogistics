using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace OegegLogistics.Shared.Components;

public class LabelTextComponent : TemplatedControl
{
    public static readonly StyledProperty<string> LabelTextProperty = AvaloniaProperty.Register<LabelTextComponent, string>(
        nameof(LabelText));

    public string LabelText
    {
        get => GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly StyledProperty<string> TextBoxTextProperty = AvaloniaProperty.Register<LabelTextComponent, string>(
        nameof(TextBoxText));

    public string TextBoxText
    {
        get => GetValue(TextBoxTextProperty);
        set => SetValue(TextBoxTextProperty, value);
    }
}