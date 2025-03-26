using CommunityToolkit.Mvvm.ComponentModel;
using Mvvm.Navigation;
using OegegLogistics.Shared;
using OegegLogistics.Vehicles;
using OegegLogistics.ViewModels;

namespace OegegLogistics.Main;

[ViewFor<MainWindowViewModel>]
public partial class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel(VehiclesView vehiclesView)
    {
        _vehiclesView = vehiclesView;
    }

    [ObservableProperty]
    public VehiclesView _vehiclesView;
}