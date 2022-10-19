using System.Threading.Tasks;
using Abp.Application.Services;
using ProcessManagement.Authorization.Accounts.Dto;

namespace ProcessManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
