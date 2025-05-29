using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using LuhnDotNet;

namespace OegegLogistics.Shared.Components;

public class UicMaskedTextBox : MaskedTextBox
{
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        TextChanged += OnTextChanged;
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if(sender is not MaskedTextBox maskedTextBox || string.IsNullOrWhiteSpace(maskedTextBox.Text) ||
           maskedTextBox.Text.Replace(" ","").Replace("_","").Replace("-","").Trim().Length != 11)
            return;

        string number = maskedTextBox.Text.Replace(" ", "").Replace("_", "").Replace("-", "").Trim();
        string checkNumber = number.ComputeLuhnCheckDigit().ToString();
        int index = maskedTextBox.Text.IndexOf("-");
        maskedTextBox.Text = maskedTextBox.Text.Substring(0, index + 1) + checkNumber;
    }
}