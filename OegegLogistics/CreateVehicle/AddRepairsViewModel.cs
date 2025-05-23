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
    private ObservableCollection<RepairModel> _repairs = new ObservableCollection<RepairModel>();
    
    [ObservableProperty]
    private RepairModel? _selectedRepair;
    
    // == constructors ==
    public AddRepairsViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService, createVehicleData)
    {
    }
    
    // == Relay Commands ==
    [RelayCommand]
    public async Task AddRepair()
    {
        RepairModel repairModel = new RepairModel();
        await NavigationService.ShowDialogAsync<AddRepairDialog>(repairModel);
        Repairs.Add(repairModel);
    }

    [RelayCommand]
    public async Task EditRepair(RepairModel repairModel)
    {
        
    }

    [RelayCommand]
    public void RemoveRepair()
    {
        if(SelectedRepair is null)
            return;
        
        Repairs.Remove(SelectedRepair);
    }
    
    // == public methods ==

    public override async Task Return()
    {
        await NavigationService.NavigateBackAsync();
    }
}