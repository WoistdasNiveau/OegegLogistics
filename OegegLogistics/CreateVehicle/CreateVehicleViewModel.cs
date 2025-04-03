using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public CreateVehicleViewModel()
    {
    }

    // == commands ==
    [RelayCommand]
    public void SelectionChanged(uint number)
    {
        Console.WriteLine("w");
    }
}   