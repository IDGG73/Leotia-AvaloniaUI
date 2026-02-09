using System;
using Avalonia;
using Avalonia.Controls;

namespace LeotiaUI;

public class DynamicWrapPanel : Panel
{
    public static readonly StyledProperty<double> MinItemWidthProperty =
        AvaloniaProperty.Register<DynamicWrapPanel, double>(
            nameof(MinItemWidth), 100);

    public static readonly StyledProperty<double> MaxItemWidthProperty =
        AvaloniaProperty.Register<DynamicWrapPanel, double>(
            nameof(MaxItemWidth), 300);

    public double MinItemWidth
    {
        get => GetValue(MinItemWidthProperty);
        set => SetValue(MinItemWidthProperty, value);
    }

    public double MaxItemWidth
    {
        get => GetValue(MaxItemWidthProperty);
        set => SetValue(MaxItemWidthProperty, value);
    }

    private (int itemsPerRow, double itemWidth) CalculateLayout(double availableWidth)
    {
        int maxItems = Math.Max(1, (int)(availableWidth / MinItemWidth));

        for (int items = 1; items <= maxItems; items++)
        {
            double width = availableWidth / items;

            if (width >= MinItemWidth && width <= MaxItemWidth)
                return (items, width);
        }

        // fallback: usar el máximo permitido
        int fallbackItems = Math.Max(1, (int)(availableWidth / MaxItemWidth));
        double fallbackWidth = Math.Clamp(
            availableWidth / fallbackItems,
            MinItemWidth,
            MaxItemWidth
        );

        return (fallbackItems, fallbackWidth);
    }


    protected override Size MeasureOverride(Size availableSize)
    {
        if (double.IsInfinity(availableSize.Width))
            return base.MeasureOverride(availableSize);

        var (itemsPerRow, itemWidth) = CalculateLayout(availableSize.Width);

        double x = 0;
        double rowHeight = 0;
        double totalHeight = 0;

        foreach (var child in Children)
        {
            child.Measure(new Size(itemWidth, availableSize.Height));

            rowHeight = Math.Max(rowHeight, child.DesiredSize.Height);
            x += itemWidth;

            if (x + itemWidth > availableSize.Width + 0.1)
            {
                totalHeight += rowHeight;
                rowHeight = 0;
                x = 0;
            }
        }

        totalHeight += rowHeight;

        return new Size(availableSize.Width, totalHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var (itemsPerRow, itemWidth) = CalculateLayout(finalSize.Width);

        double x = 0;
        double y = 0;
        double rowHeight = 0;

        foreach (var child in Children)
        {
            if (x + itemWidth > finalSize.Width + 0.1)
            {
                y += rowHeight;
                rowHeight = 0;
                x = 0;
            }

            child.Arrange(new Rect(x, y, itemWidth, child.DesiredSize.Height));

            rowHeight = Math.Max(rowHeight, child.DesiredSize.Height);
            x += itemWidth;
        }

        return finalSize;
    }

}
