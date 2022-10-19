using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ProcessManagement.Configuration.Dto;

namespace ProcessManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProcessManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
