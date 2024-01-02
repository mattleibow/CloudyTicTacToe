using System.Globalization;
using CloudyTicTacToe.Games;

namespace CloudyTicTacToe.ClientApp;

public class PlayerToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Player player)
            throw new InvalidCastException($"The only supported value is a Player instance.");

        if (player.Number == 0)
            return null;
        
        return ImageSource.FromFile($"player_{player.Number}.png");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
