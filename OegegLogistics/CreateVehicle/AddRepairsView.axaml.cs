using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Mvvm.Navigation;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

[ViewFor<AddRepairsViewModel>]
public partial class AddRepairsView : UserControl
{
    public AddRepairsView(AddRepairsViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void Visual_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        ComboBox box = sender as ComboBox;
        box.SelectedItem = ToleranceType.Percentage;
    }
}