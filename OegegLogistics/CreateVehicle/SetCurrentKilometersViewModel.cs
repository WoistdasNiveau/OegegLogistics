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

public partial class SetCurrentKilometersViewModel : BaseCreateVehicleViewModel
{
    // == observable properties ==
    [ObservableProperty]
    private string _uicNumber;
    
    [ObservableProperty]
    private string _currentKilometers = "0";

    [ObservableProperty]
    private bool _isCurrentKilometersNotValid = false;
    
    public SetCurrentKilometersViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService, createVehicleData)
    {
        UicNumber = _createVehicleData.UicNumber;
    }

    partial void OnCurrentKilometersChanged(string? oldValue, string newValue)
    {
        IsCurrentKilometersNotValid = !uint.TryParse(newValue, out uint currentKilometers);
        IsContinueButtonEnabled = !IsCurrentKilometersNotValid;
    }

    // == public methods ==
    public override async Task Continue()
    {
        _createVehicleData.CurrentKilometers = uint.Parse(CurrentKilometers);
        await NavigationService.NavigateAsync<AddRepairsViewModel>();
    }

    public override async Task Return()
    {
        await NavigationService.NavigateBackAsync();
    }
}