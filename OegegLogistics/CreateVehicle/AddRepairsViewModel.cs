using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.Models;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Shared.Services;

namespace OegegLogistics.CreateVehicle;

public partial class AddRepairsViewModel : BaseViewModel, ICreateVehicleViewModel
{
    // == observable properties ==
    [ObservableProperty]
    private string _uicNumber;

    [ObservableProperty]
    private ObservableCollection<RepairModel> _repairs = new ObservableCollection<RepairModel>();
    
    // == private fields ==
    private readonly CreateVehicleData _createVehicleData;
    
    public AddRepairsViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService)
    {
        _createVehicleData = createVehicleData;
        UicNumber = _createVehicleData.UicNumber;
    }
    
    // == Relay Commands ==
    [RelayCommand]
    public void AddRepair()
    {
        Repairs.Add(new RepairModel());
    }

    [RelayCommand]
    public void RemoveRepair(RepairModel repair)
    {
        Repairs.Remove(repair);
    }
    public Task Continue()
    {
        throw new System.NotImplementedException();
    }

    public Task Return()
    {
        throw new System.NotImplementedException();
    }
}