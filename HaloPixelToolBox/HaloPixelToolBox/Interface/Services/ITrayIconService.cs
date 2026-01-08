using Microsoft.UI.Dispatching;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace HaloPixelToolBox.Interface.Services;

public interface ITrayIconService : IDisposable, IGlobalService
{
    void Initilize(DispatcherQueue dispatcherQueue);
    void ExitApp();
    void ShowWindow();
}