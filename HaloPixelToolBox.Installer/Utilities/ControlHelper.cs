using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace HaloPixelToolBox.Installer.Utilities
{
    public static class ControlHelper
    {
        public static T? FindControlByTag<T>(DependencyObject parent, object tag) where T : FrameworkElement
        {
            if (parent is null)
                return null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var childElement = child as T;
                if (childElement is not null && Equals(childElement.Tag, tag))
                    return childElement;
                var result = FindControlByTag<T>(child, tag);
                if (result is not null)
                    return result;
            }
            return null;
        }
        public static T Clone<T>(this T control) where T : UIElement => (T)XamlReader.Load(XmlReader.Create(new StringReader(XamlWriter.Save(control))));
    }
}
