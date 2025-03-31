using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using OegegLogistics.Models;

namespace OegegLogistics.Shared.Components;

public class UICNumberComponent : TemplatedControl
{
    public static readonly StyledProperty<UICModel> UicModelProperty = AvaloniaProperty.Register<UICNumberComponent, UICModel>(
        nameof(UicModel));

    public UICModel UicModel
    {
        get => GetValue(UicModelProperty);
        set => SetValue(UicModelProperty, value);
    }

    public static readonly StyledProperty<ICommand?> SelectionChangedCommandProperty = AvaloniaProperty.Register<UICNumberComponent, ICommand?>(
        nameof(SelectionChangedCommand));

    public ICommand? SelectionChangedCommand
    {
        get => GetValue(SelectionChangedCommandProperty);
        set => SetValue(SelectionChangedCommandProperty, value);
    }

    public static readonly StyledProperty<UICModel> SelectionChangedCommandParameterProperty = AvaloniaProperty.Register<UICNumberComponent, UICModel>(
        nameof(SelectionChangedCommandParameter));

    public UICModel SelectionChangedCommandParameter
    {
        get => GetValue(SelectionChangedCommandParameterProperty);
        set => SetValue(SelectionChangedCommandParameterProperty, value);
    }

    private void UicModelSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if(sender is not UICNumberComponent uicNumberComponent)
            return;
        if(SelectionChangedCommand != null && SelectionChangedCommand.CanExecute(uicNumberComponent.UicModel))
            SelectionChangedCommand.Execute(uicNumberComponent.UicModel);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (this.GetVisualChildren().First() is not StackPanel stackPanel)
            return;
        
        List<Control> children = stackPanel.Children.ToList();
        AutoCompleteBox autoCompleteBox = children.OfType<AutoCompleteBox>().FirstOrDefault()!;
        
        autoCompleteBox.SelectionChanged += UicModelSelectionChanged;
    }
}