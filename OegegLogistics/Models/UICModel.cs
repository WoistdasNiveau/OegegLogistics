using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OegegLogistics.Models;

public abstract record UICModel
{
    public List<UICModel> Descriptions { get; set; }
    public string Description { get; set; } = string.Empty;
}

public sealed record InteroperabilityModel([Length(2, 2)] uint Number) : UICModel;

public sealed  record CountryCodeModel([Length(2, 2)] uint Number) : UICModel;

public sealed  record VehicleTypeModel([Length(2, 2)] uint Number) : UICModel;

public sealed  record VelocityHeatingModel([Length(2, 2)] uint Number) : UICModel;

public sealed  record SerialNumberModel([Length(3, 3)] uint Number) : UICModel;

public sealed  record SelfCheckNumberModel([Length(1, 1)] uint Number) : UICModel;