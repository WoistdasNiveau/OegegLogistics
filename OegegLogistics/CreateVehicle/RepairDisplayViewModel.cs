using System;
using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

public partial class RepairDisplayViewModel : ObservableObject
{
    [ObservableProperty]
    private string _type;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private uint _kilometerLimit;
    
    [ObservableProperty]
    private uint _tolerance;
    
    [ObservableProperty]
    private ToleranceType _toleranceType;

    [ObservableProperty]
    private uint _kilometerProgress;
}