using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using OegegLogistics.Shared;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.Vehicles;

public partial class VehiclesViewModel : BaseViewModel
{
    // == Observable Properties ==
    [ObservableProperty]
    private ObservableCollection<VehicleDisplayViewModel> _vehicles = new()
    {
        new VehicleDisplayViewModel()
        {
            PublicId = Guid.CreateVersion7(),
            UICNumber = "185891202",
            Name = "Schlieren",
            Type = "Personenwaggon",
            Status = VehicleStatus.FullyOperable,
            Location = "Gleis 3",
            WorkCount = 0,
            Priority = Priority.Medium,
            ResponsiblePerson = "Franz"
        }
    };
}