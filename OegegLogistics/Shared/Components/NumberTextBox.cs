using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace OegegLogistics.Shared.Components;

public class NumberTextBox : TextBox
{
    public NumberTextBox()
    {
        this.AddHandler(TextInputEvent, OnTextInput, RoutingStrategies.Tunnel);
    }

    private void OnTextInput(object? sender, TextInputEventArgs e)
    {
        if (!IsTextValid(e.Text))
        {
            e.Handled = true;
        }
    }

    private bool IsTextValid(string input)
    {
        return Regex.IsMatch(input, "^[0-9]+$");
    }
}