using System.Threading.Tasks;
using ProcessManagement.Configuration.Dto;

namespace ProcessManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
