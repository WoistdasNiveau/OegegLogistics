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
    private RepairDisplayViewModel? _newRepair;
    
    // == private fields ==
    private Window? _openedAddRepairDialog;
    
    // == constructors ==
    public AddRepairsViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService, createVehicleData)
    {
    }
    
    // == Relay Commands ==
    [RelayCommand]
    public async Task OpenAddRepairDialog()
    {
        NewRepair = new RepairDisplayViewModel();
        _openedAddRepairDialog = await NavigationService.ShowDialogAsync<AddRepairDialog>(this);
    }

    [RelayCommand]
    public void SaveRepair()
    {
        if (NewRepair == null)
            return;
        Repairs.Add(NewRepair);
        NewRepair = new RepairDisplayViewModel();
        
        _openedAddRepairDialog?.Close();
        _openedAddRepairDialog = null;
    }

    [RelayCommand]
    public async Task EditRepair(RepairDisplayViewModel repairModel)
    {
        
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
}