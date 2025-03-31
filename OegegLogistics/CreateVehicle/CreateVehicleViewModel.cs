using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OegegLogistics.Models;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleViewModel : BaseViewModel
{
    [ObservableProperty]
    private UICModel _interOperability;

    [ObservableProperty]
    private UICModel _countryCode;

    [ObservableProperty]
    private UICModel _vehicelType;
    
    [ObservableProperty]
    private UICModel _velocityHeating;
     
    [ObservableProperty]
    private UICModel _serialNumber;
    
    [ObservableProperty]
    private UICModel _selfCheck;
    
    // == commands ==
    [RelayCommand]
    public void HandleUicModelSelectionChanged(UICModel model)
    {
        var interOperability = model.GetType().Name switch
        {
            nameof(InteroperabilityModel) => InterOperability = model,
            nameof(CountryCodeModel) => CountryCode = model,
            nameof(VehicleTypeModel) => VehicelType = model,
            nameof(VelocityHeatingModel) => VelocityHeating = model,
            nameof(SerialNumberModel) => SerialNumber = model,
            _ => null
        };
    }
}   