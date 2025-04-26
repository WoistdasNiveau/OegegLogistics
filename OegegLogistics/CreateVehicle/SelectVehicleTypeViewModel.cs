using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class SelectVehicleTypeViewModel : BaseViewModel, ICreateVehicleViewModel
{
    // == Observable proeprties ==
    [ObservableProperty]
    private string _uicNumber;
    
    // == public properties ==
    public event EventHandler ReturnClicked;
    
    // == private fields ==
    private readonly CreateVehicleData _createVehicleData;

    public SelectVehicleTypeViewModel( CreateVehicleData createVehicleData)
    {
        _createVehicleData = createVehicleData;
    }
    
    // == Relay Commands ==
    [RelayCommand]
    public async Task ProcessUicNumber()
    {
        
    }

    [RelayCommand]
    public async Task Continue()
    {
        
    }

    [RelayCommand]
    public async Task Return()
    {
        ReturnClicked?.Invoke(this, EventArgs.Empty);
    }
}