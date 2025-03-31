using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.CreateVehicle;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Vehicles;
using OegegLogistics.ViewModels;

namespace OegegLogistics.Main;

[ViewFor<MainWindowViewModel>]
public partial class MainWindowViewModel : BaseViewModel
{
    private readonly NavigationService _navigationService;
    
    public MainWindowViewModel(VehiclesView vehiclesView, NavigationService navigationService)
    {
        _vehiclesView = vehiclesView;
        _navigationService = navigationService;
    }

    [ObservableProperty]
    public VehiclesView _vehiclesView;
    
    // == Commands ==
    [RelayCommand]
    public async Task OpenAddVehicleWindow()
    {
        await _navigationService.NavigateNewWindowAsync<UicNumberView>();
    }
}