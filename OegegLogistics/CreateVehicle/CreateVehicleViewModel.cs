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

    [ObservableProperty]
    private uint? _uicSerialNumber;
    
    // == public methods ==
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
}   