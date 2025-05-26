using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OegegLogistics.Models;
using OegegLogistics.Navigation;

namespace OegegLogistics.CreateVehicle;

public partial class AddRepairsViewModel : BaseCreateVehicleViewModel
{
    // == Observable Properties ==
    [ObservableProperty]
    private ObservableCollection<RepairDisplayViewModel> _repairs = new ObservableCollection<RepairDisplayViewModel>();

    [ObservableProperty]
    private RepairDisplayViewModel? _selectedRepair;
    
    // == private fields ==
    private Window? _openedAddRepairDialog;
    
    // == constructors ==
    public AddRepairsViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService, createVehicleData)
    {
    }
    
    // == Relay Commands ==
    [RelayCommand]
    public async Task OpenAddRepairDialog(RepairDisplayViewModel? repairModel)
    {
        SelectedRepair = repairModel ?? new RepairDisplayViewModel();
        _openedAddRepairDialog = await NavigationService.ShowDialogAsync<EditRepairDialog>(this);
        _openedAddRepairDialog.Closed += OpenedAddRepairDialogOnClosed;
    }

    [RelayCommand]
    public void SaveRepair()
    {
        if (SelectedRepair == null)
            return;
        if(!Repairs.Contains(SelectedRepair))
            Repairs.Add(SelectedRepair);
        
        _openedAddRepairDialog?.Close();
    }

    [RelayCommand]
    public void RemoveRepair(RepairDisplayViewModel repairModel)
    {
        Repairs.Remove(repairModel);
    }
    
    // == public methods ==

    public override async Task Return()
    {
        await NavigationService.NavigateBackAsync();
    }
    
    // == private methods ==
    private void OpenedAddRepairDialogOnClosed(object? sender, EventArgs e)
    {
        SelectedRepair = null;
        _openedAddRepairDialog!.Closed -= OpenedAddRepairDialogOnClosed;
        _openedAddRepairDialog = null;
    }
}