using System;
using System.Globalization;
using Avalonia.Data.Converters;
using OegegLogistics.Models;

namespace OegegLogistics.Converters;

public class UicModelNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is not UICModel uicModel)
            return null;

        return uicModel.GetType().Name switch
        {
            nameof(InteroperabilityModel) => "Austauschverfahren",
            nameof(CountryCodeModel) => "Eigentumsverwaltung",
            nameof(VehicleTypeModel) => "Wagengattung",
            nameof(VelocityHeatingModel) => "Geschwindigkeit und Heizung",
            nameof(SerialNumberModel) => "Laufende Nummer",
            nameof(SelfCheckNumberModel) => "Self-Check",
            _ => "Undefined"
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}