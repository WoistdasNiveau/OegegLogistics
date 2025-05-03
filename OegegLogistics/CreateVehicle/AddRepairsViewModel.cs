using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.Models;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Shared.Services;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

public partial class AddRepairsViewModel : BaseViewModel, ICreateVehicleViewModel
{
    // == observable properties ==
    [ObservableProperty]
    private string _uicNumber;

    [ObservableProperty]
    private ObservableCollection<RepairDisplayViewModel> _repairs = new ObservableCollection<RepairDisplayViewModel>();
    
    // == properties ==
    public List<ToleranceType> ToleranceTypes { get; } = Enum.GetValues<ToleranceType>().ToList();
    
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
        RepairDisplayViewModel newRepair = new RepairDisplayViewModel();
        Repairs.Add(newRepair);
    }

    [RelayCommand]
    public void RemoveRepair(RepairDisplayViewModel repair)
    {
        Repairs.Remove(repair);
    }
    
    // == public methods ==
    public Task Continue()
    {
        throw new System.NotImplementedException();
    }

    public Task Return()
    {
        throw new System.NotImplementedException();
    }
}