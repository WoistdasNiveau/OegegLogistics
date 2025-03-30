using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using OegegLogistics.Shared.Extensions;

namespace OegegLogistics.Shared.Components;

public class SwitchPageComponent : TemplatedControl
{
    public static readonly StyledProperty<uint> MaxPageNumberProperty = AvaloniaProperty.Register<SwitchPageComponent, uint>(
        nameof(MaxPageNumber),
        defaultValue: 1);

    public uint MaxPageNumber
    {
        get => GetValue(MaxPageNumberProperty);
        set
        {
            SetValue(MaxPageNumberProperty, value);
            UpdatePageNumbers();
        }
    }

    public static readonly StyledProperty<uint> CurrentPageProperty = AvaloniaProperty.Register<SwitchPageComponent, uint>(
        nameof(CurrentPage));

    public uint CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set
        {
            SetValue(CurrentPageProperty, value);
            UpdatePageNumbers();
        }
    }

    public static readonly StyledProperty<ObservableCollection<string>> PageNumbersProperty = AvaloniaProperty.Register<SwitchPageComponent, ObservableCollection<string>>(
        nameof(PageNumbers),
        defaultValue: new ObservableCollection<string>());

    public ObservableCollection<string> PageNumbers
    {
        get => GetValue(PageNumbersProperty);
        set => SetValue(PageNumbersProperty, value);
    }

    // == Constructor ==
    public SwitchPageComponent()
    {
        MaxPageNumber = 100;
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if(this.GetTemplateChildren().FirstOrDefault(t => t.Name?.Equals("PART_PageButtons") ?? false) is not ItemsRepeater pageButtonsRepeater)
            return;

        pageButtonsRepeater.ElementPrepared += PageButtonsRepeaterOnElementPrepared;
    }

    private void PageButtonsRepeaterOnElementPrepared(object? sender, ItemsRepeaterElementPreparedEventArgs e)
    {
        if(e.Element is not Button button)
            return;
        button.Click += PageButtonClicked;
    }

    private void PageButtonClicked(object? sender, RoutedEventArgs e)
    {
        uint test;
        uint.TryParse((sender as Button)?.Content?.ToString() ?? string.Empty, out test);
        CurrentPage = test;
    }


    // == private methods ==
    private void UpdatePageNumbers()
    {
        PageNumbers.Clear();
        if (MaxPageNumber <= 7)
        {
            PageNumbers.AddRange(GetPageNumbers(7).Select(n => n.ToString()));
        }
        else
        {
            if (CurrentPage <= 5)
            {
                PageNumbers.AddRange(GetPageNumbers(5).Select(n => n.ToString()));
                PageNumbers.Add("...");
                PageNumbers.Add(MaxPageNumber.ToString());
            }
            else if (CurrentPage >= MaxPageNumber - 5)
            {
                PageNumbers.Add("1");
                PageNumbers.Add("...");
                PageNumbers.AddRange(GetReversePageNumbers(MaxPageNumber, MaxPageNumber - 5).Select(n => n.ToString()).OrderDescending());
            }
            else
            {
                PageNumbers.Add("1");
                PageNumbers.Add("...");
                PageNumbers.Add((CurrentPage - 1).ToString());
                PageNumbers.Add((CurrentPage).ToString());
                PageNumbers.Add((CurrentPage + 1).ToString());
                PageNumbers.Add("...");
                PageNumbers.Add(MaxPageNumber.ToString());
                
            }
        }
    }

    private IEnumerable<uint> GetPageNumbers(int max, int min = 1)
    {
        for (int i = min; i <= max; i++)
        {
            yield return (uint)i;
        }
    }
    
    private IEnumerable<uint> GetReversePageNumbers(uint max, uint min = 1)
    {
        for (uint i = max; i >= min; i--)
        {
            yield return (uint)i;
        }
    }
    
    private void PageLabelTapped(object? sender, RoutedEventArgs e)
    {
        uint pageNumber;
        if(sender is not Button label || label.Content == null || label.Content.Equals("...") || !uint.TryParse(label.Content.ToString(), out pageNumber))
            return;
        
        if(pageNumber <= 0 || pageNumber > MaxPageNumber)
            return;
        
        CurrentPage = pageNumber;
    }
}















