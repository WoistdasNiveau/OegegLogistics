using System;
using System.Collections.Generic;
using System.Linq;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.Models;

public class RepairModel
{
    public string Type { get; set; }
    public uint? SequenceNumber { get; set; }
    public string Name { get; set; }
    public int KilometerLimit { get; set; }
    public int Tolerance { get; set; }
    public ToleranceType ToleranceType { get; set; }
}