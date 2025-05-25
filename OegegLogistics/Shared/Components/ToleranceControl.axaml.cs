using System.Collections;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace OegegLogistics.Shared.Components;

public class ToleranceControl : TemplatedControl
{
    public static readonly StyledProperty<string> LabelTextProperty = AvaloniaProperty.Register<ToleranceControl, string>(
        nameof(LabelText));

    public string LabelText
    {
        get => GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly StyledProperty<string> ToleranceProperty = AvaloniaProperty.Register<ToleranceControl, string>(
        nameof(Tolerance));

    public uint Tolerance
    {
        get
        {
            uint.TryParse(GetValue(ToleranceProperty), out uint result);
            return result;
        }
        set => SetValue(ToleranceProperty, value.ToString());
    }

    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty = AvaloniaProperty.Register<ToleranceControl, IEnumerable>(
        nameof(ItemsSource));

    public IEnumerable ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set
        {
            SetValue(ItemsSourceProperty, value);
            SelectedItem = value.Cast<object>().FirstOrDefault();
        }
    }

    public static readonly StyledProperty<object> SelectedItemProperty = AvaloniaProperty.Register<ToleranceControl, object>(
        nameof(SelectedItem));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
}