using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleWindowViewModel : BaseCreateVehicleViewModel
{
    // == Observable Properties ==
    [ObservableProperty]
    private Navigator<BaseViewModel> _navigator;
    
    [ObservableProperty]
    private BaseCreateVehicleViewModel _currentViewModel;

    public CreateVehicleWindowViewModel(Navigator<BaseViewModel> navigator, NavigationService navigationService, CreateVehicleData createVehicleData) 
        : base(navigationService, createVehicleData)
    {
        _navigator = navigator;
        _navigator.Navigate<SelectVehicleTypeViewModel>();
        CurrentViewModel = (BaseCreateVehicleViewModel)_navigator.CurrentViewModel!;
        
        _navigator.PropertyChanged += NavigatorOnPropertyChanged;
    }

    private void NavigatorOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Navigator.CurrentViewModel) && Navigator.CurrentViewModel is BaseCreateVehicleViewModel vm)
        {
            CurrentViewModel = vm;
        }
    }
}