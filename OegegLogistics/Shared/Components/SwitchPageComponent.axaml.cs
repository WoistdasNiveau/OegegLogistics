using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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

    private static readonly StyledProperty<ObservableCollection<string>> PageNumbersProperty = AvaloniaProperty.Register<SwitchPageComponent, ObservableCollection<string>>(
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

    // == private methods ==
    private void UpdatePageNumbers()
    {
        PageNumbers.Clear();
        if (MaxPageNumber <= 7)
        {
            PageNumbers.AddRange(GetPageNumbers(7).Select(n => n.ToString()).OrderDescending());
        }
        else
        {
            if (CurrentPage <= 5)
            {
                PageNumbers.AddRange(GetPageNumbers(5).Select(n => n.ToString()).OrderDescending());
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
}















