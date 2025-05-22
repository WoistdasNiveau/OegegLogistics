using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public abstract partial class BaseCreateVehicleViewModel : BaseViewModel
{
    // == Observable properties ==
    [ObservableProperty]
    private bool _isContinueButtonVisible = true;
    [ObservableProperty]
    private bool _isContinueButtonEnabled= true;
    [ObservableProperty]
    private bool _isReturnButtonVisible = true;
    [ObservableProperty]
    private bool _isReturnButtonEnabled = true;
    
    // == protected fields ==
    protected readonly CreateVehicleData _createVehicleData;
    
    // == constructor ==
    protected BaseCreateVehicleViewModel(NavigationService navigationService, CreateVehicleData createVehicleData) : base(navigationService)
    {
        _createVehicleData = createVehicleData;
    }
    
    // == methods ==
    public virtual async Task Continue() => throw new System.NotSupportedException("Continue method was not implemented.");
    public virtual async Task Return() => throw new System.NotSupportedException("Return method was not implemented.");
}