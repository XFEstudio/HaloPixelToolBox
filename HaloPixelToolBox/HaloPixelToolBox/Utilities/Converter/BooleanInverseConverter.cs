using Microsoft.UI.Xaml.Data;

namespace HaloPixelToolBox.Utilities.Converter;

public partial class BooleanInverseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
            return !boolValue;
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
            return !boolValue;
        return value;
    }
}
