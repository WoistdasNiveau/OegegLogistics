using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OegegLogistics.Models;

public record UicNumber(UicInteroperabilitySegment UicInteroperabilitySegment, UicCountryCodeSegment UicCountryCodeSegment, UicTypeSegment UicTypeSegment,
    UicVelocityHeatingSegment UicVelocityHeatingSegment, UicSerialNumberSegment UicSerialNumberSegment, UicSelfCheckSegment UicSelfCheckSegment)
{
    public static UicNumber Empty => new UicNumber(
        UicSegment.CreateEmpty<UicInteroperabilitySegment>(),
        UicSegment.CreateEmpty<UicCountryCodeSegment>(),
        UicSegment.CreateEmpty<UicTypeSegment>(),
        UicSegment.CreateEmpty<UicVelocityHeatingSegment>(),
        UicSegment.CreateEmpty<UicSerialNumberSegment>(),
        UicSegment.CreateEmpty<UicSelfCheckSegment>());
}

public abstract record UicSegment(string Number, string Description, IEnumerable<UicSegment>? PossibleItems = null)
{
    public static T CreateEmpty<T>() where T : UicSegment
    {
        return CreateUicSegment<T>("---", "default");
    }
    
    public static T CreateUicSegment<T>(string number, string description = "") where T : UicSegment
    {
        if (_factories.TryGetValue(typeof(T), out var factory))
        {
            T element = (T)factory(number, description).ValidateUicSegment();
            return element with { PossibleItems = [element] };
        }

        throw new ArgumentException($"Invalid UicSegment type: {typeof(T).Name}", nameof(T));
    }
    
    public static T CreateUicSegment<T>(uint number, string description = "") where T : UicSegment
    {
        return CreateUicSegment<T>(number.ToString(), description);
    }
    
    private static readonly Dictionary<Type, Func<string, string, UicSegment>> _factories = new()
    {
        { typeof(UicInteroperabilitySegment), (num, desc) => new UicInteroperabilitySegment(num, desc) },
        { typeof(UicCountryCodeSegment), (num, desc) => new UicCountryCodeSegment(num, desc) },
        { typeof(UicTypeSegment), (num, desc) => new UicTypeSegment(num, desc) },
        { typeof(UicVelocityHeatingSegment), (num, desc) => new UicVelocityHeatingSegment(num, desc) },
        { typeof(UicSerialNumberSegment), (num, desc) => new UicSerialNumberSegment(num, desc) },
        { typeof(UicSelfCheckSegment), (num, _) => new UicSelfCheckSegment(num) }
    };

    public virtual bool Equals(UicSegment? other)
    {
        return other is not null && Number == other.Number;
    }

    public override int GetHashCode()
    {
        return Number.GetHashCode();
    }
}
public sealed record UicInteroperabilitySegment(string Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicCountryCodeSegment(string Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicTypeSegment(string Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicVelocityHeatingSegment(string Number, string Description = "") : UicSegment(Number, Description); 
public sealed record UicSerialNumberSegment(string Number, string Description = "") : UicSegment(Number, Description);
public sealed record UicSelfCheckSegment(string Number) : UicSegment(Number, "Self check number");

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
    public static T WithPossibleValues<T>(this T uicSegment, IEnumerable<UicSegment> segments) where T : UicSegment
    {
        return uicSegment  with { PossibleItems = segments };
    }
    
    public static T WithNumber<T>(this T segment, string number, string description = "") where T : UicSegment
    {
        return segment with
        {
            Number = number ,
            Description = description
        };
    }
}

public static class UicValidation
{
    public static T ValidateUicSegment<T>(this T value) where T : UicSegment
    {
        if (value.Number == "---")
            return value;
        List<string> validationResults = new();
        switch (value)
        {
            case UicInteroperabilitySegment:
            case UicCountryCodeSegment:
            case UicTypeSegment:
            case UicVelocityHeatingSegment:
                if(value.Number.Length > 2)
                    validationResults.Add($"Invalid UicSegment number: {value.Number} for {nameof(value)}");
                while (value.Number.Length < 2) 
                    value = value with { Number = "0" + value.Number };
                break;
            case UicSerialNumberSegment:
                if(value.Number.Length > 3)
                    validationResults.Add($"Invalid UicSegment number: {value.Number} for {nameof(value)}");
                while(value.Number.Length < 3)
                    value = value with { Number = "0" + value.Number };
                break;
            case UicSelfCheckSegment:
                if(value.Number.Length > 1)
                    validationResults.Add("Self check number must be between 0 and 10");
                break;
        }

        if (validationResults.Count > 0)
        {
            throw new ArgumentException(string.Join(',', validationResults));
        }

        return value;
    }
}