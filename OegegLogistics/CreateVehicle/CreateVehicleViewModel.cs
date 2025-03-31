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
        UicSegment.CreateUicSegment(31, "test"),
        UicSegment.CreateUicSegment(20, "test"),
        UicSegment.CreateUicSegment(41, "test"),
        UicSegment.CreateUicSegment(85, "test"),
        UicSegment.CreateUicSegment(112, "test"),
        UicSegment.CreateUicSegment(1, "test"),
    });
    
    // == commands ==
    [RelayCommand]
    public void SelectionChanged(uint number)
    {
        Console.WriteLine("w");
    }
}   