using System.Collections;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.VisualTree;
using OegegLogistics.Models;

namespace OegegLogistics.Shared.Components;

public class UICNumberComponent : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<UICNumberComponent, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty = AvaloniaProperty.Register<UICNumberComponent, IEnumerable>(
        nameof(ItemsSource));
    
    public IEnumerable ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    public static readonly StyledProperty<object> SelectedItemProperty = AvaloniaProperty.Register<UICNumberComponent, object>(
        nameof(SelectedItem),
        defaultBindingMode: BindingMode.OneWayToSource);

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly StyledProperty<DataTemplate> ItemTemplateProperty = AvaloniaProperty.Register<UICNumberComponent, DataTemplate>(
        nameof(ItemTemplate));

    public DataTemplate ItemTemplate
    {
        get => GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public static readonly StyledProperty<AutoCompleteFilterMode> FilterModeProperty = AvaloniaProperty.Register<UICNumberComponent, AutoCompleteFilterMode>(
        nameof(FilterMode));

    public AutoCompleteFilterMode FilterMode
    {
        get => GetValue(FilterModeProperty);
        set => SetValue(FilterModeProperty, value);
    }

    public static readonly StyledProperty<string> DescriptionProperty = AvaloniaProperty.Register<UICNumberComponent, string>(
        nameof(Description));

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly StyledProperty<bool> IsBoxEnabledProperty = AvaloniaProperty.Register<UICNumberComponent, bool>(
        nameof(IsBoxEnabled),
        defaultValue: true);

    public bool IsBoxEnabled
    {
        get => GetValue(IsBoxEnabledProperty);
        set => SetValue(IsBoxEnabledProperty, value);
    }
}