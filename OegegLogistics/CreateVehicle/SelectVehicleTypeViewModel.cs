using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuhnDotNet;
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
    
    // == private fields ==
    private bool isComputing = false;
    

    public SelectVehicleTypeViewModel(CreateVehicleData createVehicleData, NavigationService navigationService) : base(navigationService, createVehicleData)
    {
        IsReturnButtonVisible = false;
        IsContinueButtonEnabled = false;
    }
    
    
    partial void OnUicNumberChanged(string? oldValue, string newValue)
    {
        IsContinueButtonEnabled = newValue.Replace(" ","")
            .Replace("-","")
            .Trim().Length == 12;
        /*
        string value = newValue.Substring(0, newValue.Length - 1).Replace(" ", "").Replace("_", "").Replace("-", "").Trim();
        if (string.IsNullOrWhiteSpace(value) || isComputing || value.Length != 11)
        {
            IsContinueButtonEnabled = false;
            return;
        } 

        isComputing = true;
        string controlNumber = value.ComputeLuhnCheckDigit().ToString();
        UicNumber = newValue.Substring(0, newValue.Length - 1) + controlNumber;
        isComputing = false;
        */
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