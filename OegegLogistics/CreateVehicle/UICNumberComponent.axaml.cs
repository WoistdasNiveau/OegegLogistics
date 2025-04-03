using System.Collections;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.VisualTree;

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
        nameof(SelectedItem));

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

    public static readonly StyledProperty<string> DescriptionProperty = AvaloniaProperty.Register<UICNumberComponent, string>(
        nameof(Description));

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly StyledProperty<ICommand?> SelectionChangedProperty = AvaloniaProperty.Register<UICNumberComponent, ICommand?>(
        nameof(SelectionChanged));

    public ICommand? SelectionChanged
    {
        get => GetValue(SelectionChangedProperty);
        set => SetValue(SelectionChangedProperty, value);
    }
    
    // == private fields ==
    private AutoCompleteBox _autoCompleteBox;
    
    // == private methods ==
    
    
    // == override methods ==
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        
        _autoCompleteBox = this.GetVisualChildren().OfType<Grid>().First().GetVisualChildren().OfType<AutoCompleteBox>().First();
        
        _autoCompleteBox.SelectionChanged += CompleteBoxOnSelectionChanged;
    }

    private void CompleteBoxOnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if(sender is not UICNumberComponent component) return;

        if (SelectionChanged is not null && SelectionChanged.CanExecute(_autoCompleteBox.SelectedItem))
        {
            SelectionChanged.Execute(_autoCompleteBox.SelectedItem);
        }
    }
}