using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.CreateVehicle;
using OegegLogistics.Models;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Vehicles;
using OegegLogistics.ViewModels;

namespace OegegLogistics.Main;

[ViewFor<MainViewModel>]
public partial class MainViewModel : BaseViewModel
{
    private readonly NavigationService _navigationService;
    
    
    public MainViewModel(NavigationService navigationService) : base(navigationService)
    {
        _navigationService = navigationService;
    }
    
    // == Commands ==
    [RelayCommand]
    public async Task OpenAddVehicleWindow()
    {
        await _navigationService.NavigateNewWindowAsync<CreateVehicleWindow, SelectVehicleTypeView>();
    }
}