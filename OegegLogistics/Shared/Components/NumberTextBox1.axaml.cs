using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace OegegLogistics.Shared.Components;

public class NumberTextBox1 : TextBox
{
    public NumberTextBox1()
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