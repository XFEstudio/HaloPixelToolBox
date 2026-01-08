using HaloPixelToolBox.Interface.Services;
using Windows.Foundation;
using XFEExtension.NetCore.WinUIHelper.Implements.Services;

namespace HaloPixelToolBox.Implements.Services;

public class CloseWindowService : GlobalServiceBase, ICloseWindowService
{
    Window? window;
    public event TypedEventHandler<object, WindowEventArgs>? Closed;

    public void Initialize(Window window)
    {
        this.window = window;
        this.window.Closed += (e, s) => Closed?.Invoke(e, s);
    }
}
