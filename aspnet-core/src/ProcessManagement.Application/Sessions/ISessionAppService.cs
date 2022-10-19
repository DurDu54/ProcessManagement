using System.Threading.Tasks;
using Abp.Application.Services;
using ProcessManagement.Sessions.Dto;

namespace ProcessManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
