using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ProcessManagement.Controllers
{
    public abstract class ProcessManagementControllerBase: AbpController
    {
        protected ProcessManagementControllerBase()
        {
            LocalizationSourceName = ProcessManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
