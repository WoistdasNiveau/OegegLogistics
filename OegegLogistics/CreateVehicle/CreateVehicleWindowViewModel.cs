using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleWindowViewModel : BaseViewModel
{
    // == Observable Properties ==
    [ObservableProperty]
    private Navigator<BaseViewModel> _navigator;

    public CreateVehicleWindowViewModel(Navigator<BaseViewModel> navigator, NavigationService navigationService) : base(navigationService)
    {
        _navigator = navigator;
        _navigator.Navigate<SelectVehicleTypeViewModel>();
    }

    [RelayCommand]
    public async Task ContinueClicked()
    {
        (Navigator.CurrentViewModel as ICreateVehicleViewModel)?.Continue();
    }

    [RelayCommand]
    public async Task ReturnClicked()
    {
        (Navigator.CurrentViewModel as ICreateVehicleViewModel)?.Return();
    }
}