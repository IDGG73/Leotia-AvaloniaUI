using System;
using System.Globalization;

using Avalonia.Data.Converters;

namespace LeotiaUI;

public static class NumbersConverters
{
    public static readonly IValueConverter DecimalAndDouble =
        new DecimalDoubleConverter();

    /*
    public static readonly IValueConverter DecimalAndDouble =
        new Valueconverter<object, object>(
        convert: value =>
        {
            if (value is double d)
                return (decimal)d;

            return value;
        },
        convertBack: value =>
        {
            if (value is decimal d)
                return (double)d;

            return value;
        });
    */
}

public sealed class DecimalDoubleConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double d)
            return (decimal)d;

        return value!;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal d)
            return (double)d;

        return value!;
    }
}