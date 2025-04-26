using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.Models;
using OegegLogistics.Shared;
using OegegLogistics.Shared.Services;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleViewModel : BaseViewModel 
{
    [ObservableProperty]
    private UicNumber _uicNumber = UicNumber.Empty;
    
    [ObservableProperty]
    private UicSegment _selectedUicSegment;

    [ObservableProperty]
    private uint? _uicSerialNumber;

    [ObservableProperty]
    private bool _returnButtonVisible = false;

    [ObservableProperty]
    private bool _continueButtonVisible = false;
    
    // == private fields ==
    private readonly JsonService _jonService;
    
    // == public methods ==
    public CreateVehicleViewModel(JsonService jonService)
    {
        _jonService = jonService;

        SetupUicNumber();
    }

    partial void OnSelectedUicSegmentChanged(UicSegment value)
    {
        switch (value)
        {
            case null:
                return;
            case UicInteroperabilitySegment uicInteroperabilitySegment:
                UicNumber = UicNumber.WithSegment(UicNumber.UicInteroperabilitySegment.WithNumber(uicInteroperabilitySegment.Number));
                break;
            case UicCountryCodeSegment uicCountryCodeSegment:
                UicNumber = UicNumber.WithSegment(UicNumber.UicCountryCodeSegment.WithNumber(uicCountryCodeSegment.Number));
                break;
            case UicTypeSegment uicTypeSegment:
                UicNumber = UicNumber.WithSegment(UicNumber.UicTypeSegment.WithNumber(uicTypeSegment.Number))
                    ;break;
            case UicVelocityHeatingSegment uicVelocityHeatingSegment:
                UicNumber = UicNumber.WithSegment(UicNumber.UicVelocityHeatingSegment.WithNumber(uicVelocityHeatingSegment.Number));
                break;
            case UicSerialNumberSegment uicSerialNumberSegment:
                UicNumber = UicNumber.WithSegment(UicNumber.UicSerialNumberSegment.WithNumber(uicSerialNumberSegment.Number));
                break;
            default:
                throw new ArgumentOutOfRangeException("no valid uic segment{}", value.ToString());
        }
    }

    partial void OnUicSerialNumberChanged(uint? oldValue, uint? newValue)
    {
        if(newValue == null)
            return;
        
        OnSelectedUicSegmentChanged(UicSegment.CreateUicSegment<UicSerialNumberSegment>((uint)newValue));
    }
    
    // == private methods ==
    private async Task SetupUicNumber()
    {
        UicNumber = UicNumber with {
            UicInteroperabilitySegment = await PopulateCountryCodes<UicInteroperabilitySegment>(), 
            UicCountryCodeSegment = await PopulateCountryCodes<UicCountryCodeSegment>(), 
            UicTypeSegment = await PopulateCountryCodes<UicTypeSegment>(), 
            UicSerialNumberSegment = UicSegment.CreateEmpty<UicSerialNumberSegment>(), 
            UicVelocityHeatingSegment = await PopulateCountryCodes<UicVelocityHeatingSegment>(), 
            UicSelfCheckSegment = UicSegment.CreateEmpty<UicSelfCheckSegment>()
        };
        Console.WriteLine("3");
    }

    private async Task<T> PopulateCountryCodes<T>() where T : UicSegment
    {
        Dictionary<string, string> countries = await _jonService.ReadJsonFileAsync(
            typeof(T).Name switch
            {
                nameof(UicInteroperabilitySegment) => JsonService.INTEROPERABILITIES_LOCATION,
                nameof(UicCountryCodeSegment) => JsonService.COUNTRYCODES_LOCATION,
                nameof(UicTypeSegment) => JsonService.TYPE_LOCATION,
                nameof(UicVelocityHeatingSegment) => JsonService.VELOCITYHEATING_LOCATION,
            });
            countries.Add("---", "default");
        
        T countrySegment = UicSegment.CreateEmpty<T>()
            .WithNumber("00", "default")
            .WithPossibleValues(countries.Select(t => 
                UicSegment.CreateUicSegment<T>(t.Key, t.Value)));
        
        return countrySegment;
    }
}   