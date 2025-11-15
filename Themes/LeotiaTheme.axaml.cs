using Avalonia.Markup.Xaml;
using Avalonia.Styling;

// ReSharper disable once CheckNamespace
namespace LeotiaUI;

/// <summary>
///     The main theme for the application.
/// </summary>
public class LeotiaTheme : Styles
{
    /// <summary>
    ///     Returns a new instance of the <see cref="LeotiaTheme" /> class.
    /// </summary>
    public LeotiaTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}