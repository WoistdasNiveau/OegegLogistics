using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mvvm.Navigation;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleWindowViewModel : BaseViewModel
{
    // == Observable Properties ==
    [ObservableProperty]
    private Navigator<BaseViewModel> _navigator;

    public CreateVehicleWindowViewModel(Navigator<BaseViewModel> navigator)
    {
        _navigator = navigator;
        _navigator.Navigate<SelectVehicleTypeViewModel>();
    }

    [RelayCommand]
    public async Task ContinueClicked()
    {
        
    }

    [RelayCommand]
    public async Task ReturnClicked()
    {
        (Navigator.CurrentViewModel as ICreateVehicleViewModel)?.Return();
    }
}