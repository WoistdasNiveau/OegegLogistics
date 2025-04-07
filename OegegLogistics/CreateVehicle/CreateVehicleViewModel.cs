using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OegegLogistics.Models;
using OegegLogistics.Shared;

namespace OegegLogistics.CreateVehicle;

public partial class CreateVehicleViewModel : BaseViewModel 
{
    [ObservableProperty]
    private UicNumber _uicNumber = UicNumber.Empty
        .WithSegment(UicSegment.CreateUicSegment<UicInteroperabilitySegment>(32, "Interoperability")
            .WithPossibleValues([UicSegment.CreateUicSegment<UicInteroperabilitySegment>(22,""),
                UicSegment.CreateUicSegment<UicInteroperabilitySegment>(22,""),
                UicSegment.CreateUicSegment<UicInteroperabilitySegment>(35,""),
                UicSegment.CreateUicSegment<UicInteroperabilitySegment>(12,""),
                UicSegment.CreateUicSegment<UicInteroperabilitySegment>(86,""),]))
        .WithSegment(UicSegment.CreateUicSegment<UicCountryCodeSegment>(67, "Country code"))
        .WithSegment(UicSegment.CreateUicSegment<UicTypeSegment>(31, "UicTypeSegment"))
        .WithSegment(UicSegment.CreateUicSegment<UicVelocityHeatingSegment>(82, "UicVelocityHeatingSegment"))
        .WithSegment(UicSegment.CreateUicSegment<UicSerialNumberSegment>(322, "UicSerialNumberSegment"))
        .WithSegment(UicSegment.CreateUicSegment<UicSelfCheckSegment>(3, "UicSelfCheckSegment"));
    
    [ObservableProperty]
    private UicSegment _selectedUicSegment;
    
    // == public methods ==
    partial void OnSelectedUicSegmentChanged(UicSegment value)
    {
        switch (value)
        {
            case UicInteroperabilitySegment uicInteroperabilitySegment:
                UicNumber = UicNumber with {UicInteroperabilitySegment = uicInteroperabilitySegment};
                break;
            case UicCountryCodeSegment uicCountryCodeSegment:
                UicNumber = UicNumber with {UicCountryCodeSegment = uicCountryCodeSegment};
                break;
            case UicTypeSegment uicTypeSegment:
                UicNumber = UicNumber with {UicTypeSegment = uicTypeSegment};
                break;
            case UicVelocityHeatingSegment uicVelocityHeatingSegment:
                UicNumber = UicNumber with {UicVelocityHeatingSegment = uicVelocityHeatingSegment};
                break;
            default:
                throw new ArgumentOutOfRangeException("no valid uic segment{}", value.ToString());
        }
    }
}   