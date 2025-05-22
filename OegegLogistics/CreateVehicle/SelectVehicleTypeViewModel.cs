using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OegegLogistics.Navigation;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class SelectVehicleTypeViewModel : BaseCreateVehicleViewModel
{
    // == Observable proeprties ==
    [ObservableProperty]
    private string _uicNumber;
    
    // == public properties ==
    public event EventHandler ReturnClicked;
    

    public SelectVehicleTypeViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService, createVehicleData)
    {
        
    }
    
    // == Relay Commands ==
    [RelayCommand]
    public async Task ProcessUicNumber()
    {
        
    }

    [RelayCommand]
    public override async Task Continue()
    {
        _createVehicleData.UicNumber = UicNumber;
        try
        {
            await NavigationService.NavigateAsync<SetCurrentKilometersViewModel>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [RelayCommand]
    public override async Task Return()
    {
        ReturnClicked?.Invoke(this, EventArgs.Empty);
    }
    
    // == private methods ==
}