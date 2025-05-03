using System;
using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.CreateVehicle;

public partial class RepairDisplayViewModel : ObservableObject
{
    [ObservableProperty]
    private string _type;
    
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private int _kilometerLimit;
    
    [ObservableProperty]
    private int _tolerance;
    
    [ObservableProperty]
    private ToleranceType _toleranceType;
}