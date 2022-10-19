using Abp.Application.Services;
using ProcessManagement.MultiTenancy.Dto;

namespace ProcessManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

