using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using LuhnDotNet;

namespace OegegLogistics.Shared.Components;

public class UicNumberControl : TemplatedControl
{
    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<UicNumberControl, string>(
        nameof(Text));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<string> SelfCheckNumberProperty = AvaloniaProperty.Register<UicNumberControl, string>(
        nameof(SelfCheckNumber));

    public string SelfCheckNumber
    {
        get => GetValue(SelfCheckNumberProperty);
        set => SetValue(SelfCheckNumberProperty, value);
    }

    public static readonly StyledProperty<string> UicNumberProperty = AvaloniaProperty.Register<UicNumberControl, string>(
        nameof(UicNumber));

    public string UicNumber
    {
        get
        {
            string number = Text.Replace(" ","").Trim() + SelfCheckNumber.Trim();
            return number;
        }
        set
        {
            if(value.Replace(" ", "").Trim().Length != 12)
                return;

            Text = value.Substring(0, 11);
            SelfCheckNumber = value.Substring(12, 1);
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        MaskedTextBox uicBox = e.NameScope.Find<MaskedTextBox>("MaskedTextBox")!;
        
        uicBox.TextChanged += UicBoxOnTextChanged;
    }

    private void UicBoxOnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if(sender is not MaskedTextBox uicBox)
            return;
        
        string text = uicBox.Text.Replace(" ","")
            .Replace("_","")
            .Trim();

        string checkNumber = text.ComputeLuhnCheckDigit().ToString();
        SelfCheckNumber = checkNumber;
    }
}