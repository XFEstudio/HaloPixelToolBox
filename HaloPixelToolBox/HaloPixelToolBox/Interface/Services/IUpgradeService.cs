using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace HaloPixelToolBox.Interface.Services;

public interface IUpgradeService : IGlobalService
{
    void Initialize(Func<Task> checkUpgradeAction);
    Task<bool> CheckUpgrade();
}
