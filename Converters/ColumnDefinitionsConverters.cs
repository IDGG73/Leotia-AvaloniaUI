using System;
using System.Globalization;

using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace LeotiaUI;

public static class ColumnDefinitionsConverters
{
    public static readonly IValueConverter ForField = new FuncValueConverter<bool, ColumnDefinitions>((isVisible) =>
    {
        if (isVisible)
            return new ColumnDefinitions("30*,70*");

        return new ColumnDefinitions("0*,100*");
    });
}