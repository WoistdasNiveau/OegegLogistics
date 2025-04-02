using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OegegLogistics.Models;

public record UicNumber(UicInteroperabilitySegment UicInteroperabilitySegment, UicCountryCodeSegment UicCountryCodeSegment, UicTypeSegment UicTypeSegment,
    UicVelocityHeatingSegment UicVelocityHeatingSegment, UicSerialNumberSegment UicSerialNumberSegment, UicSelfCheckSegment UicSelfCheckSegment)
{
    public static UicNumber Empty => new UicNumber(UicSegment.CreateEmpty<UicInteroperabilitySegment>(),
        UicSegment.CreateEmpty<UicCountryCodeSegment>(),
        UicSegment.CreateEmpty<UicTypeSegment>(),
        UicSegment.CreateEmpty<UicVelocityHeatingSegment>(),
        UicSegment.CreateEmpty<UicSerialNumberSegment>(),
        UicSegment.CreateEmpty<UicSelfCheckSegment>());
}

public abstract record UicSegment(uint Number, string Description, IEnumerable<UicSegment>? PossibleItems = null)
{
    public static T CreateEmpty<T>() where T : UicSegment
    {
        return CreateUicSegment<T>(0);
    }
    public static T CreateUicSegment<T>(uint number, string description = "") where T : UicSegment
    {
        if (_factories.TryGetValue(typeof(T), out var factory))
        {
            return (T)factory(number, description).ValidateUicSegment();
        }

        throw new ArgumentException($"Invalid UicSegment type: {typeof(T).Name}", nameof(T));
    }
    
    private static readonly Dictionary<Type, Func<uint, string, UicSegment>> _factories = new()
    {
        { typeof(UicInteroperabilitySegment), (num, desc) => new UicInteroperabilitySegment(num, desc) },
        { typeof(UicCountryCodeSegment), (num, desc) => new UicCountryCodeSegment(num, desc) },
        { typeof(UicTypeSegment), (num, desc) => new UicTypeSegment(num, desc) },
        { typeof(UicVelocityHeatingSegment), (num, desc) => new UicVelocityHeatingSegment(num, desc) },
        { typeof(UicSerialNumberSegment), (num, desc) => new UicSerialNumberSegment(num, desc) },
        { typeof(UicSelfCheckSegment), (num, _) => new UicSelfCheckSegment(num) }
    };
}
public sealed record UicInteroperabilitySegment(uint Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicCountryCodeSegment(uint Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicTypeSegment(uint Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicVelocityHeatingSegment(uint Number, string Description = "") : UicSegment(Number, Description); 
public sealed record UicSerialNumberSegment(uint Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicSelfCheckSegment(uint Number) : UicSegment(Number, "Self check number");

public static class UicNumberExtensions
{
    public static UicNumber WithSegment<T>(this UicNumber uicNumber, T segment) where T : UicSegment
    {
        return segment switch
        {
            UicInteroperabilitySegment uicInteroperabilitySegment => uicNumber with {UicInteroperabilitySegment = uicInteroperabilitySegment},
            UicCountryCodeSegment uicCountryCodeSegment => uicNumber with {UicCountryCodeSegment = uicCountryCodeSegment},
            UicTypeSegment uicTypeSegment => uicNumber with {UicTypeSegment = uicTypeSegment},
            UicVelocityHeatingSegment uicVelocityHeatingSegment => uicNumber with{UicVelocityHeatingSegment = uicVelocityHeatingSegment},
            UicSerialNumberSegment uicSerialNumberSegment => uicNumber with{UicSerialNumberSegment = uicSerialNumberSegment},
            UicSelfCheckSegment uicSelfCheckSegment => uicNumber with{UicSelfCheckSegment = uicSelfCheckSegment},
            _ => throw new ArgumentException(message: "Invalid UicSegment type", paramName: nameof(T)),
        };
    }
    public static UicSegment WithPossibleValues(this UicSegment uicSegment, List<UicSegment> segments)
    {
        return uicSegment  with { PossibleItems = segments };
    }
}

public static class UicValidation
{
    public static UicSegment ValidateUicSegment(this UicSegment value)
    {
        List<string> validationResults = new();
        switch (value)
        {
            case UicInteroperabilitySegment:
            case UicCountryCodeSegment:
            case UicTypeSegment:
            case UicVelocityHeatingSegment:
                if(value.Number < 10 || value.Number >= 100)
                    validationResults.Add($"{value.GetType()} must be between 10 and 100");
                break;
            case UicSerialNumberSegment:
                if(value.Number < 100 || value.Number >= 1000)
                    validationResults.Add("Serial number must be between 100 and 1000");
                break;
            case UicSelfCheckSegment:
                if(value.Number >= 10)
                    validationResults.Add("Self check number must be between 0 and 10");
                break;
        }

        if (validationResults.Count > 0)
        {
            throw new ArgumentException(string.Join(',', validationResults));
        }
    }
}