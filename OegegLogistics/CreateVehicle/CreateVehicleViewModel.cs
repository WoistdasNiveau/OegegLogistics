using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Mvvm.Navigation;
using OegegLogistics.Models;
using OegegLogistics.Shared;
using OegegLogistics.Shared.Services;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleViewModel : BaseViewModel 
{
    [ObservableProperty]
    private bool _returnButtonVisible = false;

    [ObservableProperty]
    private bool _continueButtonVisible = false;

    [ObservableProperty]
    private string _enteredUicNumber;
    
    [ObservableProperty]
    private Navigator<BaseViewModel> _navigator;
    
    // == private fields ==
    private readonly JsonService _jonService;
    
    // == public methods ==
    public CreateVehicleViewModel(JsonService jonService, Navigator<BaseViewModel> navigator)
    {
        _jonService = jonService;
        _navigator = navigator;
    }
    
    // == RelayCommands ==
    public async Task ContinueButtonPressed()
    {
        switch (nameof(Navigator.CurrentView))
        {
            case nameof(SelectVehicleTypeView):
                break;
        }
    }

    public async Task ReturnButtonPressed()
    {
        switch (nameof(Navigator.CurrentView))
        {
            case nameof(SelectVehicleTypeView):
                break;
        }
    }
    
    // == private methods ==
    private async Task<UicNumber> ProcessUicNumber()
    {
        return null;
    }
}   