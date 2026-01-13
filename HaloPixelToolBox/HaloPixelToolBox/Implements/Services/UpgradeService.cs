using HaloPixelToolBox.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Implements.Services;

namespace HaloPixelToolBox.Implements.Services;

public class UpgradeService : GlobalServiceBase, IUpgradeService
{
    Func<Task>? checkUpgradeAction;

    public async Task<bool> CheckUpgrade()
    {
        if (checkUpgradeAction is null)
            return false;
        await checkUpgradeAction();
        return true;
    }

    public void Initialize(Func<Task> checkUpgradeAction) => this.checkUpgradeAction = checkUpgradeAction;
}
