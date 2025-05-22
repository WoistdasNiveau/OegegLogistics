using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Mvvm.Navigation;
using OegegLogistics.Models;

namespace OegegLogistics.CreateVehicle;

[ViewFor<AddRepairsViewModel>]
public partial class AddRepairsView : UserControl
{
    public AddRepairsView(AddRepairsViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void DataGridCell_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (e.Source is not Border border)
            return;
        
        RepairModel? repairModel = border.GetVisualAncestors()
            .OfType<DataGridRow>()
            .FirstOrDefault()?.DataContext as RepairModel;
        
        ((AddRepairsViewModel)DataContext!).EditRepairCommand.ExecuteAsync(repairModel);
    }
}