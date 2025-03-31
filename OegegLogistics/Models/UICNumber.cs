using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OegegLogistics.Models;

public record UicNumber(List<UicSegment> UicSegments)
{
    public UicNumber() : this(new List<UicSegment>())
    {
    }
    
    public static UicNumber Empty => new UicNumber();
}

public abstract record UicSegment(string Description, List<UicSegment>? PossibleItems = null)
{
    public static UicSegment CreateUicSegment(uint number, string description = "")
    {
        switch (number)
        {
            case <= 9:
                return new UicSelfCheckSegment(number);
            case > 9 and <= 99:
                return new UicNumberSegment(number, description);
            case >= 100 and <= 999:
                return new UicSerialNumberSegment(number, description);
            default:
                throw new ArgumentException("Invalid UICNumber", nameof(number));
        }
    }
}
public sealed record UicSelfCheckSegment([Range(0,9)]uint Number) : UicSegment("Self check number");
public sealed record UicSerialNumberSegment([Range(100,999)]uint Number, string Description) : UicSegment(Description);
public sealed record UicNumberSegment([Range(10, 99)]uint Number, string Description) : UicSegment(Description);

public static class UicNumberExtensions
{
    public static UicNumber WithSegments(this UicNumber uicNumber, List<UicSegment> segments)
    {
        return uicNumber with { UicSegments = segments };
    }
    
    public static UicSegment WithPossibleValues(this UicSegment uicSegment, List<UicSegment> segments)
    {
        return uicSegment  with { PossibleItems = segments };
    }
}

