using Microsoft.UI.Xaml.Data;
using XFEExtension.NetCore.StringExtension;

namespace HaloPixelToolBox.Utilities.Converter;

public partial class StringEmptyBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var inverse = parameter is string parameterString && parameterString == "inverse";
        var empty = value is not string stringValue || stringValue.IsNullOrEmpty();
        return inverse ? !empty : empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) => value;
}