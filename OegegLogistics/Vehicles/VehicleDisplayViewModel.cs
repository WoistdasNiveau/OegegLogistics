using System;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.Vehicles;

public class VehicleDisplayViewModel
{
    public Guid PublicId { get; set; }
    public string UICNumber { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public VehicleStatus Status { get; set; }
    public string Location { get; set; }
    public uint WorkCount { get; set; }
    public Priority Priority { get; set; }
    public string ResponsiblePerson { get; set; }
}