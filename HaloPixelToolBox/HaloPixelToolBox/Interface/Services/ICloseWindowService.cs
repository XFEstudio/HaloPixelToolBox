using Windows.Foundation;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace HaloPixelToolBox.Interface.Services;

public interface ICloseWindowService : IGlobalService
{
    event TypedEventHandler<object, WindowEventArgs> Closed;
    void Initialize(Window window);
}
