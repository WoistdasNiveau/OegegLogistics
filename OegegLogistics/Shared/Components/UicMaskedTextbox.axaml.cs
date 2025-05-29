using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using LuhnDotNet;

namespace OegegLogistics.Shared.Components;

public class UicMaskedTextbox : MaskedTextBox
{
    private bool _isCalculating = false;
    public UicMaskedTextbox()
    {
        this.AddHandler(KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    { 
        if(sender is not MaskedTextBox maskedTextBox || string.IsNullOrWhiteSpace(maskedTextBox.Text) || maskedTextBox.Text.Contains('_'))
            return;
        
        _isCalculating = true;
        int length = maskedTextBox.Text.Length;
        string text = maskedTextBox.Text.Substring(0, length - 1).Replace(" ", "")
            .Replace("-", "").Trim();
        
        string checkNumber = text.ComputeLuhnCheckDigit().ToString();

        text = maskedTextBox.Text.Substring(0, maskedTextBox.Text.Length - 1) + checkNumber;

        maskedTextBox.Text = text;
        _isCalculating = false;
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if(sender is not MaskedTextBox maskedTextBox || string.IsNullOrWhiteSpace(maskedTextBox.Text) || maskedTextBox.Text.Contains('_'))
            return;

        Console.WriteLine(maskedTextBox.Text);
    }
}