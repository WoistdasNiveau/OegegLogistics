using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using OegegLogistics.ViewModels.Enums;

namespace OegegLogistics.Shared.Converters;

public class ToleranceTypeToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            ToleranceType.Km => "km",
            ToleranceType.Percentage => "%",
            _ => value?.ToString()
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            "km" => ToleranceType.Km,
            "%" => ToleranceType.Percentage,
            _ => BindingOperations.DoNothing
        };
    }
}