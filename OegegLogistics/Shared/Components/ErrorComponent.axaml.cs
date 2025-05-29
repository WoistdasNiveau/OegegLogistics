using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace OegegLogistics.Shared.Components;

public class ErrorComponent : TemplatedControl
{
    public static readonly StyledProperty<string> ErrorTextProperty = AvaloniaProperty.Register<ErrorComponent, string>(
        nameof(ErrorText));

    public string ErrorText
    {
        get => GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public static readonly StyledProperty<string> ContinueButtonTextProperty = AvaloniaProperty.Register<ErrorComponent, string>(
        nameof(ContinueButtonText),
        defaultValue: "Continue");

    public string ContinueButtonText
    {
        get => GetValue(ContinueButtonTextProperty);
        set => SetValue(ContinueButtonTextProperty, value);
    }

}