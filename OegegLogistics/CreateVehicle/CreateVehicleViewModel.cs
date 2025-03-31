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
    private UicNumber _uicNumber = UicNumber.Empty.WithSegments(new List<UicSegment>()
    {
        UicSegment.CreateUicSegment(31, "test")
            .WithPossibleValues(new List<UicSegment>()
            {
                UicSegment.CreateUicSegment(51, "fsdgh"),
                UicSegment.CreateUicSegment(56, "adfh"),
                UicSegment.CreateUicSegment(45, "dfgjh"),
                UicSegment.CreateUicSegment(8325, "cv"),
                UicSegment.CreateUicSegment(61, "teswet"),
                UicSegment.CreateUicSegment(161, "tebfdst"),
            }),
        UicSegment.CreateUicSegment(20, "test").WithPossibleValues(new List<UicSegment>()
        {
            UicSegment.CreateUicSegment(51, "fsdgh"),
            UicSegment.CreateUicSegment(56, "adfh"),
            UicSegment.CreateUicSegment(45, "dfgjh"),
            UicSegment.CreateUicSegment(8325, "cv"),
            UicSegment.CreateUicSegment(61, "teswet"),
            UicSegment.CreateUicSegment(161, "tebfdst"),
        }),
        UicSegment.CreateUicSegment(41, "test").WithPossibleValues(new List<UicSegment>()
        {
            UicSegment.CreateUicSegment(51, "fsdgh"),
            UicSegment.CreateUicSegment(56, "adfh"),
            UicSegment.CreateUicSegment(45, "dfgjh"),
            UicSegment.CreateUicSegment(8325, "cv"),
            UicSegment.CreateUicSegment(61, "teswet"),
            UicSegment.CreateUicSegment(161, "tebfdst"),
        }),
        UicSegment.CreateUicSegment(85, "test").WithPossibleValues(new List<UicSegment>()
        {
            UicSegment.CreateUicSegment(51, "fsdgh"),
            UicSegment.CreateUicSegment(56, "adfh"),
            UicSegment.CreateUicSegment(45, "dfgjh"),
            UicSegment.CreateUicSegment(8325, "cv"),
            UicSegment.CreateUicSegment(61, "teswet"),
            UicSegment.CreateUicSegment(161, "tebfdst"),
        }),
        UicSegment.CreateUicSegment(112, "test").WithPossibleValues(new List<UicSegment>()
        {
            UicSegment.CreateUicSegment(51, "fsdgh"),
            UicSegment.CreateUicSegment(56, "adfh"),
            UicSegment.CreateUicSegment(45, "dfgjh"),
            UicSegment.CreateUicSegment(8325, "cv"),
            UicSegment.CreateUicSegment(61, "teswet"),
            UicSegment.CreateUicSegment(161, "tebfdst"),
        }),
        UicSegment.CreateUicSegment(1, "test"),
    });

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